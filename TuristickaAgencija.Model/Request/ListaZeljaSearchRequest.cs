using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class ListaZeljaSearchRequest
    {
        public int? PutovanjeId { get; set; }
        public int? KorisnikId { get; set; }
    }
}
