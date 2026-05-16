import 'package:json_annotation/json_annotation.dart';

part 'Drzava.g.dart';

@JsonSerializable()
class Drzava {
    int id;
    String naziv;

    Drzava({required this.id, required this.naziv});

    factory Drzava.fromJson(Map<String, dynamic> json) => _$DrzavaFromJson(json);

    Map<String, dynamic> toJson() => _$DrzavaToJson(this);
}