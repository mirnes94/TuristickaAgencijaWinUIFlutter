using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Smjestaj
{
    public class SmjestajService
        : BaseCRUDService<Model.Smjestaj, Database.Smjestaj, SmjestajSearchRequest, SmjestajInsertUpdateRequest, SmjestajInsertUpdateRequest>, ISmjestajService
    {
        public SmjestajService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Smjestaj> AddFilter(IQueryable<Database.Smjestaj> query, SmjestajSearchRequest search)
        {
            if (!string.IsNullOrWhiteSpace(search.NazivSmjestaja))
            {
                query = query.Where(x => x.NazivSmjestaja.Contains(search.NazivSmjestaja));
            }

            return query;
        }

        protected override IQueryable<Database.Smjestaj> AddOrder(IQueryable<Database.Smjestaj> query)
        {
            return query.OrderBy(x => x.NazivSmjestaja);
        }
    }
}
