using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Uloge
{
    public interface IUlogeService : ICRUDService<Model.Uloge, UlogeSearchRequest, UlogeInsertUpdateRequest, UlogeInsertUpdateRequest>
    {
    }
}
