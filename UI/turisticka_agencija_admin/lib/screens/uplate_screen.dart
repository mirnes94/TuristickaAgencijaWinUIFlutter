import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/rezervacija.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';
import '../widgets/mixins.dart';

class UplateScreen extends StatefulWidget {
  const UplateScreen({super.key});

  @override
  State<UplateScreen> createState() => _UplateScreenState();
}

class _UplateScreenState extends State<UplateScreen> with ListaMixin<UplateScreen, Uplata> {
  DateTime? _od;
  DateTime? _do;

  @override
  Future<List<Uplata>> dohvati() =>
      context.read<UplataProvider>().get(filter: {'datumOd': _od, 'datumDo': _do});

  Future<void> _odaberiDatum(bool od) async {
    final odabran = await showDatePicker(
      context: context,
      initialDate: (od ? _od : _do) ?? DateTime.now(),
      firstDate: DateTime(2000),
      lastDate: DateTime(2100),
    );
    if (odabran == null) return;
    setState(() {
      if (od) {
        _od = odabran;
      } else {
        _do = odabran;
      }
    });
    ucitaj();
  }

  @override
  Widget build(BuildContext context) {
    final ukupno = lista.fold<double>(0, (s, u) => s + u.iznos);
    return MasterScreen(
      title: 'Uplate',
      child: Column(children: [
        PretragaTraka(
          filteri: [
            OutlinedButton.icon(
              icon: const Icon(Icons.calendar_month_outlined),
              label: Text(_od == null ? 'Datum od' : 'Od: ${formatDatum(_od)}'),
              onPressed: () => _odaberiDatum(true),
            ),
            OutlinedButton.icon(
              icon: const Icon(Icons.calendar_month_outlined),
              label: Text(_do == null ? 'Datum do' : 'Do: ${formatDatum(_do)}'),
              onPressed: () => _odaberiDatum(false),
            ),
            if (_od != null || _do != null)
              TextButton(
                onPressed: () {
                  setState(() {
                    _od = null;
                    _do = null;
                  });
                  ucitaj();
                },
                child: const Text('Poništi datume'),
              ),
          ],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const UplataForma()),
          dodajTekst: 'Evidentiraj uplatu',
        ),
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16),
          child: Align(
            alignment: Alignment.centerRight,
            child: Text('Ukupno prikazano: ${formatKM(ukupno)} (${lista.length} uplata)',
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
              DataColumn(label: Text('Rezervacija')),
              DataColumn(label: Text('Putovanje')),
              DataColumn(label: Text('Iznos'), numeric: true),
              DataColumn(label: Text('Način plaćanja')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final u in lista)
                DataRow(cells: [
                  DataCell(Text(formatDatumVrijeme(u.datum))),
                  DataCell(Text(u.korisnikImePrezime ?? '-')),
                  DataCell(Text(u.rezervacijaNaziv ?? '-')),
                  DataCell(Text(u.putovanjeNaziv ?? '-')),
                  DataCell(Text(formatKM(u.iznos))),
                  DataCell(Text(u.nacinPlacanja ?? '-')),
                  DataCell(RedAkcije(
                    onObrisi: () => obrisi(
                      naslov: 'Storniranje uplate',
                      opis: 'Da li ste sigurni da želite obrisati (stornirati) uplatu od ${formatKM(u.iznos)}?',
                      akcija: () => context.read<UplataProvider>().delete(u.id),
                      porukaUspjeha: 'Uplata je stornirana.',
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

/// Evidentiranje uplate u poslovnici (gotovina/virman). Online placanje (Stripe) je dio mobilne aplikacije.
class UplataForma extends StatefulWidget {
  const UplataForma({super.key});

  @override
  State<UplataForma> createState() => _UplataFormaState();
}

class _UplataFormaState extends State<UplataForma> with FormaMixin<UplataForma> {
  final _iznos = TextEditingController();
  DateTime? _datum = DateTime.now();
  int? _rezervacijaId;
  List<Rezervacija> _rezervacije = [];

  @override
  void initState() {
    super.initState();
    context.read<RezervacijaProvider>().get(filter: {'status': StatusRezervacije.uObradi}).then((v) {
      if (mounted) setState(() => _rezervacije = v.where((r) => r.preostalo > 0).toList());
    }).catchError((_) {});
  }

  @override
  Widget build(BuildContext context) {
    final odabrana = _rezervacije.where((r) => r.id == _rezervacijaId).firstOrNull;
    return FormaEkran(
      naslov: 'Evidentiranje uplate',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        DropdownButtonFormField<int>(
          key: ValueKey('r-${_rezervacije.length}'),
          initialValue: _rezervacijaId,
          isExpanded: true,
          decoration: poljeDekoracija('Rezervacija (status "U obradi") *', ikona: Icons.confirmation_number_outlined),
          items: [
            for (final r in _rezervacije)
              DropdownMenuItem(
                value: r.id,
                child: Text('${r.korisnikImePrezime ?? ''} - ${r.putovanjeNaziv ?? r.ime} '
                    '(preostalo ${formatKM(r.preostalo)})'),
              ),
          ],
          onChanged: (v) => setState(() {
            _rezervacijaId = v;
            final r = _rezervacije.where((x) => x.id == v).firstOrNull;
            if (r != null) _iznos.text = r.preostalo.toStringAsFixed(2);
          }),
          validator: (v) => Validators.dropdown(v, 'rezervaciju'),
        ),
        TextFormField(
          controller: _iznos,
          decoration: poljeDekoracija('Iznos (KM) *', ikona: Icons.payments_outlined,
              helper: odabrana == null ? null : 'Maksimalno ${formatKM(odabrana.preostalo)}'),
          validator: (v) {
            final osnovna = Validators.decimalniBroj()(v);
            if (osnovna != null) return osnovna;
            if (odabrana != null && Validators.parseDouble(v!) > odabrana.preostalo + 0.001) {
              return 'Iznos ne može biti veći od preostalog duga (${formatKM(odabrana.preostalo)}).';
            }
            return null;
          },
        ),
        DatumPolje(
          label: 'Datum uplate *',
          initialValue: _datum,
          lastDate: DateTime.now(),
          onChanged: (v) => setState(() => _datum = v),
          validator: (v) => v == null ? 'Odaberite datum uplate.' : null,
        ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final datum = _datum!;
        final sada = DateTime.now();
        await context.read<UplataProvider>().insert({
          'rezervacijaId': _rezervacijaId,
          'iznos': Validators.parseDouble(_iznos.text),
          'datum': DateTime(datum.year, datum.month, datum.day, sada.hour, sada.minute).toIso8601String(),
          'korisnikId': odabrana?.korisnikId ?? 0,
        });
      }, 'Uplata od ${_iznos.text} KM je uspješno evidentirana.'),
    );
  }
}
