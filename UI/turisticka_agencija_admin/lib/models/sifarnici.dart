import 'json_utils.dart';

class Drzava {
  final int id;
  final String naziv;

  Drzava({required this.id, required this.naziv});

  factory Drzava.fromJson(Map<String, dynamic> json) =>
      Drzava(id: toInt(json['id']), naziv: json['naziv'] ?? '');
}

class Grad {
  final int id;
  final String nazivGrada;
  final int drzavaId;
  final String? drzavaNaziv;

  Grad({required this.id, required this.nazivGrada, required this.drzavaId, this.drzavaNaziv});

  factory Grad.fromJson(Map<String, dynamic> json) => Grad(
        id: toInt(json['id']),
        nazivGrada: json['nazivGrada'] ?? '',
        drzavaId: toInt(json['drzavaId']),
        drzavaNaziv: json['drzavaNaziv'],
      );
}

class Firma {
  final int id;
  final String naziv;
  final int gradId;
  final String? gradNaziv;
  final String adresa;
  final String brojZiroracuna;

  Firma({
    required this.id,
    required this.naziv,
    required this.gradId,
    this.gradNaziv,
    required this.adresa,
    required this.brojZiroracuna,
  });

  factory Firma.fromJson(Map<String, dynamic> json) => Firma(
        id: toInt(json['id']),
        naziv: json['naziv'] ?? '',
        gradId: toInt(json['gradId']),
        gradNaziv: json['gradNaziv'],
        adresa: json['adresa'] ?? '',
        brojZiroracuna: json['brojZiroracuna'] ?? '',
      );
}

class Prevoz {
  final int id;
  final int firmaId;
  final String? firmaNaziv;
  final String tipPrevoza;
  final int brojMjesta;
  final double cijenaPoMjestu;

  Prevoz({
    required this.id,
    required this.firmaId,
    this.firmaNaziv,
    required this.tipPrevoza,
    required this.brojMjesta,
    required this.cijenaPoMjestu,
  });

  String get prikaz => firmaNaziv == null ? tipPrevoza : '$tipPrevoza - $firmaNaziv';

  factory Prevoz.fromJson(Map<String, dynamic> json) => Prevoz(
        id: toInt(json['id']),
        firmaId: toInt(json['firmaId']),
        firmaNaziv: json['firmaNaziv'],
        tipPrevoza: json['tipPrevoza'] ?? '',
        brojMjesta: toInt(json['brojMjesta']),
        cijenaPoMjestu: toDouble(json['cijenaPoMjestu']),
      );
}

class Smjestaj {
  final int id;
  final String nazivSmjestaja;
  final String? opisSmjestaja;
  final double cijenaNocenja;
  final String tipSobe;
  final String? slika;

  Smjestaj({
    required this.id,
    required this.nazivSmjestaja,
    this.opisSmjestaja,
    required this.cijenaNocenja,
    required this.tipSobe,
    this.slika,
  });

  factory Smjestaj.fromJson(Map<String, dynamic> json) => Smjestaj(
        id: toInt(json['id']),
        nazivSmjestaja: json['nazivSmjestaja'] ?? '',
        opisSmjestaja: json['opisSmjestaja'],
        cijenaNocenja: toDouble(json['cijenaNocenja']),
        tipSobe: json['tipSobe'] ?? '',
        slika: json['slika'],
      );
}

class Uloga {
  final int id;
  final String naziv;
  final String? opis;

  Uloga({required this.id, required this.naziv, this.opis});

  factory Uloga.fromJson(Map<String, dynamic> json) =>
      Uloga(id: toInt(json['id']), naziv: json['naziv'] ?? '', opis: json['opis']);
}

class Vodic {
  final int id;
  final String ime;
  final String prezime;
  final String kontakt;
  final String jmbg;
  final String? slika;

  Vodic({
    required this.id,
    required this.ime,
    required this.prezime,
    required this.kontakt,
    required this.jmbg,
    this.slika,
  });

  String get imePrezime => '$ime $prezime';

  factory Vodic.fromJson(Map<String, dynamic> json) => Vodic(
        id: toInt(json['id']),
        ime: json['ime'] ?? '',
        prezime: json['prezime'] ?? '',
        kontakt: json['kontakt'] ?? '',
        jmbg: json['jmbg'] ?? '',
        slika: json['slika'],
      );
}
