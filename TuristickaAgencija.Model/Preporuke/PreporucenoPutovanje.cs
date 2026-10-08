using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model.Preporuke
{
    /// <summary>
    /// Rezultat user-based collaborative filtering algoritma za jedno putovanje.
    /// </summary>
    public class PreporucenoPutovanje
    {
        public Putovanja Putovanje { get; set; }

        /// <summary>Predvidjena ocjena (1-5) koju bi korisnik dao putovanju.</summary>
        public double PredvidjenaOcjena { get; set; }

        /// <summary>Broj slicnih korisnika (susjeda) koji su ocijenili ovo putovanje.</summary>
        public int BrojSusjeda { get; set; }

        /// <summary>"Kolaborativno" (CF predikcija) ili "Popularno" (rezervna preporuka kad nema dovoljno podataka).</summary>
        public string Izvor { get; set; }
    }

    public class SlicanKorisnik
    {
        public int KorisnikId { get; set; }
        public string ImePrezime { get; set; }
        public double Slicnost { get; set; }
        public int ZajednickihOcjena { get; set; }
    }

    public class PreporukaRezultat
    {
        public int KorisnikId { get; set; }
        public string KorisnikImePrezime { get; set; }
        public double ProsjecnaOcjenaKorisnika { get; set; }
        public int BrojOcjenaKorisnika { get; set; }
        public List<SlicanKorisnik> Susjedi { get; set; } = new List<SlicanKorisnik>();
        public List<PreporucenoPutovanje> Preporuke { get; set; } = new List<PreporucenoPutovanje>();
    }
}
