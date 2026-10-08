import 'json_utils.dart';
import '../utils/formatters.dart';

class StatusRezervacije {
  static const uObradi = 'U obradi';
  static const potvrdjeno = 'Potvrđeno';
  static const otkazano = 'Otkazano';
  static const svi = [uObradi, potvrdjeno, otkazano];
}

class Rezervacija {
  final int id;
  final String ime;
  final int? korisnikId;
  final int? putovanjeId;
  final DateTime datumRezervacije;
  final int brojOsoba;
  final String status;
  final String? napomena;
  final String? korisnikImePrezime;
  final String? putovanjeNaziv;
  final DateTime? datumPolaska;
  final double ukupnaCijena;
  final double uplaceno;

  Rezervacija({
    required this.id,
    required this.ime,
    this.korisnikId,
    this.putovanjeId,
    required this.datumRezervacije,
    required this.brojOsoba,
    required this.status,
    this.napomena,
    this.korisnikImePrezime,
    this.putovanjeNaziv,
    this.datumPolaska,
    required this.ukupnaCijena,
    required this.uplaceno,
  });

  /// Zaokruzeno na 2 decimale: cijena na API-ju je float, pa razlika moze biti npr. 0.000003
  /// (tada bi se prikazalo "Plati ostatak (0,00 KM)" i Stripe bi odbio iznos manji od 0.50).
  double get preostalo {
    final p = double.parse((ukupnaCijena - uplaceno).toStringAsFixed(2));
    return p > 0 ? p : 0.0;
  }

  factory Rezervacija.fromJson(Map<String, dynamic> json) => Rezervacija(
        id: toInt(json['id']),
        ime: json['ime'] ?? '',
        korisnikId: toIntOrNull(json['korisnikId']),
        putovanjeId: toIntOrNull(json['putovanjeId']),
        datumRezervacije: parseDate(json['datumRezervacije']) ?? DateTime.now(),
        brojOsoba: toInt(json['brojOsoba']),
        status: json['status'] ?? '',
        napomena: json['napomena'],
        korisnikImePrezime: json['korisnikImePrezime'],
        putovanjeNaziv: json['putovanjeNaziv'],
        datumPolaska: parseDate(json['datumPolaska']),
        ukupnaCijena: toDouble(json['ukupnaCijena']),
        uplaceno: toDouble(json['uplaceno']),
      );
}

class Uplata {
  final int id;
  final DateTime datum;
  final double iznos;
  final int rezervacijaId;
  final int korisnikId;
  final String? korisnikImePrezime;
  final String? rezervacijaNaziv;
  final String? putovanjeNaziv;
  final String? nacinPlacanja;

  Uplata({
    required this.id,
    required this.datum,
    required this.iznos,
    required this.rezervacijaId,
    required this.korisnikId,
    this.korisnikImePrezime,
    this.rezervacijaNaziv,
    this.putovanjeNaziv,
    this.nacinPlacanja,
  });

  factory Uplata.fromJson(Map<String, dynamic> json) => Uplata(
        id: toInt(json['id']),
        datum: parseDate(json['datum']) ?? DateTime.now(),
        iznos: toDouble(json['iznos']),
        rezervacijaId: toInt(json['rezervacijaId']),
        korisnikId: toInt(json['korisnikId']),
        korisnikImePrezime: json['korisnikImePrezime'],
        rezervacijaNaziv: json['rezervacijaNaziv'],
        putovanjeNaziv: json['putovanjeNaziv'],
        nacinPlacanja: json['nacinPlacanja'],
      );
}
