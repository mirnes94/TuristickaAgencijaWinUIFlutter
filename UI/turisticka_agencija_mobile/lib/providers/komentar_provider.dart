import 'package:turisticka_agencija_mobile/models/Komentar.dart';

import 'base_provider.dart';

class KomentarProvider extends BaseProvider<Komentar> {
  KomentarProvider() : super("api/Komentar");

  @override
  Komentar fromJson(data) {
    return Komentar.fromJson(data);
  }
}