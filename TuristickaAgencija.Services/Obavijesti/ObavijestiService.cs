using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Messages;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Messaging;

namespace TuristickaAgencija.Services.Obavijesti
{
    public class ObavijestiService
        : BaseCRUDService<Model.Obavijesti, Database.Obavijesti, ObavijestiSearchRequest, ObavijestiInsertUpdateRequest, ObavijestiInsertUpdateRequest>,
          IObavijestiService
    {
        private readonly IMessageProducer _messageProducer;

        public ObavijestiService(TuristickaAgencijaContext context, IMapper mapper, IMessageProducer messageProducer)
            : base(context, mapper)
        {
            _messageProducer = messageProducer;
        }

        protected override IQueryable<Database.Obavijesti> AddInclude(IQueryable<Database.Obavijesti> query)
        {
            return query.Include(x => x.Korisnik);
        }

        protected override IQueryable<Database.Obavijesti> AddFilter(IQueryable<Database.Obavijesti> query, ObavijestiSearchRequest search)
        {
            if (search.KorisnikId.HasValue)
            {
                // korisnik vidi obavijesti namijenjene njemu i opce obavijesti (bez primaoca)
                query = query.Where(x => x.KorisnikId == search.KorisnikId || x.KorisnikId == null);
            }
            if (!string.IsNullOrWhiteSpace(search.Naziv))
            {
                query = query.Where(x => x.Naziv.Contains(search.Naziv));
            }
            return query;
        }

        protected override IQueryable<Database.Obavijesti> AddOrder(IQueryable<Database.Obavijesti> query)
        {
            return query.OrderByDescending(x => x.Datum);
        }

        protected override Task OnInsertingAsync(Database.Obavijesti entity, ObavijestiInsertUpdateRequest request)
        {
            entity.Datum = DateTime.Now;
            return Task.CompletedTask;
        }

        protected override Task OnUpdatingAsync(Database.Obavijesti entity, ObavijestiInsertUpdateRequest request)
        {
            if (request.Datum == default)
            {
                // datum se ne mijenja ako ga klijent nije poslao
                entity.Datum = Context.Entry(entity).Property(x => x.Datum).OriginalValue;
            }
            return Task.CompletedTask;
        }

        protected override async Task AfterInsertAsync(Database.Obavijesti entity, ObavijestiInsertUpdateRequest request)
        {
            if (!request.PosaljiEmail)
            {
                return;
            }

            // Obavijest za jednog korisnika ili za sve aktivne klijente.
            var upit = Context.Korisnici.AsNoTracking().Where(x => x.Status == true);
            upit = entity.KorisnikId.HasValue
                ? upit.Where(x => x.Id == entity.KorisnikId.Value)
                : upit.Where(x => x.KorisniciUloge.Any(u => u.Uloga.Naziv == UlogeNazivi.Klijent));

            var primaoci = await upit.Select(x => new { x.Email, x.Ime }).ToListAsync();

            foreach (var primalac in primaoci.Where(x => !string.IsNullOrWhiteSpace(x.Email)))
            {
                _messageProducer.Posalji(new NotifikacijaPoruka
                {
                    Tip = TipNotifikacije.Obavijest,
                    PrimalacEmail = primalac.Email,
                    PrimalacIme = primalac.Ime,
                    Naslov = entity.Naziv,
                    Sadrzaj = entity.Sadrzaj
                });
            }
        }
    }
}
