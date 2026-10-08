using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Obavijesti
{
    public interface IObavijestiService
        : ICRUDService<Model.Obavijesti, ObavijestiSearchRequest, ObavijestiInsertUpdateRequest, ObavijestiInsertUpdateRequest>
    {
    }
}
