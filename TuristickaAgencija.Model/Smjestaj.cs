using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model
{
    public class Smjestaj
    {
        public int Id { get; set; }

        public string NazivSmjestaja { get; set; }
        public string OpisSmjestaja { get; set; }
      
        public float CijenaNocenja { get; set; }

        public string Tip_sobe { get; set; }

        public byte[] Slika { get; set; }
    }
}
