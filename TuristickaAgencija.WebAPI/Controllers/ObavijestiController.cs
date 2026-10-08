using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Obavijesti;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class ObavijestiController : AdminCRUDController<Model.Obavijesti, ObavijestiSearchRequest, ObavijestiInsertUpdateRequest, ObavijestiInsertUpdateRequest>
    {
        public ObavijestiController(IObavijestiService service) : base(service)
        {
        }
    }
}
