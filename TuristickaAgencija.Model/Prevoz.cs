using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model
{
    public class Prevoz
    {
        public int Id { get; set; }
        public int FirmaId { get; set; }
        public string FirmaNaziv { get; set; }
        public string TipPrevoza { get; set; }
        public int BrojMjesta { get; set; }
        public float CijenaPoMjestu { get; set; }
    }
}
