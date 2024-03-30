using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuristickaAgencija.Model.Request;

namespace TuristickaAgencija.WebAPI.Services.ListaZelja
{
    public interface IListaZeljaService
    {
        List<Model.ListaZelja> Get(ListaZeljaSearchRequest request);
        Model.ListaZelja GetById(int id);
        Model.ListaZelja Insert(ListaZeljaInsertUpdateRequest request);
        Model.ListaZelja Update(int id, ListaZeljaInsertUpdateRequest request);
        public void Delete(int id);
    }
}
