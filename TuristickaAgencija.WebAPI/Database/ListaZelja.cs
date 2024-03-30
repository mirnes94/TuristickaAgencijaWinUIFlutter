using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class ListaZelja
    {
        public int Id { get; set; }
        public int PutovanjeId { get; set; }
        public int KorisnikId { get; set; }
        public string Opis { get; set; }

        public virtual Putovanja Putovanje { get; set; }
        public virtual Korisnici Korisnik { get; set; }
    }
}
