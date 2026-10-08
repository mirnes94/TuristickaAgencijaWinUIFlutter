using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class ObavijestiSearchRequest
    {
        public int? KorisnikId { get; set; }
        public string Naziv { get; set; }
    }
}
