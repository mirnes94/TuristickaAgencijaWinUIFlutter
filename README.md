# Turistička agencija – seminarski rad (Razvoj softvera II)

Student: Mirnes Turković, IB210290

## Arhitektura

| Projekat | Opis |
|---|---|
| `TuristickaAgencija.Model` | DTO modeli, request/search klase i poruke za RabbitMQ (dijele ih API i Subscriber) |
| `TuristickaAgencija.Services` | EF Core (SQL Server), poslovna logika, sistem preporuke, slanje poruka na RabbitMQ |
| `TuristickaAgencija.WebAPI` | **Glavni servis** – REST API za desktop i mobilnu aplikaciju |
| `TuristickaAgencija.Subscriber` | **Pomoćni servis** – sluša RabbitMQ i šalje email notifikacije (registracija, obavijesti, rezervacije, uplate) |
| `UI/turisticka_agencija_admin` | Flutter **desktop** aplikacija (administrator) |
| `UI/turisticka_agencija_mobile` | Flutter **mobilna** aplikacija (klijent) |

Komunikacija: `WebAPI → RabbitMQ (red turisticka-agencija.notifikacije) → Subscriber → SMTP (Mailpit)`.

Sva konfiguracija za Docker je na jednom mjestu – u fajlu `.env` (connection string, RabbitMQ, SMTP, Stripe, parametri preporuke).

## Pokretanje

### 1. Backend (Docker)

Konfiguracija je u fajlu `.env`, koji je u repozitoriju zipovan kao `.env.zip` (šifra: `fit`). Prije pokretanja raspakovati `.env.zip` u korijen projekta.

```bash
docker compose up --build
```

| Servis | Adresa |
|---|---|
| API (Swagger) | http://localhost:5000/swagger |
| RabbitMQ management | http://localhost:15672 (admin / admin) |
| Mailpit – pregled poslanih emailova | http://localhost:8025 |
| SQL Server | `localhost,1401` (sa / lozinka iz `.env`), baza `210290` |

Baza i testni podaci se kreiraju automatski pri prvom pokretanju API-ja.
Ako je baza ranije kreirana starijom verzijom, obrisati je sa `docker compose down -v` pa ponovo pokrenuti.

### 2. Desktop aplikacija (Windows)

```bash
cd UI/turisticka_agencija_admin
flutter pub get
flutter run -d windows --dart-define=baseUrl=http://localhost:5000/
```

### 3. Mobilna aplikacija (Android emulator)

```bash
cd UI/turisticka_agencija_mobile
flutter pub get
flutter run --dart-define=baseUrl=http://10.0.2.2:5000/
```

Nakon registracije novog korisnika u mobilnoj aplikaciji, link za aktivaciju naloga stiže na email (vidljiv u Mailpit-u: http://localhost:8025).

## Korisnički podaci

Desktop verzija:
- Korisničko ime: `desktop`
- Lozinka: `test`

Mobilna verzija:
- Korisničko ime: `mobile`
- Lozinka: `test`

Ostali testni klijenti (lozinka `test`): `mmeho`, `ssuljo`, `mmujo`, `kmujic`, `lejlah`, `eminak`, `harisb`, `adnano`, `selmad`, `tarikm`, `aidas`.

Stripe testna kartica: `4242 4242 4242 4242`, CVC bilo koja 3 broja, datum bilo koji budući.
Stripe PaymentIntent kreira API (ključevi su u `.env`), a API nakon plaćanja kod Stripe-a provjerava da je uplata uspjela prije nego što je evidentira.

## Sistem preporuke

User-based collaborative filtering (Pearsonova korelacija + K najbližih susjeda). Opis: `recommender-dokumentacija.pdf`.

## Mikroservisi

- Glavni servis (`TuristickaAgencija.WebAPI`) šalje poruke na RabbitMQ kada se:
  - korisnik registruje (email sa linkom za aktivaciju naloga),
  - objavi obavijest,
  - kreira ili promijeni status rezervacije,
  - evidentira uplata.
- Pomoćni servis (`TuristickaAgencija.Subscriber`) je zaseban projekat i kontejner. Prima te poruke i šalje email preko SMTP-a. Poslani emailovi se vide u Mailpit-u.
