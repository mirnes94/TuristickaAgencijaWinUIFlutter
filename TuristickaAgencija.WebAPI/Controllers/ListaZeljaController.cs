using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.ListaZelja;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class ListaZeljaController : KorisnikovCRUDController<Model.ListaZelja, ListaZeljaSearchRequest, ListaZeljaInsertUpdateRequest>
    {
        public ListaZeljaController(IListaZeljaService service) : base(service)
        {
        }
    }
}
