import 'package:json_annotation/json_annotation.dart';

part 'ListaZelja.g.dart';

@JsonSerializable()
class ListaZelja {
    int id;
    int korisnikId;
    String opis;
    int putovanjeId;

    ListaZelja({required this.id,required this.korisnikId,required this.opis,required this.putovanjeId});

    factory ListaZelja.fromJson(Map<String, dynamic> json) => _$ListaZeljaFromJson(json);

    Map<String, dynamic> toJson() => _$ListaZeljaToJson(this);
}