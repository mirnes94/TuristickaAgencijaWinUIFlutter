import 'dart:convert';
import 'dart:typed_data';

import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

final _datum = DateFormat('dd.MM.yyyy');
final _datumVrijeme = DateFormat('dd.MM.yyyy HH:mm');
final _novac = NumberFormat('#,##0.00', 'bs');

String formatDatum(DateTime? d) => d == null ? '-' : _datum.format(d);
String formatDatumVrijeme(DateTime? d) => d == null ? '-' : _datumVrijeme.format(d);
String formatKM(num? iznos) => iznos == null ? '-' : '${_novac.format(iznos)} KM';

const mjeseci = [
  'Januar', 'Februar', 'Mart', 'April', 'Maj', 'Juni',
  'Juli', 'August', 'Septembar', 'Oktobar', 'Novembar', 'Decembar'
];

DateTime? parseDate(dynamic value) => value == null ? null : DateTime.tryParse(value.toString())?.toLocal();

Uint8List? bytesFromBase64(String? value) {
  if (value == null || value.isEmpty) return null;
  try {
    return base64Decode(value);
  } catch (_) {
    return null;
  }
}

Widget slikaIliIkona(String? base64, {double size = 48, IconData ikona = Icons.image_not_supported_outlined}) {
  final bytes = bytesFromBase64(base64);
  if (bytes == null) {
    return SizedBox(width: size, height: size, child: Icon(ikona, color: Colors.grey));
  }
  return ClipRRect(
    borderRadius: BorderRadius.circular(6),
    child: Image.memory(bytes, width: size, height: size, fit: BoxFit.cover,
        errorBuilder: (_, _, _) =>Icon(ikona, size: size, color: Colors.grey)),
  );
}
