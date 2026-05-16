import 'Uloga.dart';
import 'package:json_annotation/json_annotation.dart';

part 'KorisniciUloge.g.dart';

@JsonSerializable()
class KorisniciUloge {
    String datumIzmjene;
    String? korisnik;
    int korisnikId;
    int korisnikUlogaId;
    Uloga uloga;
    int ulogaId;

    KorisniciUloge({required this.datumIzmjene,required this.korisnik,required this.korisnikId,required this.korisnikUlogaId,required this.uloga,required this.ulogaId});

    factory KorisniciUloge.fromJson(Map<String, dynamic> json) => _$KorisniciUlogeFromJson(json);

    Map<String, dynamic> toJson() => _$KorisniciUlogeToJson(this);
}