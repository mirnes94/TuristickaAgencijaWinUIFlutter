// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Putovanja.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Putovanja _$PutovanjaFromJson(Map<String, dynamic> json) => Putovanja(
      brojMjesta: json['brojMjesta'] as int,
      cijenaPutovanja: (json['cijenaPutovanja'] as num).toDouble(),
      datumDolaska: json['datumDolaska'] as String,
      datumPolaska: json['datumPolaska'] as String,
      gradId: json['gradId'] as int,
      id: json['id'] as int,
      nazivPutovanja: json['nazivPutovanja'] as String,
      opisPutovanja: json['opisPutovanja'] as String,
      prevozId: json['prevozId'] as int,
      slika: json['slika'] as String,
      smjestajId: json['smjestajId'] as int,
      vodiciPutovanja: (json['vodiciPutovanja'] as List<dynamic>)
          .map((e) => VodiciPutovanja.fromJson(e as Map<String, dynamic>))
          .toList(),
    );

Map<String, dynamic> _$PutovanjaToJson(Putovanja instance) => <String, dynamic>{
      'brojMjesta': instance.brojMjesta,
      'cijenaPutovanja': instance.cijenaPutovanja,
      'datumDolaska': instance.datumDolaska,
      'datumPolaska': instance.datumPolaska,
      'gradId': instance.gradId,
      'id': instance.id,
      'nazivPutovanja': instance.nazivPutovanja,
      'opisPutovanja': instance.opisPutovanja,
      'prevozId': instance.prevozId,
      'slika': instance.slika,
      'smjestajId': instance.smjestajId,
      'vodiciPutovanja': instance.vodiciPutovanja,
    };
