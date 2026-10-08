using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model
{
    public partial class Putovanja
    {
        public int Id { get; set; }
        public string NazivPutovanja { get; set; }
        public string OpisPutovanja { get; set; }
        public float CijenaPutovanja { get; set; }
        public DateTime DatumPolaska { get; set; }
        public DateTime DatumDolaska { get; set; }
        public int BrojMjesta { get; set; }
        public byte[] Slika { get; set; }
        public int GradId { get; set; }
        public int PrevozId { get; set; }
        public int SmjestajId { get; set; }

        public string GradNaziv { get; set; }
        public string PrevozNaziv { get; set; }
        public string SmjestajNaziv { get; set; }
        public double ProsjecnaOcjena { get; set; }
        public int BrojOcjena { get; set; }

        public List<int> Vodici { get; set; } = new List<int>();
        public List<string> VodiciImena { get; set; } = new List<string>();
    }
}
