using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class PutovanjaInsertUpdateRequest
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Naziv mora imati izmedju 2 i 100 znakova.")]
        public string NazivPutovanja { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(2000, ErrorMessage = "Opis moze imati najvise 2000 znakova.")]
        public string OpisPutovanja { get; set; }

        [Range(0.01, 1000000, ErrorMessage = "Cijena mora biti veca od 0.")]
        public float CijenaPutovanja { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        public DateTime DatumPolaska { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        public DateTime DatumDolaska { get; set; }

        [Range(1, 1000, ErrorMessage = "Broj mjesta mora biti izmedju 1 i 1000.")]
        public int BrojMjesta { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Odaberite grad.")]
        public int GradId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Odaberite prevoz.")]
        public int PrevozId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Odaberite smjestaj.")]
        public int SmjestajId { get; set; }

        /// <summary>Ako je null prilikom izmjene, postojeca slika se zadrzava.</summary>
        public byte[] Slika { get; set; }

        public List<int> Vodici { get; set; } = new List<int>();
    }
}
