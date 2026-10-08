using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class VodicInsertUpdateRequest
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ime mora imati izmedju 2 i 50 znakova.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Prezime mora imati izmedju 2 i 50 znakova.")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [RegularExpression(@"^\+?\d{6,15}$", ErrorMessage = "Kontakt mora biti broj telefona (6-15 cifara, npr. 061123456).")]
        public string Kontakt { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "JMBG mora imati tacno 13 cifara.")]
        public string Jmbg { get; set; }

        public byte[] Slika { get; set; }
    }
}
