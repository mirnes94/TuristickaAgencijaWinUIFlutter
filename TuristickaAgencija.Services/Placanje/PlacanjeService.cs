using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Placanje
{
    public interface IPlacanjeService
    {
        /// <summary>Kreira Stripe PaymentIntent na serveru i vraca client secret mobilnoj aplikaciji.</summary>
        Task<PaymentIntentOdgovor> KreirajPaymentIntentAsync(PaymentIntentRequest request, int korisnikId, bool jeAdmin);

        /// <summary>
        /// Provjerava kod Stripe-a da je PaymentIntent uspjesno placen, da iznos odgovara i da
        /// nije vec iskoristen za neku uplatu (klijent ne moze "lazirati" online uplatu).
        /// </summary>
        Task ProvjeriPlacanjeAsync(string paymentIntentId, double iznos, int korisnikId);
    }

    public class PlacanjeService : IPlacanjeService
    {
        private readonly TuristickaAgencijaContext _context;
        private readonly StripeOptions _options;

        public PlacanjeService(TuristickaAgencijaContext context, IOptions<StripeOptions> options)
        {
            _context = context;
            _options = options.Value;
        }

        public async Task<PaymentIntentOdgovor> KreirajPaymentIntentAsync(PaymentIntentRequest request, int korisnikId, bool jeAdmin)
        {
            if (string.IsNullOrWhiteSpace(_options.SecretKey))
            {
                throw new UserException("Online plaćanje nije konfigurisano (Stripe ključ nedostaje u .env fajlu).");
            }

            if (request.RezervacijaId.HasValue)
            {
                var rezervacija = await _context.Rezervacija
                    .AsNoTracking()
                    .Include(x => x.Putovanje)
                    .Include(x => x.Uplate)
                    .FirstOrDefaultAsync(x => x.Id == request.RezervacijaId);

                if (rezervacija == null || (!jeAdmin && rezervacija.KorisnikId != korisnikId))
                {
                    throw new NotFoundException("Rezervacija ne postoji.");
                }
                if (rezervacija.Status == Model.StatusRezervacije.Otkazano)
                {
                    throw new UserException("Nije moguće platiti otkazanu rezervaciju.");
                }

                var ukupno = rezervacija.Putovanje != null ? (double)rezervacija.Putovanje.CijenaPutovanja * rezervacija.BrojOsoba : 0;
                var preostalo = ukupno - rezervacija.Uplate.Sum(x => x.Iznos);
                if (request.Iznos > preostalo + 0.001)
                {
                    throw new UserException($"Iznos ne može biti veći od preostalog duga ({preostalo:0.00}).");
                }
            }

            var client = new StripeClient(_options.SecretKey);
            var service = new PaymentIntentService(client);
            var intent = await service.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = (long)Math.Round(request.Iznos * 100),
                Currency = _options.Valuta,
                PaymentMethodTypes = new List<string> { "card" },
                Metadata = new Dictionary<string, string>
                {
                    { "korisnikId", korisnikId.ToString() },
                    { "rezervacijaId", request.RezervacijaId?.ToString() ?? string.Empty }
                }
            });

            return new PaymentIntentOdgovor
            {
                PaymentIntentId = intent.Id,
                ClientSecret = intent.ClientSecret,
                PublishableKey = _options.PublishableKey,
                Iznos = request.Iznos,
                Valuta = _options.Valuta
            };
        }

        public async Task ProvjeriPlacanjeAsync(string paymentIntentId, double iznos, int korisnikId)
        {
            if (string.IsNullOrWhiteSpace(paymentIntentId))
            {
                throw new UserException("Online uplata mora biti izvršena preko Stripe-a.");
            }
            if (string.IsNullOrWhiteSpace(_options.SecretKey))
            {
                throw new UserException("Online plaćanje nije konfigurisano (Stripe ključ nedostaje u .env fajlu).");
            }
            if (await _context.Uplate.AnyAsync(x => x.StripePaymentIntentId == paymentIntentId))
            {
                throw new UserException("Ovo plaćanje je već evidentirano.");
            }

            var service = new PaymentIntentService(new StripeClient(_options.SecretKey));
            PaymentIntent intent;
            try
            {
                intent = await service.GetAsync(paymentIntentId);
            }
            catch (StripeException)
            {
                throw new UserException("Plaćanje nije pronađeno kod Stripe-a.");
            }

            if (intent.Status != "succeeded")
            {
                throw new UserException("Plaćanje nije uspješno završeno.");
            }
            if (intent.Amount != (long)Math.Round(iznos * 100))
            {
                throw new UserException("Iznos uplate ne odgovara iznosu plaćanja.");
            }
            if (intent.Metadata != null
                && intent.Metadata.TryGetValue("korisnikId", out var vlasnik)
                && vlasnik != korisnikId.ToString())
            {
                throw new UserException("Plaćanje ne pripada prijavljenom korisniku.");
            }
        }
    }
}
