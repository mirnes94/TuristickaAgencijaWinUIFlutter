using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Putovanja
{
    public interface IPutovanjaService
    {

        IList<Model.Putovanja> Get(PutovanjaSearchRequest request);
        Model.Putovanja GetById(int id);
        Model.Putovanja Insert(PutovanjaInsertUpdateRequest request);
        Model.Putovanja Update(int id, PutovanjaInsertUpdateRequest request);
    }
}
