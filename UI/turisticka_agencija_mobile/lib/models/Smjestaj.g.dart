// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Smjestaj.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Smjestaj _$SmjestajFromJson(Map<String, dynamic> json) => Smjestaj(
      cijenaNocenja: json['cijenaNocenja'] as int,
      id: json['id'] as int,
      nazivSmjestaja: json['nazivSmjestaja'] as String,
      opisSmjestaja: json['opisSmjestaja'] as String,
      slika: json['slika'] as String,
      tip_sobe: json['tip_sobe'] as String,
    );

Map<String, dynamic> _$SmjestajToJson(Smjestaj instance) => <String, dynamic>{
      'cijenaNocenja': instance.cijenaNocenja,
      'id': instance.id,
      'nazivSmjestaja': instance.nazivSmjestaja,
      'opisSmjestaja': instance.opisSmjestaja,
      'slika': instance.slika,
      'tip_sobe': instance.tip_sobe,
    };
