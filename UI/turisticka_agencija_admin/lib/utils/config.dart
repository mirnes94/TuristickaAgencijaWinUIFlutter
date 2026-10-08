/// Adresa API-ja se zadaje pri pokretanju:
///   flutter run -d windows --dart-define=baseUrl=http://localhost:5000/
/// Ako nije zadana, koristi se http://localhost:5000/ (API iz docker-compose).
class AppConfig {
  static String get baseUrl {
    const url = String.fromEnvironment('baseUrl', defaultValue: 'http://localhost:5000/');
    return url.endsWith('/') ? url : '$url/';
  }
}
