using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Ocjene
{
    public interface IOcjeneService
        : ICRUDService<Model.Ocjene, OcjeneSearchRequest, OcjeneInsertUpdateRequest, OcjeneInsertUpdateRequest>
    {
    }
}
