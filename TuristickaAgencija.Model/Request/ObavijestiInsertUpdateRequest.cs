using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class ObavijestiInsertUpdateRequest
    {
        public string Naziv { get; set; }
        public string Sadrzaj { get; set; }
        public int? KorisnikId { get; set; }
        public DateTime Datum { get; set; }
    }
}
