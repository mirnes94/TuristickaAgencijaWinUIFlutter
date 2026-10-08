using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class SmjestajInsertUpdateRequest
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naziv mora imati izmedju 2 i 100 znakova.")]
        public string NazivSmjestaja { get; set; }

        [StringLength(1000, ErrorMessage = "Opis moze imati najvise 1000 znakova.")]
        public string OpisSmjestaja { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Cijena nocenja mora biti veca od 0.")]
        public float CijenaNocenja { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(50, ErrorMessage = "Tip sobe moze imati najvise 50 znakova.")]
        public string TipSobe { get; set; }

        /// <summary>Ako je null prilikom izmjene, postojeca slika se zadrzava.</summary>
        public byte[] Slika { get; set; }
    }

    public class SmjestajSearchRequest
    {
        public string NazivSmjestaja { get; set; }
    }
}
