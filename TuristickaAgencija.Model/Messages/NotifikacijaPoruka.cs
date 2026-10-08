using System;
using System.Collections.Generic;

namespace TuristickaAgencija.Model.Messages
{
    /// <summary>
    /// Poruka koju glavni servis (WebAPI) salje na RabbitMQ, a pomocni servis (Subscriber) obradjuje.
    /// </summary>
    public class NotifikacijaPoruka
    {
        public const string RedPoruka = "turisticka-agencija.notifikacije";

        public string Tip { get; set; }
        public string PrimalacEmail { get; set; }
        public string PrimalacIme { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public string Link { get; set; }
        public DateTime Kreirano { get; set; } = DateTime.Now;
    }

    public static class TipNotifikacije
    {
        public const string Registracija = "Registracija";
        public const string Obavijest = "Obavijest";
        public const string Rezervacija = "Rezervacija";
        public const string Uplata = "Uplata";
    }
}
