using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class RezervacijaInsertUpdateRequest : IKorisnikovZahtjev
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naziv rezervacije mora imati izmedju 2 i 100 znakova.")]
        public string Ime { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Odaberite korisnika.")]
        public int KorisnikId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Odaberite putovanje.")]
        public int PutovanjeId { get; set; }

        public DateTime DatumRezervacije { get; set; }

        [Range(1, 50, ErrorMessage = "Broj osoba mora biti izmedju 1 i 50.")]
        public int BrojOsoba { get; set; }

        public string Status { get; set; }

        [StringLength(500, ErrorMessage = "Napomena moze imati najvise 500 znakova.")]
        public string Napomena { get; set; }
    }
}
