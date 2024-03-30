

import 'dart:convert';
import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:http/http.dart';
import 'package:http/io_client.dart';

import '../utils/util.dart';

abstract class BaseProvider<T> with ChangeNotifier {
  static String? _baseUrl;
  String? _endpoint;

  HttpClient client = HttpClient();
  IOClient? http;

  BaseProvider(String endpoint) {
    _baseUrl = const String.fromEnvironment("baseUrl", defaultValue: "http://10.0.2.2:5000/");
    print("baseurl: $_baseUrl");

    if (_baseUrl!.endsWith("/") == false) {
      _baseUrl = _baseUrl! + "/";
    }

    _endpoint = endpoint;
    print("endpoint: $_endpoint");
    client.badCertificateCallback = (cert, host, port) => true;
    http = IOClient(client);
  }

  Future<int> authenticate(String data) async {
    var url = Uri.parse("$_baseUrl$_endpoint/$data");

    Map<String, String> headers = createHeaders();
    var response;
    try {
      print("Before HTTP request");
      response = await http!.get(url, headers: headers);
      print("After HTTP request");
    } catch (e, stackTrace) {
      print("Timeout error: $e");
      print("Stack trace: $stackTrace");
      print("URL: $url");
      return 522;
    }

    if (isValidResponseCode(response)) {
      return response.statusCode;
      //return data.map((x) => fromJson(x)).cast<T>().toList();
    } else {
      return 0;
    }
  }

  Future<List> getById(int id, [dynamic additionalData]) async {
    var url = Uri.parse("$_baseUrl$_endpoint/$id");

    Map<String, String> headers = createHeaders();

    var response = await http!.get(url, headers: headers);

    print(url);

    if (isValidResponseCode(response)) {
      var data = jsonDecode(response.body);
      return const JsonDecoder().convert(response.body) as List;
      //return data.map((x) => fromJson(x)).cast<T>().toList();
    } else {
      throw Exception("Exception... handle this gracefully");
    }
  }

  Future<List> get([dynamic search]) async {
    var url = "$_baseUrl$_endpoint";
    if (search != null ) {
      String queryString = getQueryString(search);
      url = url + "?" + queryString;
    }

    var uri = Uri.parse(url);

    Map<String, String> headers = createHeaders();

    var response = await http!.get(uri, headers: headers);
    if (isValidResponseCode(response)) {
      var data = jsonDecode(response.body);
      print("Data"+ data.toString());
      return const JsonDecoder().convert(response.body) as List;
    } else {
      throw Exception("Exception... handle this gracefully");
    }
  }

  Future<T?> insert(dynamic request) async {
    var url = "$_baseUrl$_endpoint";
    var uri = Uri.parse(url);

    Map<String, String> headers = createHeaders();
    var jsonRequest = jsonEncode(request);
    var response =
    await http!.post(uri, headers: headers, body: jsonRequest);

    print(uri);
    if (isValidResponseCode(response)) {
      var data = jsonDecode(response.body);
      return fromJson(data) as T;
    } else {
      return null;
    }
  }

  Future<T?> update(int id, [dynamic request]) async {
    var url = "$_baseUrl$_endpoint/$id";
    var uri = Uri.parse(url);

    Map<String, String> headers = createHeaders();

    var response =
    await http!.put(uri, headers: headers, body: jsonEncode(request));

    if (isValidResponseCode(response)) {
      var data = jsonDecode(response.body);
      return fromJson(data) as T;
    } else {
      return null;
    }
  }

  Future<dynamic?> delete(String id) async {

    var url = "$_baseUrl$_endpoint/$id";
    var uri = Uri.parse(url);

    Map<String, String> headers = createHeaders();

    var response =
    await http!.delete(uri, headers: headers);

    print("REsponse statuus code"+ response.statusCode.toString());
    if (isValidResponseCode(response)) {
      var data = jsonDecode(response.body);
      return json.decode(response.body);
    } else {
      return null;
    }
  }

  Map<String, String> createHeaders() {
    String? username = Authorization.username;
    String? password = Authorization.password;

    String basicAuth =
        "Basic ${base64Encode(utf8.encode('$username:$password'))}";

    print(username);
    var headers = {
      "Content-Type": "application/json",
       HttpHeaders.authorizationHeader: basicAuth
      //"Authorization": basicAuth
    };
    print(headers);
    return headers;
  }

  T fromJson(data) {
    throw Exception("Override method");
  }

  String getQueryString(Map params,
      {String prefix: '&', bool inRecursion: false}) {
    String query = '';
    params.forEach((key, value) {
      if (inRecursion) {
        if (key is int) {
          key = '[$key]';
        } else if (value is List || value is Map) {
          key = '.$key';
        } else {
          key = '.$key';
        }
      }
      if (value is String || value is int || value is double || value is bool) {
        var encoded = value;
        if (value is String) {
          encoded = Uri.encodeComponent(value);
        }
        query += '$prefix$key=$encoded';
      } else if (value is DateTime) {
        query += '$prefix$key=${(value as DateTime).toIso8601String()}';
      } else if (value is List || value is Map) {
        if (value is List) value = value.asMap();
        value.forEach((k, v) {
          query +=
              getQueryString({k: v}, prefix: '$prefix$key', inRecursion: true);
        });
      }
    });
    return query;
  }

  bool isValidResponseCode(Response response) {
    if (response.statusCode == 200) {
      if (response.body.isNotEmpty) {
        return true;
      } else {
        return false;
      }
    } else if (response.statusCode == 204) {
      return true;
    } else if(response.statusCode == 201){
      return true;
    }
    else if (response.statusCode == 400) {
      return true;
    } else if (response.statusCode == 401) {
      return true;
    } else if (response.statusCode == 403) {
      return true;
    } else if (response.statusCode == 404) {
      return true;
    } else if (response.statusCode == 500) {
      return true;}
    else if (response.statusCode == 522) {
      return true;
    } else {
      return true;
    }
  }
}