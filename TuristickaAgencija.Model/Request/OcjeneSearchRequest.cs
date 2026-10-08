using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class OcjeneSearchRequest : IKorisnikovaPretraga
    {
        public int? PutovanjeId { get; set; }
        public int? KorisnikId { get; set; }
        public int? Ocjena { get; set; }
    }
}
