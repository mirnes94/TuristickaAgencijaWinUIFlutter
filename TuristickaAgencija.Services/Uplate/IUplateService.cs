using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Uplate
{
    public interface IUplateService
        : ICRUDService<Model.Uplate, UplateSearchRequest, UplateInsertUpdateRequest, UplateInsertUpdateRequest>
    {
    }
}
