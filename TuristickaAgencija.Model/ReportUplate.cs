using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model
{
    public class ReportUplate
    {
        public int Id { get; set; } 
        public DateTime DatumUplate { get; set; }
        public double Iznos { get; set; }
        public int RezervacijaId { get; set; }
    }
}
