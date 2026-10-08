namespace TuristickaAgencija.Services.RecommenderService
{
    /// <summary>
    /// User-based collaborative filtering (KNN + Pearsonova korelacija), prema predavanju
    /// "RSII P9 - Sistemi preporuke" i opisu iz prijave seminarskog rada.
    ///
    /// 1) Slicnost dva korisnika a i b racuna se Pearsonovom korelacijom SAMO nad putovanjima
    ///    koja su oba ocijenila (P = zajednicki ocijenjena putovanja):
    ///
    ///                 SUM_p (o_a,p - o_a)(o_b,p - o_b)
    ///    sim(a,b) = -----------------------------------------------
    ///               sqrt(SUM_p (o_a,p - o_a)^2) * sqrt(SUM_p (o_b,p - o_b)^2)
    ///
    ///    gdje su o_a i o_b prosjecne ocjene korisnika nad tim zajednickim putovanjima.
    ///
    /// 2) Zadrzava se K najslicnijih korisnika (K-najblizih susjeda) sa pozitivnom slicnoscu i
    ///    minimalnim brojem zajednickih ocjena (da 1-2 slucajne ocjene ne daju slicnost 1.0).
    ///
    /// 3) Predvidjena ocjena za putovanje p koje korisnik a nije ocijenio:
    ///
    ///    pred(a,p) = o_a + SUM_n sim(a,n) * (o_n,p - o_n) / SUM_n |sim(a,n)|
    ///
    ///    preko susjeda n koji su ocijenili p. Putovanja se sortiraju po predvidjenoj ocjeni.
    ///
    /// Klasa je cista (bez baze) kako bi se mogla lako testirati i objasniti.
    /// </summary>
    public static class UserBasedCollaborativeFiltering
    {
        public sealed class Susjed
        {
            public int KorisnikId { get; init; }
            public double Slicnost { get; init; }
            public int ZajednickihOcjena { get; init; }

            /// <summary>Prosjek ocjena susjeda nad putovanjima zajednickim sa ciljnim korisnikom (o_n iz formule).</summary>
            public double ProsjekSusjeda { get; init; }
        }

        public sealed class Predikcija
        {
            public int PutovanjeId { get; init; }
            public double PredvidjenaOcjena { get; init; }
            public int BrojSusjeda { get; init; }
        }

        /// <summary>
        /// Gradi matricu korisnik -> (putovanje -> ocjena). Ako postoji vise ocjena istog korisnika
        /// za isto putovanje, uzima se njihov prosjek.
        /// </summary>
        public static Dictionary<int, Dictionary<int, double>> KreirajMatricu(
            IEnumerable<(int KorisnikId, int PutovanjeId, double Ocjena)> ocjene)
        {
            return ocjene
                .GroupBy(x => x.KorisnikId)
                .ToDictionary(
                    g => g.Key,
                    g => g.GroupBy(x => x.PutovanjeId).ToDictionary(p => p.Key, p => p.Average(x => x.Ocjena)));
        }

        public static (double Slicnost, int Zajednickih, double ProsjekA, double ProsjekB) PearsonSlicnost(
            IReadOnlyDictionary<int, double> a,
            IReadOnlyDictionary<int, double> b)
        {
            var zajednicka = a.Keys.Where(b.ContainsKey).ToList();
            var n = zajednicka.Count;
            if (n == 0)
            {
                return (0, 0, 0, 0);
            }

            var prosjekA = zajednicka.Average(p => a[p]);
            var prosjekB = zajednicka.Average(p => b[p]);

            double brojnik = 0, sumaA = 0, sumaB = 0;
            foreach (var p in zajednicka)
            {
                var da = a[p] - prosjekA;
                var db = b[p] - prosjekB;
                brojnik += da * db;
                sumaA += da * da;
                sumaB += db * db;
            }

            var nazivnik = Math.Sqrt(sumaA) * Math.Sqrt(sumaB);
            if (nazivnik == 0)
            {
                // Neko od njih je dao sve iste ocjene -> korelacija nije definisana.
                return (0, n, prosjekA, prosjekB);
            }

            return (brojnik / nazivnik, n, prosjekA, prosjekB);
        }

        public static List<Susjed> NadjiSusjede(
            int korisnikId,
            IReadOnlyDictionary<int, Dictionary<int, double>> matrica,
            int brojSusjeda,
            int minZajednickihOcjena,
            double minSlicnost)
        {
            if (!matrica.TryGetValue(korisnikId, out var ocjeneKorisnika) || ocjeneKorisnika.Count == 0)
            {
                return new List<Susjed>();
            }

            var susjedi = new List<Susjed>();
            foreach (var (drugiId, ocjeneDrugog) in matrica)
            {
                if (drugiId == korisnikId)
                {
                    continue;
                }

                var (slicnost, zajednickih, _, prosjekDrugog) = PearsonSlicnost(ocjeneKorisnika, ocjeneDrugog);
                if (zajednickih >= minZajednickihOcjena && slicnost > minSlicnost)
                {
                    susjedi.Add(new Susjed
                    {
                        KorisnikId = drugiId,
                        Slicnost = slicnost,
                        ZajednickihOcjena = zajednickih,
                        ProsjekSusjeda = prosjekDrugog
                    });
                }
            }

            return susjedi
                .OrderByDescending(x => x.Slicnost)
                .ThenByDescending(x => x.ZajednickihOcjena)
                .Take(brojSusjeda)
                .ToList();
        }

        public static List<Predikcija> Predvidi(
            int korisnikId,
            IReadOnlyDictionary<int, Dictionary<int, double>> matrica,
            IReadOnlyCollection<Susjed> susjedi,
            IEnumerable<int> kandidati)
        {
            var rezultat = new List<Predikcija>();
            if (!matrica.TryGetValue(korisnikId, out var ocjeneKorisnika) || ocjeneKorisnika.Count == 0 || susjedi.Count == 0)
            {
                return rezultat;
            }

            var prosjekKorisnika = ocjeneKorisnika.Values.Average();

            foreach (var putovanjeId in kandidati.Distinct())
            {
                if (ocjeneKorisnika.ContainsKey(putovanjeId))
                {
                    continue; // vec ocijenjeno -> nema smisla preporucivati
                }

                double brojnik = 0, nazivnik = 0;
                var broj = 0;
                foreach (var susjed in susjedi)
                {
                    if (!matrica[susjed.KorisnikId].TryGetValue(putovanjeId, out var ocjenaSusjeda))
                    {
                        continue;
                    }

                    brojnik += susjed.Slicnost * (ocjenaSusjeda - susjed.ProsjekSusjeda);
                    nazivnik += Math.Abs(susjed.Slicnost);
                    broj++;
                }

                if (broj == 0 || nazivnik == 0)
                {
                    continue;
                }

                var predikcija = Math.Clamp(prosjekKorisnika + brojnik / nazivnik, 1, 5);
                rezultat.Add(new Predikcija
                {
                    PutovanjeId = putovanjeId,
                    PredvidjenaOcjena = Math.Round(predikcija, 2),
                    BrojSusjeda = broj
                });
            }

            return rezultat
                .OrderByDescending(x => x.PredvidjenaOcjena)
                .ThenByDescending(x => x.BrojSusjeda)
                .ToList();
        }
    }
}
