using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class ListaZeljaSearchRequest : IKorisnikovaPretraga
    {
        public int? PutovanjeId { get; set; }
        public int? KorisnikId { get; set; }
    }
}
