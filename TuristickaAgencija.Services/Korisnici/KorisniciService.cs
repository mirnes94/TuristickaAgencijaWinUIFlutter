using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Messages;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;
using TuristickaAgencija.Services.Messaging;
using TuristickaAgencija.Services.Security;

namespace TuristickaAgencija.Services.Korisnici
{
    public class KorisniciService
        : BaseCRUDService<Model.Korisnici, Database.Korisnici, KorisniciSearchRequest, KorisniciInsertUpdateRequest, KorisniciInsertUpdateRequest>,
          IKorisniciService
    {
        private readonly IMessageProducer _messageProducer;
        private readonly VerifikacijaOptions _verifikacija;

        public KorisniciService(
            TuristickaAgencijaContext context,
            IMapper mapper,
            IMessageProducer messageProducer,
            IOptions<VerifikacijaOptions> verifikacija) : base(context, mapper)
        {
            _messageProducer = messageProducer;
            _verifikacija = verifikacija.Value;
        }

        protected override IQueryable<Database.Korisnici> AddInclude(IQueryable<Database.Korisnici> query)
        {
            return query.Include(x => x.KorisniciUloge).ThenInclude(x => x.Uloga);
        }

        protected override IQueryable<Database.Korisnici> AddFilter(IQueryable<Database.Korisnici> query, KorisniciSearchRequest search)
        {
            if (!string.IsNullOrWhiteSpace(search.Ime))
            {
                query = query.Where(x => x.Ime.StartsWith(search.Ime));
            }
            if (!string.IsNullOrWhiteSpace(search.Prezime))
            {
                query = query.Where(x => x.Prezime.StartsWith(search.Prezime));
            }
            if (!string.IsNullOrWhiteSpace(search.KorisnickoIme))
            {
                query = query.Where(x => x.KorisnickoIme.Contains(search.KorisnickoIme));
            }
            if (search.UlogaId.HasValue)
            {
                query = query.Where(x => x.KorisniciUloge.Any(u => u.UlogaId == search.UlogaId));
            }
            return query;
        }

        protected override IQueryable<Database.Korisnici> AddOrder(IQueryable<Database.Korisnici> query)
        {
            return query.OrderBy(x => x.Prezime).ThenBy(x => x.Ime);
        }

        // ---------------- Kreiranje (administrator) ----------------

        protected override async Task BeforeInsertAsync(KorisniciInsertUpdateRequest request)
        {
            ValidirajNovuLozinku(request, obavezna: true);
            await ProvjeriJedinstvenostAsync(request, null);

            if (request.Uloge == null || request.Uloge.Count == 0)
            {
                throw new UserException("Korisniku morate dodijeliti barem jednu ulogu.");
            }
        }

        protected override Task OnInsertingAsync(Database.Korisnici entity, KorisniciInsertUpdateRequest request)
        {
            entity.LozinkaSalt = PasswordHasher.GenerateSalt();
            entity.LozinkaHash = PasswordHasher.GenerateHash(entity.LozinkaSalt, request.Password);
            entity.Status = request.Status;

            foreach (var ulogaId in request.Uloge.Distinct())
            {
                entity.KorisniciUloge.Add(new Database.KorisniciUloge
                {
                    UlogaId = ulogaId,
                    DatumIzmjene = DateTime.Now
                });
            }

            return Task.CompletedTask;
        }

        // ---------------- Izmjena (administrator) ----------------

        protected override async Task BeforeUpdateAsync(Database.Korisnici entity, KorisniciInsertUpdateRequest request)
        {
            ValidirajNovuLozinku(request, obavezna: false);
            await ProvjeriJedinstvenostAsync(request, entity.Id);

            if (request.Uloge == null || request.Uloge.Count == 0)
            {
                throw new UserException("Korisniku morate dodijeliti barem jednu ulogu.");
            }
        }

        protected override async Task OnUpdatingAsync(Database.Korisnici entity, KorisniciInsertUpdateRequest request)
        {
            entity.Status = request.Status;
            PostaviLozinkuAkoJeUnesena(entity, request);

            var postojece = await Context.KorisniciUloge.Where(x => x.KorisnikId == entity.Id).ToListAsync();
            var trazene = request.Uloge.Distinct().ToList();

            Context.KorisniciUloge.RemoveRange(postojece.Where(x => !trazene.Contains(x.UlogaId)));

            foreach (var ulogaId in trazene.Where(id => postojece.All(p => p.UlogaId != id)))
            {
                Context.KorisniciUloge.Add(new Database.KorisniciUloge
                {
                    KorisnikId = entity.Id,
                    UlogaId = ulogaId,
                    DatumIzmjene = DateTime.Now
                });
            }
        }

        protected override async Task BeforeDeleteAsync(Database.Korisnici entity)
        {
            if (await Context.Rezervacija.AnyAsync(x => x.KorisnikId == entity.Id)
                || await Context.Uplate.AnyAsync(x => x.KorisnikId == entity.Id))
            {
                throw new UserException("Nalog nije moguće obrisati jer postoje rezervacije ili uplate vezane za njega. Kontaktirajte agenciju.");
            }

            Context.Komentar.RemoveRange(await Context.Komentar.Where(x => x.KorisnikId == entity.Id).ToListAsync());
            Context.Ocjene.RemoveRange(await Context.Ocjene.Where(x => x.KorisnikId == entity.Id).ToListAsync());
            Context.Obavijesti.RemoveRange(await Context.Obavijesti.Where(x => x.KorisnikId == entity.Id).ToListAsync());

            var uloge = await Context.KorisniciUloge.Where(x => x.KorisnikId == entity.Id).ToListAsync();
            Context.KorisniciUloge.RemoveRange(uloge);

            var listaZelja = await Context.ListaZelja.Where(x => x.KorisnikId == entity.Id).ToListAsync();
            Context.ListaZelja.RemoveRange(listaZelja);
        }

        // ---------------- Profil (korisnik sam sebi) ----------------

        public async Task<Model.Korisnici> UpdateProfilAsync(int id, KorisniciInsertUpdateRequest request)
        {
            var entity = await Context.Korisnici.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Korisnik ne postoji.");
            }

            ValidirajNovuLozinku(request, obavezna: false);
            await ProvjeriJedinstvenostAsync(request, id);

            if (!string.IsNullOrWhiteSpace(request.Password)
                && !PasswordHasher.Verify(request.StaraLozinka, entity.LozinkaSalt, entity.LozinkaHash))
            {
                throw new UserException("Stara lozinka nije ispravna.");
            }

            entity.Ime = request.Ime;
            entity.Prezime = request.Prezime;
            entity.Email = request.Email;
            entity.Telefon = request.Telefon;
            entity.KorisnickoIme = request.KorisnickoIme;
            PostaviLozinkuAkoJeUnesena(entity, request);

            await Context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        // ---------------- Registracija i verifikacija ----------------

        public async Task<Model.Korisnici> RegistrujAsync(KorisniciInsertUpdateRequest request)
        {
            ValidirajNovuLozinku(request, obavezna: true);
            await ProvjeriJedinstvenostAsync(request, null);

            var klijent = await Context.Uloge.FirstOrDefaultAsync(x => x.Naziv == UlogeNazivi.Klijent);
            if (klijent == null)
            {
                throw new UserException("Uloga Klijent ne postoji u sistemu.");
            }

            var entity = Mapper.Map<Database.Korisnici>(request);
            entity.LozinkaSalt = PasswordHasher.GenerateSalt();
            entity.LozinkaHash = PasswordHasher.GenerateHash(entity.LozinkaSalt, request.Password);
            entity.Status = false;
            entity.KorisniciUloge.Add(new Database.KorisniciUloge { UlogaId = klijent.Id, DatumIzmjene = DateTime.Now });

            Context.Korisnici.Add(entity);
            await Context.SaveChangesAsync();

            var token = _verifikacija.KreirajToken(entity.KorisnickoIme);
            var link = $"{_verifikacija.ApiJavniUrl.TrimEnd('/')}/api/Korisnici/Potvrdi/{Uri.EscapeDataString(entity.KorisnickoIme)}?token={token}";

            _messageProducer.Posalji(new NotifikacijaPoruka
            {
                Tip = TipNotifikacije.Registracija,
                PrimalacEmail = entity.Email,
                PrimalacIme = entity.Ime,
                Naslov = "Potvrda registracije - Turistička agencija",
                Sadrzaj = "Hvala na registraciji. Kliknite na link ispod kako biste aktivirali svoj nalog.",
                Link = link
            });

            return await GetByIdAsync(entity.Id);
        }

        public async Task<bool> PotvrdiAsync(string username, string token)
        {
            if (!_verifikacija.ProvjeriToken(username, token))
            {
                return false;
            }

            var user = await Context.Korisnici.FirstOrDefaultAsync(x => x.KorisnickoIme == username);
            if (user == null)
            {
                return false;
            }

            user.Status = true;
            await Context.SaveChangesAsync();
            return true;
        }

        // ---------------- Autentifikacija ----------------

        public async Task<Model.Korisnici> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = await Context.Korisnici
                .AsNoTracking()
                .Include(x => x.KorisniciUloge).ThenInclude(x => x.Uloga)
                .FirstOrDefaultAsync(x => x.KorisnickoIme == username);

            if (user == null || user.Status != true)
            {
                return null;
            }

            return PasswordHasher.Verify(password, user.LozinkaSalt, user.LozinkaHash)
                ? Mapper.Map<Model.Korisnici>(user)
                : null;
        }

        // ---------------- Pomocne metode ----------------

        private static void ValidirajNovuLozinku(KorisniciInsertUpdateRequest request, bool obavezna)
        {
            if (string.IsNullOrWhiteSpace(request.Password))
            {
                if (obavezna)
                {
                    throw new UserException("Lozinka je obavezna.");
                }
                return;
            }

            if (request.Password.Length < 4)
            {
                throw new UserException("Lozinka mora imati najmanje 4 znaka.");
            }

            if (request.Password != request.PasswordConfirmation)
            {
                throw new UserException("Lozinka i potvrda lozinke se ne podudaraju.");
            }
        }

        private static void PostaviLozinkuAkoJeUnesena(Database.Korisnici entity, KorisniciInsertUpdateRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                entity.LozinkaSalt = PasswordHasher.GenerateSalt();
                entity.LozinkaHash = PasswordHasher.GenerateHash(entity.LozinkaSalt, request.Password);
            }
        }

        private async Task ProvjeriJedinstvenostAsync(KorisniciInsertUpdateRequest request, int? id)
        {
            if (await Context.Korisnici.AnyAsync(x => x.KorisnickoIme == request.KorisnickoIme && x.Id != id))
            {
                throw new UserException("Korisničko ime je zauzeto.");
            }

            if (await Context.Korisnici.AnyAsync(x => x.Email == request.Email && x.Id != id))
            {
                throw new UserException("Email adresa je već registrovana.");
            }
        }
    }
}
