import 'package:turisticka_agencija_admin/models/VodiciPutovanja.dart';

import 'base_provider.dart';

class VodiciPutovanjaProvider extends BaseProvider<VodiciPutovanja> {
  VodiciPutovanjaProvider() : super("api/VodiciPutovanja");

  @override
  VodiciPutovanja fromJson(data) {
    return VodiciPutovanja.fromJson(data);
  }
}
