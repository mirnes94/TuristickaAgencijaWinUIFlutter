import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/putovanje.dart';
import '../models/sifarnici.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../widgets/common.dart';
import 'putovanje_detalji_screen.dart';

/// Pregled i pretraga buducih putovanja + preporuke za prijavljenog klijenta.
class PutovanjaScreen extends StatefulWidget {
  const PutovanjaScreen({super.key});

  @override
  State<PutovanjaScreen> createState() => _PutovanjaScreenState();
}

class _PutovanjaScreenState extends State<PutovanjaScreen> {
  final _pretraga = TextEditingController();
  List<Putovanje> _putovanja = [];
  List<Putovanje> _preporuke = [];
  List<Grad> _gradovi = [];
  int? _gradId;
  bool _ucitavanje = true;
  String? _greska;

  @override
  void initState() {
    super.initState();
    _ucitajSve();
  }

  Future<void> _ucitajSve() async {
    try {
      final gradovi = await context.read<GradProvider>().get();
      if (mounted) setState(() => _gradovi = gradovi);
    } catch (_) {
      // filter po destinaciji tada nije dostupan
    }
    await _ucitajPreporuke();
    await _ucitaj();
  }

  Future<void> _ucitajPreporuke() async {
    try {
      final preporuke = await context.read<PutovanjeProvider>().preporuke();
      if (mounted) setState(() => _preporuke = preporuke);
    } catch (_) {
      if (mounted) setState(() => _preporuke = []);
    }
  }

  Future<void> _ucitaj() async {
    setState(() {
      _ucitavanje = true;
      _greska = null;
    });
    try {
      final lista = await context.read<PutovanjeProvider>().get(filter: {
        'nazivPutovanja': _pretraga.text,
        'gradId': _gradId,
        'samoBuduca': true,
      });
      if (mounted) setState(() => _putovanja = lista);
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    } finally {
      if (mounted) setState(() => _ucitavanje = false);
    }
  }

  Future<void> _otvori(Putovanje p) async {
    await Navigator.of(context).push(MaterialPageRoute(builder: (_) => PutovanjeDetaljiScreen(putovanjeId: p.id)));
    _ucitajPreporuke();
  }

  @override
  Widget build(BuildContext context) {
    return RefreshIndicator(
      onRefresh: _ucitajSve,
      child: ListView(padding: const EdgeInsets.all(12), children: [
        TextField(
          controller: _pretraga,
          textInputAction: TextInputAction.search,
          decoration: InputDecoration(
            hintText: 'Pretraži po nazivu (npr. Pariz)',
            prefixIcon: const Icon(Icons.search),
            border: const OutlineInputBorder(),
            suffixIcon: IconButton(icon: const Icon(Icons.arrow_forward), onPressed: _ucitaj),
          ),
          onSubmitted: (_) => _ucitaj(),
        ),
        const SizedBox(height: 10),
        DropdownButtonFormField<int?>(
          key: ValueKey('g-${_gradovi.length}'),
          initialValue: _gradId,
          isExpanded: true,
          decoration: const InputDecoration(
            labelText: 'Destinacija',
            prefixIcon: Icon(Icons.location_on_outlined),
            border: OutlineInputBorder(),
            isDense: true,
          ),
          items: [
            const DropdownMenuItem<int?>(value: null, child: Text('Sve destinacije')),
            for (final g in _gradovi)
              DropdownMenuItem<int?>(value: g.id, child: Text('${g.nazivGrada}, ${g.drzavaNaziv ?? ''}')),
          ],
          onChanged: (v) {
            setState(() => _gradId = v);
            _ucitaj();
          },
        ),
        if (_preporuke.isNotEmpty) ...[
          const SizedBox(height: 16),
          Text('Preporučeno za vas', style: Theme.of(context).textTheme.titleMedium),
          const SizedBox(height: 8),
          SizedBox(
            height: 205,
            child: ListView.separated(
              scrollDirection: Axis.horizontal,
              itemCount: _preporuke.length,
              separatorBuilder: (_, _) => const SizedBox(width: 10),
              itemBuilder: (_, i) => PutovanjeMalaKartica(putovanje: _preporuke[i], onTap: () => _otvori(_preporuke[i])),
            ),
          ),
        ],
        const SizedBox(height: 16),
        Text('Ponuda putovanja', style: Theme.of(context).textTheme.titleMedium),
        const SizedBox(height: 8),
        SizedBox(
          height: _ucitavanje || _greska != null || _putovanja.isEmpty ? 200 : null,
          child: StanjeListe(
            ucitavanje: _ucitavanje,
            greska: _greska,
            prazno: _putovanja.isEmpty,
            porukaPrazno: 'Nema putovanja za zadane kriterije.',
            onPonovo: _ucitaj,
            child: Column(children: [
              for (final p in _putovanja) PutovanjeKartica(putovanje: p, onTap: () => _otvori(p)),
            ]),
          ),
        ),
      ]),
    );
  }
}

class PutovanjeKartica extends StatelessWidget {
  final Putovanje putovanje;
  final VoidCallback onTap;

  const PutovanjeKartica({super.key, required this.putovanje, required this.onTap});

  @override
  Widget build(BuildContext context) {
    final p = putovanje;
    return Card(
      clipBehavior: Clip.antiAlias,
      margin: const EdgeInsets.only(bottom: 12),
      child: InkWell(
        onTap: onTap,
        child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
          slikaBanner(p.slika, height: 150),
          Padding(
            padding: const EdgeInsets.all(12),
            child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
              Text(p.nazivPutovanja, style: Theme.of(context).textTheme.titleMedium),
              const SizedBox(height: 6),
              Row(children: [
                const Icon(Icons.location_on_outlined, size: 16),
                const SizedBox(width: 4),
                Expanded(child: Text(p.gradNaziv ?? '-')),
                const Icon(Icons.calendar_month_outlined, size: 16),
                const SizedBox(width: 4),
                Text(formatDatum(p.datumPolaska)),
              ]),
              const SizedBox(height: 6),
              Row(children: [
                const Icon(Icons.star, size: 16, color: Colors.amber),
                const SizedBox(width: 4),
                Text(p.brojOcjena == 0 ? 'Nema ocjena' : '${p.prosjecnaOcjena.toStringAsFixed(1)} (${p.brojOcjena})'),
                const Spacer(),
                Text(formatKM(p.cijenaPutovanja),
                    style: TextStyle(fontWeight: FontWeight.bold, color: Theme.of(context).colorScheme.primary)),
              ]),
            ]),
          ),
        ]),
      ),
    );
  }
}

class PutovanjeMalaKartica extends StatelessWidget {
  final Putovanje putovanje;
  final VoidCallback onTap;

  const PutovanjeMalaKartica({super.key, required this.putovanje, required this.onTap});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 200,
      child: Card(
        clipBehavior: Clip.antiAlias,
        margin: EdgeInsets.zero,
        child: InkWell(
          onTap: onTap,
          child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
            slikaBanner(putovanje.slika, height: 100),
            Padding(
              padding: const EdgeInsets.all(8),
              child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
                Text(putovanje.nazivPutovanja, maxLines: 1, overflow: TextOverflow.ellipsis,
                    style: const TextStyle(fontWeight: FontWeight.w600)),
                const SizedBox(height: 4),
                Text(formatDatum(putovanje.datumPolaska), style: Theme.of(context).textTheme.bodySmall),
                Text(formatKM(putovanje.cijenaPutovanja),
                    style: TextStyle(color: Theme.of(context).colorScheme.primary, fontWeight: FontWeight.bold)),
              ]),
            ),
          ]),
        ),
      ),
    );
  }
}
