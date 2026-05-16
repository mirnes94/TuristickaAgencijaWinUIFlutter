// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Firma.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Firma _$FirmaFromJson(Map<String, dynamic> json) => Firma(
      adresa: json['adresa'] as String,
      brojZiroracuna: json['brojZiroracuna'] as String,
      gradId: json['gradId'] as int,
      id: json['id'] as int,
      naziv: json['naziv'] as String,
    );

Map<String, dynamic> _$FirmaToJson(Firma instance) => <String, dynamic>{
      'adresa': instance.adresa,
      'brojZiroracuna': instance.brojZiroracuna,
      'gradId': instance.gradId,
      'id': instance.id,
      'naziv': instance.naziv,
    };
