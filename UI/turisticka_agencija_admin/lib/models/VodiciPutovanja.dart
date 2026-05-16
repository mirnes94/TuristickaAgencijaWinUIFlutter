import 'Vodic.dart';

import 'package:json_annotation/json_annotation.dart';

part 'VodiciPutovanja.g.dart';

@JsonSerializable()
class VodiciPutovanja {
    int id;
    String putovanje;
    int putovanjeId;
    Vodic vodic;
    int vodicId;

    VodiciPutovanja({required this.id,required this.putovanje,required this.putovanjeId,
        required this.vodic, required this.vodicId});

    factory VodiciPutovanja.fromJson(Map<String, dynamic> json) => _$VodiciPutovanjaFromJson(json);

    Map<String, dynamic> toJson() => _$VodiciPutovanjaToJson(this);
}