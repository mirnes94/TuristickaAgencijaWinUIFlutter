using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Komentar
{
    public interface IKomentarService
        : ICRUDService<Model.Komentar, KomentarSearchRequest, KomentarInsertUpdateRequest, KomentarInsertUpdateRequest>
    {
    }
}
