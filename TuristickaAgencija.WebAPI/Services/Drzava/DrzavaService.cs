using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Drzava
{
    
    public class DrzavaService : IDrzavaService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        public DrzavaService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public List<Model.Drzava> Get()
        {
            var list = _context.Drzava.ToList();

            return _mapper.Map<List<Model.Drzava>>(list);

        }

        public Model.Drzava GetById(int id)
        {
            var entity = _context.Drzava.Find(id);

            return _mapper.Map<Model.Drzava>(entity);

        }

        public Model.Drzava Insert(DrzavaInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Drzava>(request);

            _context.Drzava.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Drzava>(entity);
        }

        public Model.Drzava Update(int id, DrzavaInsertUpdateRequest request)
        {
            var entity = _context.Drzava.Find(id);

            _context.Drzava.Attach(entity);
            _context.Drzava.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Drzava>(entity);
        }

    }
}
