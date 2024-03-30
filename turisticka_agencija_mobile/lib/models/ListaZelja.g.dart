// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'ListaZelja.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

ListaZelja _$ListaZeljaFromJson(Map<String, dynamic> json) => ListaZelja(
      id: json['id'] as int,
      korisnikId: json['korisnikId'] as int,
      opis: json['opis'] as String,
      putovanjeId: json['putovanjeId'] as int,
    );

Map<String, dynamic> _$ListaZeljaToJson(ListaZelja instance) =>
    <String, dynamic>{
      'id': instance.id,
      'korisnikId': instance.korisnikId,
      'opis': instance.opis,
      'putovanjeId': instance.putovanjeId,
    };
