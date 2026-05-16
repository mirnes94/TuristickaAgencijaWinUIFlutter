import 'package:turisticka_agencija_mobile/models/Firma.dart';

import 'base_provider.dart';

class FirmaProvider extends BaseProvider<Firma> {
  FirmaProvider() : super("api/Firma");

  @override
  Firma fromJson(data) {
    return Firma.fromJson(data);
  }
}