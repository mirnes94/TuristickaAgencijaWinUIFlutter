import '../models/Obavijesti.dart';
import 'base_provider.dart';

class ObavijestiProvider extends BaseProvider<Obavijesti> {
  ObavijestiProvider() : super("api/Obavijesti");

  @override
  Obavijesti fromJson(data) {
    return Obavijesti.fromJson(data);
  }
}