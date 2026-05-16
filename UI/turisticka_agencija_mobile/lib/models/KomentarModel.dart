import 'package:json_annotation/json_annotation.dart';

part 'KomentarModel.g.dart';

@JsonSerializable()
class KomentarModel {
  String datum;
  int korisnikId;
  int putovanjeId;
  String sadrzaj;

  KomentarModel(
      {required this.datum,
      required this.korisnikId,
      required this.putovanjeId,
      required this.sadrzaj});

  factory KomentarModel.fromJson(Map<String, dynamic> json) => _$KomentarModelFromJson(json);

  Map<String, dynamic> toJson() => _$KomentarModelToJson(this);
}
