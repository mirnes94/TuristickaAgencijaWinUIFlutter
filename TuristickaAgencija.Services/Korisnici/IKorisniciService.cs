using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Korisnici
{
    public interface IKorisniciService
        : ICRUDService<Model.Korisnici, KorisniciSearchRequest, KorisniciInsertUpdateRequest, KorisniciInsertUpdateRequest>
    {
        /// <summary>Vraca korisnika ako su podaci ispravni i nalog aktivan, inace null.</summary>
        Task<Model.Korisnici> AuthenticateAsync(string username, string password);

        /// <summary>Samostalna registracija (mobilna aplikacija): uloga Klijent, nalog neaktivan do potvrde emaila.</summary>
        Task<Model.Korisnici> RegistrujAsync(KorisniciInsertUpdateRequest request);

        /// <summary>Aktivacija naloga putem linka iz emaila.</summary>
        Task<bool> PotvrdiAsync(string username, string token);

        /// <summary>Korisnik mijenja svoj profil: ne moze mijenjati uloge/status, za novu lozinku mora potvrditi staru.</summary>
        Task<Model.Korisnici> UpdateProfilAsync(int id, KorisniciInsertUpdateRequest request);
    }
}
