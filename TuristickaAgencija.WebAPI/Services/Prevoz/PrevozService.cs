using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Prevoz
{
  
    public class PrevozService : IPrevozService
    {

        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        public PrevozService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Prevoz> Get()
        {
            var list = _context.Prevoz.ToList();

            return _mapper.Map<List<Model.Prevoz>>(list);
        }

        public Model.Prevoz GetById(int id)
        {
            var entity = _context.Prevoz.Find(id);

            return _mapper.Map<Model.Prevoz>(entity);
        }

        public Model.Prevoz Insert(PrevozInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Prevoz>(request);

            _context.Prevoz.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Prevoz>(entity);
        }

        public Model.Prevoz Update(int id, PrevozInsertUpdateRequest request)
        {
            var entity = _context.Prevoz.Find(id);

            _context.Prevoz.Attach(entity);
            _context.Prevoz.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Prevoz>(entity);
        }
    }
  
}
