using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.Rezervacija
{
    public interface IRezervacijaService
    {
        List<Model.Rezervacija> Get(RezervacijaSearchRequest request);
        Model.Rezervacija GetById(int id);
        Model.Rezervacija Insert(RezervacijaInsertUpdateRequest request);
        Model.Rezervacija Update(int id, RezervacijaInsertUpdateRequest request);

        public void Delete(int id);
    }
}
