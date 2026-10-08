import 'package:flutter/material.dart';

import '../models/rezervacija.dart';
import '../utils/dialogs.dart';

/// Red sa ikonom, nazivom i vrijednoscu (dvije kolone, poravnato).
class InfoRed extends StatelessWidget {
  final IconData ikona;
  final String naziv;
  final String vrijednost;

  const InfoRed(this.ikona, this.naziv, this.vrijednost, {super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 6),
      child: Row(crossAxisAlignment: CrossAxisAlignment.start, children: [
        Icon(ikona, size: 20, color: Theme.of(context).colorScheme.primary),
        const SizedBox(width: 12),
        SizedBox(width: 110, child: Text(naziv, style: const TextStyle(color: Colors.black54))),
        Expanded(child: Text(vrijednost, style: const TextStyle(fontWeight: FontWeight.w600))),
      ]),
    );
  }
}

class StatusChip extends StatelessWidget {
  final String status;

  const StatusChip(this.status, {super.key});

  @override
  Widget build(BuildContext context) {
    final boja = switch (status) {
      StatusRezervacije.potvrdjeno => Colors.green.shade700,
      StatusRezervacije.otkazano => Colors.red.shade700,
      _ => Colors.orange.shade800,
    };
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 4),
      decoration: BoxDecoration(
        color: boja.withValues(alpha: 0.12),
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: boja.withValues(alpha: 0.5)),
      ),
      child: Text(status, style: TextStyle(color: boja, fontWeight: FontWeight.w600, fontSize: 12)),
    );
  }
}

/// Prikaz stanja liste: ucitavanje, greska (sa ponovnim pokusajem) ili prazna lista.
class StanjeListe extends StatelessWidget {
  final bool ucitavanje;
  final String? greska;
  final bool prazno;
  final String porukaPrazno;
  final VoidCallback onPonovo;
  final Widget child;

  const StanjeListe({
    super.key,
    required this.ucitavanje,
    required this.greska,
    required this.prazno,
    required this.porukaPrazno,
    required this.onPonovo,
    required this.child,
  });

  @override
  Widget build(BuildContext context) {
    if (ucitavanje) return const Center(child: CircularProgressIndicator());
    if (greska != null) {
      return Center(
        child: Padding(
          padding: const EdgeInsets.all(24),
          child: Column(mainAxisSize: MainAxisSize.min, children: [
            const Icon(Icons.cloud_off, size: 48, color: Colors.grey),
            const SizedBox(height: 8),
            Text(greska!, textAlign: TextAlign.center),
            const SizedBox(height: 12),
            OutlinedButton(onPressed: onPonovo, child: const Text('Pokušaj ponovo')),
          ]),
        ),
      );
    }
    if (prazno) {
      return Center(
        child: Column(mainAxisSize: MainAxisSize.min, children: [
          const Icon(Icons.inbox_outlined, size: 48, color: Colors.grey),
          const SizedBox(height: 8),
          Text(porukaPrazno, textAlign: TextAlign.center),
        ]),
      );
    }
    return child;
  }
}

InputDecoration poljeDekoracija(String label, {IconData? ikona, String? hint, String? helper}) => InputDecoration(
      labelText: label,
      hintText: hint,
      helperText: helper,
      prefixIcon: ikona == null ? null : Icon(ikona),
      border: const OutlineInputBorder(),
    );

/// Zajednicka logika forme: validacija, spasavanje, poruka o uspjehu i zatvaranje.
mixin FormaMixin<W extends StatefulWidget> on State<W> {
  final formKey = GlobalKey<FormState>();
  bool spasavanje = false;

  Future<void> sacuvaj(Future<void> Function() akcija, String porukaUspjeha, {bool zatvori = true}) async {
    if (!(formKey.currentState?.validate() ?? false)) return;
    setState(() => spasavanje = true);
    try {
      await akcija();
      if (!mounted) return;
      prikaziUspjeh(context, porukaUspjeha);
      if (zatvori) Navigator.of(context).pop(true);
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    } finally {
      if (mounted) setState(() => spasavanje = false);
    }
  }
}
