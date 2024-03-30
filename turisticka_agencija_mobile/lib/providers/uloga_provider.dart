import '../models/Uloga.dart';
import 'base_provider.dart';

class UlogaProvider extends BaseProvider<Uloga> {
  UlogaProvider() : super("api/Uloga");

  @override
  Uloga fromJson(data) {
    return Uloga.fromJson(data);
  }
}