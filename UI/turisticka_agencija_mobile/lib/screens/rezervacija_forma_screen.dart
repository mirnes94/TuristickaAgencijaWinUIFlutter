import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/putovanje.dart';
import '../providers/auth_provider.dart';
import '../providers/providers.dart';
import '../utils/dialogs.dart';
import '../utils/formatters.dart';
import '../utils/online_placanje.dart';
import '../utils/validators.dart';
import '../widgets/common.dart';

enum NacinPlacanja { polovina, cijeliIznos, kasnije }

/// Online rezervacija: broj osoba, napomena i placanje polovine ili cijelog iznosa (Stripe).
class RezervacijaFormaScreen extends StatefulWidget {
  final Putovanje putovanje;

  const RezervacijaFormaScreen({super.key, required this.putovanje});

  @override
  State<RezervacijaFormaScreen> createState() => _RezervacijaFormaScreenState();
}

class _RezervacijaFormaScreenState extends State<RezervacijaFormaScreen> {
  final _formKey = GlobalKey<FormState>();
  final _brojOsoba = TextEditingController(text: '1');
  final _napomena = TextEditingController();
  NacinPlacanja _nacin = NacinPlacanja.polovina;
  bool _spasavanje = false;

  int get _osoba => int.tryParse(_brojOsoba.text.trim()) ?? 0;
  double get _ukupno => widget.putovanje.cijenaPutovanja * _osoba;
  double get _zaPlatitiSada => switch (_nacin) {
        NacinPlacanja.polovina => double.parse((_ukupno / 2).toStringAsFixed(2)),
        NacinPlacanja.cijeliIznos => _ukupno,
        NacinPlacanja.kasnije => 0.0,
      };

  Future<void> _rezervisi() async {
    if (!_formKey.currentState!.validate()) return;

    final p = widget.putovanje;
    final ok = await potvrdi(
      context,
      naslov: 'Potvrda rezervacije',
      poruka: '${p.nazivPutovanja}\n'
          'Polazak: ${formatDatum(p.datumPolaska)}\n'
          'Broj osoba: $_osoba\n'
          'Ukupna cijena: ${formatKM(_ukupno)}\n'
          '${_zaPlatitiSada > 0 ? 'Sada plaćate: ${formatKM(_zaPlatitiSada)}' : 'Uplatu ćete izvršiti kasnije.'}\n\n'
          'Cijeli iznos je potrebno uplatiti najkasnije 48 sati prije polaska.',
      potvrdaTekst: 'Rezerviši',
      opasno: false,
    );
    if (!ok || !mounted) return;

    setState(() => _spasavanje = true);
    try {
      final rezervacija = await context.read<RezervacijaProvider>().insert({
        'ime': p.nazivPutovanja,
        'korisnikId': AuthProvider.korisnikId,
        'putovanjeId': p.id,
        'brojOsoba': _osoba,
        'napomena': _napomena.text.trim(),
      });

      var poruka = 'Rezervacija je kreirana i ima status "U obradi".';
      if (_zaPlatitiSada > 0) {
        // Rezervacija je vec kreirana - greska u placanju ne smije ostaviti formu otvorenom
        // (ponovni klik bi kreirao duplu rezervaciju).
        try {
          final placeno = await OnlinePlacanje.platiRezervaciju(rezervacijaId: rezervacija.id, iznos: _zaPlatitiSada);
          poruka = placeno
              ? 'Rezervacija je kreirana i uplata od ${formatKM(_zaPlatitiSada)} je uspješno izvršena.'
              : 'Rezervacija je kreirana. Plaćanje ste prekinuli - uplatu možete izvršiti u "Moje rezervacije".';
        } catch (e) {
          if (mounted) {
            await prikaziGresku(context,
                'Rezervacija je kreirana, ali plaćanje nije uspjelo:\n$e\n\nUplatu možete izvršiti u "Moje rezervacije".');
          }
        }
      }
      if (!mounted) return;
      prikaziUspjeh(context, poruka);
      Navigator.of(context).pop(true);
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    } finally {
      if (mounted) setState(() => _spasavanje = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    final p = widget.putovanje;
    return Scaffold(
      appBar: AppBar(title: const Text('Rezervacija')),
      body: Form(
        key: _formKey,
        autovalidateMode: AutovalidateMode.onUserInteraction,
        child: ListView(padding: const EdgeInsets.all(16), children: [
          Text(p.nazivPutovanja, style: Theme.of(context).textTheme.titleLarge),
          const SizedBox(height: 8),
          InfoRed(Icons.flight_takeoff, 'Polazak', formatDatum(p.datumPolaska)),
          InfoRed(Icons.payments_outlined, 'Cijena', '${formatKM(p.cijenaPutovanja)} po osobi'),
          InfoRed(Icons.person_outline, 'Na ime', AuthProvider.trenutniKorisnik?.imePrezime ?? '-'),
          const SizedBox(height: 16),
          TextFormField(
            controller: _brojOsoba,
            keyboardType: TextInputType.number,
            decoration: poljeDekoracija('Broj osoba *', ikona: Icons.group_outlined, helper: 'Od 1 do 10 osoba.'),
            validator: Validators.cijeliBroj(min: 1, max: 10),
            onChanged: (_) => setState(() {}),
          ),
          const SizedBox(height: 14),
          TextFormField(
            controller: _napomena,
            maxLines: 3,
            decoration: poljeDekoracija('Napomena', ikona: Icons.notes_outlined),
            validator: Validators.duzina(min: 0, max: 500, obavezno: false),
          ),
          const SizedBox(height: 16),
          Text('Plaćanje', style: Theme.of(context).textTheme.titleMedium),
          _opcija(NacinPlacanja.polovina, 'Polovina iznosa (online)', null),
          _opcija(NacinPlacanja.cijeliIznos, 'Cijeli iznos (online)', null),
          _opcija(NacinPlacanja.kasnije, 'Platit ću kasnije', 'Online iz "Moje rezervacije" ili gotovinski u poslovnici.'),
          const Divider(),
          InfoRed(Icons.receipt_long_outlined, 'Ukupno', _osoba > 0 ? formatKM(_ukupno) : '-'),
          InfoRed(Icons.credit_card, 'Sada plaćate', _osoba > 0 ? formatKM(_zaPlatitiSada) : '-'),
          const SizedBox(height: 20),
          FilledButton.icon(
            style: FilledButton.styleFrom(minimumSize: const Size.fromHeight(48)),
            onPressed: _spasavanje ? null : _rezervisi,
            icon: _spasavanje
                ? const SizedBox(width: 18, height: 18, child: CircularProgressIndicator(strokeWidth: 2))
                : const Icon(Icons.check),
            label: Text(_zaPlatitiSada > 0 ? 'Rezerviši i plati' : 'Rezerviši'),
          ),
        ]),
      ),
    );
  }

  Widget _opcija(NacinPlacanja nacin, String naslov, String? opis) {
    final odabrano = _nacin == nacin;
    return ListTile(
      contentPadding: EdgeInsets.zero,
      leading: Icon(odabrano ? Icons.radio_button_checked : Icons.radio_button_unchecked,
          color: odabrano ? Theme.of(context).colorScheme.primary : null),
      title: Text(naslov),
      subtitle: opis == null ? null : Text(opis),
      onTap: () => setState(() => _nacin = nacin),
    );
  }
}
