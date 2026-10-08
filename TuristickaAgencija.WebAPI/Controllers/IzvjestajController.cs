using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Izvjestaji;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Izvjestaji;

namespace TuristickaAgencija.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = UlogeNazivi.Admin)]
    public class IzvjestajController : ControllerBase
    {
        private readonly IIzvjestajService _service;

        public IzvjestajController(IIzvjestajService service)
        {
            _service = service;
        }

        /// <summary>Podaci za izvjestaj o uplatama u odabranom mjesecu (PDF generise desktop aplikacija).</summary>
        [HttpGet("Uplate")]
        public Task<UplateIzvjestaj> Uplate([FromQuery] UplateIzvjestajRequest request)
        {
            return _service.UplateZaMjesecAsync(request.Godina, request.Mjesec);
        }

        [HttpGet("Dashboard")]
        public Task<DashboardStatistika> Dashboard()
        {
            return _service.DashboardAsync();
        }
    }
}
