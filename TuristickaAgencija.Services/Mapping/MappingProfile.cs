using AutoMapper;
using TuristickaAgencija.Model.Request;
using Db = TuristickaAgencija.Services.Database;

namespace TuristickaAgencija.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ---------- Sifarnici ----------
            CreateMap<Db.Drzava, Model.Drzava>();
            CreateMap<DrzavaInsertUpdateRequest, Db.Drzava>();

            CreateMap<Db.Gradovi, Model.Gradovi>()
                .ForMember(d => d.DrzavaNaziv, o => o.MapFrom(s => s.Drzava != null ? s.Drzava.Naziv : null));
            CreateMap<GradoviInsertUpdateRequest, Db.Gradovi>();

            CreateMap<Db.Firma, Model.Firma>()
                .ForMember(d => d.GradNaziv, o => o.MapFrom(s => s.Grad != null ? s.Grad.NazivGrada : null));
            CreateMap<FirmaInsertUpdateRequest, Db.Firma>();

            CreateMap<Db.Prevoz, Model.Prevoz>()
                .ForMember(d => d.FirmaNaziv, o => o.MapFrom(s => s.Firma != null ? s.Firma.Naziv : null));
            CreateMap<PrevozInsertUpdateRequest, Db.Prevoz>();

            CreateMap<Db.Smjestaj, Model.Smjestaj>();
            CreateMap<SmjestajInsertUpdateRequest, Db.Smjestaj>()
                .ForMember(d => d.Slika, o => o.Condition(s => s.Slika != null && s.Slika.Length > 0));

            CreateMap<Db.Uloge, Model.Uloge>();
            CreateMap<UlogeInsertUpdateRequest, Db.Uloge>();

            CreateMap<Db.Vodic, Model.Vodic>();
            CreateMap<VodicInsertUpdateRequest, Db.Vodic>()
                .ForMember(d => d.Slika, o => o.Condition(s => s.Slika != null && s.Slika.Length > 0));

            // ---------- Korisnici ----------
            CreateMap<Db.KorisniciUloge, Model.KorisniciUloge>();
            CreateMap<Db.Korisnici, Model.Korisnici>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status == true))
                .ForMember(d => d.Uloge, o => o.MapFrom(s => s.KorisniciUloge
                    .Where(x => x.Uloga != null)
                    .Select(x => x.Uloga.Naziv)
                    .ToList()));
            CreateMap<KorisniciInsertUpdateRequest, Db.Korisnici>()
                .ForMember(d => d.KorisniciUloge, o => o.Ignore())
                .ForMember(d => d.LozinkaHash, o => o.Ignore())
                .ForMember(d => d.LozinkaSalt, o => o.Ignore());

            // ---------- Putovanja ----------
            CreateMap<Db.Putovanja, Model.Putovanja>()
                .ForMember(d => d.GradNaziv, o => o.MapFrom(s => s.Grad != null ? s.Grad.NazivGrada : null))
                .ForMember(d => d.SmjestajNaziv, o => o.MapFrom(s => s.Smjestaj != null ? s.Smjestaj.NazivSmjestaja : null))
                .ForMember(d => d.PrevozNaziv, o => o.MapFrom(s => s.Prevoz == null
                    ? null
                    : (s.Prevoz.Firma != null ? s.Prevoz.TipPrevoza + " - " + s.Prevoz.Firma.Naziv : s.Prevoz.TipPrevoza)))
                .ForMember(d => d.BrojOcjena, o => o.MapFrom(s => s.Ocjene.Count))
                .ForMember(d => d.ProsjecnaOcjena, o => o.MapFrom(s => s.Ocjene.Count > 0
                    ? Math.Round(s.Ocjene.Average(x => (double)x.Ocjena), 2)
                    : 0))
                .ForMember(d => d.Vodici, o => o.MapFrom(s => s.VodiciPutovanja.Select(x => x.VodicId).ToList()))
                .ForMember(d => d.VodiciImena, o => o.MapFrom(s => s.VodiciPutovanja
                    .Where(x => x.Vodic != null)
                    .Select(x => x.Vodic.Ime + " " + x.Vodic.Prezime)
                    .ToList()));
            CreateMap<PutovanjaInsertUpdateRequest, Db.Putovanja>()
                .ForMember(d => d.Slika, o => o.Condition(s => s.Slika != null && s.Slika.Length > 0));
            CreateMap<Db.VodiciPutovanja, Model.VodiciPutovanja>();

            // ---------- Rezervacije i uplate ----------
            CreateMap<Db.Rezervacija, Model.Rezervacija>()
                .ForMember(d => d.KorisnikImePrezime, o => o.MapFrom(s => s.Korisnik != null ? s.Korisnik.Ime + " " + s.Korisnik.Prezime : null))
                .ForMember(d => d.PutovanjeNaziv, o => o.MapFrom(s => s.Putovanje != null ? s.Putovanje.NazivPutovanja : null))
                .ForMember(d => d.DatumPolaska, o => o.MapFrom(s => s.Putovanje != null ? s.Putovanje.DatumPolaska : (DateTime?)null))
                .ForMember(d => d.UkupnaCijena, o => o.MapFrom(s => s.Putovanje != null ? (double)s.Putovanje.CijenaPutovanja * s.BrojOsoba : 0))
                .ForMember(d => d.Uplaceno, o => o.MapFrom(s => s.Uplate.Sum(u => u.Iznos)));
            CreateMap<RezervacijaInsertUpdateRequest, Db.Rezervacija>();

            CreateMap<Db.Uplate, Model.Uplate>()
                .ForMember(d => d.KorisnikImePrezime, o => o.MapFrom(s => s.Korisnik != null ? s.Korisnik.Ime + " " + s.Korisnik.Prezime : null))
                .ForMember(d => d.RezervacijaNaziv, o => o.MapFrom(s => s.Rezervacija != null ? s.Rezervacija.Ime : null))
                .ForMember(d => d.PutovanjeNaziv, o => o.MapFrom(s => s.Rezervacija != null && s.Rezervacija.Putovanje != null
                    ? s.Rezervacija.Putovanje.NazivPutovanja
                    : null))
                .ForMember(d => d.NacinPlacanja, o => o.MapFrom(s => s.StripePaymentIntentId != null ? "Online (Stripe)" : "Poslovnica"));
            CreateMap<UplateInsertUpdateRequest, Db.Uplate>();

            // ---------- Interakcije korisnika ----------
            CreateMap<Db.Komentar, Model.Komentar>()
                .ForMember(d => d.KorisnikImePrezime, o => o.MapFrom(s => s.Korisnik != null ? s.Korisnik.Ime + " " + s.Korisnik.Prezime : null))
                .ForMember(d => d.PutovanjeNaziv, o => o.MapFrom(s => s.Putovanje != null ? s.Putovanje.NazivPutovanja : null));
            CreateMap<KomentarInsertUpdateRequest, Db.Komentar>();

            CreateMap<Db.Ocjene, Model.Ocjene>()
                .ForMember(d => d.KorisnikImePrezime, o => o.MapFrom(s => s.Korisnik != null ? s.Korisnik.Ime + " " + s.Korisnik.Prezime : null))
                .ForMember(d => d.PutovanjeNaziv, o => o.MapFrom(s => s.Putovanje != null ? s.Putovanje.NazivPutovanja : null));
            CreateMap<OcjeneInsertUpdateRequest, Db.Ocjene>();

            CreateMap<Db.ListaZelja, Model.ListaZelja>()
                .ForMember(d => d.KorisnikImePrezime, o => o.MapFrom(s => s.Korisnik != null ? s.Korisnik.Ime + " " + s.Korisnik.Prezime : null))
                .ForMember(d => d.PutovanjeNaziv, o => o.MapFrom(s => s.Putovanje != null ? s.Putovanje.NazivPutovanja : null));
            CreateMap<ListaZeljaInsertUpdateRequest, Db.ListaZelja>();

            CreateMap<Db.Obavijesti, Model.Obavijesti>()
                .ForMember(d => d.KorisnikImePrezime, o => o.MapFrom(s => s.Korisnik != null ? s.Korisnik.Ime + " " + s.Korisnik.Prezime : "Svi korisnici"));
            CreateMap<ObavijestiInsertUpdateRequest, Db.Obavijesti>();
        }
    }
}
