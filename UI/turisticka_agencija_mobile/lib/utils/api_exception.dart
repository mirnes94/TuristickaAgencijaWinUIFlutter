import 'dart:convert';

/// Greska sa API-ja sa porukom koja se moze direktno prikazati korisniku.
class ApiException implements Exception {
  final String message;
  final int? statusCode;

  ApiException(this.message, {this.statusCode});

  /// Cita poruke iz odgovora oblika { "errors": { "Polje": ["poruka"] } }
  /// (ErrorFilter na API-ju i automatska validacija modela vracaju isti oblik).
  static String? parseMessage(String body) {
    if (body.isEmpty) return null;
    try {
      final data = jsonDecode(body);
      if (data is Map<String, dynamic>) {
        final errors = data['errors'];
        if (errors is Map) {
          final poruke = <String>[];
          for (final value in errors.values) {
            if (value is List) {
              poruke.addAll(value.map((e) => e.toString()));
            } else if (value != null) {
              poruke.add(value.toString());
            }
          }
          if (poruke.isNotEmpty) return poruke.join('\n');
        }
        final title = data['title'];
        if (title is String && title.isNotEmpty) return title;
      }
    } catch (_) {
      // odgovor nije JSON
    }
    return null;
  }

  @override
  String toString() => message;
}
