import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/rezervacija.dart';
import '../providers/auth_provider.dart';
import '../providers/providers.dart';
import '../utils/dialogs.dart';
import '../utils/formatters.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';
import 'login_screen.dart';

class ProfilScreen extends StatelessWidget {
  const ProfilScreen({super.key});

  Future<void> _odjava(BuildContext context) async {
    final ok = await potvrdi(context, naslov: 'Odjava', poruka: 'Da li se želite odjaviti?', potvrdaTekst: 'Odjavi se', opasno: false);
    if (!ok || !context.mounted) return;
    context.read<AuthProvider>().logout();
    Navigator.of(context).pushAndRemoveUntil(MaterialPageRoute(builder: (_) => const LoginScreen()), (_) => false);
  }

  Future<void> _obrisiNalog(BuildContext context) async {
    final ok = await potvrdi(context,
        naslov: 'Brisanje naloga',
        poruka: 'Da li sigurno želite trajno obrisati svoj nalog? Ova akcija je nepovratna.',
        potvrdaTekst: 'Obriši nalog');
    if (!ok || !context.mounted) return;
    try {
      await context.read<KorisnikProvider>().delete(AuthProvider.korisnikId);
      if (!context.mounted) return;
      context.read<AuthProvider>().logout();
      prikaziUspjeh(context, 'Vaš nalog je obrisan.');
      Navigator.of(context).pushAndRemoveUntil(MaterialPageRoute(builder: (_) => const LoginScreen()), (_) => false);
    } catch (e) {
      if (context.mounted) await prikaziGresku(context, e);
    }
  }

  @override
  Widget build(BuildContext context) {
    final k = context.watch<AuthProvider>().korisnik;
    if (k == null) return const SizedBox.shrink();

    return ListView(padding: const EdgeInsets.all(16), children: [
      Center(
        child: CircleAvatar(
          radius: 40,
          child: Text('${k.ime.isNotEmpty ? k.ime[0] : ''}${k.prezime.isNotEmpty ? k.prezime[0] : ''}',
              style: const TextStyle(fontSize: 28)),
        ),
      ),
      const SizedBox(height: 12),
      Center(child: Text(k.imePrezime, style: Theme.of(context).textTheme.titleLarge)),
      const SizedBox(height: 16),
      InfoRed(Icons.account_circle_outlined, 'Korisničko ime', k.korisnickoIme),
      InfoRed(Icons.email_outlined, 'Email', k.email),
      InfoRed(Icons.phone_outlined, 'Telefon', k.telefon),
      const Divider(height: 32),
      ListTile(
        leading: const Icon(Icons.edit_outlined),
        title: const Text('Vidi i uredi profil'),
        trailing: const Icon(Icons.chevron_right),
        onTap: () => Navigator.of(context).push(MaterialPageRoute(builder: (_) => const ProfilFormaScreen())),
      ),
      ListTile(
        leading: const Icon(Icons.receipt_long_outlined),
        title: const Text('Moje uplate'),
        trailing: const Icon(Icons.chevron_right),
        onTap: () => Navigator.of(context).push(MaterialPageRoute(builder: (_) => const UplateScreen())),
      ),
      ListTile(
        leading: const Icon(Icons.logout),
        title: const Text('Odjava'),
        onTap: () => _odjava(context),
      ),
      ListTile(
        leading: Icon(Icons.delete_forever_outlined, color: Colors.red.shade700),
        title: Text('Obriši nalog', style: TextStyle(color: Colors.red.shade700)),
        onTap: () => _obrisiNalog(context),
      ),
    ]);
  }
}

/// Izmjena licnih podataka. Nova lozinka se mijenja samo uz potvrdu trenutne lozinke.
class ProfilFormaScreen extends StatefulWidget {
  const ProfilFormaScreen({super.key});

  @override
  State<ProfilFormaScreen> createState() => _ProfilFormaScreenState();
}

class _ProfilFormaScreenState extends State<ProfilFormaScreen> with FormaMixin<ProfilFormaScreen> {
  final _k = AuthProvider.trenutniKorisnik!;
  late final _ime = TextEditingController(text: _k.ime);
  late final _prezime = TextEditingController(text: _k.prezime);
  late final _email = TextEditingController(text: _k.email);
  late final _telefon = TextEditingController(text: _k.telefon);
  late final _korisnickoIme = TextEditingController(text: _k.korisnickoIme);
  final _stara = TextEditingController();
  final _nova = TextEditingController();
  final _potvrda = TextEditingController();
  bool _promijeniLozinku = false;

  @override
  Widget build(BuildContext context) {
    const razmak = SizedBox(height: 14);
    return Scaffold(
      appBar: AppBar(title: const Text('Uredi profil')),
      body: Form(
        key: formKey,
        autovalidateMode: AutovalidateMode.onUserInteraction,
        child: ListView(padding: const EdgeInsets.all(16), children: [
          TextFormField(
            controller: _ime,
            decoration: poljeDekoracija('Ime *', ikona: Icons.person_outline),
            validator: Validators.duzina(min: 2, max: 50),
          ),
          razmak,
          TextFormField(
            controller: _prezime,
            decoration: poljeDekoracija('Prezime *', ikona: Icons.person_outline),
            validator: Validators.duzina(min: 2, max: 50),
          ),
          razmak,
          TextFormField(
            controller: _email,
            keyboardType: TextInputType.emailAddress,
            decoration: poljeDekoracija('Email *', ikona: Icons.email_outlined),
            validator: Validators.email,
          ),
          razmak,
          TextFormField(
            controller: _telefon,
            keyboardType: TextInputType.phone,
            decoration: poljeDekoracija('Telefon *', ikona: Icons.phone_outlined),
            validator: Validators.telefon,
          ),
          razmak,
          TextFormField(
            controller: _korisnickoIme,
            decoration: poljeDekoracija('Korisničko ime *', ikona: Icons.account_circle_outlined),
            validator: Validators.duzina(min: 4, max: 50),
          ),
          CheckboxListTile(
            contentPadding: EdgeInsets.zero,
            title: const Text('Promijeni lozinku'),
            value: _promijeniLozinku,
            onChanged: (v) => setState(() => _promijeniLozinku = v ?? false),
          ),
          if (_promijeniLozinku) ...[
            TextFormField(
              controller: _stara,
              obscureText: true,
              decoration: poljeDekoracija('Trenutna lozinka *', ikona: Icons.lock_clock_outlined),
              validator: (v) => Validators.required(v, 'Unesite trenutnu lozinku.'),
            ),
            razmak,
            TextFormField(
              controller: _nova,
              obscureText: true,
              decoration: poljeDekoracija('Nova lozinka *', ikona: Icons.lock_outline, helper: 'Najmanje 4 znaka.'),
              validator: (v) => Validators.lozinka(v),
            ),
            razmak,
            TextFormField(
              controller: _potvrda,
              obscureText: true,
              decoration: poljeDekoracija('Potvrda nove lozinke *', ikona: Icons.lock_outline),
              validator: (v) => v != _nova.text ? 'Lozinka i potvrda se ne podudaraju.' : null,
            ),
          ],
          const SizedBox(height: 20),
          FilledButton.icon(
            style: FilledButton.styleFrom(minimumSize: const Size.fromHeight(48)),
            onPressed: spasavanje
                ? null
                : () => sacuvaj(() async {
                      final azuriran = await context.read<KorisnikProvider>().update(_k.id, {
                        'ime': _ime.text.trim(),
                        'prezime': _prezime.text.trim(),
                        'email': _email.text.trim(),
                        'telefon': _telefon.text.trim(),
                        'korisnickoIme': _korisnickoIme.text.trim(),
                        'status': true,
                        'uloge': _k.ulogeIds,
                        'staraLozinka': _promijeniLozinku ? _stara.text : null,
                        'password': _promijeniLozinku ? _nova.text : null,
                        'passwordConfirmation': _promijeniLozinku ? _potvrda.text : null,
                      });
                      if (!mounted) return;
                      context
                          .read<AuthProvider>()
                          .azurirajKorisnika(azuriran, novaLozinka: _promijeniLozinku ? _nova.text : null);
                    }, 'Vaš profil je uspješno ažuriran.'),
            icon: const Icon(Icons.save_outlined),
            label: const Text('Sačuvaj'),
          ),
        ]),
      ),
    );
  }
}

/// Pregled uplata klijenta sa pretragom po periodu.
class UplateScreen extends StatefulWidget {
  const UplateScreen({super.key});

  @override
  State<UplateScreen> createState() => _UplateScreenState();
}

class _UplateScreenState extends State<UplateScreen> {
  List<Uplata> _lista = [];
  DateTime? _od;
  DateTime? _do;
  bool _ucitavanje = true;
  String? _greska;

  @override
  void initState() {
    super.initState();
    _ucitaj();
  }

  Future<void> _ucitaj() async {
    setState(() {
      _ucitavanje = true;
      _greska = null;
    });
    try {
      final lista = await context.read<UplataProvider>().get(filter: {'datumOd': _od, 'datumDo': _do});
      if (mounted) setState(() => _lista = lista);
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    } finally {
      if (mounted) setState(() => _ucitavanje = false);
    }
  }

  Future<void> _odaberi(bool od) async {
    final d = await showDatePicker(
      context: context,
      initialDate: (od ? _od : _do) ?? DateTime.now(),
      firstDate: DateTime(2000),
      lastDate: DateTime(2100),
    );
    if (d == null) return;
    setState(() {
      if (od) {
        _od = d;
      } else {
        _do = d;
      }
    });
    _ucitaj();
  }

  @override
  Widget build(BuildContext context) {
    final ukupno = _lista.fold<double>(0, (s, u) => s + u.iznos);
    return Scaffold(
      appBar: AppBar(title: const Text('Moje uplate')),
      body: Column(children: [
        Padding(
          padding: const EdgeInsets.all(12),
          child: Row(children: [
            Expanded(
              child: OutlinedButton.icon(
                icon: const Icon(Icons.calendar_month_outlined, size: 18),
                label: Text(_od == null ? 'Od datuma' : formatDatum(_od)),
                onPressed: () => _odaberi(true),
              ),
            ),
            const SizedBox(width: 8),
            Expanded(
              child: OutlinedButton.icon(
                icon: const Icon(Icons.calendar_month_outlined, size: 18),
                label: Text(_do == null ? 'Do datuma' : formatDatum(_do)),
                onPressed: () => _odaberi(false),
              ),
            ),
            if (_od != null || _do != null)
              IconButton(
                tooltip: 'Poništi filter',
                icon: const Icon(Icons.clear),
                onPressed: () {
                  setState(() {
                    _od = null;
                    _do = null;
                  });
                  _ucitaj();
                },
              ),
          ]),
        ),
        if (_lista.isNotEmpty)
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16),
            child: Align(
              alignment: Alignment.centerRight,
              child: Text('Ukupno: ${formatKM(ukupno)}', style: Theme.of(context).textTheme.titleSmall),
            ),
          ),
        Expanded(
          child: StanjeListe(
            ucitavanje: _ucitavanje,
            greska: _greska,
            prazno: _lista.isEmpty,
            porukaPrazno: 'Nema uplata za odabrani period.',
            onPonovo: _ucitaj,
            child: ListView.builder(
              padding: const EdgeInsets.all(12),
              itemCount: _lista.length,
              itemBuilder: (_, i) {
                final u = _lista[i];
                return Card(
                  child: ListTile(
                    leading: const Icon(Icons.receipt_outlined),
                    title: Text(u.putovanjeNaziv ?? u.rezervacijaNaziv ?? 'Uplata'),
                    subtitle: Text('${formatDatumVrijeme(u.datum)} · ${u.nacinPlacanja ?? ''}'),
                    trailing: Text(formatKM(u.iznos), style: const TextStyle(fontWeight: FontWeight.bold)),
                  ),
                );
              },
            ),
          ),
        ),
      ]),
    );
  }
}
