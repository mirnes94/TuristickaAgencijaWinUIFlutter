// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'VodiciPutovanja.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

VodiciPutovanja _$VodiciPutovanjaFromJson(Map<String, dynamic> json) =>
    VodiciPutovanja(
      id: json['id'] as int,
      putovanje: json['putovanje'] as String,
      putovanjeId: json['putovanjeId'] as int,
      vodic: Vodic.fromJson(json['vodic'] as Map<String, dynamic>),
      vodicId: json['vodicId'] as int,
    );

Map<String, dynamic> _$VodiciPutovanjaToJson(VodiciPutovanja instance) =>
    <String, dynamic>{
      'id': instance.id,
      'putovanje': instance.putovanje,
      'putovanjeId': instance.putovanjeId,
      'vodic': instance.vodic,
      'vodicId': instance.vodicId,
    };
