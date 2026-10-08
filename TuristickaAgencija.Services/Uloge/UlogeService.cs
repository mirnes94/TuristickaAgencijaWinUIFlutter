using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Uloge
{
    public class UlogeService
        : BaseCRUDService<Model.Uloge, Database.Uloge, UlogeSearchRequest, UlogeInsertUpdateRequest, UlogeInsertUpdateRequest>, IUlogeService
    {
        public UlogeService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Uloge> AddFilter(IQueryable<Database.Uloge> query, UlogeSearchRequest search)
        {
            if (!string.IsNullOrWhiteSpace(search.Naziv))
            {
                query = query.Where(x => x.Naziv.Contains(search.Naziv));
            }

            return query;
        }

        protected override IQueryable<Database.Uloge> AddOrder(IQueryable<Database.Uloge> query)
        {
            return query.OrderBy(x => x.Naziv);
        }

        protected override async Task BeforeInsertAsync(UlogeInsertUpdateRequest request)
        {
            if (await Context.Uloge.AnyAsync(x => x.Naziv == request.Naziv))
            {
                throw new UserException("Uloga sa tim nazivom već postoji.");
            }
        }

        protected override async Task BeforeUpdateAsync(Database.Uloge entity, UlogeInsertUpdateRequest request)
        {
            if (await Context.Uloge.AnyAsync(x => x.Naziv == request.Naziv && x.Id != entity.Id))
            {
                throw new UserException("Uloga sa tim nazivom već postoji.");
            }
        }
    }
}
