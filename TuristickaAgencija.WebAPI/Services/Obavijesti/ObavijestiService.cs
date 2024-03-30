using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Obavijesti
{
    public class ObavijestiService : IObavijestiService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;

        public ObavijestiService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Obavijesti> Get(ObavijestiSearchRequest request)
        {
            var query = _context.Obavijesti.AsQueryable();

            if (request?.KorisnikId.HasValue == true)
            {
                query = query.Where(x => x.KorisnikId == request.KorisnikId);
            }

            var list = query.ToList();
            return _mapper.Map<List<Model.Obavijesti>>(list);
        }

        public Model.Obavijesti GetById(int id)
        {
            var entity = _context.Obavijesti.Find(id);

            return _mapper.Map<Model.Obavijesti>(entity);
        }

        public Model.Obavijesti Insert(ObavijestiInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Obavijesti>(request);

            _context.Obavijesti.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Obavijesti>(entity);
        }

        public Model.Obavijesti Update(int id, ObavijestiInsertUpdateRequest request)
        {
            var entity = _context.Obavijesti.Find(id);

            _context.Obavijesti.Attach(entity);
            _context.Obavijesti.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Obavijesti>(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Obavijesti.Find(id);
            _context.Obavijesti.Remove(entity);
            _context.SaveChanges();
        }
    }
}
