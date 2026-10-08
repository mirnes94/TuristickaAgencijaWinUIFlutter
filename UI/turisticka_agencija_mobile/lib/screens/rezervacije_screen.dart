import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/rezervacija.dart';
import '../providers/providers.dart';
import '../utils/dialogs.dart';
import '../utils/formatters.dart';
import '../utils/online_placanje.dart';
import '../widgets/common.dart';

/// Historija rezervacija prijavljenog klijenta.
class RezervacijeScreen extends StatefulWidget {
  const RezervacijeScreen({super.key});

  @override
  State<RezervacijeScreen> createState() => _RezervacijeScreenState();
}

class _RezervacijeScreenState extends State<RezervacijeScreen> {
  List<Rezervacija> _lista = [];
  String? _status;
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
      final lista = await context.read<RezervacijaProvider>().get(filter: {'status': _status});
      if (mounted) setState(() => _lista = lista);
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    } finally {
      if (mounted) setState(() => _ucitavanje = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Column(children: [
      SingleChildScrollView(
        scrollDirection: Axis.horizontal,
        padding: const EdgeInsets.fromLTRB(12, 12, 12, 4),
        child: Row(children: [
          for (final s in <String?>[null, ...StatusRezervacije.svi])
            Padding(
              padding: const EdgeInsets.only(right: 8),
              child: ChoiceChip(
                label: Text(s ?? 'Sve'),
                selected: _status == s,
                onSelected: (_) {
                  setState(() => _status = s);
                  _ucitaj();
                },
              ),
            ),
        ]),
      ),
      Expanded(
        child: StanjeListe(
          ucitavanje: _ucitavanje,
          greska: _greska,
          prazno: _lista.isEmpty,
          porukaPrazno: 'Nemate rezervacija.',
          onPonovo: _ucitaj,
          child: RefreshIndicator(
            onRefresh: _ucitaj,
            child: ListView.builder(
              padding: const EdgeInsets.all(12),
              itemCount: _lista.length,
              itemBuilder: (_, i) {
                final r = _lista[i];
                return Card(
                  child: ListTile(
                    title: Text(r.putovanjeNaziv ?? r.ime),
                    subtitle: Text('Polazak: ${formatDatum(r.datumPolaska)} · ${r.brojOsoba} os.\n'
                        'Uplaćeno ${formatKM(r.uplaceno)} od ${formatKM(r.ukupnaCijena)}'),
                    isThreeLine: true,
                    trailing: StatusChip(r.status),
                    onTap: () async {
                      await Navigator.of(context).push(
                        MaterialPageRoute(builder: (_) => RezervacijaDetaljiScreen(rezervacijaId: r.id)),
                      );
                      _ucitaj();
                    },
                  ),
                );
              },
            ),
          ),
        ),
      ),
    ]);
  }
}

class RezervacijaDetaljiScreen extends StatefulWidget {
  final int rezervacijaId;

  const RezervacijaDetaljiScreen({super.key, required this.rezervacijaId});

  @override
  State<RezervacijaDetaljiScreen> createState() => _RezervacijaDetaljiScreenState();
}

class _RezervacijaDetaljiScreenState extends State<RezervacijaDetaljiScreen> {
  Rezervacija? _r;
  List<Uplata> _uplate = [];
  bool _radi = false;
  String? _greska;

  @override
  void initState() {
    super.initState();
    _ucitaj();
  }

  Future<void> _ucitaj() async {
    try {
      final r = await context.read<RezervacijaProvider>().getById(widget.rezervacijaId);
      if (!mounted) return;
      final uplate = await context.read<UplataProvider>().get(filter: {'rezervacijaId': widget.rezervacijaId});
      if (mounted) {
        setState(() {
          _r = r;
          _uplate = uplate;
        });
      }
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    }
  }

  Future<void> _plati(double iznos) async {
    final ok = await potvrdi(context,
        naslov: 'Online plaćanje',
        poruka: 'Platiti ${formatKM(iznos)} karticom?',
        potvrdaTekst: 'Plati',
        opasno: false);
    if (!ok || !mounted) return;
    setState(() => _radi = true);
    try {
      final placeno = await OnlinePlacanje.platiRezervaciju(rezervacijaId: widget.rezervacijaId, iznos: iznos);
      if (!mounted) return;
      if (placeno) prikaziUspjeh(context, 'Uplata od ${formatKM(iznos)} je uspješno izvršena.');
      await _ucitaj();
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    } finally {
      if (mounted) setState(() => _radi = false);
    }
  }

  Future<void> _otkazi() async {
    final r = _r!;
    final ok = await potvrdi(context,
        naslov: 'Otkazivanje rezervacije',
        poruka: 'Da li sigurno želite otkazati rezervaciju "${r.putovanjeNaziv ?? r.ime}"?\n\n'
            'Prema uslovima agencije, za potvrđenu rezervaciju vraća se 70% uplaćenog iznosa, '
            'a za rezervaciju u obradi 80%.',
        potvrdaTekst: 'Otkaži rezervaciju');
    if (!ok || !mounted) return;
    setState(() => _radi = true);
    try {
      await context.read<RezervacijaProvider>().update(r.id, {
        'ime': r.ime,
        'korisnikId': r.korisnikId,
        'putovanjeId': r.putovanjeId,
        'brojOsoba': r.brojOsoba,
        'napomena': r.napomena,
        'status': StatusRezervacije.otkazano,
      });
      if (!mounted) return;
      prikaziUspjeh(context, 'Rezervacija je otkazana.');
      await _ucitaj();
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    } finally {
      if (mounted) setState(() => _radi = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    final r = _r;
    return Scaffold(
      appBar: AppBar(title: const Text('Detalji rezervacije')),
      body: _greska != null
          ? Center(child: Text(_greska!))
          : r == null
              ? const Center(child: CircularProgressIndicator())
              : _sadrzaj(r),
    );
  }

  Widget _sadrzaj(Rezervacija r) {
    final aktivna = r.status != StatusRezervacije.otkazano;
    final mozeOtkazati = aktivna &&
        r.datumPolaska != null &&
        r.datumPolaska!.isAfter(DateTime.now().add(const Duration(hours: 24)));
    final preostalo = r.preostalo;

    return ListView(padding: const EdgeInsets.all(16), children: [
      Row(children: [
        Expanded(child: Text(r.putovanjeNaziv ?? r.ime, style: Theme.of(context).textTheme.titleLarge)),
        StatusChip(r.status),
      ]),
      const SizedBox(height: 12),
      InfoRed(Icons.flight_takeoff, 'Polazak', formatDatum(r.datumPolaska)),
      InfoRed(Icons.event_note_outlined, 'Rezervisano', formatDatum(r.datumRezervacije)),
      InfoRed(Icons.group_outlined, 'Broj osoba', '${r.brojOsoba}'),
      InfoRed(Icons.receipt_long_outlined, 'Ukupno', formatKM(r.ukupnaCijena)),
      InfoRed(Icons.payments_outlined, 'Uplaćeno', formatKM(r.uplaceno)),
      InfoRed(Icons.account_balance_wallet_outlined, 'Preostalo', formatKM(preostalo)),
      if ((r.napomena ?? '').isNotEmpty) InfoRed(Icons.notes_outlined, 'Napomena', r.napomena!),
      const SizedBox(height: 16),
      if (aktivna && preostalo > 0) ...[
        FilledButton.icon(
          style: FilledButton.styleFrom(minimumSize: const Size.fromHeight(46)),
          onPressed: _radi ? null : () => _plati(preostalo),
          icon: const Icon(Icons.credit_card),
          label: Text('Plati ostatak (${formatKM(preostalo)})'),
        ),
        if (r.uplaceno == 0) ...[
          const SizedBox(height: 8),
          OutlinedButton.icon(
            style: OutlinedButton.styleFrom(minimumSize: const Size.fromHeight(46)),
            onPressed: _radi ? null : () => _plati(double.parse((preostalo / 2).toStringAsFixed(2))),
            icon: const Icon(Icons.credit_card),
            label: Text('Plati polovinu (${formatKM(preostalo / 2)})'),
          ),
        ],
      ],
      if (mozeOtkazati) ...[
        const SizedBox(height: 8),
        TextButton.icon(
          onPressed: _radi ? null : _otkazi,
          icon: Icon(Icons.cancel_outlined, color: Colors.red.shade700),
          label: Text('Otkaži rezervaciju', style: TextStyle(color: Colors.red.shade700)),
        ),
      ],
      const Divider(height: 32),
      Text('Uplate', style: Theme.of(context).textTheme.titleMedium),
      if (_uplate.isEmpty) const Padding(padding: EdgeInsets.only(top: 8), child: Text('Još nema uplata.')),
      for (final u in _uplate)
        ListTile(
          contentPadding: EdgeInsets.zero,
          leading: const Icon(Icons.receipt_outlined),
          title: Text(formatKM(u.iznos)),
          subtitle: Text('${formatDatumVrijeme(u.datum)} · ${u.nacinPlacanja ?? ''}'),
        ),
    ]);
  }
}
