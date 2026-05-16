import 'package:turisticka_agencija_mobile/models/ListaZelja.dart';

import 'base_provider.dart';

class ListaZeljaProvider extends BaseProvider<ListaZelja> {
  ListaZeljaProvider() : super("api/ListaZelja");

  @override
  ListaZelja fromJson(data) {
    return ListaZelja.fromJson(data);
  }
}