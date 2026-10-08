import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/izvjestaji.dart';
import '../models/korisnik.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';

/// Prikaz rada sistema preporuke (user-based collaborative filtering) za odabranog klijenta:
/// najslicniji korisnici (Pearson) i putovanja sa najvecom predvidjenom ocjenom.
class PreporukeScreen extends StatefulWidget {
  const PreporukeScreen({super.key});

  @override
  State<PreporukeScreen> createState() => _PreporukeScreenState();
}

class _PreporukeScreenState extends State<PreporukeScreen> {
  List<Korisnik> _klijenti = [];
  int? _korisnikId;
  PreporukaRezultat? _rezultat;
  bool _ucitavanje = false;
  String? _greska;

  @override
  void initState() {
    super.initState();
    context.read<KorisnikProvider>().get().then((v) {
      if (!mounted) return;
      setState(() => _klijenti = v.where((k) => k.uloge.contains('Klijent')).toList());
    }).catchError((e) {
      if (mounted) setState(() => _greska = e.toString());
    });
  }

  Future<void> _ucitaj(int korisnikId) async {
    setState(() {
      _korisnikId = korisnikId;
      _ucitavanje = true;
      _greska = null;
    });
    try {
      final r = await context.read<PreporukaProvider>().zaKorisnika(korisnikId);
      if (mounted) setState(() => _rezultat = r);
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    } finally {
      if (mounted) setState(() => _ucitavanje = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Preporuke',
      child: ListView(padding: const EdgeInsets.all(16), children: [
        Card(
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
              Text('Sistem preporuke - user-based collaborative filtering',
                  style: Theme.of(context).textTheme.titleMedium),
              const SizedBox(height: 8),
              const Text(
                '1) Sličnost klijenata se računa Pearsonovom korelacijom nad putovanjima koja su oba ocijenila '
                '(najmanje 2 zajednička putovanja).\n'
                '2) Uzima se K najsličnijih klijenata sa pozitivnom sličnošću (K-najbližih susjeda).\n'
                '3) Za buduća putovanja koja klijent nije ocijenio niti rezervisao predviđa se ocjena:  '
                'pred = prosjek klijenta + Σ sličnost·(ocjena susjeda − prosjek susjeda) / Σ |sličnost|.\n'
                '4) Ako nema dovoljno podataka, preporuka se dopunjava najbolje ocijenjenim putovanjima ("Popularno").',
              ),
              const SizedBox(height: 16),
              SizedBox(
                width: 360,
                child: DropdownButtonFormField<int>(
                  key: ValueKey('kl-${_klijenti.length}'),
                  initialValue: _korisnikId,
                  isExpanded: true,
                  decoration: const InputDecoration(
                    labelText: 'Odaberite klijenta',
                    prefixIcon: Icon(Icons.person_search_outlined),
                    border: OutlineInputBorder(),
                  ),
                  items: [
                    for (final k in _klijenti)
                      DropdownMenuItem(value: k.id, child: Text('${k.imePrezime} (${k.korisnickoIme})')),
                  ],
                  onChanged: (v) {
                    if (v != null) _ucitaj(v);
                  },
                ),
              ),
            ]),
          ),
        ),
        const SizedBox(height: 16),
        if (_ucitavanje) const Center(child: Padding(padding: EdgeInsets.all(32), child: CircularProgressIndicator())),
        if (_greska != null) Text(_greska!, style: TextStyle(color: Theme.of(context).colorScheme.error)),
        if (!_ucitavanje && _rezultat != null) _prikazRezultata(_rezultat!),
      ]),
    );
  }

  Widget _prikazRezultata(PreporukaRezultat r) {
    return Wrap(spacing: 16, runSpacing: 16, crossAxisAlignment: WrapCrossAlignment.start, children: [
      SizedBox(
        width: 460,
        child: Card(
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
              Text(r.korisnikImePrezime, style: Theme.of(context).textTheme.titleMedium),
              Text('Broj ocjena: ${r.brojOcjenaKorisnika}   ·   Prosječna ocjena: '
                  '${r.prosjecnaOcjenaKorisnika.toStringAsFixed(2)}'),
              const Divider(height: 24),
              Text('Najsličniji klijenti (susjedi)', style: Theme.of(context).textTheme.titleSmall),
              const SizedBox(height: 8),
              if (r.susjedi.isEmpty)
                const Text('Nema dovoljno zajedničkih ocjena sa drugim klijentima.')
              else
                DataTable(
                  columnSpacing: 20,
                  columns: const [
                    DataColumn(label: Text('Klijent')),
                    DataColumn(label: Text('Sličnost'), numeric: true),
                    DataColumn(label: Text('Zajedničkih'), numeric: true),
                  ],
                  rows: [
                    for (final s in r.susjedi)
                      DataRow(cells: [
                        DataCell(Text(s.imePrezime)),
                        DataCell(Text(s.slicnost.toStringAsFixed(3))),
                        DataCell(Text('${s.zajednickihOcjena}')),
                      ]),
                  ],
                ),
            ]),
          ),
        ),
      ),
      SizedBox(
        width: 620,
        child: Card(
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
              Text('Preporučena putovanja', style: Theme.of(context).textTheme.titleSmall),
              const SizedBox(height: 8),
              if (r.preporuke.isEmpty) const Text('Nema budućih putovanja za preporuku.'),
              for (final p in r.preporuke)
                ListTile(
                  leading: slikaIliIkona(p.putovanje.slika, size: 52, ikona: Icons.landscape_outlined),
                  title: Text(p.putovanje.nazivPutovanja),
                  subtitle: Text('${p.putovanje.gradNaziv ?? ''} · polazak ${formatDatum(p.putovanje.datumPolaska)} · '
                      '${formatKM(p.putovanje.cijenaPutovanja)}'),
                  trailing: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.end,
                    children: [
                      Row(mainAxisSize: MainAxisSize.min, children: [
                        const Icon(Icons.star, color: Colors.amber, size: 18),
                        Text(p.predvidjenaOcjena.toStringAsFixed(2),
                            style: const TextStyle(fontWeight: FontWeight.bold)),
                      ]),
                      Text(
                        p.izvor == 'Kolaborativno' ? '${p.brojSusjeda} sličnih klijenata' : 'Popularno',
                        style: Theme.of(context).textTheme.bodySmall,
                      ),
                    ],
                  ),
                ),
            ]),
          ),
        ),
      ),
    ]);
  }
}
