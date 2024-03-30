using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Gradovi
{
    public interface IGradoviService
    {
        List<Model.Gradovi> Get();
        Model.Gradovi GetById(int id);
        Model.Gradovi Insert(GradoviInsertUpdateRequest request);
        Model.Gradovi Update(int id, GradoviInsertUpdateRequest request);
    }
}

