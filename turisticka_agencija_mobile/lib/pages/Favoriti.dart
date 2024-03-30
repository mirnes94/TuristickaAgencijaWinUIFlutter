import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/providers/lista_zelja_provider.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';

import '../models/Korisnici.dart';
import '../models/ListaZelja.dart';
import '../models/Putovanja.dart';
import '../providers/korisnici_provider.dart';
import '../providers/putovanja_provider.dart';
import 'PutovanjaDetalji.dart';

class Favoriti extends StatefulWidget {
  static const String routeName = "/favoriti";
  const Favoriti({Key? key}) : super(key: key);

  @override
  State<Favoriti> createState() => _FavoritiState();
}

class _FavoritiState extends State<Favoriti> {
  final putovanjeKey = GlobalKey<_FavoritiState>();
  KorisniciProvider? _korisniciProvider = null;
  ListaZeljaProvider? _listaZeljaProvider = null;
  PutovanjaProvider? _putovanjaProvider = null;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _korisniciProvider = context.read<KorisniciProvider>();
    _listaZeljaProvider = context.read<ListaZeljaProvider>();
    _putovanjaProvider = context.read<PutovanjaProvider>();
    print("called initState");
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Favoriti"),
      ),
      body: Column(
        children: [Expanded(child: bodyWidget())],
      ),
    );
  }

  Widget bodyWidget() {
    return FutureBuilder<List<Putovanja?>>(
      future: getListaZelja(),
      builder:
          (BuildContext context, AsyncSnapshot<List<Putovanja?>> snapshot) {
        if (snapshot.connectionState == (ConnectionState.waiting)) {
          return const Center(child: Text("Loading..."));
        } else if (snapshot.hasError) {
          return Center(
            child: Text('($snapshot.error)'),
          );
        } else {
          return ListView(
            children: snapshot.data!.map((e) => FavoritiWidget(e!)).toList(),
          );
        }
      },
    );
  }

  Widget FavoritiWidget(Putovanja lz) {
    return Card(
        child: TextButton(
            onPressed: () {
              Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) =>
                          PutovanjaDetalji(key: putovanjeKey, putovanje: lz)));
            },
            child: Padding(
              padding: const EdgeInsets.all(20),
              child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    Column(children: [
                      Image(
                          width: 100,
                          height: 100,
                          image: MemoryImage(const Base64Decoder().convert(lz.slika))),
                      Text('${lz.nazivPutovanja} (${lz.cijenaPutovanja}KM)'),
                    ]),
                  ]),
            )));
  }

  Future<List<Putovanja?>> getListaZelja() async {
    Map<String, String>? queryParams;

    int? _korisnikId = 0;
    var korisnici = await _korisniciProvider?.get( null);
    var korisniciList = korisnici?.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList!) {
      if (user?.korisnickoIme == Authorization.username) {
        _korisnikId = user?.id;
      }
    }

    if (_korisnikId != 0) {
      queryParams = {'KorisnikId': _korisnikId.toString()};
    }
    var listaZelja = await _listaZeljaProvider?.get(queryParams);
    var listZeljaList =  listaZelja!.map((i) => _listaZeljaProvider?.fromJson(i)).toList();

    var putovanja = await _putovanjaProvider?.get(null);
    var putovanjaList = putovanja?.map((i) => _putovanjaProvider?.fromJson(i)).toList();

    List<Putovanja> listaPutovanja = [];

    for(ListaZelja? fav in listZeljaList){
      for(Putovanja? p in putovanjaList!){
        if(fav?.putovanjeId == p?.id){
          listaPutovanja.add(p!);
        }
      }
    }

    return listaPutovanja;
  }
}
