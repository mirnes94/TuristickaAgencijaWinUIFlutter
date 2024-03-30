using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Prevoz
    {
        public Prevoz()
        {
            Putovanja = new HashSet<Putovanja>();
        }

        public int Id { get; set; }
        public int? FirmaId { get; set; }
        public string TipPrevoza { get; set; }
        public int BrojMjesta { get; set; }
        public float CijenaPoMjestu { get; set; }

        public virtual Firma Firma { get; set; }
        public virtual ICollection<Putovanja> Putovanja { get; set; }
    }
}
