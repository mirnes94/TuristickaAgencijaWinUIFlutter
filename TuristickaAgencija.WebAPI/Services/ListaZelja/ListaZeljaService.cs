using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.ListaZelja
{
    public class ListaZeljaService : IListaZeljaService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;

        public ListaZeljaService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Delete(int id)
        {
            var entity = _context.ListaZelja.Find(id);
            _context.ListaZelja.Remove(entity);
            _context.SaveChanges();
        }

        public List<Model.ListaZelja> Get(ListaZeljaSearchRequest request)
        {
            var query = _context.Set<Database.ListaZelja>().AsQueryable();
            if (request?.PutovanjeId.HasValue == true)
            {
                query = query.Where(x => x.PutovanjeId == request.PutovanjeId);
            }
            if (request?.KorisnikId.HasValue == true)
            {
                query = query.Where(x => x.KorisnikId == request.KorisnikId);
            }
            var list = query.ToList();

            return _mapper.Map<List<Model.ListaZelja>>(list);
        }

        public Model.ListaZelja GetById(int id)
        {
            var entity = _context.ListaZelja.Find(id);

            return _mapper.Map<Model.ListaZelja>(entity);
        }

        public Model.ListaZelja Insert(ListaZeljaInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.ListaZelja>(request);

            _context.ListaZelja.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.ListaZelja>(entity);
        }

        public Model.ListaZelja Update(int id, ListaZeljaInsertUpdateRequest request)
        {
            var entity = _context.ListaZelja.Find(id);

            _context.ListaZelja.Attach(entity);
            _context.ListaZelja.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.ListaZelja>(entity);
        }
    }
}
