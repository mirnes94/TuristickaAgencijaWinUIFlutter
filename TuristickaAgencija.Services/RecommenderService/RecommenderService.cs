using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Preporuke;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.RecommenderService
{
    /// <summary>
    /// Sistem preporuke putovanja: user-based collaborative filtering (vidi UserBasedCollaborativeFiltering).
    /// Ako korisnik jos nema dovoljno ocjena (cold start) ili nema slicnih korisnika, preporuka se
    /// dopunjava najbolje ocijenjenim buducim putovanjima ("Popularno").
    /// </summary>
    public class RecommenderService : IRecommenderService
    {
        public const string IzvorKolaborativno = "Kolaborativno";
        public const string IzvorPopularno = "Popularno";

        private readonly TuristickaAgencijaContext _context;
        private readonly IMapper _mapper;
        private readonly PreporukeOptions _options;

        public RecommenderService(TuristickaAgencijaContext context, IMapper mapper, IOptions<PreporukeOptions> options)
        {
            _context = context;
            _mapper = mapper;
            _options = options.Value;
        }

        public async Task<List<Model.Putovanja>> PreporucenaPutovanjaAsync(int korisnikId, int? brojPreporuka = null, int? iskljuciPutovanjeId = null)
        {
            var rezultat = await PreporuciAsync(korisnikId, brojPreporuka, iskljuciPutovanjeId);
            return rezultat.Preporuke.Select(x => x.Putovanje).ToList();
        }

        public async Task<PreporukaRezultat> PreporuciAsync(int korisnikId, int? brojPreporuka = null, int? iskljuciPutovanjeId = null)
        {
            var korisnik = await _context.Korisnici.AsNoTracking().FirstOrDefaultAsync(x => x.Id == korisnikId);
            if (korisnik == null)
            {
                throw new NotFoundException("Korisnik ne postoji.");
            }

            var broj = brojPreporuka.GetValueOrDefault(_options.BrojPreporuka);
            if (broj <= 0)
            {
                broj = _options.BrojPreporuka;
            }

            // 1) Matrica ocjena korisnik x putovanje
            var ocjene = await _context.Ocjene
                .AsNoTracking()
                .Select(x => new { x.KorisnikId, x.PutovanjeId, x.Ocjena })
                .ToListAsync();

            var matrica = UserBasedCollaborativeFiltering.KreirajMatricu(
                ocjene.Select(x => (x.KorisnikId, x.PutovanjeId, (double)x.Ocjena)));

            // 2) Kandidati: buduca putovanja koja korisnik nije ocijenio niti rezervisao
            var danas = DateTime.Today;
            var rezervisana = await _context.Rezervacija
                .AsNoTracking()
                .Where(x => x.KorisnikId == korisnikId && x.Status != StatusRezervacije.Otkazano && x.PutovanjeId != null)
                .Select(x => x.PutovanjeId.Value)
                .ToListAsync();

            var ocijenjena = matrica.TryGetValue(korisnikId, out var mojeOcjene)
                ? mojeOcjene.Keys.ToHashSet()
                : new HashSet<int>();

            var kandidati = await _context.Putovanja
                .AsNoTracking()
                .Where(x => x.DatumPolaska >= danas)
                .Select(x => x.Id)
                .ToListAsync();

            kandidati = kandidati
                .Where(id => !ocijenjena.Contains(id) && !rezervisana.Contains(id) && id != iskljuciPutovanjeId)
                .ToList();

            // 3) KNN susjedi + predikcija
            var susjedi = UserBasedCollaborativeFiltering.NadjiSusjede(
                korisnikId, matrica, _options.BrojSusjeda, _options.MinZajednickihOcjena, _options.MinSlicnost);

            var predikcije = UserBasedCollaborativeFiltering.Predvidi(korisnikId, matrica, susjedi, kandidati)
                .Where(x => x.PredvidjenaOcjena >= 3) // ne preporucujemo ono za sta predvidjamo nisku ocjenu
                .Take(broj)
                .ToList();

            // 4) Dopuna popularnim putovanjima (cold start)
            var popularna = new List<(int PutovanjeId, double Prosjek)>();
            if (predikcije.Count < broj)
            {
                var vecPreporuceno = predikcije.Select(x => x.PutovanjeId).ToHashSet();
                popularna = kandidati
                    .Where(id => !vecPreporuceno.Contains(id))
                    .Select(id => (PutovanjeId: id, Prosjek: ProsjekPutovanja(matrica, id)))
                    .OrderByDescending(x => x.Prosjek)
                    .Take(broj - predikcije.Count)
                    .ToList();
            }

            // 5) Ucitavanje putovanja i mapiranje u DTO
            var ids = predikcije.Select(x => x.PutovanjeId).Concat(popularna.Select(x => x.PutovanjeId)).ToList();
            var putovanja = await _context.Putovanja
                .AsNoTracking()
                .Include(x => x.Grad)
                .Include(x => x.Smjestaj)
                .Include(x => x.Prevoz).ThenInclude(x => x.Firma)
                .Include(x => x.Ocjene)
                .Include(x => x.VodiciPutovanja).ThenInclude(x => x.Vodic)
                .AsSplitQuery()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            var putovanjaDto = putovanja.ToDictionary(x => x.Id, x => _mapper.Map<Model.Putovanja>(x));

            var susjediIds = susjedi.Select(s => s.KorisnikId).ToList();
            var korisniciImena = await _context.Korisnici
                .AsNoTracking()
                .Where(x => susjediIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, x => x.Ime + " " + x.Prezime);

            var rezultat = new PreporukaRezultat
            {
                KorisnikId = korisnik.Id,
                KorisnikImePrezime = korisnik.Ime + " " + korisnik.Prezime,
                BrojOcjenaKorisnika = mojeOcjene?.Count ?? 0,
                ProsjecnaOcjenaKorisnika = mojeOcjene != null && mojeOcjene.Count > 0 ? Math.Round(mojeOcjene.Values.Average(), 2) : 0,
                Susjedi = susjedi.Select(s => new SlicanKorisnik
                {
                    KorisnikId = s.KorisnikId,
                    ImePrezime = korisniciImena.TryGetValue(s.KorisnikId, out var ime) ? ime : $"Korisnik {s.KorisnikId}",
                    Slicnost = Math.Round(s.Slicnost, 3),
                    ZajednickihOcjena = s.ZajednickihOcjena
                }).ToList()
            };

            foreach (var p in predikcije.Where(p => putovanjaDto.ContainsKey(p.PutovanjeId)))
            {
                rezultat.Preporuke.Add(new PreporucenoPutovanje
                {
                    Putovanje = putovanjaDto[p.PutovanjeId],
                    PredvidjenaOcjena = p.PredvidjenaOcjena,
                    BrojSusjeda = p.BrojSusjeda,
                    Izvor = IzvorKolaborativno
                });
            }

            foreach (var p in popularna.Where(p => putovanjaDto.ContainsKey(p.PutovanjeId)))
            {
                rezultat.Preporuke.Add(new PreporucenoPutovanje
                {
                    Putovanje = putovanjaDto[p.PutovanjeId],
                    PredvidjenaOcjena = Math.Round(p.Prosjek, 2),
                    BrojSusjeda = 0,
                    Izvor = IzvorPopularno
                });
            }

            return rezultat;
        }

        private static double ProsjekPutovanja(Dictionary<int, Dictionary<int, double>> matrica, int putovanjeId)
        {
            var ocjene = matrica.Values
                .Where(x => x.ContainsKey(putovanjeId))
                .Select(x => x[putovanjeId])
                .ToList();
            return ocjene.Count > 0 ? ocjene.Average() : 0;
        }
    }
}
