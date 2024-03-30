using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model
{
    public class Ocjene
    {
        public int Id { get; set; }
        public int PutovanjeId { get; set; }
        public int KorisnikId { get; set; }
        public DateTime Datum { get; set; }
        public int Ocjena { get; set; }
    }
}
