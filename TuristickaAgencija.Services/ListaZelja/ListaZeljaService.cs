using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.ListaZelja
{
    public class ListaZeljaService
        : BaseCRUDService<Model.ListaZelja, Database.ListaZelja, ListaZeljaSearchRequest, ListaZeljaInsertUpdateRequest, ListaZeljaInsertUpdateRequest>, IListaZeljaService
    {
        public ListaZeljaService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.ListaZelja> AddInclude(IQueryable<Database.ListaZelja> query)
        {
            return query.Include(x => x.Korisnik).Include(x => x.Putovanje);
        }

        protected override IQueryable<Database.ListaZelja> AddFilter(IQueryable<Database.ListaZelja> query, ListaZeljaSearchRequest search)
        {
            if (search.PutovanjeId.HasValue)
            {
                query = query.Where(x => x.PutovanjeId == search.PutovanjeId);
            }
            if (search.KorisnikId.HasValue)
            {
                query = query.Where(x => x.KorisnikId == search.KorisnikId);
            }
            return query;
        }

        protected override IQueryable<Database.ListaZelja> AddOrder(IQueryable<Database.ListaZelja> query)
        {
            return query.OrderBy(x => x.Id);
        }

        protected override async Task BeforeInsertAsync(ListaZeljaInsertUpdateRequest request)
        {
            if (await Context.ListaZelja.AnyAsync(x => x.KorisnikId == request.KorisnikId && x.PutovanjeId == request.PutovanjeId))
            {
                throw new UserException("Putovanje je već na listi želja.");
            }
        }
    }
}
