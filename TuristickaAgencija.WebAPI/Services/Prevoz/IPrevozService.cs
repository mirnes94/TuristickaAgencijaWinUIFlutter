using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Prevoz
{
    public interface IPrevozService
    {
        List<Model.Prevoz> Get();
        Model.Prevoz GetById(int id);
        Model.Prevoz Insert(PrevozInsertUpdateRequest request);
        Model.Prevoz Update(int id, PrevozInsertUpdateRequest request);
    }
}
