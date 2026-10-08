using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Drzava
{
    public class DrzavaService
        : BaseCRUDService<Model.Drzava, Database.Drzava, DrzavaSearchRequest, DrzavaInsertUpdateRequest, DrzavaInsertUpdateRequest>, IDrzavaService
    {
        public DrzavaService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Drzava> AddFilter(IQueryable<Database.Drzava> query, DrzavaSearchRequest search)
        {
            if (!string.IsNullOrWhiteSpace(search.Naziv))
            {
                query = query.Where(x => x.Naziv.Contains(search.Naziv));
            }

            return query;
        }

        protected override IQueryable<Database.Drzava> AddOrder(IQueryable<Database.Drzava> query)
        {
            return query.OrderBy(x => x.Naziv);
        }

        protected override async Task BeforeInsertAsync(DrzavaInsertUpdateRequest request)
        {
            if (await Context.Drzava.AnyAsync(x => x.Naziv == request.Naziv))
            {
                throw new UserException("Država sa tim nazivom već postoji.");
            }
        }

        protected override async Task BeforeUpdateAsync(Database.Drzava entity, DrzavaInsertUpdateRequest request)
        {
            if (await Context.Drzava.AnyAsync(x => x.Naziv == request.Naziv && x.Id != entity.Id))
            {
                throw new UserException("Država sa tim nazivom već postoji.");
            }
        }
    }
}
