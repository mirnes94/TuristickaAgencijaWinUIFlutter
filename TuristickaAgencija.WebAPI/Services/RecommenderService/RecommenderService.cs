using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.WebAPI.Database;

namespace TuristickaAgencija.WebAPI.Services.RecommenderService
{
    public class RecommenderService : IRecommenderService
    {
        Dictionary<int, List<Model.Ocjene>> putovanja = new Dictionary<int, List<Model.Ocjene>>();
        private readonly TuristickaAgencijaContext _context;        private readonly IMapper _mapper;
        public RecommenderService(TuristickaAgencijaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }




        public List<Model.Putovanja> GetRecommendedPutovanja(int putovanjeId)
        {
            LoadPutovanja(putovanjeId);
            List<Model.Ocjene> OcjenePutovanja = new List<Model.Ocjene>();
            List<Database.Ocjene> zabiljezeneOcjene = _context.Ocjene.Where(x => x.PutovanjeId == putovanjeId).OrderBy(x => x.KorisnikId).ToList();
            _mapper.Map(zabiljezeneOcjene, OcjenePutovanja);

            List<Model.Ocjene> OcjenePutovanja1 = new List<Model.Ocjene>();
            List<Model.Ocjene> OcjenePutovanja2 = new List<Model.Ocjene>();
            List<Model.Putovanja> preporucenaPutovanja = new List<Model.Putovanja>();

            foreach (var item in putovanja)
            {
                foreach (Model.Ocjene r in OcjenePutovanja)
                {
                    if (item.Value.Where(x => x.KorisnikId == r.KorisnikId).Count() > 0)
                    {
                        OcjenePutovanja1.Add(r);
                        OcjenePutovanja2.Add(item.Value.Where(x => x.KorisnikId == r.KorisnikId).First());
                    }
                }
                double slicnost = 0;
                slicnost = GetSlicni(OcjenePutovanja1, OcjenePutovanja2);



                if (slicnost > 0.60)
                {
                    var putovanje1 = _context.Putovanja.Include(x=>x.Grad).Include(x=>x.Smjestaj).
                        Include(x=>x.Prevoz).Where(x => x.Id == item.Key).FirstOrDefault();            
                    Model.Putovanja putovanje2 = new Model.Putovanja();

                        putovanje2.BrojMjesta = putovanje1.BrojMjesta;
                        putovanje2.CijenaPutovanja = putovanje1.CijenaPutovanja;
                        putovanje2.DatumDolaska = putovanje1.DatumDolaska;
                        putovanje2.DatumPolaska = putovanje1.DatumPolaska;
                        putovanje2.GradId = (int)putovanje1.GradId;
                        putovanje2.Id = putovanje1.Id;
                        putovanje2.NazivPutovanja = putovanje1.NazivPutovanja;
                        putovanje2.OpisPutovanja = putovanje1.OpisPutovanja;
                        putovanje2.PrevozId = (int)putovanje1.PrevozId;
                        putovanje2.Slika = putovanje1.Slika;
                        putovanje1.SmjestajId = (int)putovanje2.SmjestajId;
                      

                        preporucenaPutovanja.Add(putovanje2);
                  
                }
                OcjenePutovanja1.Clear();
                OcjenePutovanja2.Clear();
            }


            return preporucenaPutovanja;
        }



        private double GetSlicni(List<Model.Ocjene> ocjeneKorisnik1, List<Model.Ocjene> ocjeneKorisnik2)
        {
            if (ocjeneKorisnik1.Count != ocjeneKorisnik2.Count)
                return 0;



            double prosjekKorisnik1, prosjekKorisnik2, sumaKorisnik1 = 0, sumaKorisnik2 = 0;
            for(int i = 0; i < ocjeneKorisnik1.Count; i++)
            {
                sumaKorisnik1 += ocjeneKorisnik1[i].Ocjena;
            }
            prosjekKorisnik1 = sumaKorisnik1 / ocjeneKorisnik1.Count;

            for (int i = 0; i < ocjeneKorisnik2.Count; i++)
            {
                sumaKorisnik2 += ocjeneKorisnik2[i].Ocjena;
            }
            prosjekKorisnik2 = sumaKorisnik2 / ocjeneKorisnik2.Count;


            double sumaBrojnik = 0;
            for (int i = 0; i < ocjeneKorisnik1.Count; i++)
            {
                sumaBrojnik += ((ocjeneKorisnik1[i].Ocjena - prosjekKorisnik1) * (ocjeneKorisnik2[i].Ocjena - prosjekKorisnik2));
            }


            double tempSuma1 = 0;
            for (int i = 0; i < ocjeneKorisnik1.Count; i++)
            {
                tempSuma1 += Math.Pow((ocjeneKorisnik1[i].Ocjena - prosjekKorisnik1), 2);
            }
            double tempKorijen1 = Math.Sqrt(tempSuma1);

            double tempSuma2 = 0;
            for (int i = 0; i < ocjeneKorisnik2.Count; i++)
            {
                tempSuma2 += Math.Pow((ocjeneKorisnik2[i].Ocjena - prosjekKorisnik2), 2);
            }
            double tempKorijen2 = Math.Sqrt(tempSuma2);

            double sumaNazivnik = tempKorijen1 * tempKorijen2;

            if (sumaNazivnik == 0)
                return 0;
            return sumaBrojnik / sumaNazivnik;



        }



        private void LoadPutovanja(int putovanjeId)
        {
           
            List<Database.Putovanja> aktivnaPutovanja = _context.Putovanja.Include(x => x.Grad).Include(x => x.Smjestaj).
                        Include(x => x.Prevoz).Where(x => x.Id != putovanjeId).ToList();
            Database.Putovanja posmatranoPutovanje = _context.Putovanja.Where(x => x.Id == putovanjeId).SingleOrDefault();



            List<Model.Putovanja> novalista = new List<Model.Putovanja>();
            _mapper.Map(aktivnaPutovanja, novalista);

            List<Model.Putovanja> listaPutovanja = new List<Model.Putovanja>();


            foreach(var item in novalista)
            {
                if(item.SmjestajId==posmatranoPutovanje.SmjestajId)
                {
                    listaPutovanja.Add(item);
                }
            }

            foreach (Model.Putovanja a in listaPutovanja)
            {
                List<Database.Ocjene> listaocjena = _context.Ocjene.Where(x => x.PutovanjeId == a.Id).ToList();
                List<Model.Ocjene> modelocjena = new List<Model.Ocjene>();
                foreach (var item in listaocjena)
                {
                    Model.Ocjene novaocjena = new Model.Ocjene();
                    novaocjena.Datum = item.Datum;
                    novaocjena.Id = item.Id;
                    novaocjena.KorisnikId = item.KorisnikId;
                    novaocjena.Ocjena = item.Ocjena;
                    novaocjena.PutovanjeId = item.PutovanjeId;
                    modelocjena.Add(novaocjena);
                }
                if (modelocjena.Count > 0)
                    putovanja.Add(a.Id, modelocjena);
            }
        
        }
    }
}
