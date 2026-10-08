using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Gradovi
{
    public class GradoviService
        : BaseCRUDService<Model.Gradovi, Database.Gradovi, GradoviSearchRequest, GradoviInsertUpdateRequest, GradoviInsertUpdateRequest>, IGradoviService
    {
        public GradoviService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Gradovi> AddInclude(IQueryable<Database.Gradovi> query)
        {
            return query.Include(x => x.Drzava);
        }

        protected override IQueryable<Database.Gradovi> AddFilter(IQueryable<Database.Gradovi> query, GradoviSearchRequest search)
        {
            if (!string.IsNullOrWhiteSpace(search.NazivGrada))
            {
                query = query.Where(x => x.NazivGrada.Contains(search.NazivGrada));
            }
            if (search.DrzavaId.HasValue)
            {
                query = query.Where(x => x.DrzavaId == search.DrzavaId);
            }

            return query;
        }

        protected override IQueryable<Database.Gradovi> AddOrder(IQueryable<Database.Gradovi> query)
        {
            return query.OrderBy(x => x.NazivGrada);
        }
    }
}
