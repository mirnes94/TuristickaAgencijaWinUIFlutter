import 'dart:convert';

import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';

import '../utils/formatters.dart';

/// Traka za pretragu iznad tabele: filteri + "Pretraži" + (opciono) "Dodaj".
class PretragaTraka extends StatelessWidget {
  final List<Widget> filteri;
  final VoidCallback onPretrazi;
  final VoidCallback? onDodaj;
  final String dodajTekst;

  const PretragaTraka({
    super.key,
    required this.filteri,
    required this.onPretrazi,
    this.onDodaj,
    this.dodajTekst = 'Dodaj',
  });

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.fromLTRB(16, 16, 16, 8),
      child: Wrap(
        spacing: 12,
        runSpacing: 12,
        crossAxisAlignment: WrapCrossAlignment.center,
        children: [
          ...filteri,
          FilledButton.tonalIcon(
            onPressed: onPretrazi,
            icon: const Icon(Icons.search),
            label: const Text('Pretraži'),
          ),
          if (onDodaj != null)
            FilledButton.icon(
              onPressed: onDodaj,
              icon: const Icon(Icons.add),
              label: Text(dodajTekst),
            ),
        ],
      ),
    );
  }
}

/// Polje za tekstualnu pretragu fiksne sirine (Enter pokrece pretragu).
class PretragaPolje extends StatelessWidget {
  final TextEditingController controller;
  final String label;
  final VoidCallback onSubmit;
  final double width;

  const PretragaPolje({
    super.key,
    required this.controller,
    required this.label,
    required this.onSubmit,
    this.width = 240,
  });

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: width,
      child: TextField(
        controller: controller,
        decoration: InputDecoration(
          labelText: label,
          prefixIcon: const Icon(Icons.search),
          border: const OutlineInputBorder(),
          isDense: true,
        ),
        onSubmitted: (_) => onSubmit(),
      ),
    );
  }
}

/// Dropdown za filter (sa opcijom "Svi").
class FilterDropdown<T> extends StatelessWidget {
  final String label;
  final T? value;
  final List<DropdownMenuItem<T?>> items;
  final ValueChanged<T?> onChanged;
  final double width;

  const FilterDropdown({
    super.key,
    required this.label,
    required this.value,
    required this.items,
    required this.onChanged,
    this.width = 220,
  });

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: width,
      child: DropdownButtonFormField<T?>(
        initialValue: value,
        isExpanded: true,
        decoration: InputDecoration(labelText: label, border: const OutlineInputBorder(), isDense: true),
        items: [DropdownMenuItem<T?>(value: null, child: const Text('Svi')), ...items],
        onChanged: onChanged,
      ),
    );
  }
}

/// Tabela sa podacima + stanja ucitavanja / prazne liste / greske.
class TabelaPodataka extends StatelessWidget {
  final bool ucitavanje;
  final String? greska;
  final List<DataColumn> kolone;
  final List<DataRow> redovi;
  final String prazno;

  const TabelaPodataka({
    super.key,
    required this.ucitavanje,
    required this.kolone,
    required this.redovi,
    this.greska,
    this.prazno = 'Nema podataka za zadane kriterije pretrage.',
  });

  @override
  Widget build(BuildContext context) {
    if (ucitavanje) {
      return const Center(child: CircularProgressIndicator());
    }
    if (greska != null) {
      return Center(
        child: Column(mainAxisSize: MainAxisSize.min, children: [
          const Icon(Icons.cloud_off, size: 48, color: Colors.grey),
          const SizedBox(height: 8),
          Text(greska!, textAlign: TextAlign.center),
        ]),
      );
    }
    if (redovi.isEmpty) {
      return Center(
        child: Column(mainAxisSize: MainAxisSize.min, children: [
          const Icon(Icons.inbox_outlined, size: 48, color: Colors.grey),
          const SizedBox(height: 8),
          Text(prazno),
        ]),
      );
    }

    return LayoutBuilder(
      builder: (context, constraints) => SingleChildScrollView(
        padding: const EdgeInsets.fromLTRB(16, 0, 16, 16),
        child: SingleChildScrollView(
          scrollDirection: Axis.horizontal,
          child: ConstrainedBox(
            constraints: BoxConstraints(minWidth: constraints.maxWidth - 32),
            child: Card(
              clipBehavior: Clip.antiAlias,
              child: DataTable(
                showCheckboxColumn: false,
                headingRowColor: WidgetStatePropertyAll(Theme.of(context).colorScheme.primaryContainer.withValues(alpha: 0.5)),
                columns: kolone,
                rows: redovi,
              ),
            ),
          ),
        ),
      ),
    );
  }
}

/// Akcije u redu tabele (uredi / obrisi).
class RedAkcije extends StatelessWidget {
  final VoidCallback? onUredi;
  final VoidCallback? onObrisi;

  const RedAkcije({super.key, this.onUredi, this.onObrisi});

  @override
  Widget build(BuildContext context) {
    return Row(mainAxisSize: MainAxisSize.min, children: [
      if (onUredi != null)
        IconButton(tooltip: 'Uredi', icon: const Icon(Icons.edit_outlined), onPressed: onUredi),
      if (onObrisi != null)
        IconButton(
          tooltip: 'Obriši',
          icon: Icon(Icons.delete_outline, color: Colors.red.shade700),
          onPressed: onObrisi,
        ),
    ]);
  }
}

/// Okvir ekrana za unos/izmjenu: naslov, "X" za zatvaranje, back dugme i dugmad Sacuvaj/Odustani.
class FormaEkran extends StatelessWidget {
  final String naslov;
  final GlobalKey<FormState> formKey;
  final List<Widget> polja;
  final Future<void> Function() onSacuvaj;
  final bool spasavanje;
  final double maxSirina;

  const FormaEkran({
    super.key,
    required this.naslov,
    required this.formKey,
    required this.polja,
    required this.onSacuvaj,
    this.spasavanje = false,
    this.maxSirina = 760,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(naslov),
        actions: [
          IconButton(
            tooltip: 'Zatvori',
            icon: const Icon(Icons.close),
            onPressed: () => Navigator.of(context).pop(false),
          ),
        ],
      ),
      body: Center(
        child: ConstrainedBox(
          constraints: BoxConstraints(maxWidth: maxSirina),
          child: Form(
            key: formKey,
            autovalidateMode: AutovalidateMode.onUserInteraction,
            child: ListView(
              padding: const EdgeInsets.all(24),
              children: [
                ...polja.expand((p) => [p, const SizedBox(height: 16)]),
                const SizedBox(height: 8),
                Row(mainAxisAlignment: MainAxisAlignment.end, children: [
                  OutlinedButton(
                    onPressed: spasavanje ? null : () => Navigator.of(context).pop(false),
                    child: const Text('Odustani'),
                  ),
                  const SizedBox(width: 12),
                  FilledButton.icon(
                    onPressed: spasavanje ? null : onSacuvaj,
                    icon: spasavanje
                        ? const SizedBox(width: 16, height: 16, child: CircularProgressIndicator(strokeWidth: 2))
                        : const Icon(Icons.save_outlined),
                    label: const Text('Sačuvaj'),
                  ),
                ]),
              ],
            ),
          ),
        ),
      ),
    );
  }
}

InputDecoration poljeDekoracija(String label, {IconData? ikona, String? hint, String? helper}) => InputDecoration(
      labelText: label,
      hintText: hint,
      helperText: helper,
      prefixIcon: ikona == null ? null : Icon(ikona),
      border: const OutlineInputBorder(),
    );

/// Unos datuma iskljucivo preko DatePicker-a.
class DatumPolje extends FormField<DateTime> {
  DatumPolje({
    super.key,
    required String label,
    DateTime? initialValue,
    required ValueChanged<DateTime?> onChanged,
    DateTime? firstDate,
    DateTime? lastDate,
    super.validator,
  }) : super(
          initialValue: initialValue,
          builder: (state) {
            return InkWell(
              onTap: () async {
                final odabran = await showDatePicker(
                  context: state.context,
                  initialDate: state.value ?? DateTime.now(),
                  firstDate: firstDate ?? DateTime(2000),
                  lastDate: lastDate ?? DateTime(2100),
                );
                if (odabran != null) {
                  state.didChange(odabran);
                  onChanged(odabran);
                }
              },
              child: InputDecorator(
                decoration: poljeDekoracija(label, ikona: Icons.calendar_month_outlined)
                    .copyWith(errorText: state.errorText),
                child: Text(state.value == null ? 'Odaberite datum' : formatDatum(state.value)),
              ),
            );
          },
        );
}

/// Odabir slike (spasava se kao base64 string).
class SlikaPolje extends FormField<String> {
  SlikaPolje({
    super.key,
    required String label,
    String? initialValue,
    required ValueChanged<String?> onChanged,
    super.validator,
  }) : super(
          initialValue: initialValue,
          builder: (state) {
            final bytes = bytesFromBase64(state.value);
            return InputDecorator(
              decoration: poljeDekoracija(label, ikona: Icons.image_outlined).copyWith(errorText: state.errorText),
              child: Row(children: [
                if (bytes != null)
                  ClipRRect(
                    borderRadius: BorderRadius.circular(8),
                    child: Image.memory(bytes, width: 180, height: 120, fit: BoxFit.cover),
                  )
                else
                  const SizedBox(
                    width: 180,
                    height: 120,
                    child: Center(child: Text('Slika nije odabrana', style: TextStyle(color: Colors.grey))),
                  ),
                const SizedBox(width: 16),
                OutlinedButton.icon(
                  icon: const Icon(Icons.upload_file),
                  label: Text(bytes == null ? 'Odaberi sliku' : 'Promijeni sliku'),
                  onPressed: () async {
                    final rezultat = await FilePicker.pickFiles(type: FileType.image, withData: true);
                    final fileBytes = rezultat?.files.single.bytes;
                    if (fileBytes != null) {
                      final b64 = base64Encode(fileBytes);
                      state.didChange(b64);
                      onChanged(b64);
                    }
                  },
                ),
              ]),
            );
          },
        );
}
