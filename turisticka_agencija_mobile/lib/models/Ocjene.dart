import 'package:json_annotation/json_annotation.dart';

part 'Ocjene.g.dart';

@JsonSerializable()
class Ocjene {
  String datum;
  int id;
  int korisnikId;
  int ocjena;

  Ocjene(
      {required this.datum,
      required this.id,
      required this.korisnikId,
      required this.ocjena});

  factory Ocjene.fromJson(Map<String, dynamic> json) => _$OcjeneFromJson(json);

  Map<String, dynamic> toJson() => _$OcjeneToJson(this);
}
