// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Uloge.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Uloge _$UlogeFromJson(Map<String, dynamic> json) => Uloge(
      id: json['id'] as int,
      naziv: json['naziv'] as String,
      opis: json['opis'] as String,
    );

Map<String, dynamic> _$UlogeToJson(Uloge instance) => <String, dynamic>{
      'id': instance.id,
      'naziv': instance.naziv,
      'opis': instance.opis,
    };
