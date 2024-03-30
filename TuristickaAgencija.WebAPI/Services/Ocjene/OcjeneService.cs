using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Ocjene
{
    public class OcjeneService : IOcjeneService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;

        public OcjeneService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Ocjene> Get(OcjeneSearchRequest request)
        {
            var query = _context.Ocjene.AsQueryable();

            if (request?.PutovanjeId.HasValue == true)
            {
                query = query.Where(x => x.PutovanjeId == request.PutovanjeId);
            }



            var list = query.ToList();
            return _mapper.Map<List<Model.Ocjene>>(list);
        }

        public Model.Ocjene GetById(int id)
        {
            var entity = _context.Ocjene.Find(id); 

            return _mapper.Map<Model.Ocjene>(entity);
        }

        public Model.Ocjene Insert(OcjeneInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Ocjene>(request);

            _context.Ocjene.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Ocjene>(entity);
        }

        public Model.Ocjene Update(int id, OcjeneInsertUpdateRequest request)
        {
            var entity = _context.Ocjene.Find(id);

            _context.Ocjene.Attach(entity);
            _context.Ocjene.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Ocjene>(entity);
        }
    }
}
