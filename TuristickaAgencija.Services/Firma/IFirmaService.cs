using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Firma
{
    public interface IFirmaService : ICRUDService<Model.Firma, FirmaSearchRequest, FirmaInsertUpdateRequest, FirmaInsertUpdateRequest>
    {
    }
}
