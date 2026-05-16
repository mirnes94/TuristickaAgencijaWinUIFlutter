import 'KorisniciUloge.dart';
import 'package:json_annotation/json_annotation.dart';

part 'Korisnici.g.dart';

@JsonSerializable()
class Korisnici {
    String email;
    int id;
    String ime;
    List<KorisniciUloge>? korisniciUloge;
    String korisnickoIme;
    String lozinkaHash;
    String lozinkaSalt;
    String prezime;
    bool status;
    String telefon;

    Korisnici({required this.email, required this.id,required this.ime,required this.korisniciUloge,required this.korisnickoIme,required this.lozinkaHash,required this.lozinkaSalt,required this.prezime,required this.status,required this.telefon});

    factory Korisnici.fromJson(Map<String, dynamic> json) => _$KorisniciFromJson(json);

    Map<String, dynamic> toJson() => _$KorisniciToJson(this);
}