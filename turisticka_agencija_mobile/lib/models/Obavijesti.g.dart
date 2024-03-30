// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Obavijesti.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Obavijesti _$ObavijestiFromJson(Map<String, dynamic> json) => Obavijesti(
      datum: json['datum'] as String,
      id: json['id'] as int,
      korisnikId: json['korisnikId'] as int,
      naziv: json['naziv'] as String,
      sadrzaj: json['sadrzaj'] as String,
    );

Map<String, dynamic> _$ObavijestiToJson(Obavijesti instance) =>
    <String, dynamic>{
      'datum': instance.datum,
      'id': instance.id,
      'korisnikId': instance.korisnikId,
      'naziv': instance.naziv,
      'sadrzaj': instance.sadrzaj,
    };
