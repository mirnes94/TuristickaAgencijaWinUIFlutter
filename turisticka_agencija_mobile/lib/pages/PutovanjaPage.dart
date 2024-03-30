import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/models/Putovanja.dart';
import 'package:turisticka_agencija_mobile/pages/PutovanjaDetalji.dart';
import 'package:turisticka_agencija_mobile/providers/putovanja_provider.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';

import '../models/Gradovi.dart';
import '../providers/gradovi_provider.dart';

class PutovanjaPage extends StatefulWidget {
  static const String routeName = "/putovanjapage";
  const PutovanjaPage({Key? key}) : super(key: key);

  @override
  State<PutovanjaPage> createState() => _PutovanjaPageState();
}

class _PutovanjaPageState extends State<PutovanjaPage> {
  final putovanjeKey = GlobalKey<_PutovanjaPageState>();
  Gradovi? _selectedGrad = null;
  List<DropdownMenuItem> items = [];
  PutovanjaProvider? _putovanjaProvider = null;
  GradoviProvider? _gradoviProvider = null;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _putovanjaProvider = context.read<PutovanjaProvider>();
    _gradoviProvider = context.read<GradoviProvider>();
    print("called initState");
  }


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Putovanja"),
      ),
      body: Column(
        children: [
          Center(child: dropDownWidget()),
          Expanded(child: bodyWidget())
        ],
      ),
    );
  }

  Widget bodyWidget() {
    return FutureBuilder<List<Putovanja?>>(
      future: getPutovanja(_selectedGrad),
      builder: (BuildContext context, AsyncSnapshot<List<Putovanja?>> snapshot) {
        if (snapshot.connectionState == (ConnectionState.waiting)) {
          return const Center(child: Text("Loading..."));
        } else if (snapshot.hasError) {
          return Center(
            child: Text('($snapshot.error)'),
          );
        } else {
          print("Putovanja:"+snapshot.data.toString());
          return ListView(
            children: snapshot.data!.map((e) => PutovanjaWidget(e!)).toList(),
          );
        }
      },
    );
  }

  Widget dropDownWidget() {
    return FutureBuilder<List<Gradovi?>>(
      future: getGradovi(_selectedGrad),
      builder: (BuildContext context, AsyncSnapshot<List<Gradovi?>> snapshot) {
        if (snapshot.connectionState == (ConnectionState.waiting)) {
          return const Center(child: Text("Loading..."));
        } else if (snapshot.hasError) {
          return Center(
            child: Text('($snapshot.error)'),
          );
        } else {
          return Padding(
            padding: const EdgeInsets.fromLTRB(30, 10, 30, 10),
            child: Center(
              child: DropdownButton<dynamic>(
                hint: const Text('Odaberite grad'),
                isExpanded: true,
                items: items,
                onChanged: (newVal) {
                  setState(() {
                    _selectedGrad = newVal as Gradovi;
                    getPutovanja(_selectedGrad);
                  });
                },
                value: _selectedGrad,
              ),
            ),
          );
        }
      },
    );
  }

  Widget PutovanjaWidget(Putovanja p) {
    return Card(
        child: TextButton(
            onPressed: () {
              Navigator.push(
                  context,
                  MaterialPageRoute(
                      builder: (context) =>
                          PutovanjaDetalji(key: putovanjeKey, putovanje: p)));
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
                          image: MemoryImage(const Base64Decoder().convert(p.slika))),
                      Text('Naziv putovanja:${p.nazivPutovanja} \nCijena putovanja:${p.cijenaPutovanja}KM'),
                    ]),
                  ]),
            )));
  }

  Future<List<Gradovi?>> getGradovi(Gradovi? selectedGrad) async {
    var gradovi = await _gradoviProvider?.get(null);
    var gradoviList = gradovi!.map((i) => _gradoviProvider?.fromJson(i)).toList();

    items = gradoviList.map((item) {
      return DropdownMenuItem<Gradovi>(
        value: item,
        child: Text(item!.nazivGrada),
      );
    }).toList();

    if (selectedGrad != null && selectedGrad.id != 0) {
      _selectedGrad =
          gradoviList.where((element) => element?.id == selectedGrad.id).first;
    }
    return gradoviList;
  }

  Future<List<Putovanja?>> getPutovanja(Gradovi? selectedItem) async {
    Map<String, String>? queryParams = null;

    if (selectedItem != null && _selectedGrad != 0) {
      queryParams = {'GradId': selectedItem.id.toString()};
    }
    var putovanja = await _putovanjaProvider?.get(queryParams);
    return putovanja!.map((i) => _putovanjaProvider?.fromJson(i)).toList();
  }
}
