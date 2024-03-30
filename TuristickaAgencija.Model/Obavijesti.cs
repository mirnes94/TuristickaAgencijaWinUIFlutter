using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model
{
    public class Obavijesti
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; }
        public int? KorisnikId { get; set; }
        public DateTime Datum { get; set; }

    }
}
