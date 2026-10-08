using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class FirmaInsertUpdateRequest
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naziv mora imati izmedju 2 i 100 znakova.")]
        public string Naziv { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Odaberite grad.")]
        public int GradId { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(200, ErrorMessage = "Adresa moze imati najvise 200 znakova.")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [RegularExpression(@"^\d{13,16}$", ErrorMessage = "Broj ziro racuna mora imati 13 do 16 cifara.")]
        public string BrojZiroracuna { get; set; }
    }

    public class FirmaSearchRequest
    {
        public string Naziv { get; set; }
        public int? GradId { get; set; }
    }
}
