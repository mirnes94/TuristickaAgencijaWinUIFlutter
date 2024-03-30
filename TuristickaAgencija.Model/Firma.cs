using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model
{
    public class Firma
    {
        public int Id { get; set; }

        public string Naziv { get; set; }

        public int GradId { get; set; }

        public string Adresa { get; set; }

        public string BrojZiroracuna { get; set; }
    }
}
