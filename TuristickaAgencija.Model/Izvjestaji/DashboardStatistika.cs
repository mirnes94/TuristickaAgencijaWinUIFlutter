using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model.Izvjestaji
{
    public class DashboardStatistika
    {
        public int BrojKorisnika { get; set; }
        public int BrojPutovanja { get; set; }
        public int BrojAktivnihPutovanja { get; set; }
        public int BrojRezervacija { get; set; }
        public int RezervacijeUObradi { get; set; }
        public double PrihodOvajMjesec { get; set; }
        public double PrihodUkupno { get; set; }
        public List<PopularnoPutovanje> NajpopularnijaPutovanja { get; set; } = new List<PopularnoPutovanje>();
        public List<MjesecniPrihod> PrihodPoMjesecima { get; set; } = new List<MjesecniPrihod>();
    }

    public class PopularnoPutovanje
    {
        public string Putovanje { get; set; }
        public int BrojRezervacija { get; set; }
        public int BrojOsoba { get; set; }
    }

    public class MjesecniPrihod
    {
        public int Godina { get; set; }
        public int Mjesec { get; set; }
        public double Iznos { get; set; }
    }
}
