import 'json_utils.dart';
import '../utils/formatters.dart';

class Komentar {
  final int id;
  final int putovanjeId;
  final int korisnikId;
  final DateTime datum;
  final String sadrzaj;
  final String? korisnikImePrezime;
  final String? putovanjeNaziv;

  Komentar({
    required this.id,
    required this.putovanjeId,
    required this.korisnikId,
    required this.datum,
    required this.sadrzaj,
    this.korisnikImePrezime,
    this.putovanjeNaziv,
  });

  factory Komentar.fromJson(Map<String, dynamic> json) => Komentar(
        id: toInt(json['id']),
        putovanjeId: toInt(json['putovanjeId']),
        korisnikId: toInt(json['korisnikId']),
        datum: parseDate(json['datum']) ?? DateTime.now(),
        sadrzaj: json['sadrzaj'] ?? '',
        korisnikImePrezime: json['korisnikImePrezime'],
        putovanjeNaziv: json['putovanjeNaziv'],
      );
}

class Ocjena {
  final int id;
  final int putovanjeId;
  final int korisnikId;
  final DateTime datum;
  final int ocjena;
  final String? korisnikImePrezime;
  final String? putovanjeNaziv;

  Ocjena({
    required this.id,
    required this.putovanjeId,
    required this.korisnikId,
    required this.datum,
    required this.ocjena,
    this.korisnikImePrezime,
    this.putovanjeNaziv,
  });

  factory Ocjena.fromJson(Map<String, dynamic> json) => Ocjena(
        id: toInt(json['id']),
        putovanjeId: toInt(json['putovanjeId']),
        korisnikId: toInt(json['korisnikId']),
        datum: parseDate(json['datum']) ?? DateTime.now(),
        ocjena: toInt(json['ocjena']),
        korisnikImePrezime: json['korisnikImePrezime'],
        putovanjeNaziv: json['putovanjeNaziv'],
      );
}

class Obavijest {
  final int id;
  final String naziv;
  final String sadrzaj;
  final int? korisnikId;
  final DateTime datum;
  final String? korisnikImePrezime;

  Obavijest({
    required this.id,
    required this.naziv,
    required this.sadrzaj,
    this.korisnikId,
    required this.datum,
    this.korisnikImePrezime,
  });

  factory Obavijest.fromJson(Map<String, dynamic> json) => Obavijest(
        id: toInt(json['id']),
        naziv: json['naziv'] ?? '',
        sadrzaj: json['sadrzaj'] ?? '',
        korisnikId: toIntOrNull(json['korisnikId']),
        datum: parseDate(json['datum']) ?? DateTime.now(),
        korisnikImePrezime: json['korisnikImePrezime'],
      );
}

class ListaZelja {
  final int id;
  final int putovanjeId;
  final int korisnikId;
  final String? opis;
  final String? putovanjeNaziv;

  ListaZelja({
    required this.id,
    required this.putovanjeId,
    required this.korisnikId,
    this.opis,
    this.putovanjeNaziv,
  });

  factory ListaZelja.fromJson(Map<String, dynamic> json) => ListaZelja(
        id: toInt(json['id']),
        putovanjeId: toInt(json['putovanjeId']),
        korisnikId: toInt(json['korisnikId']),
        opis: json['opis'],
        putovanjeNaziv: json['putovanjeNaziv'],
      );
}
