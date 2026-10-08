import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/interakcije.dart';
import '../models/putovanje.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../widgets/common.dart';
import '../widgets/mixins.dart';

/// Moderacija komentara klijenata (pregled i brisanje neprimjerenih).
class KomentariScreen extends StatefulWidget {
  const KomentariScreen({super.key});

  @override
  State<KomentariScreen> createState() => _KomentariScreenState();
}

class _KomentariScreenState extends State<KomentariScreen> with ListaMixin<KomentariScreen, Komentar> {
  final _sadrzaj = TextEditingController();
  List<Putovanje> _putovanja = [];
  int? _putovanjeId;

  @override
  void initState() {
    super.initState();
    context.read<PutovanjeProvider>().get().then((v) {
      if (mounted) setState(() => _putovanja = v);
    }).catchError((_) {});
  }

  @override
  Future<List<Komentar>> dohvati() =>
      context.read<KomentarProvider>().get(filter: {'sadrzaj': _sadrzaj.text, 'putovanjeId': _putovanjeId});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Komentari',
      child: Column(children: [
        PretragaTraka(
          filteri: [
            PretragaPolje(controller: _sadrzaj, label: 'Tekst komentara', onSubmit: ucitaj),
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
          ],
          onPretrazi: ucitaj,
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Datum')),
              DataColumn(label: Text('Klijent')),
              DataColumn(label: Text('Putovanje')),
              DataColumn(label: Text('Komentar')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final k in lista)
                DataRow(cells: [
                  DataCell(Text(formatDatumVrijeme(k.datum))),
                  DataCell(Text(k.korisnikImePrezime ?? '-')),
                  DataCell(Text(k.putovanjeNaziv ?? '-')),
                  DataCell(ConstrainedBox(
                    constraints: const BoxConstraints(maxWidth: 420),
                    child: Text(k.sadrzaj, maxLines: 2, overflow: TextOverflow.ellipsis),
                  )),
                  DataCell(RedAkcije(
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje komentara',
                      opis: 'Da li ste sigurni da želite obrisati komentar korisnika ${k.korisnikImePrezime ?? ''}?',
                      akcija: () => context.read<KomentarProvider>().delete(k.id),
                      porukaUspjeha: 'Komentar je obrisan.',
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
