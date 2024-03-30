using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Vodic
{
    public class VodicService : IVodicService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;

        public VodicService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public virtual IList<Model.Vodic> Get(VodicSearchRequest request)
        {
            var query = _context.Vodic.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request?.Ime))
            {
                query = query.Where(x => x.Ime.StartsWith(request.Ime));
            }

            if (!string.IsNullOrWhiteSpace(request?.Prezime))
            {
                query = query.Where(x => x.Prezime.StartsWith(request.Prezime));
            }


           
           
            var list = query.ToList();


           
           
            
            return _mapper.Map<IList<Model.Vodic>>(list);
        }

        public Model.Vodic GetById(int id)
        {
            var entity = _context.Vodic.Find(id);

            return _mapper.Map<Model.Vodic>(entity);
        }

        public Model.Vodic Insert(VodicInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Vodic>(request);

            _context.Vodic.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Vodic>(entity);
        }

        public Model.Vodic Update(int id, VodicInsertUpdateRequest request)
        {
            var entity = _context.Vodic.Find(id);

            _context.Vodic.Attach(entity);
            _context.Vodic.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Vodic>(entity);
        }
    }
}
