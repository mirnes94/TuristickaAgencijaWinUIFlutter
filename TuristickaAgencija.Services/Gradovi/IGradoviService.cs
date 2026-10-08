using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Gradovi
{
    public interface IGradoviService : ICRUDService<Model.Gradovi, GradoviSearchRequest, GradoviInsertUpdateRequest, GradoviInsertUpdateRequest>
    {
    }
}
