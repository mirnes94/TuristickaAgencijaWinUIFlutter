using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Preporuke;
using TuristickaAgencija.Services.RecommenderService;
using TuristickaAgencija.WebAPI.Security;

namespace TuristickaAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RecommenderController : ControllerBase
    {
        private readonly IRecommenderService _service;

        public RecommenderController(IRecommenderService service)
        {
            _service = service;
        }

        /// <summary>Preporuke za prijavljenog korisnika (mobilna aplikacija - pocetna stranica).</summary>
        [HttpGet("Moje")]
        public Task<List<Model.Putovanja>> Moje([FromQuery] int? broj)
        {
            return _service.PreporucenaPutovanjaAsync(User.KorisnikId(), broj);
        }

        /// <summary>
        /// Zadrzano zbog kompatibilnosti sa mobilnom aplikacijom (detalji putovanja -> "Preporuceno za vas").
        /// Vraca preporuke za prijavljenog korisnika, bez putovanja koje trenutno gleda.
        /// </summary>
        [HttpGet("GetRecommendedPutovanja/{putovanjeId:int}")]
        public Task<List<Model.Putovanja>> GetRecommendedPutovanja(int putovanjeId)
        {
            return _service.PreporucenaPutovanjaAsync(User.KorisnikId(), null, putovanjeId);
        }

        /// <summary>Detaljan rezultat algoritma (slicni korisnici + predvidjene ocjene) - desktop aplikacija.</summary>
        [HttpGet("Korisnik/{korisnikId:int}")]
        [Authorize(Roles = UlogeNazivi.Admin)]
        public Task<PreporukaRezultat> ZaKorisnika(int korisnikId, [FromQuery] int? broj)
        {
            return _service.PreporuciAsync(korisnikId, broj);
        }
    }
}
