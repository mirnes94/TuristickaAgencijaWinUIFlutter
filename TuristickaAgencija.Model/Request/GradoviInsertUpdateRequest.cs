using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class GradoviInsertUpdateRequest
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naziv grada mora imati izmedju 2 i 100 znakova.")]
        public string NazivGrada { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Odaberite drzavu.")]
        public int DrzavaId { get; set; }
    }

    public class GradoviSearchRequest
    {
        public string NazivGrada { get; set; }
        public int? DrzavaId { get; set; }
    }
}
