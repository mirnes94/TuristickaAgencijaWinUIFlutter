import 'dart:io';
import 'dart:typed_data';

import 'package:file_picker/file_picker.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart' show rootBundle;
import 'package:pdf/pdf.dart';
import 'package:pdf/widgets.dart' as pw;
import 'package:printing/printing.dart';
import 'package:provider/provider.dart';

import '../layouts/master_screen.dart';
import '../models/izvjestaji.dart';
import '../providers/providers.dart';
import '../utils/dialogs.dart';
import '../utils/formatters.dart';

/// Izvjestaj o uplatama za odabrani mjesec: pregled, preuzimanje (PDF) i stampa.
class IzvjestajScreen extends StatefulWidget {
  const IzvjestajScreen({super.key});

  @override
  State<IzvjestajScreen> createState() => _IzvjestajScreenState();
}

class _IzvjestajScreenState extends State<IzvjestajScreen> {
  int _godina = DateTime.now().year;
  int _mjesec = DateTime.now().month;
  UplateIzvjestaj? _izvjestaj;
  Uint8List? _pdf;
  bool _ucitavanje = false;

  Future<void> _generisi() async {
    setState(() => _ucitavanje = true);
    try {
      final izvjestaj = await context.read<IzvjestajProvider>().uplate(_godina, _mjesec);
      final pdf = await _kreirajPdf(izvjestaj);
      if (!mounted) return;
      setState(() {
        _izvjestaj = izvjestaj;
        _pdf = pdf;
      });
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    } finally {
      if (mounted) setState(() => _ucitavanje = false);
    }
  }

  String get _nazivFajla => 'izvjestaj-uplate-$_godina-${_mjesec.toString().padLeft(2, '0')}.pdf';

  Future<void> _sacuvaj() async {
    if (_pdf == null) return;
    try {
      final putanja = await FilePicker.saveFile(
        dialogTitle: 'Sačuvaj izvještaj',
        fileName: _nazivFajla,
        type: FileType.custom,
        allowedExtensions: ['pdf'],
        bytes: _pdf,
      );
      if (putanja == null) return;
      await File(putanja).writeAsBytes(_pdf!, flush: true);
      if (mounted) prikaziUspjeh(context, 'Izvještaj je sačuvan: $putanja');
    } catch (e) {
      if (mounted) await prikaziGresku(context, e);
    }
  }

  Future<void> _stampaj() async {
    if (_pdf == null) return;
    await Printing.layoutPdf(name: _nazivFajla, onLayout: (_) async => _pdf!);
  }

  Future<Uint8List> _kreirajPdf(UplateIzvjestaj r) async {
    // DejaVu Sans podrzava nasa slova (č, ć, š, ž, đ).
    final font = pw.Font.ttf(await rootBundle.load('assets/fonts/DejaVuSans.ttf'));
    final bold = pw.Font.ttf(await rootBundle.load('assets/fonts/DejaVuSans-Bold.ttf'));
    final doc = pw.Document(theme: pw.ThemeData.withFont(base: font, bold: bold));
    final period = '${mjeseci[r.mjesec - 1]} ${r.godina}.';

    doc.addPage(
      pw.MultiPage(
        pageFormat: PdfPageFormat.a4,
        margin: const pw.EdgeInsets.all(32),
        header: (ctx) => pw.Container(
          padding: const pw.EdgeInsets.only(bottom: 8),
          decoration: const pw.BoxDecoration(border: pw.Border(bottom: pw.BorderSide(width: 0.5))),
          child: pw.Row(mainAxisAlignment: pw.MainAxisAlignment.spaceBetween, children: [
            pw.Text('Turistička agencija', style: const pw.TextStyle(fontWeight: pw.FontWeight.bold)),
            pw.Text('Generisano: ${formatDatumVrijeme(DateTime.now())}', style: const pw.TextStyle(fontSize: 9)),
          ]),
        ),
        footer: (ctx) => pw.Align(
          alignment: pw.Alignment.centerRight,
          child: pw.Text('Stranica ${ctx.pageNumber} / ${ctx.pagesCount}', style: const pw.TextStyle(fontSize: 9)),
        ),
        build: (ctx) => [
          pw.SizedBox(height: 12),
          pw.Text('Izvještaj o uplatama', style: const pw.TextStyle(fontSize: 20, fontWeight: pw.FontWeight.bold)),
          pw.Text('Period: $period'),
          pw.SizedBox(height: 16),
          pw.Row(children: [
            _pdfKartica('Broj uplata', '${r.brojUplata}'),
            pw.SizedBox(width: 12),
            _pdfKartica('Ukupan iznos', formatKM(r.ukupanIznos)),
            pw.SizedBox(width: 12),
            _pdfKartica('Prosječna uplata', formatKM(r.prosjecnaUplata)),
          ]),
          pw.SizedBox(height: 20),
          pw.Text('Prihod po putovanjima', style: const pw.TextStyle(fontSize: 14, fontWeight: pw.FontWeight.bold)),
          pw.SizedBox(height: 6),
          if (r.poPutovanjima.isEmpty)
            pw.Text('Nema uplata u odabranom periodu.')
          else
            pw.TableHelper.fromTextArray(
              headers: ['Putovanje', 'Broj uplata', 'Iznos'],
              data: [for (final p in r.poPutovanjima) [p.putovanje, '${p.brojUplata}', formatKM(p.iznos)]],
              headerStyle: const pw.TextStyle(fontWeight: pw.FontWeight.bold),
              headerDecoration: const pw.BoxDecoration(color: PdfColors.grey300),
              cellAlignments: {1: pw.Alignment.centerRight, 2: pw.Alignment.centerRight},
            ),
          pw.SizedBox(height: 20),
          pw.Text('Pojedinačne uplate', style: const pw.TextStyle(fontSize: 14, fontWeight: pw.FontWeight.bold)),
          pw.SizedBox(height: 6),
          if (r.stavke.isNotEmpty)
            pw.TableHelper.fromTextArray(
              headers: ['Datum', 'Klijent', 'Putovanje', 'Iznos'],
              data: [
                for (final s in r.stavke) [formatDatumVrijeme(s.datum), s.korisnik, s.putovanje, formatKM(s.iznos)],
              ],
              headerStyle: const pw.TextStyle(fontWeight: pw.FontWeight.bold),
              headerDecoration: const pw.BoxDecoration(color: PdfColors.grey300),
              cellStyle: const pw.TextStyle(fontSize: 9),
              cellAlignments: {3: pw.Alignment.centerRight},
            ),
          pw.SizedBox(height: 12),
          pw.Align(
            alignment: pw.Alignment.centerRight,
            child: pw.Text('UKUPNO: ${formatKM(r.ukupanIznos)}',
                style: const pw.TextStyle(fontSize: 13, fontWeight: pw.FontWeight.bold)),
          ),
        ],
      ),
    );
    return doc.save();
  }

  pw.Widget _pdfKartica(String naslov, String vrijednost) => pw.Expanded(
        child: pw.Container(
          padding: const pw.EdgeInsets.all(10),
          decoration: pw.BoxDecoration(
            border: pw.Border.all(color: PdfColors.grey500, width: 0.5),
            borderRadius: const pw.BorderRadius.all(pw.Radius.circular(4)),
          ),
          child: pw.Column(crossAxisAlignment: pw.CrossAxisAlignment.start, children: [
            pw.Text(naslov, style: const pw.TextStyle(fontSize: 9, color: PdfColors.grey700)),
            pw.Text(vrijednost, style: const pw.TextStyle(fontSize: 14, fontWeight: pw.FontWeight.bold)),
          ]),
        ),
      );

  @override
  Widget build(BuildContext context) {
    final godine = [for (var g = DateTime.now().year; g >= DateTime.now().year - 4; g--) g];

    return MasterScreen(
      title: 'Izvještaj uplata',
      child: Column(children: [
        Padding(
          padding: const EdgeInsets.all(16),
          child: Wrap(spacing: 12, runSpacing: 12, crossAxisAlignment: WrapCrossAlignment.center, children: [
            SizedBox(
              width: 180,
              child: DropdownButtonFormField<int>(
                initialValue: _mjesec,
                decoration: const InputDecoration(labelText: 'Mjesec', border: OutlineInputBorder(), isDense: true),
                items: [for (var m = 1; m <= 12; m++) DropdownMenuItem(value: m, child: Text(mjeseci[m - 1]))],
                onChanged: (v) => setState(() => _mjesec = v ?? _mjesec),
              ),
            ),
            SizedBox(
              width: 140,
              child: DropdownButtonFormField<int>(
                initialValue: _godina,
                decoration: const InputDecoration(labelText: 'Godina', border: OutlineInputBorder(), isDense: true),
                items: [for (final g in godine) DropdownMenuItem(value: g, child: Text('$g'))],
                onChanged: (v) => setState(() => _godina = v ?? _godina),
              ),
            ),
            FilledButton.icon(
              onPressed: _ucitavanje ? null : _generisi,
              icon: const Icon(Icons.assessment_outlined),
              label: const Text('Generiši izvještaj'),
            ),
            if (_pdf != null) ...[
              OutlinedButton.icon(onPressed: _sacuvaj, icon: const Icon(Icons.download), label: const Text('Preuzmi PDF')),
              OutlinedButton.icon(onPressed: _stampaj, icon: const Icon(Icons.print_outlined), label: const Text('Štampaj')),
            ],
          ]),
        ),
        if (_izvjestaj != null)
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16),
            child: Text(
              '${mjeseci[_izvjestaj!.mjesec - 1]} ${_izvjestaj!.godina}: ${_izvjestaj!.brojUplata} uplata, '
              'ukupno ${formatKM(_izvjestaj!.ukupanIznos)}',
              style: Theme.of(context).textTheme.titleSmall,
            ),
          ),
        Expanded(
          child: _ucitavanje
              ? const Center(child: CircularProgressIndicator())
              : _pdf == null
                  ? const Center(child: Text('Odaberite mjesec i godinu, pa kliknite "Generiši izvještaj".'))
                  : PdfPreview(
                      build: (_) async => _pdf!,
                      allowSharing: false,
                      allowPrinting: false,
                      canChangePageFormat: false,
                      canChangeOrientation: false,
                      canDebug: false,
                      pdfFileName: _nazivFajla,
                    ),
        ),
      ]),
    );
  }
}
