import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/models/Uplate.dart';
import 'package:turisticka_agencija_mobile/providers/uplate_provider.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';

import '../models/Korisnici.dart';
import '../providers/korisnici_provider.dart';

class ListaUplata extends StatefulWidget {
  static const String routeName = "/listauplata";
  const ListaUplata({Key? key}) : super(key: key);

  @override
  State<ListaUplata> createState() => _ListaUplataState();
}

class _ListaUplataState extends State<ListaUplata> {
  KorisniciProvider? _korisniciProvider = null;
  UplateProvider? _uplateProvider = null;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _korisniciProvider = context.read<KorisniciProvider>();
    _uplateProvider = context.read<UplateProvider>();
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Lista uplata"),
      ),
      body: Column(
        children: [Expanded(child: bodyWidget())],
      ),
    );
  }

  Widget bodyWidget() {
    return FutureBuilder<List<Uplate?>?>(
      future: getUplate(),
      builder: (BuildContext context, AsyncSnapshot<List<Uplate?>?> snapshot) {
        if (snapshot.connectionState == (ConnectionState.waiting)) {
          return const Center(child: Text("Loading..."));
        } else if (snapshot.hasError) {
          return Center(
            child: Text('($snapshot.error)'),
          );
        } else {
          return ListView(
            children: snapshot.data!.map((e) => UplateWidget(e!)).toList(),
          );
        }
      },
    );
  }

  Widget UplateWidget(Uplate u) {
    return Card(
      child: Padding(
          padding: const EdgeInsets.all(20),
          child: Text('Datum uplate:${u.datum.substring(0,19)}\nIznos:${u.iznos}')),
    );
  }

  Future<List<Uplate?>> getUplate() async {
    Map<String, String>? queryParams;

    int? _korisnikId = 0;
    var korisnici = await _korisniciProvider?.get(null);
    var korisniciList = korisnici!.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList) {
      print(user?.korisnickoIme
          .toString()
          .compareTo(Authorization.username.toString()));
      if (user?.korisnickoIme
              .toString()
              .compareTo(Authorization.username.toString()) ==
          0) {
        _korisnikId = user?.id;
      }
    }

    queryParams = {'KorisnikId': _korisnikId.toString()};
    var listaUplata = await _uplateProvider?.get(queryParams);
    return listaUplata!.map((i) => _uplateProvider?.fromJson(i)).toList();
  }
}
