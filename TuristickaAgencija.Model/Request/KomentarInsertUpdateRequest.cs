using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class KomentarInsertUpdateRequest
    {
        public int PutovanjeId { get; set; }
        public int KorisnikId { get; set; }
        public DateTime Datum { get; set; }
        public string Sadrzaj { get; set; }
    }
}
