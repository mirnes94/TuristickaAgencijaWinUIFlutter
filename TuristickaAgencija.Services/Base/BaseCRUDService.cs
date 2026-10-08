using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Base
{
    public abstract class BaseCRUDService<TModel, TDb, TSearch, TInsert, TUpdate>
        : BaseReadService<TModel, TDb, TSearch>, ICRUDService<TModel, TSearch, TInsert, TUpdate>
        where TDb : class
        where TSearch : class
    {
        protected BaseCRUDService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public virtual async Task<TModel> InsertAsync(TInsert request)
        {
            await BeforeInsertAsync(request);

            var entity = Mapper.Map<TDb>(request);
            await OnInsertingAsync(entity, request);

            Context.Set<TDb>().Add(entity);
            await Context.SaveChangesAsync();

            await AfterInsertAsync(entity, request);

            var id = (int)Context.Entry(entity).Property("Id").CurrentValue;
            return await GetByIdAsync(id);
        }

        public virtual async Task<TModel> UpdateAsync(int id, TUpdate request)
        {
            var entity = await Context.Set<TDb>().FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Zapis koji pokusavate izmijeniti ne postoji.");
            }

            await BeforeUpdateAsync(entity, request);
            Mapper.Map(request, entity);
            await OnUpdatingAsync(entity, request);

            await Context.SaveChangesAsync();
            await AfterUpdateAsync(entity, request);

            return await GetByIdAsync(id);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await Context.Set<TDb>().FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Zapis koji pokusavate obrisati ne postoji.");
            }

            await BeforeDeleteAsync(entity);
            Context.Set<TDb>().Remove(entity);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new UserException("Zapis nije moguce obrisati jer se koristi u drugim podacima (npr. putovanja, rezervacije).");
            }
        }

        /// <summary>Validacija prije mapiranja (npr. jedinstvenost naziva).</summary>
        protected virtual Task BeforeInsertAsync(TInsert request) => Task.CompletedTask;

        /// <summary>Dopuna entiteta nakon mapiranja (npr. hash lozinke, datum).</summary>
        protected virtual Task OnInsertingAsync(TDb entity, TInsert request) => Task.CompletedTask;

        protected virtual Task AfterInsertAsync(TDb entity, TInsert request) => Task.CompletedTask;

        protected virtual Task BeforeUpdateAsync(TDb entity, TUpdate request) => Task.CompletedTask;

        protected virtual Task OnUpdatingAsync(TDb entity, TUpdate request) => Task.CompletedTask;

        protected virtual Task AfterUpdateAsync(TDb entity, TUpdate request) => Task.CompletedTask;

        /// <summary>Brisanje zavisnih podataka koji nemaju smisla bez roditelja.</summary>
        protected virtual Task BeforeDeleteAsync(TDb entity) => Task.CompletedTask;
    }
}
