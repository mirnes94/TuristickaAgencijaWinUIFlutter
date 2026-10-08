import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/interakcije.dart';
import '../providers/providers.dart';
import '../utils/dialogs.dart';
import '../widgets/common.dart';
import 'putovanje_detalji_screen.dart';

/// Lista zelja (oznacena putovanja) prijavljenog klijenta.
class FavoritiScreen extends StatefulWidget {
  const FavoritiScreen({super.key});

  @override
  State<FavoritiScreen> createState() => _FavoritiScreenState();
}

class _FavoritiScreenState extends State<FavoritiScreen> {
  List<ListaZelja> _lista = [];
  bool _ucitavanje = true;
  String? _greska;

  @override
  void initState() {
    super.initState();
    _ucitaj();
  }

  Future<void> _ucitaj() async {
    setState(() {
      _ucitavanje = true;
      _greska = null;
    });
    try {
      final lista = await context.read<ListaZeljaProvider>().get();
      if (mounted) setState(() => _lista = lista);
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    } finally {
      if (mounted) setState(() => _ucitavanje = false);
    }
  }

  Future<void> _ukloni(ListaZelja z) async {
    final ok = await potvrdi(context,
        naslov: 'Lista želja',
        poruka: 'Ukloniti "${z.putovanjeNaziv ?? ''}" sa liste želja?',
        potvrdaTekst: 'Ukloni');
    if (!ok || !mounted) return;
    try {
      await context.read<ListaZeljaProvider>().delete(z.id);
      if (!mounted) return;
      prikaziUspjeh(context, 'Putovanje je uklonjeno sa liste želja.');
      await _ucitaj();
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    }
  }

  @override
  Widget build(BuildContext context) {
    return StanjeListe(
      ucitavanje: _ucitavanje,
      greska: _greska,
      prazno: _lista.isEmpty,
      porukaPrazno: 'Lista želja je prazna.\nDodajte putovanje klikom na ♡ na detaljima putovanja.',
      onPonovo: _ucitaj,
      child: RefreshIndicator(
        onRefresh: _ucitaj,
        child: ListView.builder(
          padding: const EdgeInsets.all(12),
          itemCount: _lista.length,
          itemBuilder: (_, i) {
            final z = _lista[i];
            return Card(
              child: ListTile(
                leading: const Icon(Icons.favorite, color: Colors.red),
                title: Text(z.putovanjeNaziv ?? 'Putovanje'),
                subtitle: (z.opis ?? '').isEmpty ? null : Text(z.opis!),
                trailing: IconButton(
                  tooltip: 'Ukloni',
                  icon: const Icon(Icons.delete_outline),
                  onPressed: () => _ukloni(z),
                ),
                onTap: () async {
                  await Navigator.of(context).push(
                    MaterialPageRoute(builder: (_) => PutovanjeDetaljiScreen(putovanjeId: z.putovanjeId)),
                  );
                  _ucitaj();
                },
              ),
            );
          },
        ),
      ),
    );
  }
}
