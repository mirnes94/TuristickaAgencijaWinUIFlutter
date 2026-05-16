import 'package:turisticka_agencija_mobile/models/KomentarModel.dart';

import 'base_provider.dart';

class KomentarModelProvider extends BaseProvider<KomentarModel> {
  KomentarModelProvider() : super("api/KomentarModel");

  @override
  KomentarModel fromJson(data) {
    return KomentarModel.fromJson(data);
  }
}