double toDouble(dynamic v) => v == null ? 0 : (v as num).toDouble();
int toInt(dynamic v) => v == null ? 0 : (v as num).toInt();
int? toIntOrNull(dynamic v) => v == null ? null : (v as num).toInt();
List<int> toIntList(dynamic v) => v == null ? <int>[] : (v as List).map((e) => (e as num).toInt()).toList();
List<String> toStringList(dynamic v) => v == null ? <String>[] : (v as List).map((e) => e.toString()).toList();
