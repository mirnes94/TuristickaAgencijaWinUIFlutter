import 'VodiciPutovanja.dart';

import 'package:json_annotation/json_annotation.dart';

part 'Recommender.g.dart';

@JsonSerializable()
class Recommender {
    int brojMjesta;
    int cijenaPutovanja;
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

    Recommender({required this.brojMjesta,required this.cijenaPutovanja,required this.datumDolaska, required this.datumPolaska,
        required this.gradId, required this.id, required this.nazivPutovanja,required this.opisPutovanja,
        required this.prevozId,required this.slika, required this.smjestajId, required this.vodiciPutovanja});

    factory Recommender.fromJson(Map<String, dynamic> json) => _$RecommenderFromJson(json);

    Map<String, dynamic> toJson() => _$RecommenderToJson(this);
}