using System;
using System.Collections.Generic;
using System.Text;

namespace TuristickaAgencija.Model
{
    public partial class VodiciPutovanja
    {
        public int Id { get; set; }
        public int VodicId { get; set; }
        public int PutovanjeId { get; set; }

        public Putovanja Putovanje { get; set; }
        public Vodic Vodic { get; set; }
    }
}
