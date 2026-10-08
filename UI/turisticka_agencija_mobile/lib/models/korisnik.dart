import 'json_utils.dart';

class Korisnik {
  final int id;
  final String ime;
  final String prezime;
  final String email;
  final String telefon;
  final String korisnickoIme;
  final bool status;
  final List<String> uloge;
  final List<int> ulogeIds;

  Korisnik({
    required this.id,
    required this.ime,
    required this.prezime,
    required this.email,
    required this.telefon,
    required this.korisnickoIme,
    required this.status,
    required this.uloge,
    required this.ulogeIds,
  });

  String get imePrezime => '$ime $prezime';

  factory Korisnik.fromJson(Map<String, dynamic> json) => Korisnik(
        id: toInt(json['id']),
        ime: json['ime'] ?? '',
        prezime: json['prezime'] ?? '',
        email: json['email'] ?? '',
        telefon: json['telefon'] ?? '',
        korisnickoIme: json['korisnickoIme'] ?? '',
        status: json['status'] == true,
        uloge: toStringList(json['uloge']),
        ulogeIds: ((json['korisniciUloge'] as List?) ?? const [])
            .map((e) => toInt((e as Map<String, dynamic>)['ulogaId']))
            .toList(),
      );
}
