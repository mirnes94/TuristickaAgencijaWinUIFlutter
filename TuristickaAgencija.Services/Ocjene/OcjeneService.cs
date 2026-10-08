using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Ocjene
{
    public class OcjeneService
        : BaseCRUDService<Model.Ocjene, Database.Ocjene, OcjeneSearchRequest, OcjeneInsertUpdateRequest, OcjeneInsertUpdateRequest>, IOcjeneService
    {
        public OcjeneService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Ocjene> AddInclude(IQueryable<Database.Ocjene> query)
        {
            return query.Include(x => x.Korisnik).Include(x => x.Putovanje);
        }

        protected override IQueryable<Database.Ocjene> AddFilter(IQueryable<Database.Ocjene> query, OcjeneSearchRequest search)
        {
            if (search.PutovanjeId.HasValue)
            {
                query = query.Where(x => x.PutovanjeId == search.PutovanjeId);
            }
            if (search.KorisnikId.HasValue)
            {
                query = query.Where(x => x.KorisnikId == search.KorisnikId);
            }
            if (search.Ocjena.HasValue)
            {
                query = query.Where(x => x.Ocjena == search.Ocjena);
            }
            return query;
        }

        protected override IQueryable<Database.Ocjene> AddOrder(IQueryable<Database.Ocjene> query)
        {
            return query.OrderByDescending(x => x.Datum);
        }

        /// <summary>
        /// Korisnik moze ocijeniti putovanje samo jednom (bitno za sistem preporuke).
        /// Ako ocjena vec postoji, ona se azurira umjesto da se kreira duplikat.
        /// </summary>
        public override async Task<Model.Ocjene> InsertAsync(OcjeneInsertUpdateRequest request)
        {
            var postojeca = await Context.Ocjene
                .FirstOrDefaultAsync(x => x.KorisnikId == request.KorisnikId && x.PutovanjeId == request.PutovanjeId);

            if (postojeca != null)
            {
                return await UpdateAsync(postojeca.Id, request);
            }

            return await base.InsertAsync(request);
        }

        protected override Task OnInsertingAsync(Database.Ocjene entity, OcjeneInsertUpdateRequest request)
        {
            entity.Datum = DateTime.Now;
            return Task.CompletedTask;
        }

        protected override Task OnUpdatingAsync(Database.Ocjene entity, OcjeneInsertUpdateRequest request)
        {
            entity.Datum = DateTime.Now;
            return Task.CompletedTask;
        }

        protected override async Task BeforeUpdateAsync(Database.Ocjene entity, OcjeneInsertUpdateRequest request)
        {
            if (await Context.Ocjene.AnyAsync(x => x.KorisnikId == request.KorisnikId
                                                 && x.PutovanjeId == request.PutovanjeId
                                                 && x.Id != entity.Id))
            {
                throw new UserException("Korisnik je već ocijenio ovo putovanje.");
            }
        }
    }
}
