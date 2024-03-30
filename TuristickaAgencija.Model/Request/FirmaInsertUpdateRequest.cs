using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class FirmaInsertUpdateRequest
    {
        [Required]
        public string Naziv { get; set; }
        [Required]
        public int GradId { get; set; }
        [Required]
        public string Adresa { get; set; }
        [Required]
        public string BrojZiroracuna { get; set; }
    }
}
