import 'package:flutter/foundation.dart';

import '../models/korisnik.dart';
import '../utils/api_exception.dart';
import 'api_client.dart';

class AuthProvider with ChangeNotifier {
  static String username = '';
  static String password = '';
  static Korisnik? trenutniKorisnik;

  static int get korisnikId => trenutniKorisnik?.id ?? 0;

  Korisnik? get korisnik => trenutniKorisnik;

  Future<Korisnik> login(String korisnickoIme, String lozinka) async {
    username = korisnickoIme;
    password = lozinka;
    try {
      final json = await ApiClient.get('api/Korisnici/Prijava');
      final korisnik = Korisnik.fromJson(json as Map<String, dynamic>);
      if (!korisnik.uloge.contains('Klijent')) {
        throw ApiException('Mobilna aplikacija je namijenjena klijentima. Administratori koriste desktop aplikaciju.');
      }
      trenutniKorisnik = korisnik;
      notifyListeners();
      return korisnik;
    } catch (_) {
      logout();
      rethrow;
    }
  }

  /// Registracija novog klijenta (nalog postaje aktivan nakon klika na link iz emaila).
  Future<void> registracija(Map<String, dynamic> request) async {
    logout();
    await ApiClient.post('api/Korisnici', request);
  }

  void azurirajKorisnika(Korisnik korisnik, {String? novaLozinka}) {
    trenutniKorisnik = korisnik;
    username = korisnik.korisnickoIme;
    if (novaLozinka != null && novaLozinka.isNotEmpty) {
      password = novaLozinka;
    }
    notifyListeners();
  }

  void logout() {
    username = '';
    password = '';
    trenutniKorisnik = null;
    notifyListeners();
  }
}
