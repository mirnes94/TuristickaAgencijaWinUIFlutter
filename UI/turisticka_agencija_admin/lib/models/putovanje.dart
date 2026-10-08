import 'json_utils.dart';
import '../utils/formatters.dart';

class Putovanje {
  final int id;
  final String nazivPutovanja;
  final String opisPutovanja;
  final double cijenaPutovanja;
  final DateTime datumPolaska;
  final DateTime datumDolaska;
  final int brojMjesta;
  final String? slika;
  final int gradId;
  final int prevozId;
  final int smjestajId;
  final String? gradNaziv;
  final String? prevozNaziv;
  final String? smjestajNaziv;
  final double prosjecnaOcjena;
  final int brojOcjena;
  final List<int> vodici;
  final List<String> vodiciImena;

  Putovanje({
    required this.id,
    required this.nazivPutovanja,
    required this.opisPutovanja,
    required this.cijenaPutovanja,
    required this.datumPolaska,
    required this.datumDolaska,
    required this.brojMjesta,
    this.slika,
    required this.gradId,
    required this.prevozId,
    required this.smjestajId,
    this.gradNaziv,
    this.prevozNaziv,
    this.smjestajNaziv,
    required this.prosjecnaOcjena,
    required this.brojOcjena,
    required this.vodici,
    required this.vodiciImena,
  });

  factory Putovanje.fromJson(Map<String, dynamic> json) => Putovanje(
        id: toInt(json['id']),
        nazivPutovanja: json['nazivPutovanja'] ?? '',
        opisPutovanja: json['opisPutovanja'] ?? '',
        cijenaPutovanja: toDouble(json['cijenaPutovanja']),
        datumPolaska: parseDate(json['datumPolaska']) ?? DateTime.now(),
        datumDolaska: parseDate(json['datumDolaska']) ?? DateTime.now(),
        brojMjesta: toInt(json['brojMjesta']),
        slika: json['slika'],
        gradId: toInt(json['gradId']),
        prevozId: toInt(json['prevozId']),
        smjestajId: toInt(json['smjestajId']),
        gradNaziv: json['gradNaziv'],
        prevozNaziv: json['prevozNaziv'],
        smjestajNaziv: json['smjestajNaziv'],
        prosjecnaOcjena: toDouble(json['prosjecnaOcjena']),
        brojOcjena: toInt(json['brojOcjena']),
        vodici: toIntList(json['vodici']),
        vodiciImena: toStringList(json['vodiciImena']),
      );
}
