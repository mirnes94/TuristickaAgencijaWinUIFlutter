import 'package:json_annotation/json_annotation.dart';

part 'Obavijesti.g.dart';

@JsonSerializable()
class Obavijesti {
    String datum;
    int id;
    int korisnikId;
    String naziv;
    String sadrzaj;

    Obavijesti({required this.datum,required this.id,required this.korisnikId,required this.naziv,required this.sadrzaj});

    factory Obavijesti.fromJson(Map<String, dynamic> json) => _$ObavijestiFromJson(json);

    Map<String, dynamic> toJson() => _$ObavijestiToJson(this);
/*
    factory Obavijesti.fromJson(Map<String, dynamic> json) {
        return Obavijesti(
            datum: json['datum'], 
            id: json['id'], 
            korisnikId: json['korisnikId'], 
            naziv: json['naziv'], 
            sadrzaj: json['sadrzaj'], 
        );
    }

    Map<String, dynamic> toJson() {
        final Map<String, dynamic> data = new Map<String, dynamic>();
        data['datum'] = this.datum;
        data['id'] = this.id;
        data['korisnikId'] = this.korisnikId;
        data['naziv'] = this.naziv;
        data['sadrzaj'] = this.sadrzaj;
        return data;
    }

 */
}