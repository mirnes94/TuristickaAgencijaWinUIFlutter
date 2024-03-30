using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class GradoviInsertUpdateRequest
    {
        [Required]
        public string NazivGrada { get; set; }
        [Required]
        public int DrzavaId { get; set; }
    }
}
