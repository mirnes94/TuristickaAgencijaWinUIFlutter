import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/putovanje.dart';
import '../models/sifarnici.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';
import '../widgets/mixins.dart';

class PutovanjaScreen extends StatefulWidget {
  const PutovanjaScreen({super.key});

  @override
  State<PutovanjaScreen> createState() => _PutovanjaScreenState();
}

class _PutovanjaScreenState extends State<PutovanjaScreen> with ListaMixin<PutovanjaScreen, Putovanje> {
  final _naziv = TextEditingController();
  List<Grad> _gradovi = [];
  int? _gradId;
  bool _samoBuduca = false;

  @override
  void initState() {
    super.initState();
    context.read<GradProvider>().get().then((v) {
      if (mounted) setState(() => _gradovi = v);
    }).catchError((_) {});
  }

  @override
  Future<List<Putovanje>> dohvati() => context.read<PutovanjeProvider>().get(filter: {
        'nazivPutovanja': _naziv.text,
        'gradId': _gradId,
        'samoBuduca': _samoBuduca ? true : null,
      });

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Putovanja',
      child: Column(children: [
        PretragaTraka(
          filteri: [
            PretragaPolje(controller: _naziv, label: 'Naziv putovanja', onSubmit: ucitaj),
            FilterDropdown<int>(
              label: 'Destinacija',
              value: _gradId,
              items: [for (final g in _gradovi) DropdownMenuItem(value: g.id, child: Text(g.nazivGrada))],
              onChanged: (v) {
                setState(() => _gradId = v);
                ucitaj();
              },
            ),
            FilterChip(
              label: const Text('Samo buduća'),
              selected: _samoBuduca,
              onSelected: (v) {
                setState(() => _samoBuduca = v);
                ucitaj();
              },
            ),
          ],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const PutovanjeForma()),
          dodajTekst: 'Novo putovanje',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Slika')),
              DataColumn(label: Text('Naziv')),
              DataColumn(label: Text('Destinacija')),
              DataColumn(label: Text('Polazak')),
              DataColumn(label: Text('Povratak')),
              DataColumn(label: Text('Cijena'), numeric: true),
              DataColumn(label: Text('Mjesta'), numeric: true),
              DataColumn(label: Text('Ocjena')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final p in lista)
                DataRow(cells: [
                  DataCell(slikaIliIkona(p.slika, size: 44, ikona: Icons.landscape_outlined)),
                  DataCell(Text(p.nazivPutovanja)),
                  DataCell(Text(p.gradNaziv ?? '-')),
                  DataCell(Text(formatDatum(p.datumPolaska))),
                  DataCell(Text(formatDatum(p.datumDolaska))),
                  DataCell(Text(formatKM(p.cijenaPutovanja))),
                  DataCell(Text('${p.brojMjesta}')),
                  DataCell(Row(mainAxisSize: MainAxisSize.min, children: [
                    const Icon(Icons.star, size: 16, color: Colors.amber),
                    const SizedBox(width: 4),
                    Text(p.brojOcjena == 0 ? '-' : '${p.prosjecnaOcjena.toStringAsFixed(1)} (${p.brojOcjena})'),
                  ])),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(PutovanjeForma(putovanje: p)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje putovanja',
                      opis: 'Da li ste sigurni da želite obrisati putovanje "${p.nazivPutovanja}"? '
                          'Biće obrisani i komentari, ocjene i liste želja za ovo putovanje.',
                      akcija: () => context.read<PutovanjeProvider>().delete(p.id),
                      porukaUspjeha: 'Putovanje "${p.nazivPutovanja}" je obrisano.',
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

class PutovanjeForma extends StatefulWidget {
  final Putovanje? putovanje;

  const PutovanjeForma({super.key, this.putovanje});

  @override
  State<PutovanjeForma> createState() => _PutovanjeFormaState();
}

class _PutovanjeFormaState extends State<PutovanjeForma> with FormaMixin<PutovanjeForma> {
  late final _naziv = TextEditingController(text: widget.putovanje?.nazivPutovanja);
  late final _opis = TextEditingController(text: widget.putovanje?.opisPutovanja);
  late final _cijena = TextEditingController(text: widget.putovanje?.cijenaPutovanja.toStringAsFixed(2));
  late final _mjesta = TextEditingController(text: widget.putovanje?.brojMjesta.toString());
  late DateTime? _polazak = widget.putovanje?.datumPolaska;
  late DateTime? _povratak = widget.putovanje?.datumDolaska;
  late int? _gradId = widget.putovanje?.gradId;
  late int? _prevozId = widget.putovanje?.prevozId;
  late int? _smjestajId = widget.putovanje?.smjestajId;
  late final Set<int> _vodici = {...?widget.putovanje?.vodici};
  String? _novaSlika;

  List<Grad> _gradovi = [];
  List<Prevoz> _prevozi = [];
  List<Smjestaj> _smjestaji = [];
  List<Vodic> _sviVodici = [];

  @override
  void initState() {
    super.initState();
    _ucitajSifarnike();
  }

  Future<void> _ucitajSifarnike() async {
    try {
      final rezultati = await Future.wait([
        context.read<GradProvider>().get(),
        context.read<PrevozProvider>().get(),
        context.read<SmjestajProvider>().get(),
        context.read<VodicProvider>().get(),
      ]);
      if (!mounted) return;
      setState(() {
        _gradovi = rezultati[0] as List<Grad>;
        _prevozi = rezultati[1] as List<Prevoz>;
        _smjestaji = rezultati[2] as List<Smjestaj>;
        _sviVodici = rezultati[3] as List<Vodic>;
      });
    } catch (_) {
      // greska se prikazuje pri spasavanju
    }
  }

  @override
  Widget build(BuildContext context) {
    final novo = widget.putovanje == null;
    final danas = DateTime.now();
    final ucitano = _gradovi.length + _prevozi.length + _smjestaji.length;

    return FormaEkran(
      naslov: novo ? 'Novo putovanje' : 'Uredi putovanje',
      formKey: formKey,
      spasavanje: spasavanje,
      maxSirina: 900,
      polja: [
        TextFormField(
          controller: _naziv,
          decoration: poljeDekoracija('Naziv putovanja *', ikona: Icons.luggage_outlined),
          validator: Validators.duzina(min: 2, max: 100),
        ),
        TextFormField(
          controller: _opis,
          maxLines: 4,
          decoration: poljeDekoracija('Opis putovanja *', ikona: Icons.notes_outlined),
          validator: Validators.duzina(min: 10, max: 2000),
        ),
        Row(crossAxisAlignment: CrossAxisAlignment.start, children: [
          Expanded(
            child: TextFormField(
              controller: _cijena,
              decoration: poljeDekoracija('Cijena po osobi (KM) *', ikona: Icons.payments_outlined, hint: 'npr. 450'),
              validator: Validators.decimalniBroj(),
            ),
          ),
          const SizedBox(width: 16),
          Expanded(
            child: TextFormField(
              controller: _mjesta,
              decoration: poljeDekoracija('Broj mjesta *', ikona: Icons.event_seat_outlined),
              validator: Validators.cijeliBroj(min: 1, max: 1000),
            ),
          ),
        ]),
        Row(crossAxisAlignment: CrossAxisAlignment.start, children: [
          Expanded(
            child: DatumPolje(
              label: 'Datum polaska *',
              initialValue: _polazak,
              firstDate: novo ? DateTime(danas.year, danas.month, danas.day) : DateTime(2000),
              onChanged: (v) => setState(() => _polazak = v),
              validator: (v) => v == null ? 'Odaberite datum polaska.' : null,
            ),
          ),
          const SizedBox(width: 16),
          Expanded(
            child: DatumPolje(
              label: 'Datum povratka *',
              initialValue: _povratak,
              onChanged: (v) => setState(() => _povratak = v),
              validator: (v) {
                if (v == null) return 'Odaberite datum povratka.';
                if (_polazak != null && v.isBefore(_polazak!)) return 'Povratak mora biti nakon polaska.';
                return null;
              },
            ),
          ),
        ]),
        DropdownButtonFormField<int>(
          key: ValueKey('grad-$ucitano'),
          initialValue: _gradovi.any((g) => g.id == _gradId) ? _gradId : null,
          decoration: poljeDekoracija('Destinacija (grad) *', ikona: Icons.location_on_outlined),
          items: [
            for (final g in _gradovi)
              DropdownMenuItem(value: g.id, child: Text('${g.nazivGrada}, ${g.drzavaNaziv ?? ''}')),
          ],
          onChanged: (v) => setState(() => _gradId = v),
          validator: (v) => Validators.dropdown(v, 'destinaciju'),
        ),
        DropdownButtonFormField<int>(
          key: ValueKey('prevoz-$ucitano'),
          initialValue: _prevozi.any((p) => p.id == _prevozId) ? _prevozId : null,
          decoration: poljeDekoracija('Prevoz *', ikona: Icons.directions_bus_outlined),
          items: [for (final p in _prevozi) DropdownMenuItem(value: p.id, child: Text(p.prikaz))],
          onChanged: (v) => setState(() => _prevozId = v),
          validator: (v) => Validators.dropdown(v, 'prevoz'),
        ),
        DropdownButtonFormField<int>(
          key: ValueKey('smjestaj-$ucitano'),
          initialValue: _smjestaji.any((s) => s.id == _smjestajId) ? _smjestajId : null,
          decoration: poljeDekoracija('Smještaj *', ikona: Icons.hotel_outlined),
          items: [
            for (final s in _smjestaji)
              DropdownMenuItem(value: s.id, child: Text('${s.nazivSmjestaja} - ${s.tipSobe}')),
          ],
          onChanged: (v) => setState(() => _smjestajId = v),
          validator: (v) => Validators.dropdown(v, 'smještaj'),
        ),
        InputDecorator(
          decoration: poljeDekoracija('Turistički vodiči', ikona: Icons.badge_outlined),
          child: _sviVodici.isEmpty
              ? const Text('Nema unesenih vodiča.')
              : Wrap(spacing: 8, runSpacing: 4, children: [
                  for (final v in _sviVodici)
                    FilterChip(
                      label: Text(v.imePrezime),
                      selected: _vodici.contains(v.id),
                      onSelected: (s) => setState(() => s ? _vodici.add(v.id) : _vodici.remove(v.id)),
                    ),
                ]),
        ),
        SlikaPolje(
          label: novo ? 'Slika putovanja *' : 'Slika putovanja',
          initialValue: widget.putovanje?.slika,
          onChanged: (v) => _novaSlika = v,
          validator: (v) => (v == null || v.isEmpty) ? 'Odaberite sliku putovanja (JPG ili PNG).' : null,
        ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final provider = context.read<PutovanjeProvider>();
        final request = {
          'nazivPutovanja': _naziv.text.trim(),
          'opisPutovanja': _opis.text.trim(),
          'cijenaPutovanja': Validators.parseDouble(_cijena.text),
          'brojMjesta': int.parse(_mjesta.text.trim()),
          'datumPolaska': _polazak!.toIso8601String(),
          'datumDolaska': _povratak!.toIso8601String(),
          'gradId': _gradId,
          'prevozId': _prevozId,
          'smjestajId': _smjestajId,
          'vodici': _vodici.toList(),
          'slika': _novaSlika,
        };
        if (novo) {
          await provider.insert(request);
        } else {
          await provider.update(widget.putovanje!.id, request);
        }
      }, novo ? 'Putovanje "${_naziv.text.trim()}" je uspješno dodano.' : 'Putovanje je uspješno izmijenjeno.'),
    );
  }
}
