using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Exceptions;
using TuristickaAgencija.Services.Rezervacija;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class RezervacijaController : KorisnikovCRUDController<Model.Rezervacija, RezervacijaSearchRequest, RezervacijaInsertUpdateRequest>
    {
        public RezervacijaController(IRezervacijaService service) : base(service)
        {
        }

        /// <summary>Nova rezervacija klijenta je uvijek "U obradi" dok se ne uplati cijeli iznos.</summary>
        protected override Task ProvjeriUnosKlijentaAsync(RezervacijaInsertUpdateRequest request)
        {
            request.Status = StatusRezervacije.UObradi;
            return Task.CompletedTask;
        }

        /// <summary>Klijent ne moze sam potvrditi rezervaciju - moze je samo otkazati (status mijenja administrator ili uplata).</summary>
        protected override void ProvjeriIzmjenuKlijenta(Model.Rezervacija postojeci, RezervacijaInsertUpdateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Status))
            {
                request.Status = postojeci.Status;
            }

            if (request.Status != postojeci.Status && request.Status != StatusRezervacije.Otkazano)
            {
                throw new ForbiddenException("Rezervaciju možete samo otkazati.");
            }
        }
    }
}
