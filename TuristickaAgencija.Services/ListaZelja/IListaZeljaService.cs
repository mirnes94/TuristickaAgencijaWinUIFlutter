using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.ListaZelja
{
    public interface IListaZeljaService
        : ICRUDService<Model.ListaZelja, ListaZeljaSearchRequest, ListaZeljaInsertUpdateRequest, ListaZeljaInsertUpdateRequest>
    {
    }
}
