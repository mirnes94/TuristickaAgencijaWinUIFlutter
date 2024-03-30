using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.Uloge
{
  
    public class UlogeService : IUlogeService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        public UlogeService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
       
        public List<Model.Uloge> Get()
        {
            List<Model.Uloge> result = new List<Model.Uloge>();
            var list = _context.Uloge.ToList();

           
            foreach (var item in list)
            {
                Model.Uloge role = new Model.Uloge();
                role.Naziv = item.Naziv;
                role.Opis = item.Opis;
                role.Id= item.Id;

                result.Add(role);
            }
            //return _mapper.Map<List<Model.Uloge>>(list);
            return result;
        }

        public Model.Uloge GetById(int id)
        {
            var entity = _context.Uloge.Find(id);

            return _mapper.Map<Model.Uloge>(entity);
        }

        public Model.Uloge Insert(UlogeInsertUpdateRequest request)
        {
            var entity = _mapper.Map<Database.Uloge>(request);

            _context.Uloge.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Uloge>(entity);
        }

        public Model.Uloge Update(int id, UlogeInsertUpdateRequest request)
        {
            var entity = _context.Uloge.Find(id);

            _context.Uloge.Attach(entity);
            _context.Uloge.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Uloge>(entity);
        }
    }
}
