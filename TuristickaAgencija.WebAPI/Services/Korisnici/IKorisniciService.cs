using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Korisnici
{
    public interface IKorisniciService
    {
        public string GenerateSalt();
        public string GenerateHash(string salt, string password);
        List<Model.Korisnici> GetAllUsers();
        List<Model.Korisnici> Get(KorisniciSearchRequest request);
        Model.Korisnici GetById(int id);
        Model.Korisnici Insert(KorisniciInsertUpdateRequest request);
        Model.Korisnici Update(int id, KorisniciInsertUpdateRequest request);
        Model.Korisnici Authenticiraj(string username, string password);
        Model.Korisnici Potvrdi(string username);
        public void Delete(int id);
    }
}
