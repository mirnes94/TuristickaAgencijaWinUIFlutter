using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model
{
    public partial class VodiciPutovanja
    {
        public int Id { get; set; }
        public int VodicId { get; set; }
        public int PutovanjeId { get; set; }
    }
}
