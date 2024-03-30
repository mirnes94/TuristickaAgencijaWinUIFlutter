using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class ListaZeljaInsertUpdateRequest
    {
        public int PutovanjeId { get; set; }
        public int KorisnikId { get; set; }
        public string Opis { get; set; }
    }
}
