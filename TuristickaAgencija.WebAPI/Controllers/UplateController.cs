using Microsoft.AspNetCore.Mvc;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Placanje;
using TuristickaAgencija.Services.Uplate;
using TuristickaAgencija.WebAPI.Security;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class UplateController : KorisnikovCRUDController<Model.Uplate, UplateSearchRequest, UplateInsertUpdateRequest>
    {
        private readonly IPlacanjeService _placanjeService;

        public UplateController(IUplateService service, IPlacanjeService placanjeService) : base(service)
        {
            _placanjeService = placanjeService;
        }

        /// <summary>
        /// Online placanje (mobilna aplikacija): Stripe PaymentIntent se kreira na serveru,
        /// pa tajni Stripe kljuc ostaje u konfiguraciji API-ja (.env), a ne u aplikaciji.
        /// </summary>
        /// <summary>Klijent moze evidentirati samo uplatu koja je stvarno placena preko Stripe-a.</summary>
        protected override async Task ProvjeriUnosKlijentaAsync(UplateInsertUpdateRequest request)
        {
            await _placanjeService.ProvjeriPlacanjeAsync(request.StripePaymentIntentId, request.Iznos, request.KorisnikId);
            request.Datum = DateTime.Now;
        }

        [HttpPost("PaymentIntent")]
        public Task<PaymentIntentOdgovor> PaymentIntent([FromBody] PaymentIntentRequest request)
        {
            return _placanjeService.KreirajPaymentIntentAsync(request, User.KorisnikId(), User.JeAdmin());
        }
    }
}
