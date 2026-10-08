using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class OcjeneInsertUpdateRequest : IKorisnikovZahtjev
    {
        [Range(1, int.MaxValue, ErrorMessage = "Odaberite putovanje.")]
        public int PutovanjeId { get; set; }

        public int KorisnikId { get; set; }

        public DateTime Datum { get; set; }

        [Range(1, 5, ErrorMessage = "Ocjena mora biti izmedju 1 i 5.")]
        public int Ocjena { get; set; }
    }
}
