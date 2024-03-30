using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Firma
{
  
    public class FirmaService : IFirmaService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        public FirmaService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<Model.Firma> Get()
        {
            var list = _context.Firma.ToList();

            return _mapper.Map<List<Model.Firma>>(list);
        }

        public Model.Firma GetById(int id)
        {
            var entity = _context.Firma.Find(id);

            return _mapper.Map<Model.Firma>(entity);
        }

        public Model.Firma Insert(FirmaInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Firma>(request);

            _context.Firma.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Firma>(entity);
        }

        public Model.Firma Update(int id, FirmaInsertUpdateRequest request)
        {
            var entity = _context.Firma.Find(id);

            _context.Firma.Attach(entity);
            _context.Firma.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Firma>(entity);
        }
    }
}
