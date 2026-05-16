// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Uplate.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Uplate _$UplateFromJson(Map<String, dynamic> json) => Uplate(
      datum: json['datum'] as String,
      id: json['id'] as int,
      iznos: (json['iznos'] as num).toDouble(),
      rezervacijaId: json['rezervacijaId'] as int,
      korisnikId: json['korisnikId'] as int,
    );

Map<String, dynamic> _$UplateToJson(Uplate instance) => <String, dynamic>{
      'datum': instance.datum,
      'id': instance.id,
      'iznos': instance.iznos,
      'rezervacijaId': instance.rezervacijaId,
      'korisnikId': instance.korisnikId,
    };
