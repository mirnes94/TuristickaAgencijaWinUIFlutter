import 'package:flutter/material.dart';

void prikaziUspjeh(BuildContext context, String poruka) {
  ScaffoldMessenger.of(context).showSnackBar(
    SnackBar(
      content: Row(children: [
        const Icon(Icons.check_circle_outline, color: Colors.white),
        const SizedBox(width: 12),
        Expanded(child: Text(poruka)),
      ]),
      backgroundColor: Colors.green.shade700,
      behavior: SnackBarBehavior.floating,
    ),
  );
}

Future<void> prikaziGresku(BuildContext context, Object greska) {
  return showDialog<void>(
    context: context,
    builder: (ctx) => AlertDialog(
      icon: const Icon(Icons.error_outline, color: Colors.red),
      title: const Text('Greška'),
      content: Text(greska.toString().replaceFirst('Exception: ', '')),
      actions: [
        FilledButton(onPressed: () => Navigator.pop(ctx), child: const Text('U redu')),
      ],
    ),
  );
}

/// Potvrda za nepovratne akcije (brisanje, otkazivanje...).
Future<bool> potvrdi(
  BuildContext context, {
  required String naslov,
  required String poruka,
  String potvrdaTekst = 'Obriši',
  bool opasno = true,
}) async {
  final rezultat = await showDialog<bool>(
    context: context,
    builder: (ctx) => AlertDialog(
      icon: Icon(opasno ? Icons.warning_amber_rounded : Icons.help_outline,
          color: opasno ? Colors.orange.shade800 : null),
      title: Text(naslov),
      content: Text(poruka),
      actions: [
        TextButton(onPressed: () => Navigator.pop(ctx, false), child: const Text('Odustani')),
        FilledButton(
          style: opasno ? FilledButton.styleFrom(backgroundColor: Colors.red.shade700) : null,
          onPressed: () => Navigator.pop(ctx, true),
          child: Text(potvrdaTekst),
        ),
      ],
    ),
  );
  return rezultat ?? false;
}
