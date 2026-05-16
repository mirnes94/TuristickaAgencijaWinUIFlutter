import 'VodiciPutovanja.dart';

import 'package:json_annotation/json_annotation.dart';

part 'Putovanja.g.dart';

@JsonSerializable()
class Putovanja {
    int brojMjesta;
    double cijenaPutovanja;
    String datumDolaska;
    String datumPolaska;
    int gradId;
    int id;
    String nazivPutovanja;
    String opisPutovanja;
    int prevozId;
    String slika;
    int smjestajId;
    List<VodiciPutovanja> vodiciPutovanja;

    Putovanja({required this.brojMjesta,required this.cijenaPutovanja,required this.datumDolaska,
        required this.datumPolaska,required this.gradId,required this.id, required this.nazivPutovanja,
        required this.opisPutovanja,required this.prevozId,required this.slika,required this.smjestajId,
        required this.vodiciPutovanja});

    factory Putovanja.fromJson(Map<String, dynamic> json) => _$PutovanjaFromJson(json);

    Map<String, dynamic> toJson() => _$PutovanjaToJson(this);
}