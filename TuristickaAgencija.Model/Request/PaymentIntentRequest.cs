using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class PaymentIntentRequest
    {
        /// <summary>Opciono - ako je poslano, provjerava se da iznos ne prelazi preostali dug rezervacije.</summary>
        public int? RezervacijaId { get; set; }

        [Range(0.5, 1000000, ErrorMessage = "Iznos mora biti veci od 0.50.")]
        public double Iznos { get; set; }
    }

    public class PaymentIntentOdgovor
    {
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
        public string PublishableKey { get; set; }
        public double Iznos { get; set; }
        public string Valuta { get; set; }
    }
}
