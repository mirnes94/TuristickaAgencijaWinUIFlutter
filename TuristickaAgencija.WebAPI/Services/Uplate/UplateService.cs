using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Uplate
{
    
    public class UplateService : IUplateService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        public UplateService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Model.Uplate> Get(UplateSearchRequest request)
        {
            var query = _context.Uplate.AsQueryable();

            if (request?.KorisnikId.HasValue == true)
            {
                query = query.Where(x => x.KorisnikId == request.KorisnikId);
            }



            var list = query.ToList();
            return _mapper.Map<List<Model.Uplate>>(list);
        }

        public Model.Uplate GetById(int id)
        {
            var entity = _context.Uplate.Find(id);

            return _mapper.Map<Model.Uplate>(entity);
        }

        public List<Model.Uplate> GetByMonth(int mjesec)
        {
             var uplate = _context.Uplate.ToList();

           List<Database.Uplate> listaUplataZaMjesec = new List<Database.Uplate>();
           foreach(var uplata in uplate)
            {
                if(uplata.Datum.Month==mjesec)
                listaUplataZaMjesec.Add(uplata);
            }

            return _mapper.Map<List<Model.Uplate>>(listaUplataZaMjesec);
        }

        public Model.Uplate Insert(UplateInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Uplate>(request);

            _context.Uplate.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Uplate>(entity);
        }

        public Model.Uplate Update(int id, UplateInsertUpdateRequest request)
        {
            var entity = _context.Uplate.Find(id);

            _context.Uplate.Attach(entity);
            _context.Uplate.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Uplate>(entity);
        }
    }
}
