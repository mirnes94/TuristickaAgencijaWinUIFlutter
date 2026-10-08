import 'json_utils.dart';
import 'putovanje.dart';
import '../utils/formatters.dart';

class UplataStavka {
  final DateTime datum;
  final double iznos;
  final String korisnik;
  final String rezervacija;
  final String putovanje;

  UplataStavka({
    required this.datum,
    required this.iznos,
    required this.korisnik,
    required this.rezervacija,
    required this.putovanje,
  });

  factory UplataStavka.fromJson(Map<String, dynamic> json) => UplataStavka(
        datum: parseDate(json['datum']) ?? DateTime.now(),
        iznos: toDouble(json['iznos']),
        korisnik: json['korisnik'] ?? '-',
        rezervacija: json['rezervacija'] ?? '-',
        putovanje: json['putovanje'] ?? '-',
      );
}

class PutovanjePrihod {
  final String putovanje;
  final int brojUplata;
  final double iznos;

  PutovanjePrihod({required this.putovanje, required this.brojUplata, required this.iznos});

  factory PutovanjePrihod.fromJson(Map<String, dynamic> json) => PutovanjePrihod(
        putovanje: json['putovanje'] ?? '-',
        brojUplata: toInt(json['brojUplata']),
        iznos: toDouble(json['iznos']),
      );
}

class UplateIzvjestaj {
  final int godina;
  final int mjesec;
  final int brojUplata;
  final double ukupanIznos;
  final double prosjecnaUplata;
  final List<UplataStavka> stavke;
  final List<PutovanjePrihod> poPutovanjima;

  UplateIzvjestaj({
    required this.godina,
    required this.mjesec,
    required this.brojUplata,
    required this.ukupanIznos,
    required this.prosjecnaUplata,
    required this.stavke,
    required this.poPutovanjima,
  });

  factory UplateIzvjestaj.fromJson(Map<String, dynamic> json) => UplateIzvjestaj(
        godina: toInt(json['godina']),
        mjesec: toInt(json['mjesec']),
        brojUplata: toInt(json['brojUplata']),
        ukupanIznos: toDouble(json['ukupanIznos']),
        prosjecnaUplata: toDouble(json['prosjecnaUplata']),
        stavke: ((json['stavke'] as List?) ?? const [])
            .map((e) => UplataStavka.fromJson(e as Map<String, dynamic>))
            .toList(),
        poPutovanjima: ((json['poPutovanjima'] as List?) ?? const [])
            .map((e) => PutovanjePrihod.fromJson(e as Map<String, dynamic>))
            .toList(),
      );
}

class PopularnoPutovanje {
  final String putovanje;
  final int brojRezervacija;
  final int brojOsoba;

  PopularnoPutovanje({required this.putovanje, required this.brojRezervacija, required this.brojOsoba});

  factory PopularnoPutovanje.fromJson(Map<String, dynamic> json) => PopularnoPutovanje(
        putovanje: json['putovanje'] ?? '-',
        brojRezervacija: toInt(json['brojRezervacija']),
        brojOsoba: toInt(json['brojOsoba']),
      );
}

class MjesecniPrihod {
  final int godina;
  final int mjesec;
  final double iznos;

  MjesecniPrihod({required this.godina, required this.mjesec, required this.iznos});

  factory MjesecniPrihod.fromJson(Map<String, dynamic> json) => MjesecniPrihod(
        godina: toInt(json['godina']),
        mjesec: toInt(json['mjesec']),
        iznos: toDouble(json['iznos']),
      );
}

class DashboardStatistika {
  final int brojKorisnika;
  final int brojPutovanja;
  final int brojAktivnihPutovanja;
  final int brojRezervacija;
  final int rezervacijeUObradi;
  final double prihodOvajMjesec;
  final double prihodUkupno;
  final List<PopularnoPutovanje> najpopularnijaPutovanja;
  final List<MjesecniPrihod> prihodPoMjesecima;

  DashboardStatistika({
    required this.brojKorisnika,
    required this.brojPutovanja,
    required this.brojAktivnihPutovanja,
    required this.brojRezervacija,
    required this.rezervacijeUObradi,
    required this.prihodOvajMjesec,
    required this.prihodUkupno,
    required this.najpopularnijaPutovanja,
    required this.prihodPoMjesecima,
  });

  factory DashboardStatistika.fromJson(Map<String, dynamic> json) => DashboardStatistika(
        brojKorisnika: toInt(json['brojKorisnika']),
        brojPutovanja: toInt(json['brojPutovanja']),
        brojAktivnihPutovanja: toInt(json['brojAktivnihPutovanja']),
        brojRezervacija: toInt(json['brojRezervacija']),
        rezervacijeUObradi: toInt(json['rezervacijeUObradi']),
        prihodOvajMjesec: toDouble(json['prihodOvajMjesec']),
        prihodUkupno: toDouble(json['prihodUkupno']),
        najpopularnijaPutovanja: ((json['najpopularnijaPutovanja'] as List?) ?? const [])
            .map((e) => PopularnoPutovanje.fromJson(e as Map<String, dynamic>))
            .toList(),
        prihodPoMjesecima: ((json['prihodPoMjesecima'] as List?) ?? const [])
            .map((e) => MjesecniPrihod.fromJson(e as Map<String, dynamic>))
            .toList(),
      );
}

class SlicanKorisnik {
  final int korisnikId;
  final String imePrezime;
  final double slicnost;
  final int zajednickihOcjena;

  SlicanKorisnik({
    required this.korisnikId,
    required this.imePrezime,
    required this.slicnost,
    required this.zajednickihOcjena,
  });

  factory SlicanKorisnik.fromJson(Map<String, dynamic> json) => SlicanKorisnik(
        korisnikId: toInt(json['korisnikId']),
        imePrezime: json['imePrezime'] ?? '',
        slicnost: toDouble(json['slicnost']),
        zajednickihOcjena: toInt(json['zajednickihOcjena']),
      );
}

class PreporucenoPutovanje {
  final Putovanje putovanje;
  final double predvidjenaOcjena;
  final int brojSusjeda;
  final String izvor;

  PreporucenoPutovanje({
    required this.putovanje,
    required this.predvidjenaOcjena,
    required this.brojSusjeda,
    required this.izvor,
  });

  factory PreporucenoPutovanje.fromJson(Map<String, dynamic> json) => PreporucenoPutovanje(
        putovanje: Putovanje.fromJson(json['putovanje'] as Map<String, dynamic>),
        predvidjenaOcjena: toDouble(json['predvidjenaOcjena']),
        brojSusjeda: toInt(json['brojSusjeda']),
        izvor: json['izvor'] ?? '',
      );
}

class PreporukaRezultat {
  final int korisnikId;
  final String korisnikImePrezime;
  final double prosjecnaOcjenaKorisnika;
  final int brojOcjenaKorisnika;
  final List<SlicanKorisnik> susjedi;
  final List<PreporucenoPutovanje> preporuke;

  PreporukaRezultat({
    required this.korisnikId,
    required this.korisnikImePrezime,
    required this.prosjecnaOcjenaKorisnika,
    required this.brojOcjenaKorisnika,
    required this.susjedi,
    required this.preporuke,
  });

  factory PreporukaRezultat.fromJson(Map<String, dynamic> json) => PreporukaRezultat(
        korisnikId: toInt(json['korisnikId']),
        korisnikImePrezime: json['korisnikImePrezime'] ?? '',
        prosjecnaOcjenaKorisnika: toDouble(json['prosjecnaOcjenaKorisnika']),
        brojOcjenaKorisnika: toInt(json['brojOcjenaKorisnika']),
        susjedi: ((json['susjedi'] as List?) ?? const [])
            .map((e) => SlicanKorisnik.fromJson(e as Map<String, dynamic>))
            .toList(),
        preporuke: ((json['preporuke'] as List?) ?? const [])
            .map((e) => PreporucenoPutovanje.fromJson(e as Map<String, dynamic>))
            .toList(),
      );
}
