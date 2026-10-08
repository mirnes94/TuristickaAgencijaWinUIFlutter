import 'package:flutter/foundation.dart';

import 'api_client.dart';

/// Genericki CRUD provider za jedan API resurs (npr. api/Drzava).
abstract class BaseProvider<T> with ChangeNotifier {
  final String endpoint;

  BaseProvider(this.endpoint);

  T fromJson(Map<String, dynamic> json);

  Future<List<T>> get({Map<String, dynamic>? filter}) async {
    final data = await ApiClient.get(endpoint, filter);
    return (data as List).map((e) => fromJson(e as Map<String, dynamic>)).toList();
  }

  Future<T> getById(int id) async {
    final data = await ApiClient.get('$endpoint/$id');
    return fromJson(data as Map<String, dynamic>);
  }

  Future<T> insert(Map<String, dynamic> request) async {
    final data = await ApiClient.post(endpoint, request);
    notifyListeners();
    return fromJson(data as Map<String, dynamic>);
  }

  Future<T> update(int id, Map<String, dynamic> request) async {
    final data = await ApiClient.put('$endpoint/$id', request);
    notifyListeners();
    return fromJson(data as Map<String, dynamic>);
  }

  Future<void> delete(int id) async {
    await ApiClient.delete('$endpoint/$id');
    notifyListeners();
  }
}
