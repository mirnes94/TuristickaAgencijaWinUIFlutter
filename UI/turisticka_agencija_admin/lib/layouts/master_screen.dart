import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../providers/auth_provider.dart';
import '../screens/dashboard_screen.dart';
import '../screens/izvjestaj_screen.dart';
import '../screens/komentari_screen.dart';
import '../screens/korisnici_screen.dart';
import '../screens/login_screen.dart';
import '../screens/obavijesti_screen.dart';
import '../screens/ocjene_screen.dart';
import '../screens/preporuke_screen.dart';
import '../screens/profil_screen.dart';
import '../screens/putovanja_screen.dart';
import '../screens/rezervacije_screen.dart';
import '../screens/sifarnici_screens.dart';
import '../screens/uplate_screen.dart';
import '../screens/vodici_screen.dart';
import '../utils/dialogs.dart';

class _MeniStavka {
  final String naziv;
  final IconData ikona;
  final Widget Function() ekran;

  const _MeniStavka(this.naziv, this.ikona, this.ekran);
}

final List<_MeniStavka> _glavniMeni = [
  _MeniStavka('Početna', Icons.dashboard_outlined, () => const DashboardScreen()),
  _MeniStavka('Putovanja', Icons.luggage_outlined, () => const PutovanjaScreen()),
  _MeniStavka('Rezervacije', Icons.event_available_outlined, () => const RezervacijeScreen()),
  _MeniStavka('Uplate', Icons.payments_outlined, () => const UplateScreen()),
  _MeniStavka('Korisnici', Icons.people_outline, () => const KorisniciScreen()),
  _MeniStavka('Vodiči', Icons.badge_outlined, () => const VodiciScreen()),
  _MeniStavka('Komentari', Icons.forum_outlined, () => const KomentariScreen()),
  _MeniStavka('Ocjene', Icons.star_outline, () => const OcjeneScreen()),
  _MeniStavka('Obavijesti', Icons.campaign_outlined, () => const ObavijestiScreen()),
  _MeniStavka('Preporuke', Icons.recommend_outlined, () => const PreporukeScreen()),
  _MeniStavka('Izvještaj uplata', Icons.picture_as_pdf_outlined, () => const IzvjestajScreen()),
];

final List<_MeniStavka> _sifarnici = [
  _MeniStavka('Države', Icons.flag_outlined, () => const DrzaveScreen()),
  _MeniStavka('Gradovi', Icons.location_city_outlined, () => const GradoviScreen()),
  _MeniStavka('Firme', Icons.business_outlined, () => const FirmeScreen()),
  _MeniStavka('Prevoz', Icons.directions_bus_outlined, () => const PrevozScreen()),
  _MeniStavka('Smještaj', Icons.hotel_outlined, () => const SmjestajScreen()),
  _MeniStavka('Uloge', Icons.admin_panel_settings_outlined, () => const UlogeScreen()),
];

/// Okvir svih glavnih ekrana: bocni meni (kao MDI meni iz WinUI aplikacije) + sadrzaj.
class MasterScreen extends StatelessWidget {
  final String title;
  final Widget child;
  final List<Widget>? actions;

  const MasterScreen({super.key, required this.title, required this.child, this.actions});

  void _otvori(BuildContext context, _MeniStavka stavka) {
    if (stavka.naziv == title) return;
    Navigator.of(context).pushReplacement(
      PageRouteBuilder(
        pageBuilder: (_, _, _) =>stavka.ekran(),
        transitionDuration: Duration.zero,
      ),
    );
  }

  Future<void> _odjava(BuildContext context) async {
    final ok = await potvrdi(context,
        naslov: 'Odjava', poruka: 'Da li se želite odjaviti?', potvrdaTekst: 'Odjavi se', opasno: false);
    if (!ok || !context.mounted) return;
    context.read<AuthProvider>().logout();
    Navigator.of(context).pushAndRemoveUntil(
      MaterialPageRoute(builder: (_) => const LoginScreen()),
      (route) => false,
    );
  }

  @override
  Widget build(BuildContext context) {
    final scheme = Theme.of(context).colorScheme;
    final korisnik = AuthProvider.trenutniKorisnik;

    return Scaffold(
      body: Row(
        children: [
          Container(
            width: 250,
            color: scheme.surfaceContainerHighest.withValues(alpha: 0.4),
            child: Column(
              children: [
                Container(
                  width: double.infinity,
                  padding: const EdgeInsets.fromLTRB(20, 24, 20, 20),
                  color: scheme.primary,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      const Icon(Icons.card_travel, color: Colors.white, size: 34),
                      const SizedBox(height: 10),
                      const Text('Turistička agencija',
                          style: TextStyle(color: Colors.white, fontSize: 18, fontWeight: FontWeight.bold)),
                      Text(korisnik?.imePrezime ?? 'Administrator',
                          style: const TextStyle(color: Colors.white70)),
                    ],
                  ),
                ),
                Expanded(
                  child: ListView(
                    padding: const EdgeInsets.symmetric(vertical: 8),
                    children: [
                      for (final s in _glavniMeni) _stavka(context, s),
                      ExpansionTile(
                        leading: const Icon(Icons.list_alt_outlined),
                        title: const Text('Šifarnici'),
                        initiallyExpanded: _sifarnici.any((s) => s.naziv == title),
                        childrenPadding: const EdgeInsets.only(left: 12),
                        children: [for (final s in _sifarnici) _stavka(context, s)],
                      ),
                    ],
                  ),
                ),
                const Divider(height: 1),
                _stavka(context, _MeniStavka('Moj profil', Icons.account_circle_outlined, () => const ProfilScreen())),
                ListTile(
                  leading: const Icon(Icons.logout),
                  title: const Text('Odjava'),
                  onTap: () => _odjava(context),
                ),
              ],
            ),
          ),
          const VerticalDivider(width: 1),
          Expanded(
            child: Scaffold(
              appBar: AppBar(
                automaticallyImplyLeading: false,
                title: Text(title),
                actions: actions,
              ),
              body: child,
            ),
          ),
        ],
      ),
    );
  }

  Widget _stavka(BuildContext context, _MeniStavka s) {
    return ListTile(
      dense: true,
      selected: s.naziv == title,
      leading: Icon(s.ikona),
      title: Text(s.naziv),
      onTap: () => _otvori(context, s),
    );
  }
}
