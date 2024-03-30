using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Uplate
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public double Iznos { get; set; }
        public int RezervacijaId { get; set; }
        public int KorisnikId { get; set; }

        public virtual Rezervacija Rezervacija { get; set; }
        public virtual Korisnici Korisnik { get; set; }
    }
}
