using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class Firma
    {
        public Firma()
        {
            Prevoz = new HashSet<Prevoz>();
        }

        public int Id { get; set; }
        public string Naziv { get; set; }
        public int? GradId { get; set; }
        public string Adresa { get; set; }
        public string BrojZiroracuna { get; set; }

        public virtual Gradovi Grad { get; set; }
        public virtual ICollection<Prevoz> Prevoz { get; set; }
    }
}
