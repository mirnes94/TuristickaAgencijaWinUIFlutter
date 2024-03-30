import 'dart:convert';
import 'package:provider/provider.dart';
import 'package:toggle_switch/toggle_switch.dart';

import 'package:flutter/material.dart';
import 'package:turisticka_agencija_mobile/models/Komentar.dart';
import 'package:turisticka_agencija_mobile/models/Putovanja.dart';

import 'package:flutter_rating_bar/flutter_rating_bar.dart';
import 'package:turisticka_agencija_mobile/providers/komentar_provider.dart';
import 'package:turisticka_agencija_mobile/providers/korisnici_provider.dart';
import 'package:turisticka_agencija_mobile/providers/ocjene_provider.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';

import '../models/Korisnici.dart';
import '../providers/lista_zelja_provider.dart';
import '../providers/putovanja_provider.dart';
import '../providers/recommender_provider.dart';
import 'RezervacijaDetaljiPage.dart';

class PutovanjaDetalji extends StatelessWidget {
  TextEditingController komentarController = new TextEditingController();
  final Putovanja putovanje;
  bool _value = false;
  int _ratingValue = 0;
  bool rated = false;
  final putovanjeKey = GlobalKey();
  List<Color> listColor = <Color>[
    Colors.green[500]!,
    Colors.green[700]!,
  ];
  PutovanjaProvider? _putovanjaProvider = null;
  KomentarProvider? _komentarProvider = null;
  KorisniciProvider? _korisniciProvider = null;
  RecommenderProvider? _recommenderProvider = null;
  OcjeneProvider? _ocjeneProvider = null;
  ListaZeljaProvider? _listaZeljaProvider = null;

  PutovanjaDetalji({required Key key, required this.putovanje})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    _putovanjaProvider = context.read<PutovanjaProvider>();
    _komentarProvider = context.read<KomentarProvider>();
    _korisniciProvider = context.read<KorisniciProvider>();
    _recommenderProvider = context.read<RecommenderProvider>();
    _ocjeneProvider = context.read<OcjeneProvider>();
    _listaZeljaProvider = context.read<ListaZeljaProvider>();

    return Scaffold(
        appBar: AppBar(
          title: Text('Detalji putovanja'),
        ),
        body: SingleChildScrollView(
            child: Padding(
          padding: EdgeInsets.all(60),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Column(
                children: [
                  Image(
                      width: 100,
                      height: 100,
                      image: MemoryImage(
                          Base64Decoder().convert(putovanje.slika))),
                  SizedBox(
                    width: 15,
                  ),
                  Text(
                    "Naziv:" + putovanje.nazivPutovanja,
                    style: TextStyle(fontSize: 20),
                  ),
                  Text(
                    "Opis:" + putovanje.opisPutovanja,
                    style: TextStyle(fontSize: 20),
                  ),
                  Text(
                    "Datum polaska:" +
                        putovanje.datumPolaska
                            .substring(0, putovanje.datumPolaska.indexOf("T")),
                    style: TextStyle(fontSize: 20),
                  ),
                  Text(
                    "Datum dolaska:" +
                        putovanje.datumDolaska
                            .substring(0, putovanje.datumDolaska.indexOf("T")),
                    style: TextStyle(fontSize: 20),
                  ),
                  Text(
                    "Broj  mjesta:" + putovanje.brojMjesta.toString(),
                    style: TextStyle(fontSize: 20),
                  ),
                  Text(
                    "Cijena:" + putovanje.cijenaPutovanja.toString(),
                    style: TextStyle(fontSize: 20),
                  )
                ],
              ),
              SizedBox(
                height: 20,
              ),
              Text(
                "Dodaj u favorite",
                style: TextStyle(fontSize: 20),
              ),
              SizedBox(
                height: 20,
              ),
              ToggleSwitch(
                minWidth: 90.0,
                initialLabelIndex: 2,
                activeBgColor: listColor,
                inactiveBgColor: Colors.white,
                labels: ['Da', 'Ne'],
                onToggle: (index) {
                  if (index == 0) {
                    addToListaZelja();
                  }
                  print('switched to: $index');
                },
              ),
              SizedBox(
                height: 20,
              ),
              Container(
                height: 50,
                width: 300,
                decoration: BoxDecoration(
                    color: Colors.amber[500],
                    borderRadius: BorderRadius.circular(30)),
                child: TextButton(
                    onPressed: () {
                      Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => RezervacijaDetaljiPage(
                                  key: putovanjeKey, putovanje: putovanje)));
                    },
                    child: const Text(
                      "Idi na rezervaciju",
                      style: TextStyle(color: Colors.white, fontSize: 20),
                    )),
              ),
              SizedBox(
                height: 20,
              ),
              Text(
                "Ocijeni putovanje",
                style: TextStyle(fontSize: 20),
              ),
              SizedBox(
                height: 20,
              ),
              RatingBar(
                  initialRating: _ratingValue.toDouble(),
                  direction: Axis.horizontal,
                  allowHalfRating: true,
                  itemCount: 5,
                  ratingWidget: RatingWidget(
                      full: const Icon(Icons.star, color: Colors.orange),
                      half: const Icon(
                        Icons.star,
                        color: Colors.orange,
                      ),
                      empty: const Icon(
                        Icons.star_outline,
                        color: Colors.orange,
                      )),
                  onRatingUpdate: (value) {
                    _ratingValue = value.toInt();
                    addOcjena(_ratingValue);
                  }),
              SizedBox(
                height: 20,
              ),
              Text(
                "Komentari",
                style: TextStyle(fontSize: 20),
              ),
              SingleChildScrollView(
                child: FutureBuilder<List<Komentar?>>(
                  future: getKomentari(),
                  builder: (BuildContext context,
                      AsyncSnapshot<List<Komentar?>> snapshot) {
                    if (snapshot.connectionState == (ConnectionState.waiting)) {
                      return const Center(child: Text("Loading..."));
                    } else if (snapshot.hasError) {
                      return Center(
                        child: Text('($snapshot.error)'),
                      );
                    } else {
                      var data = snapshot.data;
                      return ListView.builder(
                          physics: NeverScrollableScrollPhysics(),
                          shrinkWrap: true,
                          itemBuilder: (BuildContext context, int index) {
                            print("index:" + index.toString());
                            return KomentariWidget(data![index]!);
                          },
                          itemCount: data!.length);
                    }
                  },
                ),
              ),
              SizedBox(
                height: 30,
              ),
              TextField(
                controller: komentarController,
                decoration: InputDecoration(
                    border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(30)),
                    hintText: "Unesite komentar"),
              ),
              SizedBox(
                height: 20,
              ),
              Container(
                height: 50,
                width: 300,
                decoration: BoxDecoration(
                    color: Colors.amber[500],
                    borderRadius: BorderRadius.circular(30)),
                child: TextButton(
                    onPressed: () async {
                      await addKomentar(komentarController.text);
                      print("text:" + komentarController.text);
                      komentarController.text = "";
                    },
                    child: const Text(
                      "Dodaj komentar",
                      style: TextStyle(color: Colors.white, fontSize: 20),
                    )),
              ),
              SizedBox(
                height: 30,
              ),
              Text(
                "Recommended putovanja",
                style: TextStyle(fontSize: 20),
              ),
              SingleChildScrollView(
                child: FutureBuilder<List<Putovanja?>>(
                  future: getRecomendedPutovanja(),
                  builder: (BuildContext context,
                      AsyncSnapshot<List<Putovanja?>> snapshot) {
                    if (snapshot.connectionState == (ConnectionState.waiting)) {
                      return const Center(child: Text("Loading..."));
                    } else if (snapshot.hasError) {
                      return Center(
                        child: Text('($snapshot.error)'),
                      );
                    } else {
                      var data = snapshot.data;
                      return ListView.builder(
                          physics: NeverScrollableScrollPhysics(),
                          shrinkWrap: true,
                          itemBuilder: (BuildContext context, int index) {
                            print("index:" + index.toString());
                            return PutovanjaWidget(data![index]!);
                          },
                          itemCount: data!.length);
                    }
                  },
                ),
              ),
              SizedBox(
                height: 30,
              ),
            ],
          ),
        )));
  }

  Future<List<Komentar?>> getKomentari() async {
    Map<String, String>? queryParams = null;

    queryParams = {'PutovanjeId': putovanje.id.toString()};
    var komentari = await _komentarProvider?.get(queryParams);
    return komentari!.map((i) => _komentarProvider?.fromJson(i)).toList();
  }

  Future<List<Komentar?>> getFutureKomentari() async {
    Map<String, String>? queryParams = null;

    queryParams = {'PutovanjeId': putovanje.id.toString()};
    var komentari = await _komentarProvider?.get(queryParams);
    return komentari!.map((i) => _komentarProvider?.fromJson(i)).toList();
  }

  Widget KomentariWidget(Komentar e) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(20),
        child: Text(
            '${e.datum.substring(0, e.datum.indexOf("."))} (${e.sadrzaj})'),
      ),
    );
  }

  Widget PutovanjaWidget(Putovanja p) {
    return Card(
      /*
        child: TextButton(
      onPressed: () {
        Navigator.push(
            BuildContext context,
            MaterialPageRoute(
                builder: (context) =>
                    PutovanjaDetalji(key: putovanjeKey, putovanje: p)));
      },*/
      child: Padding(
        padding: const EdgeInsets.all(20),
        child: Text('Naziv putovanja:${p.nazivPutovanja} \nCijena putovanja:${p.cijenaPutovanja}KM'),
      ),
    );
  }

  Future<List<Putovanja?>> getRecomendedPutovanja() async {
    Map<String, String>? queryParams = null;

    queryParams = {'putovanjeId': putovanje.id.toString()};
    var putovanja = await _recommenderProvider?.getById(putovanje.id,null);
    if (putovanja != null) {
      return putovanja.map((i) => _putovanjaProvider?.fromJson(i)).toList();
    } else {
      queryParams = {'SmjestajId': putovanje.smjestajId.toString()};
      var putovanjaTemp = await _putovanjaProvider?.get(queryParams);
      return putovanjaTemp!.map((i) => _putovanjaProvider?.fromJson(i)).toList();
    }
  }

  Future<Komentar?> addKomentar(String sadrzaj) async {
    int id = 0;

    var korisnici = await _korisniciProvider?.get(null);
    var korisniciList = korisnici!.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList) {
      if (user!.korisnickoIme
              .toString()
              .compareTo(Authorization.username.toString()) ==
          0) {
        id = user.id;
      }
    }

    Map<String, dynamic> body = {
      "datum": DateTime.now().toString(),
      "korisnikId": id,
      "putovanjeId": putovanje.id,
      "sadrzaj": sadrzaj
    };

    return await _komentarProvider?.insert(body);
  }

  Future addOcjena(int ocjena) async {
    int korisnikId = 0;

    var korisnici = await _korisniciProvider?.get(null);
    var korisniciList = korisnici!.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList) {
      if (user!.korisnickoIme
              .toString()
              .compareTo(Authorization.username.toString()) ==
          0) {
        korisnikId = user.id;
      }
    }

    Map<String, dynamic> body = {
      "datum": DateTime.now().toString(),
      "korisnikId": korisnikId,
      "putovanjeId": putovanje.id,
      "ocjena": ocjena
    };

    return await _ocjeneProvider?.insert(body);
  }

  Future addToListaZelja() async {
    int korisnikId = 0;

    var korisnici = await _korisniciProvider?.get(null);
    var korisniciList = korisnici!.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList) {
      if (user!.korisnickoIme
              .toString()
              .compareTo(Authorization.username.toString()) ==
          0) {
        korisnikId = user.id;
      }
    }

    Map<String, dynamic> body = {
      "korisnikId": korisnikId,
      "putovanjeId": putovanje.id,
      "opis": putovanje.opisPutovanja
    };

    return await _listaZeljaProvider?.insert(body);
  }
}
