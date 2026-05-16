import 'package:turisticka_agencija_admin/models/Uplate.dart';

import 'base_provider.dart';

class UplateProvider extends BaseProvider<Uplate> {
  UplateProvider() : super("api/Uplate");

  @override
  Uplate fromJson(data) {
    return Uplate.fromJson(data);
  }
}
