using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Putovanja;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class PutovanjaController : AdminCRUDController<Model.Putovanja, PutovanjaSearchRequest, PutovanjaInsertUpdateRequest, PutovanjaInsertUpdateRequest>
    {
        public PutovanjaController(IPutovanjaService service) : base(service)
        {
        }
    }
}
