import 'package:json_annotation/json_annotation.dart';

part 'Smjestaj.g.dart';

@JsonSerializable()
class Smjestaj {
    int cijenaNocenja;
    int id;
    String nazivSmjestaja;
    String opisSmjestaja;
    String slika;
    String tip_sobe;

    Smjestaj({required this.cijenaNocenja,required this.id,required this.nazivSmjestaja,required this.opisSmjestaja,
        required this.slika, required this.tip_sobe});

    factory Smjestaj.fromJson(Map<String, dynamic> json) => _$SmjestajFromJson(json);

    Map<String, dynamic> toJson() => _$SmjestajToJson(this);
}