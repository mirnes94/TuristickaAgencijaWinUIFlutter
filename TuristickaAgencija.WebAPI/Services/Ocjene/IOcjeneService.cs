using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Ocjene
{
    public interface IOcjeneService
    {
        List<Model.Ocjene> Get(OcjeneSearchRequest request);
        Model.Ocjene GetById(int id);
        Model.Ocjene Insert(OcjeneInsertUpdateRequest request);
        Model.Ocjene Update(int id, OcjeneInsertUpdateRequest request);
    }
}
