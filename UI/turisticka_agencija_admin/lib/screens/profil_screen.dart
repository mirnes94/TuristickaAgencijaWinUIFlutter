import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../providers/auth_provider.dart';
import '../providers/providers.dart';
import '../utils/dialogs.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';

/// Izmjena vlastitog profila. Za promjenu lozinke potrebno je potvrditi staru lozinku.
class ProfilScreen extends StatefulWidget {
  const ProfilScreen({super.key});

  @override
  State<ProfilScreen> createState() => _ProfilScreenState();
}

class _ProfilScreenState extends State<ProfilScreen> {
  final _formKey = GlobalKey<FormState>();
  final _korisnik = AuthProvider.trenutniKorisnik!;
  late final _ime = TextEditingController(text: _korisnik.ime);
  late final _prezime = TextEditingController(text: _korisnik.prezime);
  late final _email = TextEditingController(text: _korisnik.email);
  late final _telefon = TextEditingController(text: _korisnik.telefon);
  late final _korisnickoIme = TextEditingController(text: _korisnik.korisnickoIme);
  final _staraLozinka = TextEditingController();
  final _novaLozinka = TextEditingController();
  final _potvrda = TextEditingController();
  bool _promijeniLozinku = false;
  bool _spasavanje = false;

  Future<void> _sacuvaj() async {
    if (!_formKey.currentState!.validate()) return;
    setState(() => _spasavanje = true);
    try {
      final azuriran = await context.read<KorisnikProvider>().update(_korisnik.id, {
        'ime': _ime.text.trim(),
        'prezime': _prezime.text.trim(),
        'email': _email.text.trim(),
        'telefon': _telefon.text.trim(),
        'korisnickoIme': _korisnickoIme.text.trim(),
        'status': true,
        'uloge': _korisnik.ulogeIds,
        'staraLozinka': _promijeniLozinku ? _staraLozinka.text : null,
        'password': _promijeniLozinku ? _novaLozinka.text : null,
        'passwordConfirmation': _promijeniLozinku ? _potvrda.text : null,
      });
      if (!mounted) return;
      context.read<AuthProvider>().azurirajKorisnika(azuriran, novaLozinka: _promijeniLozinku ? _novaLozinka.text : null);
      prikaziUspjeh(context, 'Vaš profil je uspješno ažuriran.');
      setState(() {
        _promijeniLozinku = false;
        _staraLozinka.clear();
        _novaLozinka.clear();
        _potvrda.clear();
      });
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    } finally {
      if (mounted) setState(() => _spasavanje = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Moj profil',
      child: Center(
        child: ConstrainedBox(
          constraints: const BoxConstraints(maxWidth: 700),
          child: Form(
            key: _formKey,
            autovalidateMode: AutovalidateMode.onUserInteraction,
            child: ListView(padding: const EdgeInsets.all(24), children: [
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
              const SizedBox(height: 16),
              TextFormField(
                controller: _email,
                decoration: poljeDekoracija('Email *', ikona: Icons.email_outlined),
                validator: Validators.email,
              ),
              const SizedBox(height: 16),
              TextFormField(
                controller: _telefon,
                decoration: poljeDekoracija('Telefon *', ikona: Icons.phone_outlined),
                validator: Validators.telefon,
              ),
              const SizedBox(height: 16),
              TextFormField(
                controller: _korisnickoIme,
                decoration: poljeDekoracija('Korisničko ime *', ikona: Icons.account_circle_outlined),
                validator: Validators.duzina(min: 4, max: 50),
              ),
              const SizedBox(height: 8),
              CheckboxListTile(
                title: const Text('Promijeni lozinku'),
                value: _promijeniLozinku,
                onChanged: (v) => setState(() => _promijeniLozinku = v ?? false),
              ),
              if (_promijeniLozinku) ...[
                TextFormField(
                  controller: _staraLozinka,
                  obscureText: true,
                  decoration: poljeDekoracija('Trenutna lozinka *', ikona: Icons.lock_clock_outlined),
                  validator: (v) => Validators.required(v, 'Unesite trenutnu lozinku.'),
                ),
                const SizedBox(height: 16),
                TextFormField(
                  controller: _novaLozinka,
                  obscureText: true,
                  decoration: poljeDekoracija('Nova lozinka *', ikona: Icons.lock_outline, helper: 'Najmanje 4 znaka.'),
                  validator: (v) => Validators.lozinka(v),
                ),
                const SizedBox(height: 16),
                TextFormField(
                  controller: _potvrda,
                  obscureText: true,
                  decoration: poljeDekoracija('Potvrda nove lozinke *', ikona: Icons.lock_outline),
                  validator: (v) => v != _novaLozinka.text ? 'Lozinka i potvrda se ne podudaraju.' : null,
                ),
              ],
              const SizedBox(height: 24),
              Align(
                alignment: Alignment.centerRight,
                child: FilledButton.icon(
                  onPressed: _spasavanje ? null : _sacuvaj,
                  icon: const Icon(Icons.save_outlined),
                  label: const Text('Sačuvaj promjene'),
                ),
              ),
            ]),
          ),
        ),
      ),
    );
  }
}
