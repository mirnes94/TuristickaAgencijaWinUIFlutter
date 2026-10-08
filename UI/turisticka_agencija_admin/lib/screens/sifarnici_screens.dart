import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/sifarnici.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';
import '../widgets/mixins.dart';

// =====================================================================
// DRŽAVE
// =====================================================================

class DrzaveScreen extends StatefulWidget {
  const DrzaveScreen({super.key});

  @override
  State<DrzaveScreen> createState() => _DrzaveScreenState();
}

class _DrzaveScreenState extends State<DrzaveScreen> with ListaMixin<DrzaveScreen, Drzava> {
  final _naziv = TextEditingController();

  @override
  Future<List<Drzava>> dohvati() => context.read<DrzavaProvider>().get(filter: {'naziv': _naziv.text});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Države',
      child: Column(children: [
        PretragaTraka(
          filteri: [PretragaPolje(controller: _naziv, label: 'Naziv države', onSubmit: ucitaj)],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const DrzavaForma()),
          dodajTekst: 'Nova država',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [DataColumn(label: Text('Naziv')), DataColumn(label: Text('Akcije'))],
            redovi: [
              for (final d in lista)
                DataRow(cells: [
                  DataCell(Text(d.naziv)),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(DrzavaForma(drzava: d)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje države',
                      opis: 'Da li ste sigurni da želite obrisati državu "${d.naziv}"?',
                      akcija: () => context.read<DrzavaProvider>().delete(d.id),
                      porukaUspjeha: 'Država "${d.naziv}" je obrisana.',
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

class DrzavaForma extends StatefulWidget {
  final Drzava? drzava;

  const DrzavaForma({super.key, this.drzava});

  @override
  State<DrzavaForma> createState() => _DrzavaFormaState();
}

class _DrzavaFormaState extends State<DrzavaForma> with FormaMixin<DrzavaForma> {
  late final _naziv = TextEditingController(text: widget.drzava?.naziv);

  @override
  Widget build(BuildContext context) {
    final nova = widget.drzava == null;
    return FormaEkran(
      naslov: nova ? 'Nova država' : 'Uredi državu',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        TextFormField(
          controller: _naziv,
          decoration: poljeDekoracija('Naziv države *', ikona: Icons.flag_outlined),
          validator: Validators.duzina(min: 2, max: 100),
        ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final provider = context.read<DrzavaProvider>();
        final request = {'naziv': _naziv.text.trim()};
        nova ? await provider.insert(request) : await provider.update(widget.drzava!.id, request);
      }, nova ? 'Država "${_naziv.text.trim()}" je uspješno dodana.' : 'Država je uspješno izmijenjena.'),
    );
  }
}

// =====================================================================
// GRADOVI
// =====================================================================

class GradoviScreen extends StatefulWidget {
  const GradoviScreen({super.key});

  @override
  State<GradoviScreen> createState() => _GradoviScreenState();
}

class _GradoviScreenState extends State<GradoviScreen> with ListaMixin<GradoviScreen, Grad> {
  final _naziv = TextEditingController();
  List<Drzava> _drzave = [];
  int? _drzavaId;

  @override
  void initState() {
    super.initState();
    context.read<DrzavaProvider>().get().then((v) {
      if (mounted) setState(() => _drzave = v);
    }).catchError((_) {});
  }

  @override
  Future<List<Grad>> dohvati() =>
      context.read<GradProvider>().get(filter: {'nazivGrada': _naziv.text, 'drzavaId': _drzavaId});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Gradovi',
      child: Column(children: [
        PretragaTraka(
          filteri: [
            PretragaPolje(controller: _naziv, label: 'Naziv grada', onSubmit: ucitaj),
            FilterDropdown<int>(
              label: 'Država',
              value: _drzavaId,
              items: [for (final d in _drzave) DropdownMenuItem(value: d.id, child: Text(d.naziv))],
              onChanged: (v) {
                setState(() => _drzavaId = v);
                ucitaj();
              },
            ),
          ],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const GradForma()),
          dodajTekst: 'Novi grad',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Grad')),
              DataColumn(label: Text('Država')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final g in lista)
                DataRow(cells: [
                  DataCell(Text(g.nazivGrada)),
                  DataCell(Text(g.drzavaNaziv ?? '-')),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(GradForma(grad: g)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje grada',
                      opis: 'Da li ste sigurni da želite obrisati grad "${g.nazivGrada}"?',
                      akcija: () => context.read<GradProvider>().delete(g.id),
                      porukaUspjeha: 'Grad "${g.nazivGrada}" je obrisan.',
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

class GradForma extends StatefulWidget {
  final Grad? grad;

  const GradForma({super.key, this.grad});

  @override
  State<GradForma> createState() => _GradFormaState();
}

class _GradFormaState extends State<GradForma> with FormaMixin<GradForma> {
  late final _naziv = TextEditingController(text: widget.grad?.nazivGrada);
  late int? _drzavaId = widget.grad?.drzavaId;
  List<Drzava> _drzave = [];

  @override
  void initState() {
    super.initState();
    context.read<DrzavaProvider>().get().then((v) {
      if (mounted) setState(() => _drzave = v);
    }).catchError((_) {});
  }

  @override
  Widget build(BuildContext context) {
    final novi = widget.grad == null;
    return FormaEkran(
      naslov: novi ? 'Novi grad' : 'Uredi grad',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        TextFormField(
          controller: _naziv,
          decoration: poljeDekoracija('Naziv grada *', ikona: Icons.location_city_outlined),
          validator: Validators.duzina(min: 2, max: 100),
        ),
        DropdownButtonFormField<int>(
          key: ValueKey('drzave-${_drzave.length}'),
          initialValue: _drzave.any((d) => d.id == _drzavaId) ? _drzavaId : null,
          decoration: poljeDekoracija('Država *', ikona: Icons.flag_outlined),
          items: [for (final d in _drzave) DropdownMenuItem(value: d.id, child: Text(d.naziv))],
          onChanged: (v) => setState(() => _drzavaId = v),
          validator: (v) => Validators.dropdown(v, 'državu'),
        ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final provider = context.read<GradProvider>();
        final request = {'nazivGrada': _naziv.text.trim(), 'drzavaId': _drzavaId};
        novi ? await provider.insert(request) : await provider.update(widget.grad!.id, request);
      }, novi ? 'Grad "${_naziv.text.trim()}" je uspješno dodan.' : 'Grad je uspješno izmijenjen.'),
    );
  }
}

// =====================================================================
// FIRME (prevoznici)
// =====================================================================

class FirmeScreen extends StatefulWidget {
  const FirmeScreen({super.key});

  @override
  State<FirmeScreen> createState() => _FirmeScreenState();
}

class _FirmeScreenState extends State<FirmeScreen> with ListaMixin<FirmeScreen, Firma> {
  final _naziv = TextEditingController();

  @override
  Future<List<Firma>> dohvati() => context.read<FirmaProvider>().get(filter: {'naziv': _naziv.text});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Firme',
      child: Column(children: [
        PretragaTraka(
          filteri: [PretragaPolje(controller: _naziv, label: 'Naziv firme', onSubmit: ucitaj)],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const FirmaForma()),
          dodajTekst: 'Nova firma',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Naziv')),
              DataColumn(label: Text('Grad')),
              DataColumn(label: Text('Adresa')),
              DataColumn(label: Text('Žiro račun')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final f in lista)
                DataRow(cells: [
                  DataCell(Text(f.naziv)),
                  DataCell(Text(f.gradNaziv ?? '-')),
                  DataCell(Text(f.adresa)),
                  DataCell(Text(f.brojZiroracuna)),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(FirmaForma(firma: f)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje firme',
                      opis: 'Da li ste sigurni da želite obrisati firmu "${f.naziv}"?',
                      akcija: () => context.read<FirmaProvider>().delete(f.id),
                      porukaUspjeha: 'Firma "${f.naziv}" je obrisana.',
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

class FirmaForma extends StatefulWidget {
  final Firma? firma;

  const FirmaForma({super.key, this.firma});

  @override
  State<FirmaForma> createState() => _FirmaFormaState();
}

class _FirmaFormaState extends State<FirmaForma> with FormaMixin<FirmaForma> {
  late final _naziv = TextEditingController(text: widget.firma?.naziv);
  late final _adresa = TextEditingController(text: widget.firma?.adresa);
  late final _racun = TextEditingController(text: widget.firma?.brojZiroracuna);
  late int? _gradId = widget.firma?.gradId;
  List<Grad> _gradovi = [];

  @override
  void initState() {
    super.initState();
    context.read<GradProvider>().get().then((v) {
      if (mounted) setState(() => _gradovi = v);
    }).catchError((_) {});
  }

  @override
  Widget build(BuildContext context) {
    final nova = widget.firma == null;
    return FormaEkran(
      naslov: nova ? 'Nova firma' : 'Uredi firmu',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        TextFormField(
          controller: _naziv,
          decoration: poljeDekoracija('Naziv firme *', ikona: Icons.business_outlined),
          validator: Validators.duzina(min: 2, max: 100),
        ),
        DropdownButtonFormField<int>(
          key: ValueKey('gradovi-${_gradovi.length}'),
          initialValue: _gradovi.any((g) => g.id == _gradId) ? _gradId : null,
          decoration: poljeDekoracija('Grad *', ikona: Icons.location_city_outlined),
          items: [
            for (final g in _gradovi)
              DropdownMenuItem(value: g.id, child: Text('${g.nazivGrada} (${g.drzavaNaziv ?? ''})')),
          ],
          onChanged: (v) => setState(() => _gradId = v),
          validator: (v) => Validators.dropdown(v, 'grad'),
        ),
        TextFormField(
          controller: _adresa,
          decoration: poljeDekoracija('Adresa *', ikona: Icons.home_work_outlined),
          validator: Validators.duzina(min: 3, max: 200),
        ),
        TextFormField(
          controller: _racun,
          decoration: poljeDekoracija('Broj žiro računa *',
              ikona: Icons.account_balance_outlined, hint: 'npr. 1610000012345678'),
          validator: Validators.ziroRacun,
        ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final provider = context.read<FirmaProvider>();
        final request = {
          'naziv': _naziv.text.trim(),
          'gradId': _gradId,
          'adresa': _adresa.text.trim(),
          'brojZiroracuna': _racun.text.trim(),
        };
        nova ? await provider.insert(request) : await provider.update(widget.firma!.id, request);
      }, nova ? 'Firma "${_naziv.text.trim()}" je uspješno dodana.' : 'Firma je uspješno izmijenjena.'),
    );
  }
}

// =====================================================================
// PREVOZ
// =====================================================================

class PrevozScreen extends StatefulWidget {
  const PrevozScreen({super.key});

  @override
  State<PrevozScreen> createState() => _PrevozScreenState();
}

class _PrevozScreenState extends State<PrevozScreen> with ListaMixin<PrevozScreen, Prevoz> {
  final _tip = TextEditingController();

  @override
  Future<List<Prevoz>> dohvati() => context.read<PrevozProvider>().get(filter: {'tipPrevoza': _tip.text});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Prevoz',
      child: Column(children: [
        PretragaTraka(
          filteri: [PretragaPolje(controller: _tip, label: 'Tip prevoza', onSubmit: ucitaj)],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const PrevozForma()),
          dodajTekst: 'Novi prevoz',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Tip prevoza')),
              DataColumn(label: Text('Firma')),
              DataColumn(label: Text('Broj mjesta'), numeric: true),
              DataColumn(label: Text('Cijena po mjestu'), numeric: true),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final p in lista)
                DataRow(cells: [
                  DataCell(Text(p.tipPrevoza)),
                  DataCell(Text(p.firmaNaziv ?? '-')),
                  DataCell(Text('${p.brojMjesta}')),
                  DataCell(Text(formatKM(p.cijenaPoMjestu))),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(PrevozForma(prevoz: p)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje prevoza',
                      opis: 'Da li ste sigurni da želite obrisati prevoz "${p.prikaz}"?',
                      akcija: () => context.read<PrevozProvider>().delete(p.id),
                      porukaUspjeha: 'Prevoz je obrisan.',
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

class PrevozForma extends StatefulWidget {
  final Prevoz? prevoz;

  const PrevozForma({super.key, this.prevoz});

  @override
  State<PrevozForma> createState() => _PrevozFormaState();
}

class _PrevozFormaState extends State<PrevozForma> with FormaMixin<PrevozForma> {
  late final _tip = TextEditingController(text: widget.prevoz?.tipPrevoza);
  late final _mjesta = TextEditingController(text: widget.prevoz?.brojMjesta.toString());
  late final _cijena = TextEditingController(text: widget.prevoz?.cijenaPoMjestu.toStringAsFixed(2));
  late int? _firmaId = widget.prevoz?.firmaId;
  List<Firma> _firme = [];

  @override
  void initState() {
    super.initState();
    context.read<FirmaProvider>().get().then((v) {
      if (mounted) setState(() => _firme = v);
    }).catchError((_) {});
  }

  @override
  Widget build(BuildContext context) {
    final novi = widget.prevoz == null;
    return FormaEkran(
      naslov: novi ? 'Novi prevoz' : 'Uredi prevoz',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        TextFormField(
          controller: _tip,
          decoration: poljeDekoracija('Tip prevoza *', ikona: Icons.directions_bus_outlined, hint: 'npr. Autobus, Avion'),
          validator: Validators.duzina(min: 2, max: 50),
        ),
        DropdownButtonFormField<int>(
          key: ValueKey('firme-${_firme.length}'),
          initialValue: _firme.any((f) => f.id == _firmaId) ? _firmaId : null,
          decoration: poljeDekoracija('Firma (prevoznik) *', ikona: Icons.business_outlined),
          items: [for (final f in _firme) DropdownMenuItem(value: f.id, child: Text(f.naziv))],
          onChanged: (v) => setState(() => _firmaId = v),
          validator: (v) => Validators.dropdown(v, 'firmu'),
        ),
        TextFormField(
          controller: _mjesta,
          decoration: poljeDekoracija('Broj mjesta *', ikona: Icons.event_seat_outlined),
          validator: Validators.cijeliBroj(min: 1, max: 1000),
        ),
        TextFormField(
          controller: _cijena,
          decoration: poljeDekoracija('Cijena po mjestu (KM) *', ikona: Icons.payments_outlined, hint: 'npr. 35.50'),
          validator: Validators.decimalniBroj(max: 100000),
        ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final provider = context.read<PrevozProvider>();
        final request = {
          'tipPrevoza': _tip.text.trim(),
          'firmaId': _firmaId,
          'brojMjesta': int.parse(_mjesta.text.trim()),
          'cijenaPoMjestu': Validators.parseDouble(_cijena.text),
        };
        novi ? await provider.insert(request) : await provider.update(widget.prevoz!.id, request);
      }, novi ? 'Prevoz je uspješno dodan.' : 'Prevoz je uspješno izmijenjen.'),
    );
  }
}

// =====================================================================
// SMJEŠTAJ
// =====================================================================

class SmjestajScreen extends StatefulWidget {
  const SmjestajScreen({super.key});

  @override
  State<SmjestajScreen> createState() => _SmjestajScreenState();
}

class _SmjestajScreenState extends State<SmjestajScreen> with ListaMixin<SmjestajScreen, Smjestaj> {
  final _naziv = TextEditingController();

  @override
  Future<List<Smjestaj>> dohvati() =>
      context.read<SmjestajProvider>().get(filter: {'nazivSmjestaja': _naziv.text});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Smještaj',
      child: Column(children: [
        PretragaTraka(
          filteri: [PretragaPolje(controller: _naziv, label: 'Naziv smještaja', onSubmit: ucitaj)],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const SmjestajForma()),
          dodajTekst: 'Novi smještaj',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Slika')),
              DataColumn(label: Text('Naziv')),
              DataColumn(label: Text('Tip sobe')),
              DataColumn(label: Text('Cijena noćenja'), numeric: true),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final s in lista)
                DataRow(cells: [
                  DataCell(slikaIliIkona(s.slika, size: 40, ikona: Icons.hotel_outlined)),
                  DataCell(Text(s.nazivSmjestaja)),
                  DataCell(Text(s.tipSobe)),
                  DataCell(Text(formatKM(s.cijenaNocenja))),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(SmjestajForma(smjestaj: s)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje smještaja',
                      opis: 'Da li ste sigurni da želite obrisati smještaj "${s.nazivSmjestaja}"?',
                      akcija: () => context.read<SmjestajProvider>().delete(s.id),
                      porukaUspjeha: 'Smještaj "${s.nazivSmjestaja}" je obrisan.',
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

class SmjestajForma extends StatefulWidget {
  final Smjestaj? smjestaj;

  const SmjestajForma({super.key, this.smjestaj});

  @override
  State<SmjestajForma> createState() => _SmjestajFormaState();
}

class _SmjestajFormaState extends State<SmjestajForma> with FormaMixin<SmjestajForma> {
  late final _naziv = TextEditingController(text: widget.smjestaj?.nazivSmjestaja);
  late final _opis = TextEditingController(text: widget.smjestaj?.opisSmjestaja);
  late final _cijena = TextEditingController(text: widget.smjestaj?.cijenaNocenja.toStringAsFixed(2));
  late final _tipSobe = TextEditingController(text: widget.smjestaj?.tipSobe);
  late String? _slika = widget.smjestaj?.slika;
  String? _novaSlika;

  @override
  Widget build(BuildContext context) {
    final novi = widget.smjestaj == null;
    return FormaEkran(
      naslov: novi ? 'Novi smještaj' : 'Uredi smještaj',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        TextFormField(
          controller: _naziv,
          decoration: poljeDekoracija('Naziv smještaja *', ikona: Icons.hotel_outlined),
          validator: Validators.duzina(min: 2, max: 100),
        ),
        TextFormField(
          controller: _tipSobe,
          decoration: poljeDekoracija('Tip sobe *', ikona: Icons.bed_outlined, hint: 'npr. Dvokrevetna'),
          validator: Validators.duzina(min: 2, max: 50),
        ),
        TextFormField(
          controller: _cijena,
          decoration: poljeDekoracija('Cijena noćenja (KM) *', ikona: Icons.payments_outlined, hint: 'npr. 80'),
          validator: Validators.decimalniBroj(max: 100000),
        ),
        TextFormField(
          controller: _opis,
          maxLines: 3,
          decoration: poljeDekoracija('Opis', ikona: Icons.notes_outlined),
          validator: Validators.duzina(min: 0, max: 1000, obavezno: false),
        ),
        SlikaPolje(
          label: 'Slika',
          initialValue: _slika,
          onChanged: (v) => setState(() {
            _slika = v;
            _novaSlika = v;
          }),
        ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final provider = context.read<SmjestajProvider>();
        final request = {
          'nazivSmjestaja': _naziv.text.trim(),
          'tipSobe': _tipSobe.text.trim(),
          'cijenaNocenja': Validators.parseDouble(_cijena.text),
          'opisSmjestaja': _opis.text.trim(),
          // slika se salje samo ako je promijenjena (API zadrzava postojecu)
          'slika': _novaSlika,
        };
        novi ? await provider.insert(request) : await provider.update(widget.smjestaj!.id, request);
      }, novi ? 'Smještaj "${_naziv.text.trim()}" je uspješno dodan.' : 'Smještaj je uspješno izmijenjen.'),
    );
  }
}

// =====================================================================
// ULOGE
// =====================================================================

class UlogeScreen extends StatefulWidget {
  const UlogeScreen({super.key});

  @override
  State<UlogeScreen> createState() => _UlogeScreenState();
}

class _UlogeScreenState extends State<UlogeScreen> with ListaMixin<UlogeScreen, Uloga> {
  final _naziv = TextEditingController();

  @override
  Future<List<Uloga>> dohvati() => context.read<UlogaProvider>().get(filter: {'naziv': _naziv.text});

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Uloge',
      child: Column(children: [
        PretragaTraka(
          filteri: [PretragaPolje(controller: _naziv, label: 'Naziv uloge', onSubmit: ucitaj)],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const UlogaForma()),
          dodajTekst: 'Nova uloga',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Naziv')),
              DataColumn(label: Text('Opis')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final u in lista)
                DataRow(cells: [
                  DataCell(Text(u.naziv)),
                  DataCell(Text(u.opis ?? '')),
                  DataCell(RedAkcije(
                    onUredi: () => otvoriFormu(UlogaForma(uloga: u)),
                    onObrisi: () => obrisi(
                      naslov: 'Brisanje uloge',
                      opis: 'Da li ste sigurni da želite obrisati ulogu "${u.naziv}"?',
                      akcija: () => context.read<UlogaProvider>().delete(u.id),
                      porukaUspjeha: 'Uloga "${u.naziv}" je obrisana.',
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

class UlogaForma extends StatefulWidget {
  final Uloga? uloga;

  const UlogaForma({super.key, this.uloga});

  @override
  State<UlogaForma> createState() => _UlogaFormaState();
}

class _UlogaFormaState extends State<UlogaForma> with FormaMixin<UlogaForma> {
  late final _naziv = TextEditingController(text: widget.uloga?.naziv);
  late final _opis = TextEditingController(text: widget.uloga?.opis);

  @override
  Widget build(BuildContext context) {
    final nova = widget.uloga == null;
    return FormaEkran(
      naslov: nova ? 'Nova uloga' : 'Uredi ulogu',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        TextFormField(
          controller: _naziv,
          decoration: poljeDekoracija('Naziv uloge *', ikona: Icons.admin_panel_settings_outlined),
          validator: Validators.duzina(min: 2, max: 50),
        ),
        TextFormField(
          controller: _opis,
          decoration: poljeDekoracija('Opis', ikona: Icons.notes_outlined),
          validator: Validators.duzina(min: 0, max: 200, obavezno: false),
        ),
      ],
      onSacuvaj: () => sacuvaj(() async {
        final provider = context.read<UlogaProvider>();
        final request = {'naziv': _naziv.text.trim(), 'opis': _opis.text.trim()};
        nova ? await provider.insert(request) : await provider.update(widget.uloga!.id, request);
      }, nova ? 'Uloga "${_naziv.text.trim()}" je uspješno dodana.' : 'Uloga je uspješno izmijenjena.'),
    );
  }
}
