using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class ObavijestiInsertUpdateRequest
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naslov mora imati izmedju 2 i 100 znakova.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(2000, ErrorMessage = "Sadrzaj moze imati najvise 2000 znakova.")]
        public string Sadrzaj { get; set; }

        /// <summary>Ako je null, obavijest je za sve korisnike.</summary>
        public int? KorisnikId { get; set; }

        public DateTime Datum { get; set; }

        /// <summary>Ako je true, pomocni servis salje email (preko RabbitMQ).</summary>
        public bool PosaljiEmail { get; set; } = true;
    }
}
