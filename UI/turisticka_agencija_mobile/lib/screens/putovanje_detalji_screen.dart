import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/interakcije.dart';
import '../models/putovanje.dart';
import '../providers/auth_provider.dart';
import '../providers/providers.dart';
import '../utils/dialogs.dart';
import '../utils/formatters.dart';
import '../widgets/common.dart';
import 'putovanja_screen.dart';
import 'rezervacija_forma_screen.dart';

/// Detalji putovanja: informacije, vodici, ocjenjivanje, komentari, lista zelja, rezervacija i preporuke.
class PutovanjeDetaljiScreen extends StatefulWidget {
  final int putovanjeId;

  const PutovanjeDetaljiScreen({super.key, required this.putovanjeId});

  @override
  State<PutovanjeDetaljiScreen> createState() => _PutovanjeDetaljiScreenState();
}

class _PutovanjeDetaljiScreenState extends State<PutovanjeDetaljiScreen> {
  final _komentarKey = GlobalKey<FormState>();
  final _komentar = TextEditingController();

  Putovanje? _putovanje;
  List<Komentar> _komentari = [];
  List<Putovanje> _preporuke = [];
  Ocjena? _mojaOcjena;
  ListaZelja? _uListiZelja;
  String? _greska;
  bool _salje = false;

  int get _mojId => AuthProvider.korisnikId;

  @override
  void initState() {
    super.initState();
    _ucitaj();
  }

  Future<void> _ucitaj() async {
    try {
      final id = widget.putovanjeId;
      final putovanje = await context.read<PutovanjeProvider>().getById(id);
      if (!mounted) return;
      final komentari = await context.read<KomentarProvider>().get(filter: {'putovanjeId': id});
      if (!mounted) return;
      final ocjene = await context.read<OcjenaProvider>().get(filter: {'putovanjeId': id, 'korisnikId': _mojId});
      if (!mounted) return;
      final zelje = await context.read<ListaZeljaProvider>().get(filter: {'putovanjeId': id});
      if (!mounted) return;
      setState(() {
        _putovanje = putovanje;
        _komentari = komentari;
        _mojaOcjena = ocjene.where((o) => o.korisnikId == _mojId).firstOrNull;
        _uListiZelja = zelje.where((z) => z.korisnikId == _mojId).firstOrNull;
      });
      final preporuke = await context.read<PutovanjeProvider>().preporuke(bezPutovanja: id);
      if (mounted) setState(() => _preporuke = preporuke);
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    }
  }

  Future<void> _ocijeni(int ocjena) async {
    try {
      final nova = await context.read<OcjenaProvider>().insert({
        'putovanjeId': widget.putovanjeId,
        'korisnikId': _mojId,
        'ocjena': ocjena,
      });
      if (!mounted) return;
      setState(() => _mojaOcjena = nova);
      prikaziUspjeh(context, 'Hvala! Putovanje ste ocijenili ocjenom $ocjena.');
      final putovanje = await context.read<PutovanjeProvider>().getById(widget.putovanjeId);
      if (mounted) setState(() => _putovanje = putovanje);
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    }
  }

  Future<void> _promijeniListuZelja() async {
    final provider = context.read<ListaZeljaProvider>();
    try {
      if (_uListiZelja != null) {
        await provider.delete(_uListiZelja!.id);
        if (!mounted) return;
        setState(() => _uListiZelja = null);
        prikaziUspjeh(context, 'Putovanje je uklonjeno sa liste želja.');
      } else {
        final z = await provider.insert({'putovanjeId': widget.putovanjeId, 'korisnikId': _mojId, 'opis': 'Želim posjetiti'});
        if (!mounted) return;
        setState(() => _uListiZelja = z);
        prikaziUspjeh(context, 'Putovanje je dodano na listu želja.');
      }
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    }
  }

  Future<void> _posaljiKomentar() async {
    if (!_komentarKey.currentState!.validate()) return;
    setState(() => _salje = true);
    try {
      await context.read<KomentarProvider>().insert({
        'putovanjeId': widget.putovanjeId,
        'korisnikId': _mojId,
        'sadrzaj': _komentar.text.trim(),
      });
      if (!mounted) return;
      _komentar.clear();
      _komentarKey.currentState!.reset();
      prikaziUspjeh(context, 'Komentar je objavljen.');
      final komentari = await context.read<KomentarProvider>().get(filter: {'putovanjeId': widget.putovanjeId});
      if (mounted) setState(() => _komentari = komentari);
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    } finally {
      if (mounted) setState(() => _salje = false);
    }
  }

  Future<void> _obrisiKomentar(Komentar k) async {
    final ok = await potvrdi(context, naslov: 'Brisanje komentara', poruka: 'Da li želite obrisati svoj komentar?');
    if (!ok || !mounted) return;
    try {
      await context.read<KomentarProvider>().delete(k.id);
      if (!mounted) return;
      setState(() => _komentari.removeWhere((x) => x.id == k.id));
      prikaziUspjeh(context, 'Komentar je obrisan.');
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    }
  }

  @override
  Widget build(BuildContext context) {
    final p = _putovanje;
    return Scaffold(
      appBar: AppBar(
        title: Text(p?.nazivPutovanja ?? 'Putovanje'),
        actions: [
          if (p != null)
            IconButton(
              tooltip: _uListiZelja == null ? 'Dodaj na listu želja' : 'Ukloni sa liste želja',
              icon: Icon(_uListiZelja == null ? Icons.favorite_border : Icons.favorite, color: Colors.red),
              onPressed: _promijeniListuZelja,
            ),
        ],
      ),
      body: _greska != null
          ? Center(child: Padding(padding: const EdgeInsets.all(24), child: Text(_greska!)))
          : p == null
              ? const Center(child: CircularProgressIndicator())
              : _sadrzaj(p),
      bottomNavigationBar: p == null || !p.datumPolaska.isAfter(DateTime.now())
          ? null
          : SafeArea(
              child: Padding(
                padding: const EdgeInsets.all(12),
                child: FilledButton.icon(
                  style: FilledButton.styleFrom(minimumSize: const Size.fromHeight(48)),
                  icon: const Icon(Icons.event_available),
                  label: Text('Rezerviši · ${formatKM(p.cijenaPutovanja)} po osobi'),
                  onPressed: () async {
                    final rezervisano = await Navigator.of(context).push<bool>(
                      MaterialPageRoute(builder: (_) => RezervacijaFormaScreen(putovanje: p)),
                    );
                    if (rezervisano == true && mounted) Navigator.of(context).pop();
                  },
                ),
              ),
            ),
    );
  }

  Widget _sadrzaj(Putovanje p) {
    final tema = Theme.of(context).textTheme;
    return ListView(children: [
      slikaBanner(p.slika, height: 220),
      Padding(
        padding: const EdgeInsets.all(16),
        child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
          Text(p.nazivPutovanja, style: tema.headlineSmall),
          const SizedBox(height: 12),
          InfoRed(Icons.location_on_outlined, 'Destinacija', p.gradNaziv ?? '-'),
          InfoRed(Icons.flight_takeoff, 'Polazak', formatDatum(p.datumPolaska)),
          InfoRed(Icons.flight_land, 'Povratak', formatDatum(p.datumDolaska)),
          InfoRed(Icons.directions_bus_outlined, 'Prevoz', p.prevozNaziv ?? '-'),
          InfoRed(Icons.hotel_outlined, 'Smještaj', p.smjestajNaziv ?? '-'),
          InfoRed(Icons.payments_outlined, 'Cijena', '${formatKM(p.cijenaPutovanja)} po osobi'),
          InfoRed(Icons.event_seat_outlined, 'Broj mjesta', '${p.brojMjesta}'),
          InfoRed(Icons.star_outline, 'Ocjena',
              p.brojOcjena == 0 ? 'Još nema ocjena' : '${p.prosjecnaOcjena.toStringAsFixed(1)} / 5 (${p.brojOcjena} ocjena)'),
          const Divider(height: 28),
          Text('Opis', style: tema.titleMedium),
          const SizedBox(height: 6),
          Text(p.opisPutovanja),
          if (p.vodiciImena.isNotEmpty) ...[
            const Divider(height: 28),
            Text('Turistički vodiči', style: tema.titleMedium),
            const SizedBox(height: 6),
            Wrap(spacing: 8, children: [
              for (final v in p.vodiciImena) Chip(avatar: const Icon(Icons.badge_outlined, size: 18), label: Text(v)),
            ]),
          ],
          const Divider(height: 28),
          Text('Vaša ocjena', style: tema.titleMedium),
          Row(children: [
            for (var i = 1; i <= 5; i++)
              IconButton(
                tooltip: 'Ocjena $i',
                icon: Icon(i <= (_mojaOcjena?.ocjena ?? 0) ? Icons.star : Icons.star_border, color: Colors.amber, size: 32),
                onPressed: () => _ocijeni(i),
              ),
          ]),
          const Divider(height: 28),
          Text('Komentari (${_komentari.length})', style: tema.titleMedium),
          const SizedBox(height: 8),
          Form(
            key: _komentarKey,
            child: TextFormField(
              controller: _komentar,
              maxLines: 3,
              minLines: 1,
              decoration: InputDecoration(
                hintText: 'Napišite komentar...',
                border: const OutlineInputBorder(),
                suffixIcon: IconButton(
                  icon: _salje
                      ? const SizedBox(width: 18, height: 18, child: CircularProgressIndicator(strokeWidth: 2))
                      : const Icon(Icons.send),
                  onPressed: _salje ? null : _posaljiKomentar,
                ),
              ),
              validator: (v) {
                final t = v?.trim() ?? '';
                if (t.isEmpty) return 'Unesite tekst komentara.';
                if (t.length > 1000) return 'Komentar može imati najviše 1000 znakova (trenutno ${t.length}).';
                return null;
              },
            ),
          ),
          const SizedBox(height: 8),
          if (_komentari.isEmpty) const Text('Budite prvi koji će komentarisati ovo putovanje.'),
          for (final k in _komentari)
            ListTile(
              contentPadding: EdgeInsets.zero,
              leading: const CircleAvatar(child: Icon(Icons.person_outline)),
              title: Text(k.korisnikImePrezime ?? 'Korisnik'),
              subtitle: Text('${k.sadrzaj}\n${formatDatumVrijeme(k.datum)}'),
              isThreeLine: true,
              trailing: k.korisnikId == _mojId
                  ? IconButton(
                      tooltip: 'Obriši',
                      icon: const Icon(Icons.delete_outline),
                      onPressed: () => _obrisiKomentar(k),
                    )
                  : null,
            ),
          if (_preporuke.isNotEmpty) ...[
            const Divider(height: 28),
            Text('Možda će vam se svidjeti', style: tema.titleMedium),
            const SizedBox(height: 8),
            SizedBox(
              height: 205,
              child: ListView.separated(
                scrollDirection: Axis.horizontal,
                itemCount: _preporuke.length,
                separatorBuilder: (_, _) => const SizedBox(width: 10),
                itemBuilder: (_, i) => PutovanjeMalaKartica(
                  putovanje: _preporuke[i],
                  onTap: () => Navigator.of(context).pushReplacement(MaterialPageRoute(
                    builder: (_) => PutovanjeDetaljiScreen(putovanjeId: _preporuke[i].id),
                  )),
                ),
              ),
            ),
          ],
        ]),
      ),
    ]);
  }
}
