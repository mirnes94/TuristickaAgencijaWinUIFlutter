using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model
{
    public partial class Rezervacija : IKorisnikovZapis
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public int? KorisnikId { get; set; }
        public int? PutovanjeId { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public int BrojOsoba { get; set; }
        public string Status { get; set; }
        public string Napomena { get; set; }

        public string KorisnikImePrezime { get; set; }
        public string PutovanjeNaziv { get; set; }
        public DateTime? DatumPolaska { get; set; }
        public double UkupnaCijena { get; set; }
        public double Uplaceno { get; set; }

        int? IKorisnikovZapis.VlasnikId => KorisnikId;
    }
}
