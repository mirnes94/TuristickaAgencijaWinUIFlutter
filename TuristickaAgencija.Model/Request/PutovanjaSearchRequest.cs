using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class PutovanjaSearchRequest
    {
        public string NazivPutovanja { get; set; }
        public int? SmjestajId { get; set; }
        public int? GradId { get; set; }

    }
}
