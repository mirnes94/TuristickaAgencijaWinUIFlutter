using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Prevoz;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class PrevozController : AdminCRUDController<Model.Prevoz, PrevozSearchRequest, PrevozInsertUpdateRequest, PrevozInsertUpdateRequest>
    {
        public PrevozController(IPrevozService service) : base(service)
        {
        }
    }
}
