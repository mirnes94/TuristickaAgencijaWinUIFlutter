using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Firma;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class FirmaController : AdminCRUDController<Model.Firma, FirmaSearchRequest, FirmaInsertUpdateRequest, FirmaInsertUpdateRequest>
    {
        public FirmaController(IFirmaService service) : base(service)
        {
        }
    }
}
