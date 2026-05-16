// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Prevoz.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Prevoz _$PrevozFromJson(Map<String, dynamic> json) => Prevoz(
      brojMjesta: json['brojMjesta'] as int,
      cijenaPoMjestu: json['cijenaPoMjestu'] as int,
      firmaId: json['firmaId'] as int,
      id: json['id'] as int,
      tipPrevoza: json['tipPrevoza'] as String,
    );

Map<String, dynamic> _$PrevozToJson(Prevoz instance) => <String, dynamic>{
      'brojMjesta': instance.brojMjesta,
      'cijenaPoMjestu': instance.cijenaPoMjestu,
      'firmaId': instance.firmaId,
      'id': instance.id,
      'tipPrevoza': instance.tipPrevoza,
    };
