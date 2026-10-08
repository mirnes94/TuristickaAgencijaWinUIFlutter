using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Smjestaj;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class SmjestajController : AdminCRUDController<Model.Smjestaj, SmjestajSearchRequest, SmjestajInsertUpdateRequest, SmjestajInsertUpdateRequest>
    {
        public SmjestajController(ISmjestajService service) : base(service)
        {
        }
    }
}
