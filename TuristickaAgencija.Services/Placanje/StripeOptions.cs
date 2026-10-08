namespace TuristickaAgencija.Services.Placanje
{
    /// <summary>Stripe kljucevi (sekcija "Stripe" / .env). Tajni kljuc nikad ne ide u mobilnu aplikaciju.</summary>
    public class StripeOptions
    {
        public const string Sekcija = "Stripe";

        public string SecretKey { get; set; }
        public string PublishableKey { get; set; }
        public string Valuta { get; set; } = "usd";
    }
}
