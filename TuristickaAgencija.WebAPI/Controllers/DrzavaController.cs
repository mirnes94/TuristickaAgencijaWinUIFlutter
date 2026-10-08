using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Drzava;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class DrzavaController : AdminCRUDController<Model.Drzava, DrzavaSearchRequest, DrzavaInsertUpdateRequest, DrzavaInsertUpdateRequest>
    {
        public DrzavaController(IDrzavaService service) : base(service)
        {
        }
    }
}
