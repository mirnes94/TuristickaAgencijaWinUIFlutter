using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Ocjene;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class OcjeneController : KorisnikovCRUDController<Model.Ocjene, OcjeneSearchRequest, OcjeneInsertUpdateRequest>
    {
        public OcjeneController(IOcjeneService service) : base(service)
        {
        }

        // komentare i ocjene putovanja vide svi korisnici (prikaz na detaljima putovanja)
        protected override bool OgraniciPregledNaVlasnika => false;
    }
}
