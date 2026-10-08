using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class UlogeInsertUpdateRequest
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Naziv mora imati izmedju 2 i 50 znakova.")]
        public string Naziv { get; set; }

        [StringLength(200, ErrorMessage = "Opis moze imati najvise 200 znakova.")]
        public string Opis { get; set; }
    }

    public class UlogeSearchRequest
    {
        public string Naziv { get; set; }
    }
}
