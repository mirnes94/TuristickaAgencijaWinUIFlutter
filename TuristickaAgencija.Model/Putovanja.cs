using System;
using System.Collections.Generic;
using System.Text;

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

        public ICollection<VodiciPutovanja> VodiciPutovanja { get; set; }
    }
}
