import 'package:json_annotation/json_annotation.dart';

part 'Uloga.g.dart';

@JsonSerializable()
class Uloga {
    int id;
    String naziv;
    String opis;

    Uloga({required this.id,required this.naziv,required this.opis});

    factory Uloga.fromJson(Map<String, dynamic> json) => _$UlogaFromJson(json);

    Map<String, dynamic> toJson() => _$UlogaToJson(this);
}