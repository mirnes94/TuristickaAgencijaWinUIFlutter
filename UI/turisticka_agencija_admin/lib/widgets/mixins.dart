import 'package:flutter/material.dart';

import '../utils/dialogs.dart';

/// Zajednicka logika ekrana sa listom: ucitavanje, otvaranje forme i brisanje uz potvrdu.
mixin ListaMixin<W extends StatefulWidget, T> on State<W> {
  List<T> lista = [];
  bool ucitavanje = true;
  String? greska;

  Future<List<T>> dohvati();

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) => ucitaj());
  }

  Future<void> ucitaj() async {
    setState(() {
      ucitavanje = true;
      greska = null;
    });
    try {
      final podaci = await dohvati();
      if (!mounted) return;
      setState(() => lista = podaci);
    } catch (e) {
      if (!mounted) return;
      setState(() => greska = e.toString());
    } finally {
      if (mounted) setState(() => ucitavanje = false);
    }
  }

  /// Otvara formu; ako je zapis spasen (forma vrati true), lista se automatski osvjezava.
  Future<void> otvoriFormu(Widget forma) async {
    final spaseno = await Navigator.of(context).push<bool>(MaterialPageRoute(builder: (_) => forma));
    if (spaseno == true) {
      await ucitaj();
    }
  }

  Future<void> obrisi({
    required String naslov,
    required String opis,
    required Future<void> Function() akcija,
    required String porukaUspjeha,
  }) async {
    final ok = await potvrdi(context, naslov: naslov, poruka: opis);
    if (!ok || !mounted) return;
    try {
      await akcija();
      if (!mounted) return;
      prikaziUspjeh(context, porukaUspjeha);
      await ucitaj();
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    }
  }
}

/// Zajednicka logika forme: validacija, spasavanje, poruka o uspjehu i zatvaranje.
mixin FormaMixin<W extends StatefulWidget> on State<W> {
  final formKey = GlobalKey<FormState>();
  bool spasavanje = false;

  Future<void> sacuvaj(Future<void> Function() akcija, String porukaUspjeha) async {
    if (!(formKey.currentState?.validate() ?? false)) return;
    setState(() => spasavanje = true);
    try {
      await akcija();
      if (!mounted) return;
      prikaziUspjeh(context, porukaUspjeha);
      Navigator.of(context).pop(true);
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    } finally {
      if (mounted) setState(() => spasavanje = false);
    }
  }
}
