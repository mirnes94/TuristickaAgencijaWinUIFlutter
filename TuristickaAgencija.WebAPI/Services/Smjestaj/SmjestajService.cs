using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Smjestaj
{
  
    public class SmjestajService : ISmjestajService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        public SmjestajService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Model.Smjestaj> Get()
        {
            var list = _context.Smjestaj.ToList();
           
            return _mapper.Map<List<Model.Smjestaj>>(list);
        }

        public Model.Smjestaj GetById(int id)
        {
            var entity = _context.Smjestaj.Find(id);

            return _mapper.Map<Model.Smjestaj>(entity);
        }

        public Model.Smjestaj Insert(SmjestajInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Smjestaj>(request);

            _context.Smjestaj.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Smjestaj>(entity);
        }

        public Model.Smjestaj Update(int id, SmjestajInsertUpdateRequest request)
        {
            var entity = _context.Smjestaj.Find(id);

            _context.Smjestaj.Attach(entity);
            _context.Smjestaj.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Smjestaj>(entity);
        }
    }
}
