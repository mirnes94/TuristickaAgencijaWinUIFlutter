using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class UplateInsertUpdateRequest
    {
       [Required]
       public DateTime Datum{ get; set; }
        [Required]
        public double Iznos { get; set; }
        [Required]
        public int RezervacijaId { get; set; }
        public int KorisnikId { get; set; }
    }
}
