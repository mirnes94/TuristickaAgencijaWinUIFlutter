using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class SmjestajInsertUpdateRequest
    {
        public string NazivSmjestaja { get; set; }
        public string OpisSmjestaja { get; set; }

        public float CijenaNocenja { get; set; }

        public string Tip_sobe { get; set; }

        public byte[] Slika { get; set; }
    }
}
