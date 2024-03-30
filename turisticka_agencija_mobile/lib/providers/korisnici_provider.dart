import 'package:turisticka_agencija_mobile/models/Korisnici.dart';

import 'base_provider.dart';

class KorisniciProvider extends BaseProvider<Korisnici> {
  KorisniciProvider() : super("api/Korisnici");

  @override
  Korisnici fromJson(data) {
    return Korisnici.fromJson(data);
  }
}