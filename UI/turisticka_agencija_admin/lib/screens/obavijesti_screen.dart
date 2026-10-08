import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/interakcije.dart';
import '../models/korisnik.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';
import '../widgets/mixins.dart';

class ObavijestiScreen extends StatefulWidget {
  const ObavijestiScreen({super.key});

  @override
  State<ObavijestiScreen> createState() => _ObavijestiScreenState();
}

class _ObavijestiScreenState extends State<ObavijestiScreen> with ListaMixin<ObavijestiScreen, Obavijest> {
  final _naziv = TextEditingController();

  @override
  Future<List<Obavijest>> dohvati() => context.read<ObavijestProvider>().get(filter: {'naziv': _naziv.text});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Obavijesti',
      child: Column(children: [
        PretragaTraka(
          filteri: [PretragaPolje(controller: _naziv, label: 'Naslov obavijesti', onSubmit: ucitaj)],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const ObavijestForma()),
          dodajTekst: 'Nova obavijest',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Datum')),
              DataColumn(label: Text('Naslov')),
              DataColumn(label: Text('Primalac')),
              DataColumn(label: Text('Sadržaj')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final o in lista)
                DataRow(cells: [
                  DataCell(Text(formatDatumVrijeme(o.datum))),
                  DataCell(Text(o.naziv)),
                  DataCell(Text(o.korisnikImePrezime ?? 'Svi korisnici')),
                  DataCell(ConstrainedBox(
                    constraints: const BoxConstraints(maxWidth: 380),
                    child: Text(o.sadrzaj, maxLines: 2, overflow: TextOverflow.ellipsis),
                  )),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(ObavijestForma(obavijest: o)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje obavijesti',
                      opis: 'Da li ste sigurni da želite obrisati obavijest "${o.naziv}"?',
                      akcija: () => context.read<ObavijestProvider>().delete(o.id),
                      porukaUspjeha: 'Obavijest "${o.naziv}" je obrisana.',
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

class ObavijestForma extends StatefulWidget {
  final Obavijest? obavijest;

  const ObavijestForma({super.key, this.obavijest});

  @override
  State<ObavijestForma> createState() => _ObavijestFormaState();
}

class _ObavijestFormaState extends State<ObavijestForma> with FormaMixin<ObavijestForma> {
  late final _naziv = TextEditingController(text: widget.obavijest?.naziv);
  late final _sadrzaj = TextEditingController(text: widget.obavijest?.sadrzaj);
  late int? _korisnikId = widget.obavijest?.korisnikId;
  late bool _posaljiEmail = widget.obavijest == null;
  List<Korisnik> _korisnici = [];

  @override
  void initState() {
    super.initState();
    context.read<KorisnikProvider>().get().then((v) {
      if (mounted) setState(() => _korisnici = v.where((k) => k.uloge.contains('Klijent')).toList());
    }).catchError((_) {});
  }

  @override
  Widget build(BuildContext context) {
    final nova = widget.obavijest == null;
    return FormaEkran(
      naslov: nova ? 'Nova obavijest' : 'Uredi obavijest',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        TextFormField(
          controller: _naziv,
          decoration: poljeDekoracija('Naslov *', ikona: Icons.title),
          validator: Validators.duzina(min: 2, max: 100),
        ),
        TextFormField(
          controller: _sadrzaj,
          maxLines: 6,
          decoration: poljeDekoracija('Sadržaj *', ikona: Icons.notes_outlined),
          validator: Validators.duzina(min: 5, max: 2000),
        ),
        DropdownButtonFormField<int?>(
          key: ValueKey('k-${_korisnici.length}'),
          initialValue: _korisnici.any((k) => k.id == _korisnikId) ? _korisnikId : null,
          decoration: poljeDekoracija('Primalac', ikona: Icons.person_outline),
          items: [
            const DropdownMenuItem<int?>(value: null, child: Text('Svi klijenti')),
            for (final k in _korisnici) DropdownMenuItem<int?>(value: k.id, child: Text(k.imePrezime)),
          ],
          onChanged: (v) => setState(() => _korisnikId = v),
        ),
        if (nova)
          SwitchListTile(
            title: const Text('Pošalji i email obavijest'),
            subtitle: const Text('Email šalje pomoćni servis preko RabbitMQ-a.'),
            value: _posaljiEmail,
            onChanged: (v) => setState(() => _posaljiEmail = v),
          ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final provider = context.read<ObavijestProvider>();
        final request = {
          'naziv': _naziv.text.trim(),
          'sadrzaj': _sadrzaj.text.trim(),
          'korisnikId': _korisnikId,
          'posaljiEmail': nova && _posaljiEmail,
        };
        if (nova) {
          await provider.insert(request);
        } else {
          await provider.update(widget.obavijest!.id, request);
        }
      }, nova ? 'Obavijest "${_naziv.text.trim()}" je uspješno objavljena.' : 'Obavijest je uspješno izmijenjena.'),
    );
  }
}
