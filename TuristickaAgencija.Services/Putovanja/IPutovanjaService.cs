using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Putovanja
{
    public interface IPutovanjaService
        : ICRUDService<Model.Putovanja, PutovanjaSearchRequest, PutovanjaInsertUpdateRequest, PutovanjaInsertUpdateRequest>
    {
    }
}
