using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model
{
    public partial class Rezervacija
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public int? KorisnikId { get; set; }
        public int? PutovanjeId { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public int BrojOsoba { get; set; }
        public string Status { get; set; }
        public string Napomena { get; set; }
    }
}
