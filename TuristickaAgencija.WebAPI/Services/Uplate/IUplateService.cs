using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Uplate
{
    public interface IUplateService
    {
        List<Model.Uplate> Get(UplateSearchRequest request);
        List<Model.Uplate> GetByMonth(int mjesec);
        Model.Uplate GetById(int id);
        Model.Uplate Insert(UplateInsertUpdateRequest request);
        Model.Uplate Update(int id, UplateInsertUpdateRequest request);
    }
}
