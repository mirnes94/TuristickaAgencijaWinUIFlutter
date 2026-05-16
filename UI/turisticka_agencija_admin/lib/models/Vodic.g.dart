// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Vodic.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Vodic _$VodicFromJson(Map<String, dynamic> json) => Vodic(
      id: json['id'] as int,
      ime: json['ime'] as String,
      jmbg: json['jmbg'] as String,
      kontakt: json['kontakt'] as String,
      prezime: json['prezime'] as String,
      slika: json['slika'] as String,
    );

Map<String, dynamic> _$VodicToJson(Vodic instance) => <String, dynamic>{
      'id': instance.id,
      'ime': instance.ime,
      'jmbg': instance.jmbg,
      'kontakt': instance.kontakt,
      'prezime': instance.prezime,
      'slika': instance.slika,
    };
