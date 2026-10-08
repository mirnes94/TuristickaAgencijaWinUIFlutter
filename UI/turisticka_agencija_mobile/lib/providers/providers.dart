import '../models/interakcije.dart';
import '../models/korisnik.dart';
import '../models/putovanje.dart';
import '../models/rezervacija.dart';
import '../models/sifarnici.dart';
import 'api_client.dart';
import 'base_provider.dart';

class GradProvider extends BaseProvider<Grad> {
  GradProvider() : super('api/Gradovi');
  @override
  Grad fromJson(Map<String, dynamic> json) => Grad.fromJson(json);
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

  /// Preporuke za prijavljenog korisnika (user-based collaborative filtering na API-ju).
  Future<List<Putovanje>> preporuke({int? bezPutovanja}) async {
    final data = bezPutovanja == null
        ? await ApiClient.get('api/Recommender/Moje')
        : await ApiClient.get('api/Recommender/GetRecommendedPutovanja/$bezPutovanja');
    return (data as List).map((e) => Putovanje.fromJson(e as Map<String, dynamic>)).toList();
  }
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

class ListaZeljaProvider extends BaseProvider<ListaZelja> {
  ListaZeljaProvider() : super('api/ListaZelja');
  @override
  ListaZelja fromJson(Map<String, dynamic> json) => ListaZelja.fromJson(json);
}

class ObavijestProvider extends BaseProvider<Obavijest> {
  ObavijestProvider() : super('api/Obavijesti');
  @override
  Obavijest fromJson(Map<String, dynamic> json) => Obavijest.fromJson(json);
}
