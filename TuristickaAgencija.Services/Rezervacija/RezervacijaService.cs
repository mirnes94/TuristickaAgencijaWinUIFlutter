using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Messages;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;
using TuristickaAgencija.Services.Messaging;

namespace TuristickaAgencija.Services.Rezervacija
{
    public class RezervacijaService
        : BaseCRUDService<Model.Rezervacija, Database.Rezervacija, RezervacijaSearchRequest, RezervacijaInsertUpdateRequest, RezervacijaInsertUpdateRequest>,
          IRezervacijaService
    {
        private static readonly string[] DozvoljeniStatusi =
        {
            StatusRezervacije.UObradi, StatusRezervacije.Potvrdjeno, StatusRezervacije.Otkazano
        };

        private readonly IMessageProducer _messageProducer;
        private string _stariStatus;

        public RezervacijaService(TuristickaAgencijaContext context, IMapper mapper, IMessageProducer messageProducer)
            : base(context, mapper)
        {
            _messageProducer = messageProducer;
        }

        protected override IQueryable<Database.Rezervacija> AddInclude(IQueryable<Database.Rezervacija> query)
        {
            return query
                .Include(x => x.Korisnik)
                .Include(x => x.Putovanje)
                .Include(x => x.Uplate);
        }

        protected override IQueryable<Database.Rezervacija> AddFilter(IQueryable<Database.Rezervacija> query, RezervacijaSearchRequest search)
        {
            if (search.KorisnikId.HasValue)
            {
                query = query.Where(x => x.KorisnikId == search.KorisnikId);
            }
            if (search.PutovanjeId.HasValue)
            {
                query = query.Where(x => x.PutovanjeId == search.PutovanjeId);
            }
            if (!string.IsNullOrWhiteSpace(search.Status))
            {
                query = query.Where(x => x.Status == search.Status);
            }
            if (!string.IsNullOrWhiteSpace(search.Ime))
            {
                query = query.Where(x => x.Ime.Contains(search.Ime)
                                         || x.Korisnik.Ime.Contains(search.Ime)
                                         || x.Korisnik.Prezime.Contains(search.Ime));
            }
            return query;
        }

        protected override IQueryable<Database.Rezervacija> AddOrder(IQueryable<Database.Rezervacija> query)
        {
            return query.OrderByDescending(x => x.DatumRezervacije);
        }

        protected override async Task BeforeInsertAsync(RezervacijaInsertUpdateRequest request)
        {
            var putovanje = await Context.Putovanja.FindAsync(request.PutovanjeId);
            if (putovanje == null)
            {
                throw new UserException("Odabrano putovanje ne postoji.");
            }
            if (putovanje.DatumPolaska.Date <= DateTime.Today)
            {
                throw new UserException("Nije moguće rezervisati putovanje koje je već počelo.");
            }

            await ProvjeriSlobodnaMjestaAsync(putovanje, request.BrojOsoba, null);
        }

        protected override Task OnInsertingAsync(Database.Rezervacija entity, RezervacijaInsertUpdateRequest request)
        {
            entity.DatumRezervacije = request.DatumRezervacije == default ? DateTime.Now : request.DatumRezervacije;
            entity.Status = string.IsNullOrWhiteSpace(request.Status) ? StatusRezervacije.UObradi : request.Status;
            ValidirajStatus(entity.Status);
            return Task.CompletedTask;
        }

        protected override async Task AfterInsertAsync(Database.Rezervacija entity, RezervacijaInsertUpdateRequest request)
        {
            await PosaljiObavijestAsync(entity, $"Vaša rezervacija \"{entity.Ime}\" je zaprimljena i ima status: {entity.Status}.");
        }

        protected override async Task BeforeUpdateAsync(Database.Rezervacija entity, RezervacijaInsertUpdateRequest request)
        {
            _stariStatus = entity.Status;

            var putovanje = await Context.Putovanja.FindAsync(request.PutovanjeId);
            if (putovanje == null)
            {
                throw new UserException("Odabrano putovanje ne postoji.");
            }

            if (request.Status != StatusRezervacije.Otkazano)
            {
                await ProvjeriSlobodnaMjestaAsync(putovanje, request.BrojOsoba, entity.Id);
            }
        }

        protected override Task OnUpdatingAsync(Database.Rezervacija entity, RezervacijaInsertUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(entity.Status))
            {
                entity.Status = _stariStatus;
            }
            if (request.DatumRezervacije == default)
            {
                // datum rezervacije se ne mijenja ako ga klijent nije poslao
                entity.DatumRezervacije = Context.Entry(entity).Property(x => x.DatumRezervacije).OriginalValue;
            }
            ValidirajStatus(entity.Status);
            return Task.CompletedTask;
        }

        protected override async Task AfterUpdateAsync(Database.Rezervacija entity, RezervacijaInsertUpdateRequest request)
        {
            if (_stariStatus != entity.Status)
            {
                await PosaljiObavijestAsync(entity, $"Status vaše rezervacije \"{entity.Ime}\" je promijenjen u: {entity.Status}.");
            }
        }

        protected override async Task BeforeDeleteAsync(Database.Rezervacija entity)
        {
            var uplate = await Context.Uplate.Where(x => x.RezervacijaId == entity.Id).ToListAsync();
            Context.Uplate.RemoveRange(uplate);
        }

        private async Task ProvjeriSlobodnaMjestaAsync(Database.Putovanja putovanje, int brojOsoba, int? rezervacijaId)
        {
            var zauzeto = await Context.Rezervacija
                .Where(x => x.PutovanjeId == putovanje.Id
                            && x.Status != StatusRezervacije.Otkazano
                            && x.Id != rezervacijaId)
                .SumAsync(x => (int?)x.BrojOsoba) ?? 0;

            var slobodno = putovanje.BrojMjesta - zauzeto;
            if (brojOsoba > slobodno)
            {
                throw new UserException($"Nema dovoljno slobodnih mjesta. Slobodno je još {Math.Max(slobodno, 0)} mjesta.");
            }
        }

        private static void ValidirajStatus(string status)
        {
            if (!DozvoljeniStatusi.Contains(status))
            {
                throw new UserException($"Status mora biti jedan od: {string.Join(", ", DozvoljeniStatusi)}.");
            }
        }

        private async Task PosaljiObavijestAsync(Database.Rezervacija entity, string sadrzaj)
        {
            if (!entity.KorisnikId.HasValue)
            {
                return;
            }

            var korisnik = await Context.Korisnici.AsNoTracking().FirstOrDefaultAsync(x => x.Id == entity.KorisnikId);
            if (korisnik == null || string.IsNullOrWhiteSpace(korisnik.Email))
            {
                return;
            }

            _messageProducer.Posalji(new NotifikacijaPoruka
            {
                Tip = TipNotifikacije.Rezervacija,
                PrimalacEmail = korisnik.Email,
                PrimalacIme = korisnik.Ime,
                Naslov = "Rezervacija - Turistička agencija",
                Sadrzaj = sadrzaj
            });
        }
    }
}
