using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class RezervacijaInsertUpdateRequest
    {
       
        [Required]
        public string Ime { get; set; }
        [Required]
        public int KorisnikId { get; set; }
        [Required]
        public int PutovanjeId { get; set; }
        [Required]
        public DateTime DatumRezervacije { get; set; }
        [Required]
        public int BrojOsoba { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Napomena { get; set; }
    }
}
