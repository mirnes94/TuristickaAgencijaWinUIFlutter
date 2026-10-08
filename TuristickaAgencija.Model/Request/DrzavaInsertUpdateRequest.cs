using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class DrzavaInsertUpdateRequest
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naziv mora imati izmedju 2 i 100 znakova.")]
        public string Naziv { get; set; }
    }

    public class DrzavaSearchRequest
    {
        public string Naziv { get; set; }
    }
}
