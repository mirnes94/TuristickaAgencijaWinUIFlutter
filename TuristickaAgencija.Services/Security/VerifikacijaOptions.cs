using System.Security.Cryptography;
using System.Text;

namespace TuristickaAgencija.Services.Security
{
    /// <summary>
    /// Konfiguracija za verifikaciju email adrese pri registraciji (sekcija "Verifikacija").
    /// Token = HMAC-SHA256(korisnicko ime) sa tajnim kljucem iz konfiguracije, pa nije potrebna dodatna kolona u bazi.
    /// </summary>
    public class VerifikacijaOptions
    {
        public const string Sekcija = "Verifikacija";

        public string Kljuc { get; set; }

        /// <summary>Javna adresa API-ja koja ide u link u emailu, npr. http://localhost:5000</summary>
        public string ApiJavniUrl { get; set; } = "http://localhost:5000";

        public string KreirajToken(string korisnickoIme)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(Kljuc ?? string.Empty));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(korisnickoIme.ToLowerInvariant()));
            return Convert.ToHexString(hash);
        }

        public bool ProvjeriToken(string korisnickoIme, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            var ocekivani = Encoding.ASCII.GetBytes(KreirajToken(korisnickoIme));
            var dobiveni = Encoding.ASCII.GetBytes(token.ToUpperInvariant());
            return CryptographicOperations.FixedTimeEquals(ocekivani, dobiveni);
        }
    }
}
