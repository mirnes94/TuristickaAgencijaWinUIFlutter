using System.Collections.Generic;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.Services.Drzava
{
    public interface IDrzavaService
    {
        List<Model.Drzava> Get();
        Model.Drzava GetById(int id);
        Model.Drzava Insert(DrzavaInsertUpdateRequest request);
        Model.Drzava Update(int id, DrzavaInsertUpdateRequest request);
     
    }
}
