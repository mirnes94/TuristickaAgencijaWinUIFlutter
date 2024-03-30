// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'Rezervacija.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Rezervacija _$RezervacijaFromJson(Map<String, dynamic> json) => Rezervacija(
      brojOsoba: json['brojOsoba'] as int,
      datumRezervacije: json['datumRezervacije'] as String,
      id: json['id'] as int,
      ime: json['ime'] as String,
      korisnikId: json['korisnikId'] as int,
      napomena: json['napomena'] as String,
      putovanjeId: json['putovanjeId'] as int,
      status: json['status'] as String,
    );

Map<String, dynamic> _$RezervacijaToJson(Rezervacija instance) =>
    <String, dynamic>{
      'brojOsoba': instance.brojOsoba,
      'datumRezervacije': instance.datumRezervacije,
      'id': instance.id,
      'ime': instance.ime,
      'korisnikId': instance.korisnikId,
      'napomena': instance.napomena,
      'putovanjeId': instance.putovanjeId,
      'status': instance.status,
    };
