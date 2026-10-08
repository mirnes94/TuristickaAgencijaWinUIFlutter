using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Putovanja
{
    public class PutovanjaService
        : BaseCRUDService<Model.Putovanja, Database.Putovanja, PutovanjaSearchRequest, PutovanjaInsertUpdateRequest, PutovanjaInsertUpdateRequest>,
          IPutovanjaService
    {
        public PutovanjaService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Putovanja> AddInclude(IQueryable<Database.Putovanja> query)
        {
            return query
                .Include(x => x.Grad)
                .Include(x => x.Smjestaj)
                .Include(x => x.Prevoz).ThenInclude(x => x.Firma)
                .Include(x => x.Ocjene)
                .Include(x => x.VodiciPutovanja).ThenInclude(x => x.Vodic)
                .AsSplitQuery();
        }

        protected override IQueryable<Database.Putovanja> AddFilter(IQueryable<Database.Putovanja> query, PutovanjaSearchRequest search)
        {
            if (!string.IsNullOrWhiteSpace(search.NazivPutovanja))
            {
                query = query.Where(x => x.NazivPutovanja.Contains(search.NazivPutovanja));
            }
            if (search.GradId.HasValue)
            {
                query = query.Where(x => x.GradId == search.GradId);
            }
            if (search.SmjestajId.HasValue)
            {
                query = query.Where(x => x.SmjestajId == search.SmjestajId);
            }
            if (search.SamoBuduca == true)
            {
                var danas = DateTime.Today;
                query = query.Where(x => x.DatumPolaska >= danas);
            }
            return query;
        }

        protected override IQueryable<Database.Putovanja> AddOrder(IQueryable<Database.Putovanja> query)
        {
            return query.OrderBy(x => x.DatumPolaska);
        }

        protected override Task BeforeInsertAsync(PutovanjaInsertUpdateRequest request)
        {
            ValidirajDatume(request);

            if (request.DatumPolaska.Date < DateTime.Today)
            {
                throw new UserException("Datum polaska ne može biti u prošlosti.");
            }

            if (request.Slika == null || request.Slika.Length == 0)
            {
                throw new UserException("Slika putovanja je obavezna.");
            }

            return Task.CompletedTask;
        }

        protected override Task OnInsertingAsync(Database.Putovanja entity, PutovanjaInsertUpdateRequest request)
        {
            foreach (var vodicId in (request.Vodici ?? new List<int>()).Distinct())
            {
                entity.VodiciPutovanja.Add(new Database.VodiciPutovanja { VodicId = vodicId });
            }
            return Task.CompletedTask;
        }

        protected override async Task BeforeUpdateAsync(Database.Putovanja entity, PutovanjaInsertUpdateRequest request)
        {
            ValidirajDatume(request);

            var rezervisano = await Context.Rezervacija
                .Where(x => x.PutovanjeId == entity.Id && x.Status != Model.StatusRezervacije.Otkazano)
                .SumAsync(x => (int?)x.BrojOsoba) ?? 0;

            if (request.BrojMjesta < rezervisano)
            {
                throw new UserException($"Broj mjesta ne može biti manji od broja već rezervisanih mjesta ({rezervisano}).");
            }
        }

        protected override async Task OnUpdatingAsync(Database.Putovanja entity, PutovanjaInsertUpdateRequest request)
        {
            var trazeni = (request.Vodici ?? new List<int>()).Distinct().ToList();
            var postojeci = await Context.VodiciPutovanja.Where(x => x.PutovanjeId == entity.Id).ToListAsync();

            Context.VodiciPutovanja.RemoveRange(postojeci.Where(x => !trazeni.Contains(x.VodicId)));

            foreach (var vodicId in trazeni.Where(id => postojeci.All(p => p.VodicId != id)))
            {
                Context.VodiciPutovanja.Add(new Database.VodiciPutovanja { PutovanjeId = entity.Id, VodicId = vodicId });
            }
        }

        protected override async Task BeforeDeleteAsync(Database.Putovanja entity)
        {
            if (await Context.Rezervacija.AnyAsync(x => x.PutovanjeId == entity.Id))
            {
                throw new UserException("Putovanje nije moguće obrisati jer postoje rezervacije za njega.");
            }

            Context.VodiciPutovanja.RemoveRange(await Context.VodiciPutovanja.Where(x => x.PutovanjeId == entity.Id).ToListAsync());
            Context.ListaZelja.RemoveRange(await Context.ListaZelja.Where(x => x.PutovanjeId == entity.Id).ToListAsync());
            Context.Komentar.RemoveRange(await Context.Komentar.Where(x => x.PutovanjeId == entity.Id).ToListAsync());
            Context.Ocjene.RemoveRange(await Context.Ocjene.Where(x => x.PutovanjeId == entity.Id).ToListAsync());
        }

        private static void ValidirajDatume(PutovanjaInsertUpdateRequest request)
        {
            if (request.DatumDolaska < request.DatumPolaska)
            {
                throw new UserException("Datum povratka mora biti nakon datuma polaska.");
            }
        }
    }
}
