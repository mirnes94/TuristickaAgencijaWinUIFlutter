using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Base;

namespace TuristickaAgencija.Services.Vodic
{
    public interface IVodicService : ICRUDService<Model.Vodic, VodicSearchRequest, VodicInsertUpdateRequest, VodicInsertUpdateRequest>
    {
    }
}
