using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Putovanja
    {
        public Putovanja()
        {
            Komentar = new HashSet<Komentar>();
            Ocjene = new HashSet<Ocjene>();
            Rezervacija = new HashSet<Rezervacija>();
            VodiciPutovanja = new HashSet<VodiciPutovanja>();
            ListaZelja = new HashSet<ListaZelja>();
        }

        public int Id { get; set; }
        public string NazivPutovanja { get; set; }
        public string OpisPutovanja { get; set; }
        public float CijenaPutovanja { get; set; }
        public DateTime DatumPolaska { get; set; }
        public DateTime DatumDolaska { get; set; }
        public int BrojMjesta { get; set; }
        public int? GradId { get; set; }
        public int? PrevozId { get; set; }
        public int? SmjestajId { get; set; }
        public byte[] Slika { get; set; }

        public virtual Gradovi Grad { get; set; }
        public virtual Prevoz Prevoz { get; set; }
        public virtual Smjestaj Smjestaj { get; set; }
        public virtual ICollection<Komentar> Komentar { get; set; }
        public virtual ICollection<Ocjene> Ocjene { get; set; }
        public virtual ICollection<Rezervacija> Rezervacija { get; set; }
        public virtual ICollection<VodiciPutovanja> VodiciPutovanja { get; set; }
        public virtual ICollection<ListaZelja> ListaZelja { get; set; }
    }
}
