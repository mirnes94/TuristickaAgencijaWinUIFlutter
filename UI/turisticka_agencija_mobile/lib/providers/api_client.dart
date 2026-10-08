import 'dart:convert';

import 'package:http/http.dart' as http;

import '../utils/api_exception.dart';
import '../utils/config.dart';
import 'auth_provider.dart';

/// Zajednicki HTTP klijent: Basic auth zaglavlje, JSON i obrada gresaka.
class ApiClient {
  static Map<String, String> headers() {
    final h = {
      'Content-Type': 'application/json; charset=utf-8',
      'Accept': 'application/json',
    };
    if (AuthProvider.username.isNotEmpty) {
      final credentials = base64Encode(utf8.encode('${AuthProvider.username}:${AuthProvider.password}'));
      h['Authorization'] = 'Basic $credentials';
    }
    return h;
  }

  static Uri uri(String path, [Map<String, dynamic>? query]) {
    final params = <String, String>{};
    query?.forEach((key, value) {
      if (value == null) return;
      if (value is String && value.trim().isEmpty) return;
      params[key] = value is DateTime ? value.toIso8601String() : value.toString();
    });
    final base = Uri.parse('${AppConfig.baseUrl}$path');
    return params.isEmpty ? base : base.replace(queryParameters: params);
  }

  static Future<dynamic> get(String path, [Map<String, dynamic>? query]) async {
    final response = await _send(() => http.get(uri(path, query), headers: headers()));
    return _decode(response);
  }

  static Future<dynamic> post(String path, Object body) async {
    final response = await _send(() => http.post(uri(path), headers: headers(), body: jsonEncode(body)));
    return _decode(response);
  }

  static Future<dynamic> put(String path, Object body) async {
    final response = await _send(() => http.put(uri(path), headers: headers(), body: jsonEncode(body)));
    return _decode(response);
  }

  static Future<void> delete(String path) async {
    await _send(() => http.delete(uri(path), headers: headers()));
  }

  static Future<http.Response> _send(Future<http.Response> Function() request) async {
    http.Response response;
    try {
      response = await request().timeout(const Duration(seconds: 30));
    } catch (_) {
      throw ApiException('Server nije dostupan. Provjerite internet konekciju i da li je API pokrenut.');
    }

    if (response.statusCode >= 200 && response.statusCode < 300) {
      return response;
    }

    final body = utf8.decode(response.bodyBytes);
    final poruka = ApiException.parseMessage(body);
    switch (response.statusCode) {
      case 401:
        throw ApiException(
          'Pogrešno korisničko ime ili lozinka, ili nalog još nije aktiviran (provjerite email).',
          statusCode: 401,
        );
      case 403:
        throw ApiException(poruka ?? 'Nemate pravo na ovu akciju.', statusCode: 403);
      case 404:
        throw ApiException(poruka ?? 'Traženi podatak ne postoji.', statusCode: 404);
      default:
        if (response.statusCode >= 500) {
          throw ApiException(poruka ?? 'Greška na serveru. Pokušajte ponovo.', statusCode: response.statusCode);
        }
        throw ApiException(poruka ?? 'Zahtjev nije ispravan.', statusCode: response.statusCode);
    }
  }

  static dynamic _decode(http.Response response) {
    if (response.bodyBytes.isEmpty) return null;
    return jsonDecode(utf8.decode(response.bodyBytes));
  }
}
