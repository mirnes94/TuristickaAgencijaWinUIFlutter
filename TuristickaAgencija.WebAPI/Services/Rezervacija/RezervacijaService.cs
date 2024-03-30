using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Rezervacija
{
    
    public class RezervacijaService : IRezervacijaService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        public RezervacijaService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Delete(int id)
        {
            var entity = _context.Rezervacija.Find(id);
            var uplata = _context.Uplate.FirstOrDefault(e => e.RezervacijaId == entity.Id);
            _context.Rezervacija.Remove(entity);
            _context.Uplate.Remove(uplata);
            _context.SaveChanges();
        }

        public List<Model.Rezervacija> Get(RezervacijaSearchRequest request)
        {
            var query = _context.Rezervacija.AsQueryable();

            if (request?.KorisnikId.HasValue == true)
            {
                query = query.Where(x => x.KorisnikId == request.KorisnikId);
            }
          
          

            var list = query.ToList();
            return _mapper.Map<List<Model.Rezervacija>>(list);
        }

        public Model.Rezervacija GetById(int id)
        {
            var entity = _context.Rezervacija.Find(id);

            return _mapper.Map<Model.Rezervacija>(entity);
        }

        public Model.Rezervacija Insert(RezervacijaInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Rezervacija>(request);

            _context.Rezervacija.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Rezervacija>(entity);
        }

        public Model.Rezervacija Update(int id, RezervacijaInsertUpdateRequest request)
        {
            var entity = _context.Rezervacija.Find(id);

            _context.Rezervacija.Attach(entity);
            _context.Rezervacija.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Rezervacija>(entity);
        }
    }
    
}
