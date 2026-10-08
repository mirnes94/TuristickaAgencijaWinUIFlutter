using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class ListaZeljaInsertUpdateRequest : IKorisnikovZahtjev
    {
        [Range(1, int.MaxValue, ErrorMessage = "Odaberite putovanje.")]
        public int PutovanjeId { get; set; }

        public int KorisnikId { get; set; }

        [StringLength(500, ErrorMessage = "Opis moze imati najvise 500 znakova.")]
        public string Opis { get; set; }
    }
}
