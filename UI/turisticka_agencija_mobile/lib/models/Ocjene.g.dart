// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Ocjene.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Ocjene _$OcjeneFromJson(Map<String, dynamic> json) => Ocjene(
      datum: json['datum'] as String,
      id: json['id'] as int,
      korisnikId: json['korisnikId'] as int,
      ocjena: json['ocjena'] as int,
    );

Map<String, dynamic> _$OcjeneToJson(Ocjene instance) => <String, dynamic>{
      'datum': instance.datum,
      'id': instance.id,
      'korisnikId': instance.korisnikId,
      'ocjena': instance.ocjena,
    };
