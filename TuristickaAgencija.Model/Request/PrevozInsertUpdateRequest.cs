using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class PrevozInsertUpdateRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Odaberite firmu.")]
        public int FirmaId { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tip prevoza mora imati izmedju 2 i 50 znakova.")]
        public string TipPrevoza { get; set; }

        [Range(1, 1000, ErrorMessage = "Broj mjesta mora biti izmedju 1 i 1000.")]
        public int BrojMjesta { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Cijena po mjestu mora biti veca od 0.")]
        public float CijenaPoMjestu { get; set; }
    }

    public class PrevozSearchRequest
    {
        public string TipPrevoza { get; set; }
        public int? FirmaId { get; set; }
    }
}
