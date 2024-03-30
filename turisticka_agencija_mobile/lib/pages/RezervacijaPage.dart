import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/providers/korisnici_provider.dart';
import 'package:turisticka_agencija_mobile/providers/rezervacija_provider.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';

import '../models/Korisnici.dart';
import '../models/Rezervacija.dart';

class RezervacijaPage extends StatefulWidget {
  static const String routeName = "/rezervacijapage";
  const RezervacijaPage({Key? key}) : super(key: key);

  @override
  State<RezervacijaPage> createState() => _RezervacijaPageState();
}

class _RezervacijaPageState extends State<RezervacijaPage> {
 RezervacijaProvider? _rezervacijaProvider = null;
 KorisniciProvider? _korisniciProvider = null;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _rezervacijaProvider = context.read<RezervacijaProvider>();
    _korisniciProvider = context.read<KorisniciProvider>();
    print("called initState");
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Rezervacije"),
      ),
      body: Column(
        children: [Expanded(child: bodyWidget())],
      ),
    );
  }

  Widget bodyWidget() {
    return FutureBuilder<List<Rezervacija?>>(
        future: getRezervacije(),
        builder:
            (BuildContext context, AsyncSnapshot<List<Rezervacija?>> snapshot) {
          if (snapshot.connectionState == (ConnectionState.waiting)) {
            return const Center(child: Text("Loading..."));
          } else if (snapshot.hasError) {
            return Center(
              child: Text('($snapshot.error)'),
            );
          } else {
            var data = snapshot.data;
            return ListView.builder(
              itemCount: data?.length,
              itemBuilder: (context, index) {
                return Card(
                  child: ListTile(
                    title: Text(
                        'Ime:${data![index]?.ime} \nBroj osoba:${data[index]?.brojOsoba} \nDatum rezervacije:${data[index]?.datumRezervacije.substring(0,19)} \nStatus rezervacije:${data[index]?.status}'),
                    subtitle: Text("Otkazi rezervaciju"),
                    onTap: () {
                      deleteRezervacija(data[index]!.id);
                      Navigator.pop(context);
                    },
                  ),
                );
              },
            );
          }
        });
  }

  Widget RezervacijaWidget(Rezervacija r) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(20),
        child: Text(
            '${r.ime} (${r.brojOsoba}) (${r.datumRezervacije}) (${r.status})'),
      ),
    );
  }

  Future<List<Rezervacija?>> getRezervacije() async {
    Map<String, String>? queryParams;

    int _korisnikId = 0;
    var korisnici = await _korisniciProvider?.get(null);
    var korisniciList = korisnici!.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList) {
      if (user?.korisnickoIme
              .toString()
              .compareTo(Authorization.username.toString()) ==
          0) {
        _korisnikId = user!.id;
      }
    }

    if (_korisnikId != 0) {
      queryParams = {'KorisnikId': _korisnikId.toString()};
    }
    var rezervacije = await _rezervacijaProvider?.get(queryParams);
    return rezervacije!.map((i) => _rezervacijaProvider?.fromJson(i)).toList();
  }

  Future deleteRezervacija(int id) async {
    Map<String, String>? queryParams;

    var _korisnikId = 0;
    var korisnici = await _korisniciProvider?.get(null);
    var korisniciList = korisnici!.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList) {
      if (user?.korisnickoIme
              .toString()
              .compareTo(Authorization.username.toString()) ==
          0) {
        _korisnikId = user!.id;
      }
    }

    if (_korisnikId != 0) {
      queryParams = {'KorisnikId': _korisnikId.toString()};
    }
    var rezervacije = await _rezervacijaProvider?.get(queryParams);
    var rezervacijaList =
        rezervacije!.map((i) => _rezervacijaProvider?.fromJson(i)).toList();

    int rezervacijaId = 0;
    for (Rezervacija? rez in rezervacijaList) {
      if (rez?.id == id) {
        rezervacijaId = rez!.id;
      }
    }
    await _rezervacijaProvider?.delete(rezervacijaId.toString());
  }
}
