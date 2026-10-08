using TuristickaAgencija.Model;
using TuristickaAgencija.Services.Security;

namespace TuristickaAgencija.Services.Database
{
    /// <summary>
    /// Kreiranje baze i pocetnih (testnih) podataka prilikom pokretanja aplikacije.
    /// Pokrece se samo ako je baza prazna.
    /// </summary>
    public static class Data
    {
        public const string TestnaLozinka = "test";

        public static void Seed(TuristickaAgencijaContext context)
        {
            context.Database.EnsureCreated();

            if (context.Korisnici.Any())
            {
                return;
            }

            using var transakcija = context.Database.BeginTransaction();

            var danas = DateTime.Today;
            var putanjaSlike = Path.Combine(AppContext.BaseDirectory, "TestImage", "OIP1.jpg");
            var slika = File.Exists(putanjaSlike) ? File.ReadAllBytes(putanjaSlike) : Array.Empty<byte>();

            static Korisnici NoviKorisnik(string ime, string prezime, string email, string telefon, string korisnickoIme, Uloge uloga)
            {
                var salt = PasswordHasher.GenerateSalt();
                var korisnik = new Korisnici
                {
                    Ime = ime,
                    Prezime = prezime,
                    Email = email,
                    Telefon = telefon,
                    KorisnickoIme = korisnickoIme,
                    LozinkaSalt = salt,
                    LozinkaHash = PasswordHasher.GenerateHash(salt, TestnaLozinka),
                    Status = true
                };
                korisnik.KorisniciUloge.Add(new KorisniciUloge { Uloga = uloga, DatumIzmjene = DateTime.Now });
                return korisnik;
            }

            var drzave = new[] { "Bosna i Hercegovina", "Srbija", "Hrvatska", "Crna Gora", "Holandija", "Turska", "Austrija", "Njemačka", "Francuska", "Španija", "Italija", "Grčka" }
                .Select(n => new Drzava { Naziv = n }).ToList();
            context.Drzava.AddRange(drzave);
            context.SaveChanges();

            var gradovi = new List<Gradovi>
            {
                new Gradovi { NazivGrada = "Sarajevo", Drzava = drzave[0] },
                new Gradovi { NazivGrada = "Mostar", Drzava = drzave[0] },
                new Gradovi { NazivGrada = "Banja Luka", Drzava = drzave[0] },
                new Gradovi { NazivGrada = "Tuzla", Drzava = drzave[0] },
                new Gradovi { NazivGrada = "Beograd", Drzava = drzave[1] },
                new Gradovi { NazivGrada = "Zagreb", Drzava = drzave[2] },
                new Gradovi { NazivGrada = "Dubrovnik", Drzava = drzave[2] },
                new Gradovi { NazivGrada = "Split", Drzava = drzave[2] },
                new Gradovi { NazivGrada = "Budva", Drzava = drzave[3] },
                new Gradovi { NazivGrada = "Amsterdam", Drzava = drzave[4] },
                new Gradovi { NazivGrada = "Istanbul", Drzava = drzave[5] },
                new Gradovi { NazivGrada = "Beč", Drzava = drzave[6] },
                new Gradovi { NazivGrada = "Minhen", Drzava = drzave[7] },
                new Gradovi { NazivGrada = "Pariz", Drzava = drzave[8] },
                new Gradovi { NazivGrada = "Madrid", Drzava = drzave[9] },
                new Gradovi { NazivGrada = "Barcelona", Drzava = drzave[9] },
                new Gradovi { NazivGrada = "Rim", Drzava = drzave[10] },
                new Gradovi { NazivGrada = "Venecija", Drzava = drzave[10] },
                new Gradovi { NazivGrada = "Atina", Drzava = drzave[11] },
                new Gradovi { NazivGrada = "Santorini", Drzava = drzave[11] },
            };
            context.Gradovi.AddRange(gradovi);
            context.SaveChanges();

            Gradovi Grad(string naziv) => gradovi.First(g => g.NazivGrada == naziv);

            var firme = new List<Firma>
            {
                new Firma { Naziv = "Centrotrans", Adresa = "Ul. Kurta Schorka 4", Grad = gradovi[0], BrojZiroracuna = "1610000012345678" },
                new Firma { Naziv = "Autoprevoz-Bus", Adresa = "Ul. Bišće polje bb", Grad = gradovi[1], BrojZiroracuna = "3380002210123456" },
                new Firma { Naziv = "Air Balkan Charter", Adresa = "Aerodrom Sarajevo, Kurta Schorka 36", Grad = gradovi[0], BrojZiroracuna = "1990490012345671" },
            };
            context.Firma.AddRange(firme);

            var prevozi = new List<Prevoz>
            {
                new Prevoz { Firma = firme[0], TipPrevoza = "Autobus", BrojMjesta = 50, CijenaPoMjestu = 30 },
                new Prevoz { Firma = firme[1], TipPrevoza = "Autobus", BrojMjesta = 45, CijenaPoMjestu = 35 },
                new Prevoz { Firma = firme[2], TipPrevoza = "Avion", BrojMjesta = 150, CijenaPoMjestu = 180 },
            };
            context.Prevoz.AddRange(prevozi);

            var smjestaji = new List<Smjestaj>
            {
                new Smjestaj { NazivSmjestaja = "Hotel Adriatic ****", OpisSmjestaja = "Hotel sa 4 zvjezdice, polupansion, blizu centra.", CijenaNocenja = 80, TipSobe = "Dvokrevetna", Slika = slika },
                new Smjestaj { NazivSmjestaja = "Hotel Panorama *****", OpisSmjestaja = "Hotel sa 5 zvjezdica, doručak uključen, bazen i spa.", CijenaNocenja = 140, TipSobe = "Dvokrevetna", Slika = slika },
                new Smjestaj { NazivSmjestaja = "Hostel Central", OpisSmjestaja = "Moderan hostel u centru grada, doručak uključen.", CijenaNocenja = 35, TipSobe = "Trokrevetna", Slika = slika },
            };
            context.Smjestaj.AddRange(smjestaji);

            var vodici = new List<Vodic>
            {
                new Vodic { Ime = "Amra", Prezime = "Hadžić", Kontakt = "061200300", Jmbg = "1203985175001", Slika = slika },
                new Vodic { Ime = "Kenan", Prezime = "Bešlić", Kontakt = "062200301", Jmbg = "0507990170002", Slika = slika },
                new Vodic { Ime = "Lamija", Prezime = "Karić", Kontakt = "061200302", Jmbg = "2311992175003", Slika = slika },
                new Vodic { Ime = "Dino", Prezime = "Ramić", Kontakt = "063200303", Jmbg = "1408987170004", Slika = slika },
                new Vodic { Ime = "Nermina", Prezime = "Zukić", Kontakt = "061200304", Jmbg = "0902991175005", Slika = slika },
                new Vodic { Ime = "Faruk", Prezime = "Alić", Kontakt = "062200305", Jmbg = "3006986170006", Slika = slika },
            };
            context.Vodic.AddRange(vodici);
            context.SaveChanges();

            var uloge = new List<Uloge>
            {
                new Uloge { Naziv = UlogeNazivi.Admin, Opis = "Administrator sistema (desktop aplikacija)" },
                new Uloge { Naziv = UlogeNazivi.Zaposleni, Opis = "Zaposlenik agencije" },
                new Uloge { Naziv = UlogeNazivi.Klijent, Opis = "Klijent agencije (mobilna aplikacija)" },
            };
            context.Uloge.AddRange(uloge);
            context.SaveChanges();

            Uloge Uloga(string naziv) => uloge.First(u => u.Naziv == naziv);

            // Lozinka za sve korisnike je: test
            var korisnici = new List<Korisnici>
            {
                NoviKorisnik("Mirnes", "Turković", "desktop@turisticka-agencija.ba", "061111111", "desktop", Uloga(UlogeNazivi.Admin)),
                NoviKorisnik("Amila", "Turković", "mobile@turisticka-agencija.ba", "062222222", "mobile", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Edin", "Hodžić", "zaposleni@turisticka-agencija.ba", "061333333", "zaposleni", Uloga(UlogeNazivi.Zaposleni)),
                NoviKorisnik("Meho", "Mehić", "meho@mail.com", "061478951", "mmeho", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Suljo", "Suljić", "suljo@mail.com", "060147895", "ssuljo", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Mujo", "Mujić", "mujo@mail.com", "060147896", "mmujo", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Kemo", "Mujić", "kemo@mail.com", "060147885", "kmujic", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Lejla", "Hasić", "lejla@mail.com", "062555111", "lejlah", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Emina", "Kovač", "emina@mail.com", "063555222", "eminak", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Haris", "Begić", "haris@mail.com", "061555333", "harisb", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Adnan", "Omerović", "adnan@mail.com", "062555444", "adnano", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Selma", "Delić", "selma@mail.com", "061555555", "selmad", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Tarik", "Mehmedović", "tarik@mail.com", "062555666", "tarikm", Uloga(UlogeNazivi.Klijent)),
                NoviKorisnik("Aida", "Softić", "aida@mail.com", "061555777", "aidas", Uloga(UlogeNazivi.Klijent)),
            };
            context.Korisnici.AddRange(korisnici);
            context.SaveChanges();

            Korisnici Korisnik(string korisnickoIme) => korisnici.First(k => k.KorisnickoIme == korisnickoIme);

            var putovanja = new List<Putovanja>
            {
                new Putovanja { NazivPutovanja = "Ljeto u Budvi", OpisPutovanja = "Sedam dana ljetovanja u Budvi uz smještaj blizu plaže i izlet do Kotora.", CijenaPutovanja = 420, DatumPolaska = danas.AddDays(-40), DatumDolaska = danas.AddDays(-33), BrojMjesta = 40, Grad = Grad("Budva"), Prevoz = prevozi[0], Smjestaj = smjestaji[1], Slika = slika },
                new Putovanja { NazivPutovanja = "Vikend u Beogradu", OpisPutovanja = "Obilazak Kalemegdana, Knez Mihailove i Skadarlije uz stručnog vodiča.", CijenaPutovanja = 180, DatumPolaska = danas.AddDays(-75), DatumDolaska = danas.AddDays(-72), BrojMjesta = 45, Grad = Grad("Beograd"), Prevoz = prevozi[0], Smjestaj = smjestaji[2], Slika = slika },
                new Putovanja { NazivPutovanja = "Istanbul - grad na dva kontinenta", OpisPutovanja = "Aja Sofija, Plava džamija, Topkapi i krstarenje Bosforom.", CijenaPutovanja = 650, DatumPolaska = danas.AddDays(-20), DatumDolaska = danas.AddDays(-15), BrojMjesta = 40, Grad = Grad("Istanbul"), Prevoz = prevozi[1], Smjestaj = smjestaji[0], Slika = slika },
                new Putovanja { NazivPutovanja = "Dubrovnik i Elafiti", OpisPutovanja = "Zidine Dubrovnika, Lokrum i cjelodnevni izlet brodom na Elafitske otoke.", CijenaPutovanja = 520, DatumPolaska = danas.AddDays(25), DatumDolaska = danas.AddDays(30), BrojMjesta = 35, Grad = Grad("Dubrovnik"), Prevoz = prevozi[1], Smjestaj = smjestaji[1], Slika = slika },
                new Putovanja { NazivPutovanja = "Split i Hvar", OpisPutovanja = "Dioklecijanova palača, Marjan i izlet na otok Hvar.", CijenaPutovanja = 480, DatumPolaska = danas.AddDays(40), DatumDolaska = danas.AddDays(46), BrojMjesta = 40, Grad = Grad("Split"), Prevoz = prevozi[0], Smjestaj = smjestaji[1], Slika = slika },
                new Putovanja { NazivPutovanja = "Santorini - grčka bajka", OpisPutovanja = "Zalazak sunca u Oiji, vulkanske plaže i tradicionalna grčka kuhinja.", CijenaPutovanja = 1150, DatumPolaska = danas.AddDays(60), DatumDolaska = danas.AddDays(67), BrojMjesta = 30, Grad = Grad("Santorini"), Prevoz = prevozi[2], Smjestaj = smjestaji[0], Slika = slika },
                new Putovanja { NazivPutovanja = "Barcelona i Costa Brava", OpisPutovanja = "Sagrada Familia, Park Güell i dva dana odmora na Costa Bravi.", CijenaPutovanja = 980, DatumPolaska = danas.AddDays(75), DatumDolaska = danas.AddDays(82), BrojMjesta = 35, Grad = Grad("Barcelona"), Prevoz = prevozi[2], Smjestaj = smjestaji[0], Slika = slika },
                new Putovanja { NazivPutovanja = "Advent u Zagrebu", OpisPutovanja = "Najljepši božićni sajam u Evropi, Gornji grad i adventska atmosfera.", CijenaPutovanja = 160, DatumPolaska = danas.AddDays(55), DatumDolaska = danas.AddDays(58), BrojMjesta = 45, Grad = Grad("Zagreb"), Prevoz = prevozi[0], Smjestaj = smjestaji[2], Slika = slika },
                new Putovanja { NazivPutovanja = "Bečki valcer", OpisPutovanja = "Schönbrunn, Belvedere, Prater i večer klasične muzike.", CijenaPutovanja = 380, DatumPolaska = danas.AddDays(30), DatumDolaska = danas.AddDays(34), BrojMjesta = 40, Grad = Grad("Beč"), Prevoz = prevozi[1], Smjestaj = smjestaji[1], Slika = slika },
                new Putovanja { NazivPutovanja = "Minhen i dvorac Neuschwanstein", OpisPutovanja = "Marienplatz, BMW muzej i izlet do bajkovitog dvorca Neuschwanstein.", CijenaPutovanja = 450, DatumPolaska = danas.AddDays(90), DatumDolaska = danas.AddDays(94), BrojMjesta = 40, Grad = Grad("Minhen"), Prevoz = prevozi[1], Smjestaj = smjestaji[1], Slika = slika },
                new Putovanja { NazivPutovanja = "Pariz - grad svjetlosti", OpisPutovanja = "Eiffelov toranj, Louvre, Montmartre i krstarenje Senom.", CijenaPutovanja = 890, DatumPolaska = danas.AddDays(45), DatumDolaska = danas.AddDays(51), BrojMjesta = 35, Grad = Grad("Pariz"), Prevoz = prevozi[2], Smjestaj = smjestaji[0], Slika = slika },
                new Putovanja { NazivPutovanja = "Amsterdam city break", OpisPutovanja = "Kanali, muzej Van Gogha, Zaanse Schans i biciklistička tura.", CijenaPutovanja = 760, DatumPolaska = danas.AddDays(100), DatumDolaska = danas.AddDays(105), BrojMjesta = 35, Grad = Grad("Amsterdam"), Prevoz = prevozi[2], Smjestaj = smjestaji[1], Slika = slika },
                new Putovanja { NazivPutovanja = "Rim - vječni grad", OpisPutovanja = "Koloseum, Forum, Vatikanski muzeji i Fontana di Trevi.", CijenaPutovanja = 720, DatumPolaska = danas.AddDays(35), DatumDolaska = danas.AddDays(40), BrojMjesta = 40, Grad = Grad("Rim"), Prevoz = prevozi[2], Smjestaj = smjestaji[0], Slika = slika },
                new Putovanja { NazivPutovanja = "Atina i Akropolj", OpisPutovanja = "Akropolj, Plaka, rt Sounion i Pirej.", CijenaPutovanja = 690, DatumPolaska = danas.AddDays(80), DatumDolaska = danas.AddDays(85), BrojMjesta = 35, Grad = Grad("Atina"), Prevoz = prevozi[2], Smjestaj = smjestaji[1], Slika = slika },
                new Putovanja { NazivPutovanja = "Madrid i Toledo", OpisPutovanja = "Prado, Kraljevska palača i cjelodnevni izlet u srednjovjekovni Toledo.", CijenaPutovanja = 870, DatumPolaska = danas.AddDays(110), DatumDolaska = danas.AddDays(116), BrojMjesta = 35, Grad = Grad("Madrid"), Prevoz = prevozi[2], Smjestaj = smjestaji[0], Slika = slika },
                new Putovanja { NazivPutovanja = "Venecija i Verona", OpisPutovanja = "Trg sv. Marka, vožnja gondolom i Julijin balkon u Veroni.", CijenaPutovanja = 540, DatumPolaska = danas.AddDays(65), DatumDolaska = danas.AddDays(69), BrojMjesta = 40, Grad = Grad("Venecija"), Prevoz = prevozi[1], Smjestaj = smjestaji[1], Slika = slika },
            };
            context.Putovanja.AddRange(putovanja);
            context.SaveChanges();

            var vodiciPutovanja = new (int Putovanje, int Vodic)[]
            {
                (1, 1), (1, 4), (2, 2), (3, 3), (3, 6), (4, 4), (5, 5), (5, 2), (6, 6), (7, 1), (7, 4), (8, 2), (9, 3), (9, 6), (10, 4), (11, 5), (11, 2), (12, 6), (13, 1), (13, 4), (14, 2), (15, 3), (15, 6), (16, 4)
            };
            context.VodiciPutovanja.AddRange(vodiciPutovanja.Select(x => new VodiciPutovanja { Putovanje = putovanja[x.Putovanje - 1], Vodic = vodici[x.Vodic - 1] }));

            // Ocjene (korisnik, putovanje, ocjena) - dovoljno preklapanja da user-based CF ima smisla.
            var ocjene = new (string Korisnik, int Putovanje, int Ocjena)[]
            {
                ("mobile", 1, 5), ("mobile", 4, 5), ("mobile", 2, 2), ("mobile", 10, 2), ("mobile", 3, 3), ("mobile", 9, 2),
                ("mobile", 13, 4), ("mobile", 8, 2), ("mmeho", 5, 5), ("mmeho", 12, 1), ("mmeho", 3, 4), ("mmeho", 2, 2),
                ("mmeho", 16, 4), ("mmeho", 1, 4), ("mmeho", 4, 5), ("mmeho", 7, 5), ("mmeho", 6, 5), ("ssuljo", 9, 2),
                ("ssuljo", 16, 4), ("ssuljo", 6, 4), ("ssuljo", 5, 5), ("ssuljo", 4, 4), ("ssuljo", 10, 2), ("ssuljo", 3, 4),
                ("ssuljo", 12, 2), ("ssuljo", 8, 3), ("ssuljo", 11, 3), ("mmujo", 7, 2), ("mmujo", 2, 5), ("mmujo", 13, 2),
                ("mmujo", 16, 3), ("mmujo", 1, 1), ("mmujo", 6, 2), ("mmujo", 11, 5), ("mmujo", 12, 5), ("mmujo", 4, 3),
                ("mmujo", 5, 2), ("mmujo", 9, 5), ("kmujic", 1, 1), ("kmujic", 9, 5), ("kmujic", 6, 3), ("kmujic", 4, 1),
                ("kmujic", 13, 4), ("kmujic", 3, 4), ("kmujic", 5, 3), ("kmujic", 7, 3), ("kmujic", 16, 5), ("kmujic", 12, 5),
                ("lejlah", 15, 2), ("lejlah", 7, 5), ("lejlah", 11, 3), ("lejlah", 14, 2), ("lejlah", 9, 2), ("lejlah", 16, 2),
                ("lejlah", 12, 3), ("lejlah", 2, 3), ("lejlah", 6, 4), ("lejlah", 13, 1), ("eminak", 2, 3), ("eminak", 13, 5),
                ("eminak", 12, 3), ("eminak", 8, 2), ("eminak", 9, 2), ("eminak", 4, 2), ("eminak", 7, 4), ("eminak", 5, 3),
                ("eminak", 16, 5), ("eminak", 1, 3), ("harisb", 13, 5), ("harisb", 3, 5), ("harisb", 15, 5), ("harisb", 2, 4),
                ("harisb", 9, 4), ("harisb", 12, 4), ("harisb", 4, 2), ("harisb", 6, 2), ("adnano", 7, 5), ("adnano", 4, 5),
                ("adnano", 6, 5), ("adnano", 5, 5), ("adnano", 1, 4), ("adnano", 12, 2), ("adnano", 16, 2), ("adnano", 10, 2),
                ("selmad", 4, 4), ("selmad", 5, 4), ("selmad", 6, 4), ("selmad", 16, 3), ("selmad", 10, 4), ("selmad", 8, 4),
                ("selmad", 9, 5), ("selmad", 1, 4), ("tarikm", 11, 4), ("tarikm", 7, 1), ("tarikm", 13, 4), ("tarikm", 5, 1),
                ("tarikm", 2, 5), ("tarikm", 3, 4), ("tarikm", 10, 5), ("tarikm", 14, 4), ("tarikm", 16, 5), ("aidas", 10, 3),
                ("aidas", 12, 3), ("aidas", 14, 5), ("aidas", 11, 3), ("aidas", 7, 5), ("aidas", 9, 2), ("aidas", 15, 5),
                ("aidas", 4, 5), ("aidas", 2, 3), ("aidas", 13, 4), ("aidas", 1, 5),
            };
            context.Ocjene.AddRange(ocjene.Select((x, i) => new Ocjene { Korisnik = Korisnik(x.Korisnik), Putovanje = putovanja[x.Putovanje - 1], Ocjena = x.Ocjena, Datum = danas.AddDays(-(i % 60)) }));

            var rezervacije = new List<Rezervacija>();
            var uplate = new List<Uplate>();
            void Rezervisi(string korisnik, int putovanje, int brojOsoba, string status, params (int DanaPrije, double Udio)[] placanja)
            {
                var k = Korisnik(korisnik);
                var p = putovanja[putovanje - 1];
                var prvaUplata = placanja.Length > 0 ? placanja.Max(x => x.DanaPrije) : 3;
                var r = new Rezervacija
                {
                    Ime = $"{p.NazivPutovanja} - {k.Ime} {k.Prezime}",
                    Korisnik = k,
                    Putovanje = p,
                    BrojOsoba = brojOsoba,
                    Status = status,
                    DatumRezervacije = danas.AddDays(-prvaUplata - 1),
                    Napomena = brojOsoba > 2 ? "Porodična rezervacija" : null
                };
                rezervacije.Add(r);
                foreach (var (danaPrije, udio) in placanja)
                {
                    uplate.Add(new Uplate { Rezervacija = r, Korisnik = k, Datum = danas.AddDays(-danaPrije).AddHours(10), Iznos = Math.Round(p.CijenaPutovanja * brojOsoba * udio, 2) });
                }
            }

            Rezervisi("mobile", 1, 2, StatusRezervacije.Potvrdjeno, (70, 0.5), (50, 0.5));
            Rezervisi("mobile", 4, 2, StatusRezervacije.UObradi, (10, 0.5));
            Rezervisi("mmeho", 2, 1, StatusRezervacije.Potvrdjeno, (69, 1.0));
            Rezervisi("mmeho", 10, 1, StatusRezervacije.Potvrdjeno, (4, 1.0));
            Rezervisi("ssuljo", 3, 1, StatusRezervacije.Potvrdjeno, (115, 1.0));
            Rezervisi("ssuljo", 4, 4, StatusRezervacije.Potvrdjeno, (6, 1.0));
            Rezervisi("mmujo", 3, 1, StatusRezervacije.Potvrdjeno, (88, 1.0));
            Rezervisi("mmujo", 4, 1, StatusRezervacije.UObradi, (51, 0.5));
            Rezervisi("mmujo", 13, 1, StatusRezervacije.UObradi);
            Rezervisi("kmujic", 1, 4, StatusRezervacije.Potvrdjeno, (78, 1.0));
            Rezervisi("kmujic", 8, 1, StatusRezervacije.UObradi, (72, 0.5));
            Rezervisi("lejlah", 3, 2, StatusRezervacije.Potvrdjeno, (107, 1.0));
            Rezervisi("lejlah", 5, 1, StatusRezervacije.UObradi, (9, 0.5));
            Rezervisi("eminak", 3, 2, StatusRezervacije.Potvrdjeno, (123, 1.0));
            Rezervisi("eminak", 13, 4, StatusRezervacije.UObradi, (60, 0.5));
            Rezervisi("harisb", 3, 2, StatusRezervacije.Potvrdjeno, (161, 1.0));
            Rezervisi("harisb", 9, 2, StatusRezervacije.UObradi, (32, 0.5));
            Rezervisi("harisb", 8, 1, StatusRezervacije.UObradi, (68, 0.5));
            Rezervisi("adnano", 2, 3, StatusRezervacije.Potvrdjeno, (137, 1.0));
            Rezervisi("adnano", 15, 1, StatusRezervacije.Potvrdjeno, (27, 1.0));
            Rezervisi("adnano", 11, 2, StatusRezervacije.UObradi, (20, 0.5));
            Rezervisi("selmad", 2, 1, StatusRezervacije.Potvrdjeno, (157, 1.0));
            Rezervisi("selmad", 4, 3, StatusRezervacije.Potvrdjeno, (23, 1.0));
            Rezervisi("selmad", 14, 4, StatusRezervacije.UObradi, (59, 0.5));
            Rezervisi("tarikm", 1, 4, StatusRezervacije.Potvrdjeno, (149, 1.0));
            Rezervisi("tarikm", 8, 1, StatusRezervacije.Potvrdjeno, (45, 1.0));
            Rezervisi("aidas", 2, 4, StatusRezervacije.Potvrdjeno, (145, 1.0));
            Rezervisi("aidas", 8, 3, StatusRezervacije.Potvrdjeno, (30, 1.0));
            Rezervisi("aidas", 15, 3, StatusRezervacije.Potvrdjeno, (8, 1.0));
            Rezervisi("tarikm", 11, 2, StatusRezervacije.Otkazano);
            context.Rezervacija.AddRange(rezervacije);
            context.Uplate.AddRange(uplate);

            var komentari = new (string Korisnik, int Putovanje, string Sadrzaj)[]
            {
                ("mobile", 1, "Odlična organizacija i predivna plaža, vodič je bio sjajan!"),
                ("mmeho", 1, "Smještaj blizu plaže, sve preporuke."),
                ("mmujo", 2, "Kratko ali veoma zanimljivo putovanje."),
                ("kmujic", 2, "Skadarlija je bila vrhunac vikenda."),
                ("eminak", 3, "Istanbul je nevjerovatan, krstarenje Bosforom obavezno."),
                ("harisb", 3, "Malo naporan tempo, ali vrijedi svake marke."),
                ("lejlah", 4, "Jedva čekam! Ima li slobodnih mjesta za još dvoje?"),
                ("selmad", 9, "Da li je ulaznica za Schönbrunn uključena u cijenu?"),
                ("tarikm", 11, "Pariz u jesen je poseban."),
                ("aidas", 6, "Santorini je na mojoj listi želja godinama."),
                ("adnano", 5, "Hvar je predivan, preporučujem."),
                ("ssuljo", 13, "Odlična ponuda za ovu cijenu."),
            };
            context.Komentar.AddRange(komentari.Select((x, i) => new Komentar { Korisnik = Korisnik(x.Korisnik), Putovanje = putovanja[x.Putovanje - 1], Sadrzaj = x.Sadrzaj, Datum = danas.AddDays(-i * 3).AddHours(14) }));

            var listaZelja = new (string Korisnik, int Putovanje)[]
            {
                ("mobile", 6), ("mobile", 5), ("mmeho", 7), ("lejlah", 6), ("eminak", 13), ("eminak", 14), ("harisb", 15), ("mmujo", 10), ("kmujic", 12), ("aidas", 4), ("selmad", 11), ("tarikm", 9)
            };
            context.ListaZelja.AddRange(listaZelja.Select(x => new ListaZelja { Korisnik = Korisnik(x.Korisnik), Putovanje = putovanja[x.Putovanje - 1], Opis = "Želim posjetiti" }));

            var obavijesti = new (string Korisnik, string Naziv, string Sadrzaj)[]
            {
                (null, "Nova sezona putovanja", "Objavljena je nova ponuda putovanja za jesen i zimu. Rani booking popust 10% do kraja mjeseca."),
                (null, "Advent u Zagrebu", "Otvorene su prijave za adventska putovanja u Zagreb i Beč."),
                ("mobile", "Podsjetnik za uplatu", "Podsjećamo vas da je ostatak iznosa za rezervaciju potrebno uplatiti najkasnije 48 sati prije polaska."),
                ("mmeho", "Promjena termina", "Polazak za Barcelonu je pomjeren za 2 sata ranije."),
                (null, "Radno vrijeme poslovnice", "Poslovnica u Sarajevu radi radnim danima od 08 do 20h, subotom od 09 do 14h."),
                ("lejlah", "Potvrda rezervacije", "Vaša rezervacija je zaprimljena, hvala na povjerenju."),
            };
            context.Obavijesti.AddRange(obavijesti.Select((x, i) => new Obavijesti { Korisnik = x.Korisnik != null ? Korisnik(x.Korisnik) : null, Naziv = x.Naziv, Sadrzaj = x.Sadrzaj, Datum = danas.AddDays(-i * 5).AddHours(9) }));

            context.SaveChanges();

            transakcija.Commit();
        }
    }
}
