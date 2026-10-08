import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../providers/auth_provider.dart';
import '../utils/validators.dart';
import 'home_screen.dart';
import 'registracija_screen.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final _formKey = GlobalKey<FormState>();
  final _username = TextEditingController();
  final _password = TextEditingController();
  bool _ucitavanje = false;
  bool _sakrijLozinku = true;
  String? _greska;

  @override
  void dispose() {
    _username.dispose();
    _password.dispose();
    super.dispose();
  }

  Future<void> _prijava() async {
    if (!_formKey.currentState!.validate()) return;
    setState(() {
      _ucitavanje = true;
      _greska = null;
    });
    try {
      await context.read<AuthProvider>().login(_username.text.trim(), _password.text);
      if (!mounted) return;
      Navigator.of(context).pushReplacement(MaterialPageRoute(builder: (_) => const HomeScreen()));
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    } finally {
      if (mounted) setState(() => _ucitavanje = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Center(
          child: SingleChildScrollView(
            padding: const EdgeInsets.all(24),
            child: Form(
              key: _formKey,
              child: Column(children: [
                ClipRRect(
                  borderRadius: BorderRadius.circular(20),
                  child: Image.asset('assets/tourist_agency_icon.jpg', width: 110, height: 110, fit: BoxFit.cover),
                ),
                const SizedBox(height: 16),
                Text('Turistička agencija', style: Theme.of(context).textTheme.headlineSmall),
                const SizedBox(height: 32),
                TextFormField(
                  controller: _username,
                  decoration: const InputDecoration(
                    labelText: 'Korisničko ime',
                    prefixIcon: Icon(Icons.person_outline),
                    border: OutlineInputBorder(),
                  ),
                  validator: (v) => Validators.required(v, 'Unesite korisničko ime.'),
                  textInputAction: TextInputAction.next,
                ),
                const SizedBox(height: 16),
                TextFormField(
                  controller: _password,
                  obscureText: _sakrijLozinku,
                  decoration: InputDecoration(
                    labelText: 'Lozinka',
                    prefixIcon: const Icon(Icons.lock_outline),
                    border: const OutlineInputBorder(),
                    suffixIcon: IconButton(
                      icon: Icon(_sakrijLozinku ? Icons.visibility_outlined : Icons.visibility_off_outlined),
                      onPressed: () => setState(() => _sakrijLozinku = !_sakrijLozinku),
                    ),
                  ),
                  validator: (v) => Validators.required(v, 'Unesite lozinku.'),
                  onFieldSubmitted: (_) => _prijava(),
                ),
                if (_greska != null) ...[
                  const SizedBox(height: 16),
                  Text(_greska!, textAlign: TextAlign.center, style: TextStyle(color: Theme.of(context).colorScheme.error)),
                ],
                const SizedBox(height: 24),
                SizedBox(
                  width: double.infinity,
                  height: 48,
                  child: FilledButton(
                    onPressed: _ucitavanje ? null : _prijava,
                    child: _ucitavanje
                        ? const SizedBox(width: 20, height: 20, child: CircularProgressIndicator(strokeWidth: 2))
                        : const Text('Prijava'),
                  ),
                ),
                const SizedBox(height: 12),
                TextButton(
                  onPressed: () => Navigator.of(context).push(MaterialPageRoute(builder: (_) => const RegistracijaScreen())),
                  child: const Text('Nemate nalog? Registrujte se'),
                ),
              ]),
            ),
          ),
        ),
      ),
    );
  }
}
