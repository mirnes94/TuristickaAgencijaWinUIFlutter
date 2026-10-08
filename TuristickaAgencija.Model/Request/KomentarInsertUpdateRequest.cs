using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class KomentarInsertUpdateRequest : IKorisnikovZahtjev
    {
        [Range(1, int.MaxValue, ErrorMessage = "Odaberite putovanje.")]
        public int PutovanjeId { get; set; }

        public int KorisnikId { get; set; }

        public DateTime Datum { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "Komentar moze imati najvise 1000 znakova.")]
        public string Sadrzaj { get; set; }
    }
}
