// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Komentar.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Komentar _$KomentarFromJson(Map<String, dynamic> json) => Komentar(
      datum: json['datum'] as String,
      id: json['id'] as int?,
      korisnikId: json['korisnikId'] as int,
      putovanjeId: json['putovanjeId'] as int,
      sadrzaj: json['sadrzaj'] as String,
    );

Map<String, dynamic> _$KomentarToJson(Komentar instance) => <String, dynamic>{
      'datum': instance.datum,
      'id': instance.id,
      'korisnikId': instance.korisnikId,
      'putovanjeId': instance.putovanjeId,
      'sadrzaj': instance.sadrzaj,
    };
