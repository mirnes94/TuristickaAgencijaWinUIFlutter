// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Korisnici.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Korisnici _$KorisniciFromJson(Map<String, dynamic> json) => Korisnici(
      email: json['email'] as String,
      id: json['id'] as int,
      ime: json['ime'] as String,
      korisniciUloge: (json['korisniciUloge'] as List<dynamic>?)
          ?.map((e) => KorisniciUloge.fromJson(e as Map<String, dynamic>))
          .toList(),
      korisnickoIme: json['korisnickoIme'] as String,
      lozinkaHash: json['lozinkaHash'] as String,
      lozinkaSalt: json['lozinkaSalt'] as String,
      prezime: json['prezime'] as String,
      status: json['status'] as bool,
      telefon: json['telefon'] as String,
    );

Map<String, dynamic> _$KorisniciToJson(Korisnici instance) => <String, dynamic>{
      'email': instance.email,
      'id': instance.id,
      'ime': instance.ime,
      'korisniciUloge': instance.korisniciUloge,
      'korisnickoIme': instance.korisnickoIme,
      'lozinkaHash': instance.lozinkaHash,
      'lozinkaSalt': instance.lozinkaSalt,
      'prezime': instance.prezime,
      'status': instance.status,
      'telefon': instance.telefon,
    };
