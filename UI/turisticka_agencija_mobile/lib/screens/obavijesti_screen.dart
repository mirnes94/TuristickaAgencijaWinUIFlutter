import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/interakcije.dart';
import '../providers/auth_provider.dart';
import '../providers/providers.dart';
import '../utils/formatters.dart';
import '../widgets/common.dart';

/// Obavijesti agencije (opce i one upucene prijavljenom klijentu).
class ObavijestiScreen extends StatefulWidget {
  const ObavijestiScreen({super.key});

  @override
  State<ObavijestiScreen> createState() => _ObavijestiScreenState();
}

class _ObavijestiScreenState extends State<ObavijestiScreen> {
  List<Obavijest> _lista = [];
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
      final lista = await context.read<ObavijestProvider>().get(filter: {'korisnikId': AuthProvider.korisnikId});
      if (mounted) setState(() => _lista = lista);
    } catch (e) {
      if (mounted) setState(() => _greska = e.toString());
    } finally {
      if (mounted) setState(() => _ucitavanje = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return StanjeListe(
      ucitavanje: _ucitavanje,
      greska: _greska,
      prazno: _lista.isEmpty,
      porukaPrazno: 'Nema obavijesti.',
      onPonovo: _ucitaj,
      child: RefreshIndicator(
        onRefresh: _ucitaj,
        child: ListView.builder(
          padding: const EdgeInsets.all(12),
          itemCount: _lista.length,
          itemBuilder: (_, i) {
            final o = _lista[i];
            return Card(
              child: ExpansionTile(
                leading: Icon(o.korisnikId == null ? Icons.campaign_outlined : Icons.mark_email_unread_outlined),
                title: Text(o.naziv),
                subtitle: Text(formatDatumVrijeme(o.datum)),
                childrenPadding: const EdgeInsets.fromLTRB(16, 0, 16, 16),
                expandedCrossAxisAlignment: CrossAxisAlignment.start,
                children: [Text(o.sadrzaj)],
              ),
            );
          },
        ),
      ),
    );
  }
}
