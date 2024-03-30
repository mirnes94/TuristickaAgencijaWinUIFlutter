using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Vodic
{
    public interface IVodicService
    {
        IList<Model.Vodic> Get(VodicSearchRequest request);
        Model.Vodic GetById(int id);
        Model.Vodic Insert(VodicInsertUpdateRequest request);
        Model.Vodic Update(int id, VodicInsertUpdateRequest request);
    }
}
