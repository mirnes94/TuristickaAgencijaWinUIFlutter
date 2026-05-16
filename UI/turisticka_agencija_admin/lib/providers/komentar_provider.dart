import 'package:turisticka_agencija_admin/models/Komentar.dart';

import 'base_provider.dart';

class KomentarProvider extends BaseProvider<Komentar> {
  KomentarProvider() : super("api/Komentar");

  @override
  Komentar fromJson(data) {
    return Komentar.fromJson(data);
  }
}
