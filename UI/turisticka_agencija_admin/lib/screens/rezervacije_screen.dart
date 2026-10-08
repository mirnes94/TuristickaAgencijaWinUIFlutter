import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/korisnik.dart';
import '../models/putovanje.dart';
import '../models/rezervacija.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';
import '../widgets/mixins.dart';

Color bojaStatusa(String status) {
  switch (status) {
    case StatusRezervacije.potvrdjeno:
      return Colors.green.shade700;
    case StatusRezervacije.otkazano:
      return Colors.red.shade700;
    default:
      return Colors.orange.shade800;
  }
}

class StatusChip extends StatelessWidget {
  final String status;

  const StatusChip(this.status, {super.key});

  @override
  Widget build(BuildContext context) {
    final boja = bojaStatusa(status);
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 4),
      decoration: BoxDecoration(
        color: boja.withValues(alpha: 0.12),
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: boja.withValues(alpha: 0.5)),
      ),
      child: Text(status, style: TextStyle(color: boja, fontWeight: FontWeight.w600)),
    );
  }
}

class RezervacijeScreen extends StatefulWidget {
  const RezervacijeScreen({super.key});

  @override
  State<RezervacijeScreen> createState() => _RezervacijeScreenState();
}

class _RezervacijeScreenState extends State<RezervacijeScreen> with ListaMixin<RezervacijeScreen, Rezervacija> {
  final _tekst = TextEditingController();
  String? _status;

  @override
  Future<List<Rezervacija>> dohvati() =>
      context.read<RezervacijaProvider>().get(filter: {'ime': _tekst.text, 'status': _status});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Rezervacije',
      child: Column(children: [
        PretragaTraka(
          filteri: [
            PretragaPolje(controller: _tekst, label: 'Rezervacija ili klijent', onSubmit: ucitaj, width: 260),
            FilterDropdown<String>(
              label: 'Status',
              value: _status,
              width: 180,
              items: [for (final s in StatusRezervacije.svi) DropdownMenuItem(value: s, child: Text(s))],
              onChanged: (v) {
                setState(() => _status = v);
                ucitaj();
              },
            ),
          ],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const RezervacijaForma()),
          dodajTekst: 'Nova rezervacija',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Klijent')),
              DataColumn(label: Text('Putovanje')),
              DataColumn(label: Text('Polazak')),
              DataColumn(label: Text('Rezervisano')),
              DataColumn(label: Text('Osoba'), numeric: true),
              DataColumn(label: Text('Ukupno'), numeric: true),
              DataColumn(label: Text('Uplaćeno'), numeric: true),
              DataColumn(label: Text('Status')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final r in lista)
                DataRow(cells: [
                  DataCell(Text(r.korisnikImePrezime ?? '-')),
                  DataCell(Text(r.putovanjeNaziv ?? '-')),
                  DataCell(Text(formatDatum(r.datumPolaska))),
                  DataCell(Text(formatDatum(r.datumRezervacije))),
                  DataCell(Text('${r.brojOsoba}')),
                  DataCell(Text(formatKM(r.ukupnaCijena))),
                  DataCell(Text(formatKM(r.uplaceno))),
                  DataCell(StatusChip(r.status)),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(RezervacijaForma(rezervacija: r)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje rezervacije',
                      opis: 'Da li ste sigurni da želite obrisati rezervaciju "${r.ime}"? '
                          'Biće obrisane i sve uplate vezane za nju. Ako klijent odustaje, '
                          'preporučeno je umjesto brisanja postaviti status "Otkazano".',
                      akcija: () => context.read<RezervacijaProvider>().delete(r.id),
                      porukaUspjeha: 'Rezervacija je obrisana.',
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

class RezervacijaForma extends StatefulWidget {
  final Rezervacija? rezervacija;

  const RezervacijaForma({super.key, this.rezervacija});

  @override
  State<RezervacijaForma> createState() => _RezervacijaFormaState();
}

class _RezervacijaFormaState extends State<RezervacijaForma> with FormaMixin<RezervacijaForma> {
  late final _naziv = TextEditingController(text: widget.rezervacija?.ime);
  late final _brojOsoba = TextEditingController(text: widget.rezervacija?.brojOsoba.toString() ?? '1');
  late final _napomena = TextEditingController(text: widget.rezervacija?.napomena);
  late int? _korisnikId = widget.rezervacija?.korisnikId;
  late int? _putovanjeId = widget.rezervacija?.putovanjeId;
  late String _status = widget.rezervacija?.status ?? StatusRezervacije.uObradi;

  List<Korisnik> _korisnici = [];
  List<Putovanje> _putovanja = [];

  @override
  void initState() {
    super.initState();
    _ucitaj();
  }

  Future<void> _ucitaj() async {
    try {
      final korisnici = await context.read<KorisnikProvider>().get();
      if (!mounted) return;
      final putovanja = await context.read<PutovanjeProvider>().get(
            filter: {'samoBuduca': widget.rezervacija == null ? true : null},
          );
      if (!mounted) return;
      setState(() {
        _korisnici = korisnici.where((k) => k.uloge.contains('Klijent') || k.id == _korisnikId).toList();
        _putovanja = putovanja;
      });
    } catch (_) {
      // greska se prikazuje pri spasavanju
    }
  }

  @override
  Widget build(BuildContext context) {
    final nova = widget.rezervacija == null;
    final ucitano = _korisnici.length + _putovanja.length;
    final odabrano = _putovanja.where((p) => p.id == _putovanjeId).firstOrNull;
    final brojOsoba = int.tryParse(_brojOsoba.text) ?? 0;

    return FormaEkran(
      naslov: nova ? 'Nova rezervacija' : 'Uredi rezervaciju',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        DropdownButtonFormField<int>(
          key: ValueKey('k-$ucitano'),
          initialValue: _korisnici.any((k) => k.id == _korisnikId) ? _korisnikId : null,
          decoration: poljeDekoracija('Klijent *', ikona: Icons.person_outline),
          items: [
            for (final k in _korisnici)
              DropdownMenuItem(value: k.id, child: Text('${k.imePrezime} (${k.korisnickoIme})')),
          ],
          onChanged: (v) => setState(() => _korisnikId = v),
          validator: (v) => Validators.dropdown(v, 'klijenta'),
        ),
        DropdownButtonFormField<int>(
          key: ValueKey('p-$ucitano'),
          initialValue: _putovanja.any((p) => p.id == _putovanjeId) ? _putovanjeId : null,
          decoration: poljeDekoracija('Putovanje *', ikona: Icons.luggage_outlined),
          items: [
            for (final p in _putovanja)
              DropdownMenuItem(value: p.id, child: Text('${p.nazivPutovanja} (${formatDatum(p.datumPolaska)})')),
          ],
          onChanged: (v) => setState(() {
            _putovanjeId = v;
            if (_naziv.text.trim().isEmpty) {
              final p = _putovanja.where((x) => x.id == v).firstOrNull;
              if (p != null) _naziv.text = p.nazivPutovanja;
            }
          }),
          validator: (v) => Validators.dropdown(v, 'putovanje'),
        ),
        TextFormField(
          controller: _naziv,
          decoration: poljeDekoracija('Naziv rezervacije *', ikona: Icons.confirmation_number_outlined),
          validator: Validators.duzina(min: 2, max: 100),
        ),
        TextFormField(
          controller: _brojOsoba,
          decoration: poljeDekoracija('Broj osoba *', ikona: Icons.group_outlined,
              helper: odabrano == null
                  ? null
                  : 'Ukupna cijena: ${formatKM(odabrano.cijenaPutovanja * brojOsoba)}'),
          validator: Validators.cijeliBroj(min: 1, max: 50),
          onChanged: (_) => setState(() {}),
        ),
        DropdownButtonFormField<String>(
          initialValue: _status,
          decoration: poljeDekoracija('Status *', ikona: Icons.flag_outlined),
          items: [for (final s in StatusRezervacije.svi) DropdownMenuItem(value: s, child: Text(s))],
          onChanged: (v) => setState(() => _status = v ?? _status),
        ),
        TextFormField(
          controller: _napomena,
          maxLines: 3,
          decoration: poljeDekoracija('Napomena', ikona: Icons.notes_outlined),
          validator: Validators.duzina(min: 0, max: 500, obavezno: false),
        ),
      ],
      onSacuvaj: () async {
        if (!nova && _status != widget.rezervacija!.status && _status == StatusRezervacije.otkazano) {
          final ok = await potvrdiOtkazivanje(context);
          if (!ok || !mounted) return;
        }
        await sacuvaj(() async {
          final provider = context.read<RezervacijaProvider>();
          final request = {
            'ime': _naziv.text.trim(),
            'korisnikId': _korisnikId,
            'putovanjeId': _putovanjeId,
            'brojOsoba': int.parse(_brojOsoba.text.trim()),
            'status': _status,
            'napomena': _napomena.text.trim(),
            if (widget.rezervacija != null)
              'datumRezervacije': widget.rezervacija!.datumRezervacije.toIso8601String(),
          };
          if (nova) {
            await provider.insert(request);
          } else {
            await provider.update(widget.rezervacija!.id, request);
          }
        }, nova ? 'Rezervacija je uspješno kreirana. Klijent će dobiti email potvrdu.' : 'Rezervacija je uspješno izmijenjena.');
      },
    );
  }
}

Future<bool> potvrdiOtkazivanje(BuildContext context) async {
  final rezultat = await showDialog<bool>(
    context: context,
    builder: (ctx) => AlertDialog(
      icon: Icon(Icons.warning_amber_rounded, color: Colors.orange.shade800),
      title: const Text('Otkazivanje rezervacije'),
      content: const Text('Otkazivanje oslobađa mjesta na putovanju i klijent dobija obavijest. Nastaviti?'),
      actions: [
        TextButton(onPressed: () => Navigator.pop(ctx, false), child: const Text('Ne')),
        FilledButton(onPressed: () => Navigator.pop(ctx, true), child: const Text('Da, otkaži')),
      ],
    ),
  );
  return rezultat ?? false;
}
