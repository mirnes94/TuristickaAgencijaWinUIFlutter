using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Komentar
{
    public class KomentarService : IKomentarService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;

        public KomentarService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Model.Komentar> Get(KomentarSearchRequest request)
        {
            var query = _context.Komentar.AsQueryable();

            if (request?.PutovanjeId.HasValue == true)
            {
                query = query.Where(x => x.PutovanjeId == request.PutovanjeId);
            }



            var list = query.ToList();
            return _mapper.Map<List<Model.Komentar>>(list);
        }

        public Model.Komentar GetById(int id)
        {
            var entity = _context.Komentar.Find(id);

            return _mapper.Map<Model.Komentar>(entity);
        }

        public Model.Komentar Insert(KomentarInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Komentar>(request);

            _context.Komentar.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Komentar>(entity);
        }

        public Model.Komentar Update(int id, KomentarInsertUpdateRequest request)
        {
            var entity = _context.Komentar.Find(id);

            _context.Komentar.Attach(entity);
            _context.Komentar.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Komentar>(entity);
        }
        public void Delete(int id)
        {
            var entity = _context.Komentar.Find(id);
            _context.Komentar.Remove(entity);
            _context.SaveChanges();
        }
    }
}
