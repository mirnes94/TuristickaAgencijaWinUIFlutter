using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Mapper
{
    public class Mapper :Profile
    {
        
        public Mapper()
        {
            
            CreateMap<Drzava, Model.Drzava>();
            CreateMap<Drzava, DrzavaInsertUpdateRequest>().ReverseMap();

            CreateMap<Gradovi, Model.Gradovi>();
            CreateMap<Gradovi, GradoviInsertUpdateRequest>().ReverseMap();

         
            CreateMap<Firma, Model.Firma>();
            CreateMap<FirmaInsertUpdateRequest, Firma>();

            CreateMap<Prevoz, Model.Prevoz>();
            CreateMap<PrevozInsertUpdateRequest, Prevoz>();

            CreateMap<Smjestaj, Model.Smjestaj>();
            CreateMap<SmjestajInsertUpdateRequest, Smjestaj>();

            CreateMap<Putovanja, Model.Putovanja>();
            CreateMap<PutovanjaInsertUpdateRequest, Putovanja>().ReverseMap();

            CreateMap<Korisnici, Model.Korisnici>().ReverseMap();
            CreateMap<KorisniciInsertUpdateRequest, Korisnici>().ReverseMap();

            CreateMap<Uloge, Model.Uloge>().ReverseMap();
            CreateMap<UlogeInsertUpdateRequest, Uloge>().ReverseMap();

            CreateMap<KorisniciUloge, Model.KorisniciUloge>().ReverseMap();

            CreateMap<Rezervacija, Model.Rezervacija>().ReverseMap();
            CreateMap<RezervacijaInsertUpdateRequest, Rezervacija>().ReverseMap();

            CreateMap<Uplate, Model.Uplate>().ReverseMap();
            CreateMap<UplateInsertUpdateRequest, Uplate>().ReverseMap();

            CreateMap<Komentar, Model.Komentar>();
            CreateMap<Komentar ,KomentarInsertUpdateRequest>().ReverseMap();

            CreateMap<Ocjene, Model.Ocjene>();
            CreateMap<Ocjene, OcjeneInsertUpdateRequest>().ReverseMap();

            CreateMap<Obavijesti, Model.Obavijesti>();
            CreateMap<Obavijesti, ObavijestiInsertUpdateRequest>().ReverseMap();

            CreateMap<Vodic, Model.Vodic>();
            CreateMap<Vodic, VodicInsertUpdateRequest>().ReverseMap();

            CreateMap<VodiciPutovanja, Model.VodiciPutovanja>().ReverseMap();

            CreateMap<Database.ListaZelja, Model.ListaZelja>();
            CreateMap<Database.ListaZelja, ListaZeljaInsertUpdateRequest>().ReverseMap();

        }
       
    }
}
