import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/izvjestaji.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';

class DashboardScreen extends StatefulWidget {
  const DashboardScreen({super.key});

  @override
  State<DashboardScreen> createState() => _DashboardScreenState();
}

class _DashboardScreenState extends State<DashboardScreen> {
  DashboardStatistika? _podaci;
  String? _greska;

  @override
  void initState() {
    super.initState();
    _ucitaj();
  }

  Future<void> _ucitaj() async {
    setState(() => _greska = null);
    try {
      final podaci = await context.read<IzvjestajProvider>().dashboard();
      if (mounted) setState(() => _podaci = podaci);
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreen(
      title: 'Početna',
      actions: [IconButton(tooltip: 'Osvježi', onPressed: _ucitaj, icon: const Icon(Icons.refresh))],
      child: _greska != null
          ? Center(child: Text(_greska!))
          : _podaci == null
              ? const Center(child: CircularProgressIndicator())
              : _sadrzaj(_podaci!),
    );
  }

  Widget _sadrzaj(DashboardStatistika p) {
    final maxIznos = p.prihodPoMjesecima.fold<double>(0, (m, e) => e.iznos > m ? e.iznos : m);

    return ListView(
      padding: const EdgeInsets.all(16),
      children: [
        Wrap(spacing: 12, runSpacing: 12, children: [
          StatKarticaDashboard('Korisnici', '${p.brojKorisnika}', Icons.people_outline, Colors.indigo),
          StatKarticaDashboard('Aktivna putovanja', '${p.brojAktivnihPutovanja} / ${p.brojPutovanja}',
              Icons.luggage_outlined, Colors.teal),
          StatKarticaDashboard('Rezervacije', '${p.brojRezervacija}', Icons.event_available_outlined, Colors.orange),
          StatKarticaDashboard('U obradi', '${p.rezervacijeUObradi}', Icons.hourglass_empty, Colors.deepOrange),
          StatKarticaDashboard('Prihod ovaj mjesec', formatKM(p.prihodOvajMjesec), Icons.payments_outlined, Colors.green),
          StatKarticaDashboard('Prihod ukupno', formatKM(p.prihodUkupno), Icons.account_balance_wallet_outlined,
              Colors.blueGrey),
        ]),
        const SizedBox(height: 16),
        Wrap(spacing: 16, runSpacing: 16, children: [
          SizedBox(
            width: 560,
            child: Card(
              child: Padding(
                padding: const EdgeInsets.all(16),
                child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
                  Text('Prihod po mjesecima (zadnjih 6)', style: Theme.of(context).textTheme.titleMedium),
                  const SizedBox(height: 16),
                  SizedBox(
                    height: 220,
                    child: Row(
                      crossAxisAlignment: CrossAxisAlignment.end,
                      children: [
                        for (final m in p.prihodPoMjesecima)
                          Expanded(
                            child: Padding(
                              padding: const EdgeInsets.symmetric(horizontal: 6),
                              child: Column(mainAxisAlignment: MainAxisAlignment.end, children: [
                                Text(m.iznos.toStringAsFixed(0), style: Theme.of(context).textTheme.labelSmall),
                                const SizedBox(height: 4),
                                Tooltip(
                                  message: formatKM(m.iznos),
                                  child: Container(
                                    height: maxIznos == 0 ? 2 : 170 * (m.iznos / maxIznos) + 2,
                                    decoration: BoxDecoration(
                                      color: Theme.of(context).colorScheme.primary,
                                      borderRadius: const BorderRadius.vertical(top: Radius.circular(4)),
                                    ),
                                  ),
                                ),
                                const SizedBox(height: 6),
                                Text(mjeseci[m.mjesec - 1].substring(0, 3)),
                              ]),
                            ),
                          ),
                      ],
                    ),
                  ),
                ]),
              ),
            ),
          ),
          SizedBox(
            width: 460,
            child: Card(
              child: Padding(
                padding: const EdgeInsets.all(16),
                child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
                  Text('Najpopularnija putovanja', style: Theme.of(context).textTheme.titleMedium),
                  const SizedBox(height: 8),
                  if (p.najpopularnijaPutovanja.isEmpty) const Text('Još nema rezervacija.'),
                  for (final (i, n) in p.najpopularnijaPutovanja.indexed)
                    ListTile(
                      dense: true,
                      leading: CircleAvatar(radius: 14, child: Text('${i + 1}')),
                      title: Text(n.putovanje),
                      subtitle: Text('${n.brojRezervacija} rezervacija'),
                      trailing: Text('${n.brojOsoba} osoba'),
                    ),
                ]),
              ),
            ),
          ),
        ]),
      ],
    );
  }
}

class StatKarticaDashboard extends StatelessWidget {
  final String naslov;
  final String vrijednost;
  final IconData ikona;
  final Color boja;

  const StatKarticaDashboard(this.naslov, this.vrijednost, this.ikona, this.boja, {super.key});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 250,
      child: Card(
        child: Padding(
          padding: const EdgeInsets.all(16),
          child: Row(children: [
            CircleAvatar(backgroundColor: boja.withValues(alpha: 0.15), child: Icon(ikona, color: boja)),
            const SizedBox(width: 16),
            Expanded(
              child: Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
                Text(naslov, style: Theme.of(context).textTheme.bodySmall),
                const SizedBox(height: 4),
                Text(vrijednost,
                    style: Theme.of(context).textTheme.titleMedium?.copyWith(fontWeight: FontWeight.bold)),
              ]),
            ),
          ]),
        ),
      ),
    );
  }
}
