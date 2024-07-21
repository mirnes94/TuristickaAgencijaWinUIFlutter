using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.Services.RecommenderService
{
    public interface IRecommenderService
    {
        List<Model.Putovanja> GetRecommendedPutovanja(int putovanjeId);
    }
}
