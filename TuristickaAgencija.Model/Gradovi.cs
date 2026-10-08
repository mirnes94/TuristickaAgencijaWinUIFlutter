using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model
{
    public class Gradovi
    {
        public int Id { get; set; }
        public string NazivGrada { get; set; }
        public int DrzavaId { get; set; }
        public string DrzavaNaziv { get; set; }
    }
}
