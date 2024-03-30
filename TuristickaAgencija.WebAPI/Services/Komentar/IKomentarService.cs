using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Komentar
{
    public interface IKomentarService
    {
        List<Model.Komentar> Get(KomentarSearchRequest request);
        Model.Komentar GetById(int id);
        Model.Komentar Insert(KomentarInsertUpdateRequest request);
        Model.Komentar Update(int id, KomentarInsertUpdateRequest request);
        public void Delete(int id);
    }
}
