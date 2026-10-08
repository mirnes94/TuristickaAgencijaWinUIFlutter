import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../providers/auth_provider.dart';
import '../utils/dialogs.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';

class RegistracijaScreen extends StatefulWidget {
  const RegistracijaScreen({super.key});

  @override
  State<RegistracijaScreen> createState() => _RegistracijaScreenState();
}

class _RegistracijaScreenState extends State<RegistracijaScreen> {
  final _formKey = GlobalKey<FormState>();
  final _ime = TextEditingController();
  final _prezime = TextEditingController();
  final _email = TextEditingController();
  final _telefon = TextEditingController();
  final _korisnickoIme = TextEditingController();
  final _lozinka = TextEditingController();
  final _potvrda = TextEditingController();
  bool _spasavanje = false;

  Future<void> _registruj() async {
    if (!_formKey.currentState!.validate()) return;
    setState(() => _spasavanje = true);
    try {
      await context.read<AuthProvider>().registracija({
        'ime': _ime.text.trim(),
        'prezime': _prezime.text.trim(),
        'email': _email.text.trim(),
        'telefon': _telefon.text.trim(),
        'korisnickoIme': _korisnickoIme.text.trim(),
        'password': _lozinka.text,
        'passwordConfirmation': _potvrda.text,
        'status': false,
        'uloge': <int>[],
      });
      if (!mounted) return;
      await showDialog<void>(
        context: context,
        builder: (ctx) => AlertDialog(
          icon: const Icon(Icons.mark_email_read_outlined, color: Colors.green),
          title: const Text('Registracija uspješna'),
          content: Text('Na adresu ${_email.text.trim()} poslan je link za aktivaciju naloga. '
              'Nakon aktivacije se možete prijaviti.'),
          actions: [FilledButton(onPressed: () => Navigator.pop(ctx), child: const Text('U redu'))],
        ),
      );
      if (mounted) Navigator.of(context).pop();
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    } finally {
      if (mounted) setState(() => _spasavanje = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    const razmak = SizedBox(height: 14);
    return Scaffold(
      appBar: AppBar(title: const Text('Registracija')),
      body: Form(
        key: _formKey,
        autovalidateMode: AutovalidateMode.onUserInteraction,
        child: ListView(padding: const EdgeInsets.all(20), children: [
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
            decoration: poljeDekoracija('Email *', ikona: Icons.email_outlined, hint: 'ime@domena.com'),
            validator: Validators.email,
          ),
          razmak,
          TextFormField(
            controller: _telefon,
            keyboardType: TextInputType.phone,
            decoration: poljeDekoracija('Telefon *', ikona: Icons.phone_outlined, hint: '061123456'),
            validator: Validators.telefon,
          ),
          razmak,
          TextFormField(
            controller: _korisnickoIme,
            decoration: poljeDekoracija('Korisničko ime *', ikona: Icons.account_circle_outlined),
            validator: Validators.duzina(min: 4, max: 50),
          ),
          razmak,
          TextFormField(
            controller: _lozinka,
            obscureText: true,
            decoration: poljeDekoracija('Lozinka *', ikona: Icons.lock_outline, helper: 'Najmanje 4 znaka.'),
            validator: (v) => Validators.lozinka(v),
          ),
          razmak,
          TextFormField(
            controller: _potvrda,
            obscureText: true,
            decoration: poljeDekoracija('Potvrda lozinke *', ikona: Icons.lock_outline),
            validator: (v) => v != _lozinka.text ? 'Lozinka i potvrda se ne podudaraju.' : null,
          ),
          const SizedBox(height: 24),
          SizedBox(
            height: 48,
            child: FilledButton(
              onPressed: _spasavanje ? null : _registruj,
              child: _spasavanje
                  ? const SizedBox(width: 20, height: 20, child: CircularProgressIndicator(strokeWidth: 2))
                  : const Text('Registruj se'),
            ),
          ),
        ]),
      ),
    );
  }
}
