using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Putovanja
{
    
    public class PutovanjaService : IPutovanjaService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        public PutovanjaService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public virtual IList<Model.Putovanja> Get(PutovanjaSearchRequest request)
        {
            var query = _context.Putovanja.AsQueryable();

            if (request?.GradId.HasValue==true)
            {
                query = query.Where(x=>x.GradId==request.GradId);
            }
            if (request?.SmjestajId.HasValue == true)
            {
                query = query.Where(x => x.SmjestajId == request.SmjestajId);
            }
            if (!string.IsNullOrWhiteSpace(request?.NazivPutovanja))
            {
                query = query.Where(x => x.NazivPutovanja.StartsWith(request.NazivPutovanja));
            }

            var list = query.ToList();

           
            return _mapper.Map<IList<Model.Putovanja>>(list);
        }

        public Model.Putovanja GetById(int id)
        {
            var entity = _context.Putovanja.Find(id);

            return _mapper.Map<Model.Putovanja>(entity);
        }

        public Model.Putovanja Insert(PutovanjaInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Putovanja>(request);

            _context.Putovanja.Add(entity);
            _context.SaveChanges();

            foreach (var vodic in request.Vodici)
            {
                Database.VodiciPutovanja vodiciPutovanja = new Database.VodiciPutovanja();
                vodiciPutovanja.PutovanjeId = entity.Id;
                vodiciPutovanja.VodicId = vodic;
               
                _context.VodiciPutovanja.Add(vodiciPutovanja);
            }
            _context.SaveChanges();

            return _mapper.Map<Model.Putovanja>(entity);
        }

        public Model.Putovanja Update(int id, PutovanjaInsertUpdateRequest request)
        {
            var entity = _context.Putovanja.Find(id);

            foreach (var vodic in request.Vodici)
            {
                Database.VodiciPutovanja vodiciPutovanja = new Database.VodiciPutovanja();
                vodiciPutovanja.PutovanjeId = entity.Id;
                vodiciPutovanja.VodicId = vodic;

                _context.VodiciPutovanja.Add(vodiciPutovanja);
            }

            _context.Putovanja.Attach(entity);
            _context.Putovanja.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Putovanja>(entity);
        }
    }
}
