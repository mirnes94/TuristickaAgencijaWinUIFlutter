using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class VodicSearchRequest
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}
