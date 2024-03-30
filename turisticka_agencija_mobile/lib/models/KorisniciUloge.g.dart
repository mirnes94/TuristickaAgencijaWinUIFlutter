// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'KorisniciUloge.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

KorisniciUloge _$KorisniciUlogeFromJson(Map<String, dynamic> json) =>
    KorisniciUloge(
      datumIzmjene: json['datumIzmjene'] as String,
      korisnik: json['korisnik'] as String?,
      korisnikId: json['korisnikId'] as int,
      korisnikUlogaId: json['korisnikUlogaId'] as int,
      uloga: Uloga.fromJson(json['uloga'] as Map<String, dynamic>),
      ulogaId: json['ulogaId'] as int,
    );

Map<String, dynamic> _$KorisniciUlogeToJson(KorisniciUloge instance) =>
    <String, dynamic>{
      'datumIzmjene': instance.datumIzmjene,
      'korisnik': instance.korisnik,
      'korisnikId': instance.korisnikId,
      'korisnikUlogaId': instance.korisnikUlogaId,
      'uloga': instance.uloga,
      'ulogaId': instance.ulogaId,
    };
