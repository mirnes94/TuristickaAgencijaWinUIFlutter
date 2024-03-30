using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model
{
    public class ListaZelja
    {
        public int Id { get; set; }
        public int PutovanjeId { get; set; }
        public int KorisnikId { get; set; }
        public string Opis { get; set; }
    }
}
