using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class KorisniciInsertUpdateRequest
    {
        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ime mora imati izmedju 2 i 50 znakova.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Prezime mora imati izmedju 2 i 50 znakova.")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [EmailAddress(ErrorMessage = "Unesite validan email (npr. ime@domena.com).")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [RegularExpression(@"^\+?\d{6,15}$", ErrorMessage = "Telefon mora imati 6-15 cifara (npr. 061123456).")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Polje je obavezno.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Korisnicko ime mora imati izmedju 4 i 50 znakova.")]
        public string KorisnickoIme { get; set; }

        /// <summary>Obavezno pri kreiranju; pri izmjeni se mijenja samo ako je uneseno.</summary>
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }

        /// <summary>Potrebno samo kada korisnik sam mijenja svoju lozinku.</summary>
        public string StaraLozinka { get; set; }

        public bool Status { get; set; }
        public List<int> Uloge { get; set; } = new List<int>();
    }
}
