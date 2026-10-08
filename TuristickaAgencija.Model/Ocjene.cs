using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model
{
    public class Ocjene : IKorisnikovZapis
    {
        public int Id { get; set; }
        public int PutovanjeId { get; set; }
        public int KorisnikId { get; set; }
        public DateTime Datum { get; set; }
        public int Ocjena { get; set; }

        public string KorisnikImePrezime { get; set; }
        public string PutovanjeNaziv { get; set; }

        int? IKorisnikovZapis.VlasnikId => KorisnikId;
    }
}
