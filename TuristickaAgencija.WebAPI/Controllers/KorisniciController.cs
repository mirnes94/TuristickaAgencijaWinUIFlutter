using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Exceptions;
using TuristickaAgencija.Services.Korisnici;
using TuristickaAgencija.WebAPI.Security;

namespace TuristickaAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class KorisniciController : ControllerBase
    {
        private readonly IKorisniciService _korisniciService;

        public KorisniciController(IKorisniciService korisniciService)
        {
            _korisniciService = korisniciService;
        }

        /// <summary>Administrator pretrazuje sve korisnike; klijent dobija samo svoj zapis.</summary>
        [HttpGet]
        public async Task<List<Model.Korisnici>> Get([FromQuery] KorisniciSearchRequest search)
        {
            if (User.JeAdmin())
            {
                return await _korisniciService.GetAsync(search);
            }

            return new List<Model.Korisnici> { await _korisniciService.GetByIdAsync(User.KorisnikId()) };
        }

        [HttpGet("{id:int}")]
        public Task<Model.Korisnici> GetById(int id)
        {
            ProvjeriAdminIliVlasnik(id);
            return _korisniciService.GetByIdAsync(id);
        }

        /// <summary>Podaci o prijavljenom korisniku (koristi se za login u desktop i mobilnoj aplikaciji).</summary>
        [HttpGet("Prijava")]
        public Task<Model.Korisnici> Prijava()
        {
            return _korisniciService.GetByIdAsync(User.KorisnikId());
        }

        /// <summary>Zadrzano zbog kompatibilnosti sa postojecom mobilnom aplikacijom (provjera se radi kroz Basic auth zaglavlje).</summary>
        [HttpGet("Authenticiraj/{username},{password}")]
        public Task<Model.Korisnici> Authenticiraj(string username, string password)
        {
            return _korisniciService.GetByIdAsync(User.KorisnikId());
        }

        /// <summary>
        /// Administrator kreira korisnika sa proizvoljnim ulogama. Neprijavljeni korisnik se registruje
        /// kao Klijent i dobija email (preko RabbitMQ -> Subscriber) sa linkom za aktivaciju naloga.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        public Task<Model.Korisnici> Insert([FromBody] KorisniciInsertUpdateRequest request)
        {
            return User.JeAdmin()
                ? _korisniciService.InsertAsync(request)
                : _korisniciService.RegistrujAsync(request);
        }

        [HttpPut("{id:int}")]
        public Task<Model.Korisnici> Update(int id, [FromBody] KorisniciInsertUpdateRequest request)
        {
            // Administrator mijenja druge korisnike (uloge, status, lozinka bez stare lozinke).
            if (User.JeAdmin() && id != User.KorisnikId())
            {
                return _korisniciService.UpdateAsync(id, request);
            }

            // Svako (i administrator) svoj profil mijenja uz potvrdu stare lozinke.
            ProvjeriAdminIliVlasnik(id);
            return _korisniciService.UpdateProfilAsync(id, request);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ProvjeriAdminIliVlasnik(id);
            if (User.JeAdmin() && id == User.KorisnikId())
            {
                throw new UserException("Ne možete obrisati nalog sa kojim ste trenutno prijavljeni.");
            }

            await _korisniciService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("Potvrdi/{username}")]
        [AllowAnonymous]
        public async Task<ContentResult> Potvrdi(string username, [FromQuery] string token)
        {
            var uspjeh = await _korisniciService.PotvrdiAsync(username, token);
            var poruka = uspjeh
                ? "Vaš nalog je uspješno aktiviran. Sada se možete prijaviti u mobilnu aplikaciju."
                : "Link za aktivaciju nije ispravan ili je nalog već obrisan.";

            return Content(
                $"<html><head><meta charset=\"utf-8\"><title>Turistička agencija</title></head>" +
                $"<body style=\"font-family:sans-serif;padding:40px\"><h2>Turistička agencija</h2><p>{poruka}</p></body></html>",
                "text/html; charset=utf-8");
        }

        private void ProvjeriAdminIliVlasnik(int id)
        {
            if (!User.JeAdmin() && User.KorisnikId() != id)
            {
                throw new ForbiddenException("Nemate pravo pristupa podacima drugog korisnika.");
            }
        }
    }
}
