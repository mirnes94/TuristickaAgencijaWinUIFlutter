using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Komentar
{
    public class KomentarService
        : BaseCRUDService<Model.Komentar, Database.Komentar, KomentarSearchRequest, KomentarInsertUpdateRequest, KomentarInsertUpdateRequest>, IKomentarService
    {
        public KomentarService(TuristickaAgencijaContext context, IMapper mapper) : base(context, mapper)
        {
        }

        protected override IQueryable<Database.Komentar> AddInclude(IQueryable<Database.Komentar> query)
        {
            return query.Include(x => x.Korisnik).Include(x => x.Putovanje);
        }

        protected override IQueryable<Database.Komentar> AddFilter(IQueryable<Database.Komentar> query, KomentarSearchRequest search)
        {
            if (search.PutovanjeId.HasValue)
            {
                query = query.Where(x => x.PutovanjeId == search.PutovanjeId);
            }
            if (search.KorisnikId.HasValue)
            {
                query = query.Where(x => x.KorisnikId == search.KorisnikId);
            }
            if (!string.IsNullOrWhiteSpace(search.Sadrzaj))
            {
                query = query.Where(x => x.Sadrzaj.Contains(search.Sadrzaj));
            }
            return query;
        }

        protected override IQueryable<Database.Komentar> AddOrder(IQueryable<Database.Komentar> query)
        {
            return query.OrderByDescending(x => x.Datum);
        }

        protected override Task OnInsertingAsync(Database.Komentar entity, KomentarInsertUpdateRequest request)
        {
            if (entity.Datum == default)
            {
                entity.Datum = DateTime.Now;
            }
            return Task.CompletedTask;
        }

        protected override Task OnUpdatingAsync(Database.Komentar entity, KomentarInsertUpdateRequest request)
        {
            if (request.Datum == default)
            {
                // datum se ne mijenja ako ga klijent nije poslao
                entity.Datum = Context.Entry(entity).Property(x => x.Datum).OriginalValue;
            }
            return Task.CompletedTask;
        }
    }
}
