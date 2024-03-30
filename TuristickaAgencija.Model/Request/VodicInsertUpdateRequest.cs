using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class VodicInsertUpdateRequest
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Kontakt { get; set; }
        public string Jmbg { get; set; }
        public byte[] Slika { get; set; }
    }
}
