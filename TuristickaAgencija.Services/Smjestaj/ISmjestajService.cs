using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Smjestaj
{
    public interface ISmjestajService : ICRUDService<Model.Smjestaj, SmjestajSearchRequest, SmjestajInsertUpdateRequest, SmjestajInsertUpdateRequest>
    {
    }
}
