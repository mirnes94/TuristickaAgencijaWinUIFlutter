using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model.Izvjestaji
{
    public class UplateIzvjestaj
    {
        public int Godina { get; set; }
        public int Mjesec { get; set; }
        public int BrojUplata { get; set; }
        public double UkupanIznos { get; set; }
        public double ProsjecnaUplata { get; set; }
        public List<UplataStavka> Stavke { get; set; } = new List<UplataStavka>();
        public List<PutovanjePrihod> PoPutovanjima { get; set; } = new List<PutovanjePrihod>();
    }

    public class UplataStavka
    {
        public DateTime Datum { get; set; }
        public double Iznos { get; set; }
        public string Korisnik { get; set; }
        public string Rezervacija { get; set; }
        public string Putovanje { get; set; }
    }

    public class PutovanjePrihod
    {
        public string Putovanje { get; set; }
        public int BrojUplata { get; set; }
        public double Iznos { get; set; }
    }
}
