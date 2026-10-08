using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Model;
using TuristickaAgencija.Services.Base;
using TuristickaAgencija.Services.Exceptions;
using TuristickaAgencija.WebAPI.Security;

namespace TuristickaAgencija.WebAPI.Controllers
{
    /// <summary>Pregled i pretraga - dostupno svim prijavljenim korisnicima.</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class BaseReadController<TModel, TSearch> : ControllerBase
        where TSearch : class
    {
        protected readonly IReadService<TModel, TSearch> Service;

        protected BaseReadController(IReadService<TModel, TSearch> service)
        {
            Service = service;
        }

        [HttpGet]
        public virtual Task<List<TModel>> Get([FromQuery] TSearch search)
        {
            return Service.GetAsync(search);
        }

        [HttpGet("{id:int}")]
        public virtual Task<TModel> GetById(int id)
        {
            return Service.GetByIdAsync(id);
        }
    }

    /// <summary>
    /// Referentni podaci i ponuda agencije: svi prijavljeni korisnici citaju,
    /// a samo administrator dodaje, mijenja i brise.
    /// </summary>
    public abstract class AdminCRUDController<TModel, TSearch, TInsert, TUpdate> : BaseReadController<TModel, TSearch>
        where TSearch : class
    {
        protected new readonly ICRUDService<TModel, TSearch, TInsert, TUpdate> Service;

        protected AdminCRUDController(ICRUDService<TModel, TSearch, TInsert, TUpdate> service) : base(service)
        {
            Service = service;
        }

        [HttpPost]
        [Authorize(Roles = UlogeNazivi.Admin)]
        public Task<TModel> Insert([FromBody] TInsert request)
        {
            return Service.InsertAsync(request);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = UlogeNazivi.Admin)]
        public Task<TModel> Update(int id, [FromBody] TUpdate request)
        {
            return Service.UpdateAsync(id, request);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = UlogeNazivi.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await Service.DeleteAsync(id);
            return NoContent();
        }
    }

    /// <summary>
    /// Podaci koji pripadaju klijentu (rezervacije, uplate, komentari, ocjene, lista zelja).
    /// Administrator ima pristup svemu, a klijent samo svojim zapisima.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public abstract class KorisnikovCRUDController<TModel, TSearch, TInsert> : ControllerBase
        where TModel : IKorisnikovZapis
        where TSearch : class, IKorisnikovaPretraga
        where TInsert : IKorisnikovZahtjev
    {
        protected readonly ICRUDService<TModel, TSearch, TInsert, TInsert> Service;

        protected KorisnikovCRUDController(ICRUDService<TModel, TSearch, TInsert, TInsert> service)
        {
            Service = service;
        }

        /// <summary>Ako je false (komentari, ocjene), klijent vidi zapise svih korisnika.</summary>
        protected virtual bool OgraniciPregledNaVlasnika => true;

        [HttpGet]
        public Task<List<TModel>> Get([FromQuery] TSearch search)
        {
            if (OgraniciPregledNaVlasnika && !User.JeAdmin())
            {
                search.KorisnikId = User.KorisnikId();
            }
            return Service.GetAsync(search);
        }

        [HttpGet("{id:int}")]
        public async Task<TModel> GetById(int id)
        {
            var zapis = await Service.GetByIdAsync(id);
            if (OgraniciPregledNaVlasnika)
            {
                ProvjeriVlasnistvo(zapis);
            }
            return zapis;
        }

        [HttpPost]
        public async Task<TModel> Insert([FromBody] TInsert request)
        {
            if (!User.JeAdmin())
            {
                request.KorisnikId = User.KorisnikId();
                await ProvjeriUnosKlijentaAsync(request);
            }
            return await Service.InsertAsync(request);
        }

        [HttpPut("{id:int}")]
        public async Task<TModel> Update(int id, [FromBody] TInsert request)
        {
            var postojeci = await Service.GetByIdAsync(id);
            ProvjeriVlasnistvo(postojeci);
            if (!User.JeAdmin())
            {
                request.KorisnikId = User.KorisnikId();
                ProvjeriIzmjenuKlijenta(postojeci, request);
            }
            return await Service.UpdateAsync(id, request);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ProvjeriVlasnistvo(await Service.GetByIdAsync(id));
            await Service.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>Dodatna pravila kada klijent (ne administrator) kreira zapis.</summary>
        protected virtual Task ProvjeriUnosKlijentaAsync(TInsert request)
        {
            return Task.CompletedTask;
        }

        /// <summary>Dodatna pravila kada klijent (ne administrator) mijenja svoj zapis.</summary>
        protected virtual void ProvjeriIzmjenuKlijenta(TModel postojeci, TInsert request)
        {
        }

        protected void ProvjeriVlasnistvo(TModel zapis)
        {
            if (!User.JeAdmin() && zapis.VlasnikId != User.KorisnikId())
            {
                throw new ForbiddenException("Nemate pravo pristupa ovom zapisu.");
            }
        }
    }
}
