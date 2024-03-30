import 'package:json_annotation/json_annotation.dart';

part 'Komentar.g.dart';

@JsonSerializable()
class Komentar {
  String datum;
  int? id;
  int korisnikId;
  int putovanjeId;
  String sadrzaj;

  Komentar(
      {required this.datum,
      this.id,
      required this.korisnikId,
      required this.putovanjeId,
      required this.sadrzaj});

  factory Komentar.fromJson(Map<String, dynamic> json) => _$KomentarFromJson(json);

  Map<String, dynamic> toJson() => _$KomentarToJson(this);
}
