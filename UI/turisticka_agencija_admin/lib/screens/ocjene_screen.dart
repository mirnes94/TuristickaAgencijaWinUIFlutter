import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/interakcije.dart';
import '../models/putovanje.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../widgets/common.dart';
import '../widgets/mixins.dart';

/// Pregled ocjena putovanja (ulazni podaci za sistem preporuke).
class OcjeneScreen extends StatefulWidget {
  const OcjeneScreen({super.key});

  @override
  State<OcjeneScreen> createState() => _OcjeneScreenState();
}

class _OcjeneScreenState extends State<OcjeneScreen> with ListaMixin<OcjeneScreen, Ocjena> {
  List<Putovanje> _putovanja = [];
  int? _putovanjeId;
  int? _ocjena;

  @override
  void initState() {
    super.initState();
    context.read<PutovanjeProvider>().get().then((v) {
      if (mounted) setState(() => _putovanja = v);
    }).catchError((_) {});
  }

  @override
  Future<List<Ocjena>> dohvati() =>
      context.read<OcjenaProvider>().get(filter: {'putovanjeId': _putovanjeId, 'ocjena': _ocjena});

  @override
  Widget build(BuildContext context) {
    final prosjek = lista.isEmpty ? 0 : lista.map((o) => o.ocjena).reduce((a, b) => a + b) / lista.length;
    return MasterScreen(
      title: 'Ocjene',
      child: Column(children: [
        PretragaTraka(
          filteri: [
            FilterDropdown<int>(
              label: 'Putovanje',
              value: _putovanjeId,
              width: 280,
              items: [for (final p in _putovanja) DropdownMenuItem(value: p.id, child: Text(p.nazivPutovanja))],
              onChanged: (v) {
                setState(() => _putovanjeId = v);
                ucitaj();
              },
            ),
            FilterDropdown<int>(
              label: 'Ocjena',
              value: _ocjena,
              width: 140,
              items: [for (var i = 5; i >= 1; i--) DropdownMenuItem(value: i, child: Text('$i ★'))],
              onChanged: (v) {
                setState(() => _ocjena = v);
                ucitaj();
              },
            ),
          ],
          onPretrazi: ucitaj,
        ),
        if (lista.isNotEmpty)
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16),
            child: Align(
              alignment: Alignment.centerRight,
              child: Text('Prosječna ocjena prikazanih: ${prosjek.toStringAsFixed(2)} (${lista.length} ocjena)',
                  style: Theme.of(context).textTheme.titleSmall),
            ),
          ),
        const SizedBox(height: 8),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Datum')),
              DataColumn(label: Text('Klijent')),
              DataColumn(label: Text('Putovanje')),
              DataColumn(label: Text('Ocjena')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final o in lista)
                DataRow(cells: [
                  DataCell(Text(formatDatum(o.datum))),
                  DataCell(Text(o.korisnikImePrezime ?? '-')),
                  DataCell(Text(o.putovanjeNaziv ?? '-')),
                  DataCell(Row(mainAxisSize: MainAxisSize.min, children: [
                    for (var i = 1; i <= 5; i++)
                      Icon(i <= o.ocjena ? Icons.star : Icons.star_border, size: 18, color: Colors.amber),
                  ])),
                  DataCell(RedAkcije(
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje ocjene',
                      opis: 'Da li ste sigurni da želite obrisati ocjenu ${o.ocjena} za "${o.putovanjeNaziv ?? ''}"?',
                      akcija: () => context.read<OcjenaProvider>().delete(o.id),
                      porukaUspjeha: 'Ocjena je obrisana.',
                    ),
                  )),
                ]),
            ],
          ),
        ),
      ]),
    );
  }
}
