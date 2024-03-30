using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Uloge
{
   public interface IUlogeService
    {
      
        List<Model.Uloge> Get();
        Model.Uloge GetById(int id);
        Model.Uloge Insert(UlogeInsertUpdateRequest request);
        Model.Uloge Update(int id, UlogeInsertUpdateRequest request);
    }
}
