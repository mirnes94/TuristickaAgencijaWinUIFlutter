import 'package:json_annotation/json_annotation.dart';

part 'Rezervacija.g.dart';

@JsonSerializable()
class Rezervacija {
    int brojOsoba;
    String datumRezervacije;
    int id;
    String ime;
    int korisnikId;
    String napomena;
    int putovanjeId;
    String status;

    Rezervacija({required this.brojOsoba,required this.datumRezervacije,required this.id,required this.ime,
        required this.korisnikId,required this.napomena,required this.putovanjeId,required this.status});

    factory Rezervacija.fromJson(Map<String, dynamic> json) => _$RezervacijaFromJson(json);

    Map<String, dynamic> toJson() => _$RezervacijaToJson(this);
}