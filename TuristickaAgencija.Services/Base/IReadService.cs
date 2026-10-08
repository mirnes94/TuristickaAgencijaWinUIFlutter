namespace TuristickaAgencija.Services.Base
{
    public interface IReadService<TModel, TSearch> where TSearch : class
    {
        Task<List<TModel>> GetAsync(TSearch search);
        Task<TModel> GetByIdAsync(int id);
    }

    public interface ICRUDService<TModel, TSearch, TInsert, TUpdate> : IReadService<TModel, TSearch>
        where TSearch : class
    {
        Task<TModel> InsertAsync(TInsert request);
        Task<TModel> UpdateAsync(int id, TUpdate request);
        Task DeleteAsync(int id);
    }
}
