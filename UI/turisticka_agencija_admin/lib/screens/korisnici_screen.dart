import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/korisnik.dart';
import '../models/sifarnici.dart';
import '../providers/auth_provider.dart';
import '../providers/providers.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';
import '../widgets/mixins.dart';

class KorisniciScreen extends StatefulWidget {
  const KorisniciScreen({super.key});

  @override
  State<KorisniciScreen> createState() => _KorisniciScreenState();
}

class _KorisniciScreenState extends State<KorisniciScreen> with ListaMixin<KorisniciScreen, Korisnik> {
  final _ime = TextEditingController();
  final _prezime = TextEditingController();
  List<Uloga> _uloge = [];
  int? _ulogaId;

  @override
  void initState() {
    super.initState();
    context.read<UlogaProvider>().get().then((v) {
      if (mounted) setState(() => _uloge = v);
    }).catchError((_) {});
  }

  @override
  Future<List<Korisnik>> dohvati() => context.read<KorisnikProvider>().get(filter: {
        'ime': _ime.text,
        'prezime': _prezime.text,
        'ulogaId': _ulogaId,
      });

  @override
  Widget build(BuildContext context) {
    final mojId = AuthProvider.trenutniKorisnik?.id;
    return MasterScreen(
      title: 'Korisnici',
      child: Column(children: [
        PretragaTraka(
          filteri: [
            PretragaPolje(controller: _ime, label: 'Ime', onSubmit: ucitaj, width: 180),
            PretragaPolje(controller: _prezime, label: 'Prezime', onSubmit: ucitaj, width: 180),
            FilterDropdown<int>(
              label: 'Uloga',
              value: _ulogaId,
              width: 180,
              items: [for (final u in _uloge) DropdownMenuItem(value: u.id, child: Text(u.naziv))],
              onChanged: (v) {
                setState(() => _ulogaId = v);
                ucitaj();
              },
            ),
          ],
          onPretrazi: ucitaj,
          onDodaj: () => otvoriFormu(const KorisnikForma()),
          dodajTekst: 'Novi korisnik',
        ),
        Expanded(
          child: TabelaPodataka(
            ucitavanje: ucitavanje,
            greska: greska,
            kolone: const [
              DataColumn(label: Text('Ime i prezime')),
              DataColumn(label: Text('Korisničko ime')),
              DataColumn(label: Text('Email')),
              DataColumn(label: Text('Telefon')),
              DataColumn(label: Text('Uloge')),
              DataColumn(label: Text('Aktivan')),
              DataColumn(label: Text('Akcije')),
            ],
            redovi: [
              for (final k in lista)
                DataRow(cells: [
                  DataCell(Text(k.imePrezime)),
                  DataCell(Text(k.korisnickoIme)),
                  DataCell(Text(k.email)),
                  DataCell(Text(k.telefon)),
                  DataCell(Text(k.uloge.join(', '))),
                  DataCell(Icon(k.status ? Icons.check_circle : Icons.cancel_outlined,
                      color: k.status ? Colors.green : Colors.grey)),
                  DataCell(RedAkcije(
                    // svoj nalog administrator mijenja kroz "Moj profil" (uz potvrdu stare lozinke)
                    onUredi: k.id == mojId ? null : () => otvoriFormu(KorisnikForma(korisnik: k)),
                    onObrisi: k.id == mojId
                        ? null
                        : () => obrisi(
                              naslov: 'Brisanje korisnika',
                              opis: 'Da li ste sigurni da želite obrisati korisnika "${k.imePrezime}"? '
                                  'Ova akcija je nepovratna.',
                              akcija: () => context.read<KorisnikProvider>().delete(k.id),
                              porukaUspjeha: 'Korisnik "${k.imePrezime}" je obrisan.',
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

class KorisnikForma extends StatefulWidget {
  final Korisnik? korisnik;

  const KorisnikForma({super.key, this.korisnik});

  @override
  State<KorisnikForma> createState() => _KorisnikFormaState();
}

class _KorisnikFormaState extends State<KorisnikForma> with FormaMixin<KorisnikForma> {
  late final _ime = TextEditingController(text: widget.korisnik?.ime);
  late final _prezime = TextEditingController(text: widget.korisnik?.prezime);
  late final _email = TextEditingController(text: widget.korisnik?.email);
  late final _telefon = TextEditingController(text: widget.korisnik?.telefon);
  late final _korisnickoIme = TextEditingController(text: widget.korisnik?.korisnickoIme);
  final _lozinka = TextEditingController();
  final _potvrda = TextEditingController();
  late bool _status = widget.korisnik?.status ?? true;
  late final Set<int> _odabraneUloge = {...?widget.korisnik?.ulogeIds};
  late bool _promijeniLozinku = widget.korisnik == null;
  List<Uloga> _uloge = [];
  String? _greskaUloge;

  @override
  void initState() {
    super.initState();
    context.read<UlogaProvider>().get().then((v) {
      if (mounted) setState(() => _uloge = v);
    }).catchError((_) {});
  }

  @override
  Widget build(BuildContext context) {
    final novi = widget.korisnik == null;
    return FormaEkran(
      naslov: novi ? 'Novi korisnik' : 'Uredi korisnika',
      formKey: formKey,
      spasavanje: spasavanje,
      polja: [
        Row(children: [
          Expanded(
            child: TextFormField(
              controller: _ime,
              decoration: poljeDekoracija('Ime *', ikona: Icons.person_outline),
              validator: Validators.duzina(min: 2, max: 50),
            ),
          ),
          const SizedBox(width: 16),
          Expanded(
            child: TextFormField(
              controller: _prezime,
              decoration: poljeDekoracija('Prezime *'),
              validator: Validators.duzina(min: 2, max: 50),
            ),
          ),
        ]),
        TextFormField(
          controller: _email,
          decoration: poljeDekoracija('Email *', ikona: Icons.email_outlined, hint: 'ime@domena.com'),
          validator: Validators.email,
        ),
        TextFormField(
          controller: _telefon,
          decoration: poljeDekoracija('Telefon *', ikona: Icons.phone_outlined, hint: '061123456'),
          validator: Validators.telefon,
        ),
        TextFormField(
          controller: _korisnickoIme,
          decoration: poljeDekoracija('Korisničko ime *', ikona: Icons.account_circle_outlined),
          validator: Validators.duzina(min: 4, max: 50),
        ),
        InputDecorator(
          decoration: poljeDekoracija('Uloge *', ikona: Icons.admin_panel_settings_outlined)
              .copyWith(errorText: _greskaUloge),
          child: Wrap(spacing: 8, children: [
            for (final u in _uloge)
              FilterChip(
                label: Text(u.naziv),
                selected: _odabraneUloge.contains(u.id),
                onSelected: (odabrano) => setState(() {
                  odabrano ? _odabraneUloge.add(u.id) : _odabraneUloge.remove(u.id);
                  _greskaUloge = null;
                }),
              ),
          ]),
        ),
        SwitchListTile(
          title: const Text('Nalog aktivan'),
          subtitle: const Text('Neaktivan korisnik se ne može prijaviti.'),
          value: _status,
          onChanged: (v) => setState(() => _status = v),
        ),
        if (!novi)
          CheckboxListTile(
            title: const Text('Promijeni lozinku'),
            value: _promijeniLozinku,
            onChanged: (v) => setState(() {
              _promijeniLozinku = v ?? false;
              _lozinka.clear();
              _potvrda.clear();
            }),
          ),
        if (_promijeniLozinku) ...[
          TextFormField(
            controller: _lozinka,
            obscureText: true,
            decoration: poljeDekoracija(novi ? 'Lozinka *' : 'Nova lozinka *', ikona: Icons.lock_outline,
                helper: 'Najmanje 4 znaka.'),
            validator: (v) => Validators.lozinka(v),
          ),
          TextFormField(
            controller: _potvrda,
            obscureText: true,
            decoration: poljeDekoracija('Potvrda lozinke *', ikona: Icons.lock_outline),
            validator: (v) => v != _lozinka.text ? 'Lozinka i potvrda se ne podudaraju.' : null,
          ),
        ],
      ],
      onSacuvaj: () async {
        if (_odabraneUloge.isEmpty) {
          setState(() => _greskaUloge = 'Odaberite barem jednu ulogu.');
          formKey.currentState?.validate();
          return;
        }
        await sacuvaj(() async {
          final provider = context.read<KorisnikProvider>();
          final request = {
            'ime': _ime.text.trim(),
            'prezime': _prezime.text.trim(),
            'email': _email.text.trim(),
            'telefon': _telefon.text.trim(),
            'korisnickoIme': _korisnickoIme.text.trim(),
            'status': _status,
            'uloge': _odabraneUloge.toList(),
            'password': _promijeniLozinku ? _lozinka.text : null,
            'passwordConfirmation': _promijeniLozinku ? _potvrda.text : null,
          };
          if (novi) {
            await provider.insert(request);
          } else {
            await provider.update(widget.korisnik!.id, request);
          }
        }, novi
            ? 'Korisnik "${_ime.text.trim()} ${_prezime.text.trim()}" je uspješno dodan.'
            : 'Podaci korisnika su uspješno izmijenjeni.');
      },
    );
  }
}
