import '../models/interakcije.dart';
import '../models/izvjestaji.dart';
import '../models/korisnik.dart';
import '../models/putovanje.dart';
import '../models/rezervacija.dart';
import '../models/sifarnici.dart';
import 'api_client.dart';
import 'base_provider.dart';

class DrzavaProvider extends BaseProvider<Drzava> {
  DrzavaProvider() : super('api/Drzava');
  @override
  Drzava fromJson(Map<String, dynamic> json) => Drzava.fromJson(json);
}

class GradProvider extends BaseProvider<Grad> {
  GradProvider() : super('api/Gradovi');
  @override
  Grad fromJson(Map<String, dynamic> json) => Grad.fromJson(json);
}

class FirmaProvider extends BaseProvider<Firma> {
  FirmaProvider() : super('api/Firma');
  @override
  Firma fromJson(Map<String, dynamic> json) => Firma.fromJson(json);
}

class PrevozProvider extends BaseProvider<Prevoz> {
  PrevozProvider() : super('api/Prevoz');
  @override
  Prevoz fromJson(Map<String, dynamic> json) => Prevoz.fromJson(json);
}

class SmjestajProvider extends BaseProvider<Smjestaj> {
  SmjestajProvider() : super('api/Smjestaj');
  @override
  Smjestaj fromJson(Map<String, dynamic> json) => Smjestaj.fromJson(json);
}

class UlogaProvider extends BaseProvider<Uloga> {
  UlogaProvider() : super('api/Uloge');
  @override
  Uloga fromJson(Map<String, dynamic> json) => Uloga.fromJson(json);
}

class VodicProvider extends BaseProvider<Vodic> {
  VodicProvider() : super('api/Vodic');
  @override
  Vodic fromJson(Map<String, dynamic> json) => Vodic.fromJson(json);
}

class KorisnikProvider extends BaseProvider<Korisnik> {
  KorisnikProvider() : super('api/Korisnici');
  @override
  Korisnik fromJson(Map<String, dynamic> json) => Korisnik.fromJson(json);
}

class PutovanjeProvider extends BaseProvider<Putovanje> {
  PutovanjeProvider() : super('api/Putovanja');
  @override
  Putovanje fromJson(Map<String, dynamic> json) => Putovanje.fromJson(json);
}

class RezervacijaProvider extends BaseProvider<Rezervacija> {
  RezervacijaProvider() : super('api/Rezervacija');
  @override
  Rezervacija fromJson(Map<String, dynamic> json) => Rezervacija.fromJson(json);
}

class UplataProvider extends BaseProvider<Uplata> {
  UplataProvider() : super('api/Uplate');
  @override
  Uplata fromJson(Map<String, dynamic> json) => Uplata.fromJson(json);
}

class KomentarProvider extends BaseProvider<Komentar> {
  KomentarProvider() : super('api/Komentar');
  @override
  Komentar fromJson(Map<String, dynamic> json) => Komentar.fromJson(json);
}

class OcjenaProvider extends BaseProvider<Ocjena> {
  OcjenaProvider() : super('api/Ocjene');
  @override
  Ocjena fromJson(Map<String, dynamic> json) => Ocjena.fromJson(json);
}

class ObavijestProvider extends BaseProvider<Obavijest> {
  ObavijestProvider() : super('api/Obavijesti');
  @override
  Obavijest fromJson(Map<String, dynamic> json) => Obavijest.fromJson(json);
}

class IzvjestajProvider {
  Future<UplateIzvjestaj> uplate(int godina, int mjesec) async {
    final data = await ApiClient.get('api/Izvjestaj/Uplate', {'godina': godina, 'mjesec': mjesec});
    return UplateIzvjestaj.fromJson(data as Map<String, dynamic>);
  }

  Future<DashboardStatistika> dashboard() async {
    final data = await ApiClient.get('api/Izvjestaj/Dashboard');
    return DashboardStatistika.fromJson(data as Map<String, dynamic>);
  }
}

class PreporukaProvider {
  Future<PreporukaRezultat> zaKorisnika(int korisnikId, {int broj = 5}) async {
    final data = await ApiClient.get('api/Recommender/Korisnik/$korisnikId', {'broj': broj});
    return PreporukaRezultat.fromJson(data as Map<String, dynamic>);
  }
}
