using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Gradovi
    {
        public Gradovi()
        {
            Firma = new HashSet<Firma>();
            Putovanja = new HashSet<Putovanja>();
        }

        public int Id { get; set; }
        public string NazivGrada { get; set; }
        public int DrzavaId { get; set; }

        public virtual Drzava Drzava { get; set; }
        public virtual ICollection<Firma> Firma { get; set; }
        public virtual ICollection<Putovanja> Putovanja { get; set; }
    }
}
