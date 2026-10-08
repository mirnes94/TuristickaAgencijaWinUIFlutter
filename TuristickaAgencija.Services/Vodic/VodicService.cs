using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Vodic
{
    public class VodicService
        : BaseCRUDService<Model.Vodic, Database.Vodic, VodicSearchRequest, VodicInsertUpdateRequest, VodicInsertUpdateRequest>, IVodicService
    {
        public VodicService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Vodic> AddFilter(IQueryable<Database.Vodic> query, VodicSearchRequest search)
        {
            if (!string.IsNullOrWhiteSpace(search.Ime))
            {
                query = query.Where(x => x.Ime.StartsWith(search.Ime));
            }
            if (!string.IsNullOrWhiteSpace(search.Prezime))
            {
                query = query.Where(x => x.Prezime.StartsWith(search.Prezime));
            }

            return query;
        }

        protected override IQueryable<Database.Vodic> AddOrder(IQueryable<Database.Vodic> query)
        {
            return query.OrderBy(x => x.Prezime).ThenBy(x => x.Ime);
        }

        protected override async Task BeforeInsertAsync(VodicInsertUpdateRequest request)
        {
            if (await Context.Vodic.AnyAsync(x => x.Jmbg == request.Jmbg))
            {
                throw new UserException("Vodič sa tim JMBG-om već postoji.");
            }
        }

        protected override async Task BeforeUpdateAsync(Database.Vodic entity, VodicInsertUpdateRequest request)
        {
            if (await Context.Vodic.AnyAsync(x => x.Jmbg == request.Jmbg && x.Id != entity.Id))
            {
                throw new UserException("Vodič sa tim JMBG-om već postoji.");
            }
        }
    }
}
