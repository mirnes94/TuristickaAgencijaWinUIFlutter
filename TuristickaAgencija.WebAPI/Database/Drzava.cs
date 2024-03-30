using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Drzava
    {
        public Drzava()
        {
            Gradovi = new HashSet<Gradovi>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }

        public virtual ICollection<Gradovi> Gradovi { get; set; }
    }
}
