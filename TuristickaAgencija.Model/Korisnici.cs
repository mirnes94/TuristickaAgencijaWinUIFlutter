using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model
{
    public partial class Korisnici
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string KorisnickoIme { get; set; }
        public bool Status { get; set; }

        // Lozinka (hash/salt) se namjerno NE vraca klijentima.
        public List<KorisniciUloge> KorisniciUloge { get; set; } = new List<KorisniciUloge>();
        public List<string> Uloge { get; set; } = new List<string>();
    }
}
