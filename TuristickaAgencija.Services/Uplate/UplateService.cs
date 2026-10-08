using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Messages;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;
using TuristickaAgencija.Services.Messaging;

namespace TuristickaAgencija.Services.Uplate
{
    public class UplateService
        : BaseCRUDService<Model.Uplate, Database.Uplate, UplateSearchRequest, UplateInsertUpdateRequest, UplateInsertUpdateRequest>,
          IUplateService
    {
        private readonly IMessageProducer _messageProducer;

        public UplateService(TuristickaAgencijaContext context, IMapper mapper, IMessageProducer messageProducer)
            : base(context, mapper)
        {
            _messageProducer = messageProducer;
        }

        protected override IQueryable<Database.Uplate> AddInclude(IQueryable<Database.Uplate> query)
        {
            return query
                .Include(x => x.Korisnik)
                .Include(x => x.Rezervacija).ThenInclude(x => x.Putovanje);
        }

        protected override IQueryable<Database.Uplate> AddFilter(IQueryable<Database.Uplate> query, UplateSearchRequest search)
        {
            if (search.KorisnikId.HasValue)
            {
                query = query.Where(x => x.KorisnikId == search.KorisnikId);
            }
            if (search.RezervacijaId.HasValue)
            {
                query = query.Where(x => x.RezervacijaId == search.RezervacijaId);
            }
            if (search.DatumOd.HasValue)
            {
                var od = search.DatumOd.Value.Date;
                query = query.Where(x => x.Datum >= od);
            }
            if (search.DatumDo.HasValue)
            {
                var doDatuma = search.DatumDo.Value.Date.AddDays(1);
                query = query.Where(x => x.Datum < doDatuma);
            }
            return query;
        }

        protected override IQueryable<Database.Uplate> AddOrder(IQueryable<Database.Uplate> query)
        {
            return query.OrderByDescending(x => x.Datum);
        }

        protected override async Task OnInsertingAsync(Database.Uplate entity, UplateInsertUpdateRequest request)
        {
            var rezervacija = await Context.Rezervacija
                .Include(x => x.Putovanje)
                .FirstOrDefaultAsync(x => x.Id == request.RezervacijaId);

            if (rezervacija == null)
            {
                throw new UserException("Rezervacija ne postoji.");
            }
            if (rezervacija.Status == StatusRezervacije.Otkazano)
            {
                throw new UserException("Nije moguće uplatiti otkazanu rezervaciju.");
            }

            if (entity.Datum == default)
            {
                entity.Datum = DateTime.Now;
            }
            if (entity.KorisnikId == 0 && rezervacija.KorisnikId.HasValue)
            {
                entity.KorisnikId = rezervacija.KorisnikId.Value;
            }
        }

        protected override async Task AfterInsertAsync(Database.Uplate entity, UplateInsertUpdateRequest request)
        {
            var rezervacija = await Context.Rezervacija
                .Include(x => x.Putovanje)
                .Include(x => x.Korisnik)
                .FirstAsync(x => x.Id == entity.RezervacijaId);

            var uplaceno = await Context.Uplate.Where(x => x.RezervacijaId == rezervacija.Id).SumAsync(x => x.Iznos);
            var ukupno = rezervacija.Putovanje != null ? (double)rezervacija.Putovanje.CijenaPutovanja * rezervacija.BrojOsoba : 0;

            // Prema opisu sistema: rezervacija je "Potvrđena" tek kada je uplaćen cijeli iznos.
            // tolerancija 0.005: CijenaPutovanja je float (npr. 75.3f = 75.3000030518), a uplate su zaokruzene na 2 decimale
            if (ukupno > 0 && uplaceno + 0.005 >= ukupno && rezervacija.Status != StatusRezervacije.Potvrdjeno)
            {
                rezervacija.Status = StatusRezervacije.Potvrdjeno;
                await Context.SaveChangesAsync();
            }

            if (rezervacija.Korisnik != null && !string.IsNullOrWhiteSpace(rezervacija.Korisnik.Email))
            {
                _messageProducer.Posalji(new NotifikacijaPoruka
                {
                    Tip = TipNotifikacije.Uplata,
                    PrimalacEmail = rezervacija.Korisnik.Email,
                    PrimalacIme = rezervacija.Korisnik.Ime,
                    Naslov = "Potvrda uplate - Turistička agencija",
                    Sadrzaj = $"Evidentirana je uplata od {entity.Iznos:0.00} KM za rezervaciju \"{rezervacija.Ime}\". " +
                              $"Ukupno uplaćeno: {uplaceno:0.00} / {ukupno:0.00} KM. Status rezervacije: {rezervacija.Status}."
                });
            }
        }
    }
}
