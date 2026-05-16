import 'package:json_annotation/json_annotation.dart';

part 'Firma.g.dart';

@JsonSerializable()
class Firma {
    String adresa;
    String brojZiroracuna;
    int gradId;
    int id;
    String naziv;

    Firma({required this.adresa, required this.brojZiroracuna, required this.gradId,
        required this.id, required this.naziv});

    factory Firma.fromJson(Map<String, dynamic> json) => _$FirmaFromJson(json);

    Map<String, dynamic> toJson() => _$FirmaToJson(this);
}