using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Prevoz
{
    public interface IPrevozService : ICRUDService<Model.Prevoz, PrevozSearchRequest, PrevozInsertUpdateRequest, PrevozInsertUpdateRequest>
    {
    }
}
