import 'package:json_annotation/json_annotation.dart';

part 'Prevoz.g.dart';

@JsonSerializable()
class Prevoz {
    int brojMjesta;
    int cijenaPoMjestu;
    int firmaId;
    int id;
    String tipPrevoza;

    Prevoz({required this.brojMjesta,required this.cijenaPoMjestu,required this.firmaId,required this.id,required this.tipPrevoza});

    factory Prevoz.fromJson(Map<String, dynamic> json) => _$PrevozFromJson(json);

    Map<String, dynamic> toJson() => _$PrevozToJson(this);
}