import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/models/Korisnici.dart';
import 'package:turisticka_agencija_mobile/models/Obavijesti.dart';
import 'package:turisticka_agencija_mobile/providers/obavijesti_provider.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';

import '../providers/korisnici_provider.dart';

class ObavijestiPage extends StatefulWidget {
  static const String routeName = "/obavijestipage";
  const ObavijestiPage({Key? key}) : super(key: key);

  @override
  State<ObavijestiPage> createState() => _ObavijestiPageState();
}

class _ObavijestiPageState extends State<ObavijestiPage> {
  KorisniciProvider? _korisniciProvider = null;
  ObavijestiProvider? _obavijestiProvider = null;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _korisniciProvider = context.read<KorisniciProvider>();
    _obavijestiProvider = context.read<ObavijestiProvider>();
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Obavijesti"),
      ),
      body: Column(
        children: [Expanded(child: bodyWidget())],
      ),
    );
  }

  Widget bodyWidget() {
    return FutureBuilder<List<Obavijesti?>>(
      future: getObavijesti(),
      builder:
          (BuildContext context, AsyncSnapshot<List<Obavijesti?>> snapshot) {
        if (snapshot.connectionState == (ConnectionState.waiting)) {
          return const Center(child: Text("Loading..."));
        } else if (snapshot.hasError) {
          return Center(
            child: Text('($snapshot.error)'),
          );
        } else {
          return ListView(
            children: snapshot.data!.map((e) => ObavijestiWidget(e!)).toList(),
          );
        }
      },
    );
  }

  Widget ObavijestiWidget(Obavijesti o) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(20),
        child: Text(
            'Naziv:${o.naziv}\nSadrzaj: ${o.sadrzaj}\nDatum: ${o.datum.substring(0,19)}'),
      ),
    );
  }

  Future<List<Obavijesti?>> getObavijesti() async {
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
    var obavijesti = await _obavijestiProvider?.get(queryParams);
    return obavijesti!.map((i) => _obavijestiProvider?.fromJson(i)).toList();
  }
}
