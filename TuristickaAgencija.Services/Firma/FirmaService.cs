using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Firma
{
    public class FirmaService
        : BaseCRUDService<Model.Firma, Database.Firma, FirmaSearchRequest, FirmaInsertUpdateRequest, FirmaInsertUpdateRequest>, IFirmaService
    {
        public FirmaService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Firma> AddInclude(IQueryable<Database.Firma> query)
        {
            return query.Include(x => x.Grad);
        }

        protected override IQueryable<Database.Firma> AddFilter(IQueryable<Database.Firma> query, FirmaSearchRequest search)
        {
            if (!string.IsNullOrWhiteSpace(search.Naziv))
            {
                query = query.Where(x => x.Naziv.Contains(search.Naziv));
            }
            if (search.GradId.HasValue)
            {
                query = query.Where(x => x.GradId == search.GradId);
            }

            return query;
        }

        protected override IQueryable<Database.Firma> AddOrder(IQueryable<Database.Firma> query)
        {
            return query.OrderBy(x => x.Naziv);
        }
    }
}
