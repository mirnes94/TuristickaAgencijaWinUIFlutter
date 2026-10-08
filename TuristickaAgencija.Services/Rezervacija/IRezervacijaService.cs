using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Rezervacija
{
    public interface IRezervacijaService
        : ICRUDService<Model.Rezervacija, RezervacijaSearchRequest, RezervacijaInsertUpdateRequest, RezervacijaInsertUpdateRequest>
    {
    }
}
