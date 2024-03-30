using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Korisnici
    {
        public Korisnici()
        {
            Komentar = new HashSet<Komentar>();
            KorisniciUloge = new HashSet<KorisniciUloge>();
            Obavijesti = new HashSet<Obavijesti>();
            Ocjene = new HashSet<Ocjene>();
            Rezervacija = new HashSet<Rezervacija>();
            ListaZelja = new HashSet<ListaZelja>();
        }

        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string KorisnickoIme { get; set; }
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Komentar> Komentar { get; set; }
        public virtual ICollection<KorisniciUloge> KorisniciUloge { get; set; }
        public virtual ICollection<Obavijesti> Obavijesti { get; set; }
        public virtual ICollection<Ocjene> Ocjene { get; set; }
        public virtual ICollection<Rezervacija> Rezervacija { get; set; }
        public virtual ICollection<ListaZelja> ListaZelja { get; set; }
    }
}
