using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TuristickaAgencija.Model.Request
{
    public class PutovanjaInsertUpdateRequest
    {
        [Required]
        public string NazivPutovanja { get; set; }
        [Required]
        public string OpisPutovanja { get; set; }
        [Required]
        public float CijenaPutovanja { get; set; }
        [Required]
        public DateTime DatumPolaska { get; set; }
        [Required]
        public DateTime DatumDolaska { get; set; }
        [Required]
        public int BrojMjesta { get; set; }
        [Required]
        public int GradId { get; set; }
        [Required]
        public int PrevozId { get; set; }
        [Required]
        public int SmjestajId { get; set; }
        [Required]
        public byte[] Slika { get; set; }

        public List<int> Vodici { get; set; } = new List<int>();
    }
}
