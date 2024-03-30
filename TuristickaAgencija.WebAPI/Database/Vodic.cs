using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Vodic
    {
        public Vodic()
        {
            VodiciPutovanja = new HashSet<VodiciPutovanja>();
        }

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Kontakt { get; set; }
        public string Jmbg { get; set; }
        public byte[] Slika { get; set; }

        public virtual ICollection<VodiciPutovanja> VodiciPutovanja { get; set; }
    }
}
