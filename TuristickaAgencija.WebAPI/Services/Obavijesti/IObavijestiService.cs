using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Obavijesti
{
    public interface IObavijestiService
    {
        List<Model.Obavijesti> Get(ObavijestiSearchRequest request);
        Model.Obavijesti GetById(int id);
        Model.Obavijesti Insert(ObavijestiInsertUpdateRequest request);
        Model.Obavijesti Update(int id, ObavijestiInsertUpdateRequest request);

        public void Delete(int id);
    }
}
