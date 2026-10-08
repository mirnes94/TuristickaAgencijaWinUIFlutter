using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Prevoz
{
    public class PrevozService
        : BaseCRUDService<Model.Prevoz, Database.Prevoz, PrevozSearchRequest, PrevozInsertUpdateRequest, PrevozInsertUpdateRequest>, IPrevozService
    {
        public PrevozService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Prevoz> AddInclude(IQueryable<Database.Prevoz> query)
        {
            return query.Include(x => x.Firma);
        }

        protected override IQueryable<Database.Prevoz> AddFilter(IQueryable<Database.Prevoz> query, PrevozSearchRequest search)
        {
            if (!string.IsNullOrWhiteSpace(search.TipPrevoza))
            {
                query = query.Where(x => x.TipPrevoza.Contains(search.TipPrevoza));
            }
            if (search.FirmaId.HasValue)
            {
                query = query.Where(x => x.FirmaId == search.FirmaId);
            }

            return query;
        }

        protected override IQueryable<Database.Prevoz> AddOrder(IQueryable<Database.Prevoz> query)
        {
            return query.OrderBy(x => x.TipPrevoza);
        }
    }
}
