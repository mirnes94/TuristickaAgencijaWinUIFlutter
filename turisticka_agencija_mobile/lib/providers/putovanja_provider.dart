
import '../models/Putovanja.dart';
import 'base_provider.dart';

class PutovanjaProvider extends BaseProvider<Putovanja> {
  PutovanjaProvider() : super("api/Putovanja");

  @override
  Putovanja fromJson(data) {
    return Putovanja.fromJson(data);
  }
}