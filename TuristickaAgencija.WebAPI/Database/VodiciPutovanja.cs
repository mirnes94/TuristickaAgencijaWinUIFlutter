using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class VodiciPutovanja
    {
        public int Id { get; set; }
        public int VodicId { get; set; }
        public int PutovanjeId { get; set; }

        public virtual Putovanja Putovanje { get; set; }
        public virtual Vodic Vodic { get; set; }
    }
}
