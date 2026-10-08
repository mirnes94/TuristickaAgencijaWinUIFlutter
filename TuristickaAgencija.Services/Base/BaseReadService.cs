using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Base
{
    /// <summary>
    /// Zajednicka logika citanja: include -> filter -> sortiranje -> mapiranje u DTO.
    /// Konkretni servisi samo nadjacavaju AddInclude/AddFilter/AddOrder.
    /// </summary>
    public abstract class BaseReadService<TModel, TDb, TSearch> : IReadService<TModel, TSearch>
        where TDb : class
        where TSearch : class
    {
        protected readonly TuristickaAgencijaContext Context;
        protected readonly IMapper Mapper;

        protected BaseReadService(TuristickaAgencijaContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }

        public virtual async Task<List<TModel>> GetAsync(TSearch search)
        {
            IQueryable<TDb> query = Context.Set<TDb>().AsNoTracking();
            query = AddInclude(query);
            if (search != null)
            {
                query = AddFilter(query, search);
            }
            query = AddOrder(query);

            var list = await query.ToListAsync();
            return Mapper.Map<List<TModel>>(list);
        }

        public virtual async Task<TModel> GetByIdAsync(int id)
        {
            var query = AddInclude(Context.Set<TDb>().AsNoTracking());
            var entity = await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
            if (entity == null)
            {
                throw new NotFoundException("Trazeni zapis ne postoji.");
            }
            return Mapper.Map<TModel>(entity);
        }

        protected virtual IQueryable<TDb> AddInclude(IQueryable<TDb> query) => query;

        protected virtual IQueryable<TDb> AddFilter(IQueryable<TDb> query, TSearch search) => query;

        protected virtual IQueryable<TDb> AddOrder(IQueryable<TDb> query) => query.OrderBy(e => EF.Property<int>(e, "Id"));
    }
}
