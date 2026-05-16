import '../models/Korisnici.dart';
import 'base_provider.dart';

class AuthenticateProvider extends BaseProvider<Korisnici> {
  AuthenticateProvider() : super("api/Korisnici/Authenticiraj");
}