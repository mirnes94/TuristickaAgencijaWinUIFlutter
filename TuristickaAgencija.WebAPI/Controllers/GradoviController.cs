using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Gradovi;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class GradoviController : AdminCRUDController<Model.Gradovi, GradoviSearchRequest, GradoviInsertUpdateRequest, GradoviInsertUpdateRequest>
    {
        public GradoviController(IGradoviService service) : base(service)
        {
        }
    }
}
