using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Rezervacija
    {
        public Rezervacija()
        {
            Uplate = new HashSet<Uplate>();
        }

        public int Id { get; set; }
        public string Ime { get; set; }
        public int? KorisnikId { get; set; }
        public int? PutovanjeId { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public int BrojOsoba { get; set; }
        public string Status { get; set; }
        public string Napomena { get; set; }

        public virtual Korisnici Korisnik { get; set; }
        public virtual Putovanja Putovanje { get; set; }
        public virtual ICollection<Uplate> Uplate { get; set; }
    }
}
