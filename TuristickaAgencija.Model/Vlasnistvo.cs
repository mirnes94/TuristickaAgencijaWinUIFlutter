namespace TuristickaAgencija.Model
{
    /// <summary>DTO koji pripada nekom korisniku (rezervacija, uplata, komentar...). Koristi se za provjeru vlasnistva.</summary>
    public interface IKorisnikovZapis
    {
        int? VlasnikId { get; }
    }

    /// <summary>Pretraga koja se moze ograniciti na jednog korisnika.</summary>
    public interface IKorisnikovaPretraga
    {
        int? KorisnikId { get; set; }
    }

    /// <summary>Insert/update zahtjev koji se odnosi na jednog korisnika.</summary>
    public interface IKorisnikovZahtjev
    {
        int KorisnikId { get; set; }
    }
}
