using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class PrevozInsertUpdateRequest
    {
        public int FirmaId { get; set; }

        public string TipPrevoza { get; set; }

        public int BrojMjesta { get; set; }

        public float CijenaPoMjestu { get; set; }
    }
}
