import 'package:flutter/foundation.dart';

import '../models/korisnik.dart';
import '../utils/api_exception.dart';
import 'api_client.dart';

class AuthProvider with ChangeNotifier {
  static String username = '';
  static String password = '';
  static Korisnik? trenutniKorisnik;

  Korisnik? get korisnik => trenutniKorisnik;

  /// Prijava: API provjerava Basic auth, a desktop aplikacija dozvoljava samo ulogu Admin.
  Future<Korisnik> login(String korisnickoIme, String lozinka) async {
    username = korisnickoIme;
    password = lozinka;
    try {
      final json = await ApiClient.get('api/Korisnici/Prijava');
      final korisnik = Korisnik.fromJson(json as Map<String, dynamic>);
      if (!korisnik.uloge.contains('Admin')) {
        throw ApiException('Pristup desktop aplikaciji imaju samo administratori.');
      }
      trenutniKorisnik = korisnik;
      notifyListeners();
      return korisnik;
    } catch (_) {
      logout();
      rethrow;
    }
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
