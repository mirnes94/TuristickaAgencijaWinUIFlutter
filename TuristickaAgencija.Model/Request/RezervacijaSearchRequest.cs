using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class RezervacijaSearchRequest : IKorisnikovaPretraga
    {
        public int? KorisnikId { get; set; }
        public int? PutovanjeId { get; set; }
        public string Status { get; set; }
        public string Ime { get; set; }
    }
}
