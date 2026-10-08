using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model
{
    public class Uplate : IKorisnikovZapis
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public double Iznos { get; set; }
        public int RezervacijaId { get; set; }
        public int KorisnikId { get; set; }

        public string KorisnikImePrezime { get; set; }
        public string RezervacijaNaziv { get; set; }
        public string PutovanjeNaziv { get; set; }
        public string NacinPlacanja { get; set; }

        int? IKorisnikovZapis.VlasnikId => KorisnikId;
    }
}
