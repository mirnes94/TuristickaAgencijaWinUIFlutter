using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Uloge;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class UlogeController : AdminCRUDController<Model.Uloge, UlogeSearchRequest, UlogeInsertUpdateRequest, UlogeInsertUpdateRequest>
    {
        public UlogeController(IUlogeService service) : base(service)
        {
        }
    }
}
