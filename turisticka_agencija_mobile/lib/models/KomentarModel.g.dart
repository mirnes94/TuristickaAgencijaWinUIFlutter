// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'KomentarModel.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

KomentarModel _$KomentarModelFromJson(Map<String, dynamic> json) =>
    KomentarModel(
      datum: json['datum'] as String,
      korisnikId: json['korisnikId'] as int,
      putovanjeId: json['putovanjeId'] as int,
      sadrzaj: json['sadrzaj'] as String,
    );

Map<String, dynamic> _$KomentarModelToJson(KomentarModel instance) =>
    <String, dynamic>{
      'datum': instance.datum,
      'korisnikId': instance.korisnikId,
      'putovanjeId': instance.putovanjeId,
      'sadrzaj': instance.sadrzaj,
    };
