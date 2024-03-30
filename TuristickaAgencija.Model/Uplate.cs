using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model
{
    public class Uplate
    {

        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public double Iznos { get; set; }
        public int RezervacijaId { get; set; }
        public int KorisnikId { get; set; }
    }
}
