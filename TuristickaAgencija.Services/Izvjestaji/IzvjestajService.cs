using Microsoft.EntityFrameworkCore;
using TuristickaAgencija.Model;
using TuristickaAgencija.Model.Izvjestaji;
using TuristickaAgencija.Services.Database;
using TuristickaAgencija.Services.Exceptions;

namespace TuristickaAgencija.Services.Izvjestaji
{
    public class IzvjestajService : IIzvjestajService
    {
        private readonly TuristickaAgencijaContext _context;

        public IzvjestajService(TuristickaAgencijaContext context)
        {
            _context = context;
        }

        public async Task<UplateIzvjestaj> UplateZaMjesecAsync(int godina, int mjesec)
        {
            if (mjesec < 1 || mjesec > 12)
            {
                throw new UserException("Mjesec mora biti između 1 i 12.");
            }
            if (godina < 2000 || godina > 2100)
            {
                throw new UserException("Unesite validnu godinu.");
            }

            var od = new DateTime(godina, mjesec, 1);
            var doDatuma = od.AddMonths(1);

            var uplate = await _context.Uplate
                .AsNoTracking()
                .Include(x => x.Korisnik)
                .Include(x => x.Rezervacija).ThenInclude(x => x.Putovanje)
                .Where(x => x.Datum >= od && x.Datum < doDatuma)
                .OrderBy(x => x.Datum)
                .ToListAsync();

            var stavke = uplate.Select(x => new UplataStavka
            {
                Datum = x.Datum,
                Iznos = x.Iznos,
                Korisnik = x.Korisnik != null ? x.Korisnik.Ime + " " + x.Korisnik.Prezime : "-",
                Rezervacija = x.Rezervacija?.Ime ?? "-",
                Putovanje = x.Rezervacija?.Putovanje?.NazivPutovanja ?? "-"
            }).ToList();

            return new UplateIzvjestaj
            {
                Godina = godina,
                Mjesec = mjesec,
                BrojUplata = stavke.Count,
                UkupanIznos = Math.Round(stavke.Sum(x => x.Iznos), 2),
                ProsjecnaUplata = stavke.Count > 0 ? Math.Round(stavke.Average(x => x.Iznos), 2) : 0,
                Stavke = stavke,
                PoPutovanjima = stavke
                    .GroupBy(x => x.Putovanje)
                    .Select(g => new PutovanjePrihod { Putovanje = g.Key, BrojUplata = g.Count(), Iznos = Math.Round(g.Sum(x => x.Iznos), 2) })
                    .OrderByDescending(x => x.Iznos)
                    .ToList()
            };
        }

        public async Task<DashboardStatistika> DashboardAsync()
        {
            var danas = DateTime.Today;
            var pocetakMjeseca = new DateTime(danas.Year, danas.Month, 1);
            var prije6Mjeseci = pocetakMjeseca.AddMonths(-5);

            var uplate = await _context.Uplate
                .AsNoTracking()
                .Select(x => new { x.Datum, x.Iznos })
                .ToListAsync();

            var najpopularnija = await _context.Rezervacija
                .AsNoTracking()
                .Where(x => x.Status != StatusRezervacije.Otkazano && x.Putovanje != null)
                .GroupBy(x => x.Putovanje.NazivPutovanja)
                .Select(g => new PopularnoPutovanje { Putovanje = g.Key, BrojRezervacija = g.Count(), BrojOsoba = g.Sum(x => x.BrojOsoba) })
                .OrderByDescending(x => x.BrojOsoba)
                .Take(5)
                .ToListAsync();

            var poMjesecima = Enumerable.Range(0, 6)
                .Select(i => prije6Mjeseci.AddMonths(i))
                .Select(m => new MjesecniPrihod
                {
                    Godina = m.Year,
                    Mjesec = m.Month,
                    Iznos = Math.Round(uplate.Where(u => u.Datum.Year == m.Year && u.Datum.Month == m.Month).Sum(u => u.Iznos), 2)
                })
                .ToList();

            return new DashboardStatistika
            {
                BrojKorisnika = await _context.Korisnici.CountAsync(),
                BrojPutovanja = await _context.Putovanja.CountAsync(),
                BrojAktivnihPutovanja = await _context.Putovanja.CountAsync(x => x.DatumPolaska >= danas),
                BrojRezervacija = await _context.Rezervacija.CountAsync(),
                RezervacijeUObradi = await _context.Rezervacija.CountAsync(x => x.Status == StatusRezervacije.UObradi),
                PrihodOvajMjesec = Math.Round(uplate.Where(x => x.Datum >= pocetakMjeseca).Sum(x => x.Iznos), 2),
                PrihodUkupno = Math.Round(uplate.Sum(x => x.Iznos), 2),
                NajpopularnijaPutovanja = najpopularnija,
                PrihodPoMjesecima = poMjesecima
            };
        }
    }
}
