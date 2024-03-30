import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/models/Korisnici.dart';
import 'package:turisticka_agencija_mobile/providers/korisnici_provider.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';


class Profile extends StatefulWidget {
  static const String routeName = "/profile";
  const Profile({Key? key}) : super(key: key);

  @override
  State<Profile> createState() => _ProfileState();
}

class _ProfileState extends State<Profile> {
  KorisniciProvider? _korisniciProvider = null;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _korisniciProvider = context.read<KorisniciProvider>();
    print("called initState");
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Profil korisnika"),
      ),
      body: Column(
        children: [Expanded(child: bodyWidget())],
      ),
    );
  }

  Widget bodyWidget() {
    return FutureBuilder<List<Korisnici>>(
      future: getKorisnik(),
      builder: (BuildContext context, AsyncSnapshot<List<Korisnici>> snapshot) {
        if (snapshot.connectionState == (ConnectionState.waiting)) {
          return const Center(child: Text("Loading..."));
        } else if (snapshot.hasError) {
          return Center(
            child: Text('($snapshot.error)'),
          );
        } else {
          var data = snapshot.data;
          return Card(
            child: Padding(
              padding: const EdgeInsets.all(20),
              child: Text(
                  'Ime:${data?.first.ime}\nPrezime: ${data?.first.prezime}\nKorisnicko ime: ${data?.first.korisnickoIme}\nEmail: ${data?.first.email} \nTelefon:${data?.first.telefon}'),
            ),
          );
        }
      },
    );
  }

  Future<List<Korisnici>> getKorisnik() async {
    Map<String, String>? queryParams;

    String ime = "";
    String prezime = "";

    var korisnici = await _korisniciProvider?.get( null);
    var korisniciList = korisnici?.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (int j = 0; j < korisniciList!.length; j++) {
      Korisnici? user = korisniciList[j];
      if (user!.korisnickoIme
              .toString()
              .compareTo(Authorization.username.toString()) ==
          0) {
        ime = user.ime;
        prezime = user.prezime;
      }
    }
    print("Ime:" + ime.toString());

    if (ime.isNotEmpty && prezime.isNotEmpty) {
      queryParams = {'Ime': ime, 'Prezime': prezime};
    }

    var korisnik = await _korisniciProvider?.get(queryParams);
    return korisnik!.map((i) => _korisniciProvider!.fromJson(i)).toList();
  }
}
