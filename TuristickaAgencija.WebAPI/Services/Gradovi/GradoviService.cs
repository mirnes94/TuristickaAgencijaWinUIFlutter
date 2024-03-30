using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Gradovi
{
    
   public class GradoviService:IGradoviService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        public GradoviService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Gradovi> Get()
        {
            var list = _context.Gradovi.ToList();

            return _mapper.Map<List<Model.Gradovi>>(list);
        }

        public Model.Gradovi GetById(int id)
        {
            var entity = _context.Gradovi.Find(id);
          

            return _mapper.Map<Model.Gradovi>(entity);
        }

        public Model.Gradovi Insert(GradoviInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Gradovi>(request);

            _context.Gradovi.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Gradovi>(entity);
        }

        public Model.Gradovi Update(int id, GradoviInsertUpdateRequest request)
        {
            var entity = _context.Gradovi.Find(id);

            _context.Gradovi.Attach(entity);
            _context.Gradovi.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Gradovi>(entity);
        }
    }
}
