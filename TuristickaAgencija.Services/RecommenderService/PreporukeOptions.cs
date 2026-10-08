namespace TuristickaAgencija.Services.RecommenderService
{
    /// <summary>Parametri algoritma preporuke (sekcija "Preporuke" u appsettings.json).</summary>
    public class PreporukeOptions
    {
        public const string Sekcija = "Preporuke";

        /// <summary>K u KNN - koliko najslicnijih korisnika se koristi.</summary>
        public int BrojSusjeda { get; set; } = 5;

        /// <summary>Minimalan broj zajednicki ocijenjenih putovanja da bi se slicnost uzela u obzir.</summary>
        public int MinZajednickihOcjena { get; set; } = 2;

        /// <summary>Uzimaju se samo susjedi cija je slicnost veca od ove vrijednosti.</summary>
        public double MinSlicnost { get; set; } = 0.0;

        /// <summary>Podrazumijevani broj preporuka.</summary>
        public int BrojPreporuka { get; set; } = 5;
    }
}
