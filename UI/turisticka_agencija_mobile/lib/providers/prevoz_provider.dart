import 'package:turisticka_agencija_mobile/models/Prevoz.dart';

import 'base_provider.dart';

class PrevozProvider extends BaseProvider<Prevoz> {
  PrevozProvider() : super("api/Prevoz");

  @override
  Prevoz fromJson(data) {
    return Prevoz.fromJson(data);
  }
}