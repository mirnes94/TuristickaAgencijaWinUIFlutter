using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Vodic;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class VodicController : AdminCRUDController<Model.Vodic, VodicSearchRequest, VodicInsertUpdateRequest, VodicInsertUpdateRequest>
    {
        public VodicController(IVodicService service) : base(service)
        {
        }
    }
}
