import 'package:flutter/material.dart';

import 'favoriti_screen.dart';
import 'obavijesti_screen.dart';
import 'profil_screen.dart';
import 'putovanja_screen.dart';
import 'rezervacije_screen.dart';

/// Glavni ekran sa donjom navigacijom.
class HomeScreen extends StatefulWidget {
  const HomeScreen({super.key});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  int _indeks = 0;

  static const _naslovi = ['Putovanja', 'Lista želja', 'Moje rezervacije', 'Obavijesti', 'Profil'];

  Widget _ekran() {
    switch (_indeks) {
      case 1:
        return const FavoritiScreen();
      case 2:
        return const RezervacijeScreen();
      case 3:
        return const ObavijestiScreen();
      case 4:
        return const ProfilScreen();
      default:
        return const PutovanjaScreen();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text(_naslovi[_indeks])),
      body: KeyedSubtree(key: ValueKey(_indeks), child: _ekran()),
      bottomNavigationBar: NavigationBar(
        selectedIndex: _indeks,
        onDestinationSelected: (i) => setState(() => _indeks = i),
        destinations: const [
          NavigationDestination(icon: Icon(Icons.luggage_outlined), label: 'Putovanja'),
          NavigationDestination(icon: Icon(Icons.favorite_border), label: 'Želje'),
          NavigationDestination(icon: Icon(Icons.event_available_outlined), label: 'Rezervacije'),
          NavigationDestination(icon: Icon(Icons.notifications_none), label: 'Obavijesti'),
          NavigationDestination(icon: Icon(Icons.person_outline), label: 'Profil'),
        ],
      ),
    );
  }
}
