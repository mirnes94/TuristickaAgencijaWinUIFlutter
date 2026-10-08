using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class UplateSearchRequest : IKorisnikovaPretraga
    {
        public int? KorisnikId { get; set; }
        public int? RezervacijaId { get; set; }
        public DateTime? DatumOd { get; set; }
        public DateTime? DatumDo { get; set; }
    }
}
