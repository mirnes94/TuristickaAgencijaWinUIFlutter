using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class UplateInsertUpdateRequest : IKorisnikovZahtjev
    {
        public DateTime Datum { get; set; }

        [Range(0.01, 1000000, ErrorMessage = "Iznos mora biti veci od 0.")]
        public double Iznos { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Odaberite rezervaciju.")]
        public int RezervacijaId { get; set; }

        public int KorisnikId { get; set; }

        /// <summary>Obavezno za online uplatu klijenta - API provjerava kod Stripe-a da je placanje uspjelo.</summary>
        public string StripePaymentIntentId { get; set; }
    }
}
