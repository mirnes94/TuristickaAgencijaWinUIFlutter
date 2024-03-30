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
        
        public string LozinkaHash { get; set; }
        public string LozinkaSalt { get; set; }

        public List<KorisniciUloge> KorisniciUloge { get; set; }
    }
}
