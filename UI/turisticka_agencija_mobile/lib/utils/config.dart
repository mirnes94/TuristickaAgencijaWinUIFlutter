/// Adresa API-ja se zadaje pri pokretanju:
///   flutter run --dart-define=baseUrl=http://10.0.2.2:5000/
/// 10.0.2.2 je adresa racunara (localhost) iz Android emulatora.
class AppConfig {
  static String get baseUrl {
    const url = String.fromEnvironment('baseUrl', defaultValue: 'http://10.0.2.2:5000/');
    return url.endsWith('/') ? url : '$url/';
  }
}
