import 'package:json_annotation/json_annotation.dart';

part 'Gradovi.g.dart';

@JsonSerializable()
class Gradovi {
    int drzavaId;
    int id;
    String nazivGrada;

    Gradovi({ required this.drzavaId, required this.id, required this.nazivGrada});

    factory Gradovi.fromJson(Map<String, dynamic> json) => _$GradoviFromJson(json);

    Map<String, dynamic> toJson() => _$GradoviToJson(this);
}