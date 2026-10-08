import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/sifarnici.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';
import '../widgets/mixins.dart';

class VodiciScreen extends StatefulWidget {
  const VodiciScreen({super.key});

  @override
  State<VodiciScreen> createState() => _VodiciScreenState();
}

class _VodiciScreenState extends State<VodiciScreen> with ListaMixin<VodiciScreen, Vodic> {
  final _ime = TextEditingController();
  final _prezime = TextEditingController();

  @override
  Future<List<Vodic>> dohvati() =>
      context.read<VodicProvider>().get(filter: {'ime': _ime.text, 'prezime': _prezime.text});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Vodiči',
      child: Column(children: [
        PretragaTraka(
          filteri: [
            PretragaPolje(controller: _ime, label: 'Ime', onSubmit: ucitaj, width: 200),
            PretragaPolje(controller: _prezime, label: 'Prezime', onSubmit: ucitaj, width: 200),
          ],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const VodicForma()),
          dodajTekst: 'Novi vodič',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Slika')),
              DataColumn(label: Text('Ime i prezime')),
              DataColumn(label: Text('Kontakt')),
              DataColumn(label: Text('JMBG')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final v in lista)
                DataRow(cells: [
                  DataCell(slikaIliIkona(v.slika, size: 40, ikona: Icons.person_outline)),
                  DataCell(Text(v.imePrezime)),
                  DataCell(Text(v.kontakt)),
                  DataCell(Text(v.jmbg)),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(VodicForma(vodic: v)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje vodiča',
                      opis: 'Da li ste sigurni da želite obrisati vodiča "${v.imePrezime}"?',
                      akcija: () => context.read<VodicProvider>().delete(v.id),
                      porukaUspjeha: 'Vodič "${v.imePrezime}" je obrisan.',
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

class VodicForma extends StatefulWidget {
  final Vodic? vodic;

  const VodicForma({super.key, this.vodic});

  @override
  State<VodicForma> createState() => _VodicFormaState();
}

class _VodicFormaState extends State<VodicForma> with FormaMixin<VodicForma> {
  late final _ime = TextEditingController(text: widget.vodic?.ime);
  late final _prezime = TextEditingController(text: widget.vodic?.prezime);
  late final _kontakt = TextEditingController(text: widget.vodic?.kontakt);
  late final _jmbg = TextEditingController(text: widget.vodic?.jmbg);
  String? _novaSlika;

  @override
  Widget build(BuildContext context) {
    final novi = widget.vodic == null;
    return FormaEkran(
      naslov: novi ? 'Novi vodič' : 'Uredi vodiča',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        TextFormField(
          controller: _ime,
          decoration: poljeDekoracija('Ime *', ikona: Icons.person_outline),
          validator: Validators.duzina(min: 2, max: 50),
        ),
        TextFormField(
          controller: _prezime,
          decoration: poljeDekoracija('Prezime *', ikona: Icons.person_outline),
          validator: Validators.duzina(min: 2, max: 50),
        ),
        TextFormField(
          controller: _kontakt,
          decoration: poljeDekoracija('Kontakt telefon *', ikona: Icons.phone_outlined, hint: '061123456'),
          validator: Validators.telefon,
        ),
        TextFormField(
          controller: _jmbg,
          decoration: poljeDekoracija('JMBG *', ikona: Icons.badge_outlined, hint: '13 cifara'),
          validator: Validators.jmbg,
        ),
        SlikaPolje(
          label: 'Fotografija',
          initialValue: widget.vodic?.slika,
          onChanged: (v) => _novaSlika = v,
        ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final provider = context.read<VodicProvider>();
        final request = {
          'ime': _ime.text.trim(),
          'prezime': _prezime.text.trim(),
          'kontakt': _kontakt.text.trim(),
          'jmbg': _jmbg.text.trim(),
          'slika': _novaSlika,
        };
        if (novi) {
          await provider.insert(request);
        } else {
          await provider.update(widget.vodic!.id, request);
        }
      }, novi ? 'Vodič "${_ime.text.trim()} ${_prezime.text.trim()}" je uspješno dodan.' : 'Vodič je uspješno izmijenjen.'),
    );
  }
}
