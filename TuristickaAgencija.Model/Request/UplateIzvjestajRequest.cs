using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TuristickaAgencija.Model.Request
{
    public class UplateIzvjestajRequest
    {
        [Range(2000, 2100, ErrorMessage = "Unesite validnu godinu.")]
        public int Godina { get; set; }

        [Range(1, 12, ErrorMessage = "Mjesec mora biti izmedju 1 i 12.")]
        public int Mjesec { get; set; }
    }
}
