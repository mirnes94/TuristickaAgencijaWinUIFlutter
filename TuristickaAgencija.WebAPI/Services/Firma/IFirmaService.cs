using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;


namespace TuristickaAgencija.WebAPI.Services.Firma
{
    public interface IFirmaService
    {
        List<Model.Firma> Get();
        Model.Firma GetById(int id);
        Model.Firma Insert(FirmaInsertUpdateRequest request);
        Model.Firma Update(int id, FirmaInsertUpdateRequest request);
    }
}
