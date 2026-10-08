using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Drzava
{
    public interface IDrzavaService : ICRUDService<Model.Drzava, DrzavaSearchRequest, DrzavaInsertUpdateRequest, DrzavaInsertUpdateRequest>
    {
    }
}
