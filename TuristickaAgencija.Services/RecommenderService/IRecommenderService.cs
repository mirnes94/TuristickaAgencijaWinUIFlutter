using TuristickaAgencija.Model.Preporuke;

namespace TuristickaAgencija.Services.RecommenderService
{
    public interface IRecommenderService
    {
        /// <summary>Kompletan rezultat (susjedi + preporuke) - koristi ga desktop aplikacija za prikaz rada algoritma.</summary>
        Task<PreporukaRezultat> PreporuciAsync(int korisnikId, int? brojPreporuka = null, int? iskljuciPutovanjeId = null);

        /// <summary>Samo lista preporucenih putovanja - koristi je mobilna aplikacija.</summary>
        Task<List<Model.Putovanja>> PreporucenaPutovanjaAsync(int korisnikId, int? brojPreporuka = null, int? iskljuciPutovanjeId = null);
    }
}
