using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Smjestaj
    {
        public Smjestaj()
        {
            Putovanja = new HashSet<Putovanja>();
        }

        public int Id { get; set; }
        public string NazivSmjestaja { get; set; }
        public string OpisSmjestaja { get; set; }
        public float CijenaNocenja { get; set; }
        public string TipSobe { get; set; }
        public byte[] Slika { get; set; }

        public virtual ICollection<Putovanja> Putovanja { get; set; }
    }
}
