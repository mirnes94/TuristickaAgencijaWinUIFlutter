using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Smjestaj
{
    public interface ISmjestajService
    {
        List<Model.Smjestaj> Get();
        Model.Smjestaj GetById(int id);
        Model.Smjestaj Insert(SmjestajInsertUpdateRequest request);
        Model.Smjestaj Update(int id, SmjestajInsertUpdateRequest request);
    }
}
