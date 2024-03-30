import 'package:json_annotation/json_annotation.dart';

part 'Vodic.g.dart';

@JsonSerializable()
class Vodic {
    int id;
    String ime;
    String jmbg;
    String kontakt;
    String prezime;
    String slika;

    Vodic({required this.id,required this.ime,required this.jmbg,required this.kontakt,
        required this.prezime,required this.slika});

    factory Vodic.fromJson(Map<String, dynamic> json) => _$VodicFromJson(json);

    Map<String, dynamic> toJson() => _$VodicToJson(this);
}