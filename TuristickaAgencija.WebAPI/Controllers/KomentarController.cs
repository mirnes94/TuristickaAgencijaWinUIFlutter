using TuristickaAgencija.Model.Request;
using TuristickaAgencija.Services.Komentar;

namespace TuristickaAgencija.WebAPI.Controllers
{
    public class KomentarController : KorisnikovCRUDController<Model.Komentar, KomentarSearchRequest, KomentarInsertUpdateRequest>
    {
        public KomentarController(IKomentarService service) : base(service)
        {
        }

        // komentare i ocjene putovanja vide svi korisnici (prikaz na detaljima putovanja)
        protected override bool OgraniciPregledNaVlasnika => false;
    }
}
