import '../models/Vodic.dart';
import 'base_provider.dart';

class VodicProvider extends BaseProvider<Vodic> {
  VodicProvider() : super("api/Vodic");

  @override
  Vodic fromJson(data) {
    return Vodic.fromJson(data);
  }
}