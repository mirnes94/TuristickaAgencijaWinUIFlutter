import 'package:json_annotation/json_annotation.dart';

part 'Uloge.g.dart';

@JsonSerializable()
class Uloge {
    int id;
    String naziv;
    String opis;

    Uloge({required this.id,required this.naziv,required this.opis});

    factory Uloge.fromJson(Map<String, dynamic> json) => _$UlogeFromJson(json);

    Map<String, dynamic> toJson() => _$UlogeToJson(this);
}