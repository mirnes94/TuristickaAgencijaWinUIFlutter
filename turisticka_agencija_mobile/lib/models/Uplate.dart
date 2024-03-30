import 'package:json_annotation/json_annotation.dart';

part 'Uplate.g.dart';

@JsonSerializable()
class Uplate {
  String datum;
  int id;
  double iznos;
  int rezervacijaId;
  int korisnikId;

  Uplate(
      {required this.datum,
      required this.id,
      required this.iznos,
      required this.rezervacijaId,
      required this.korisnikId});

  factory Uplate.fromJson(Map<String, dynamic> json) => _$UplateFromJson(json);

  Map<String, dynamic> toJson() => _$UplateToJson(this);
}
