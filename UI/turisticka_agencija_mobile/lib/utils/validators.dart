/// Validatori sa jasnim porukama o formatu i ogranicenjima unosa.
/// Poruke se prikazuju ispod polja (TextFormField.validator).
class Validators {
  static String? required(String? value, [String poruka = 'Polje je obavezno.']) {
    if (value == null || value.trim().isEmpty) return poruka;
    return null;
  }

  static String? Function(String?) duzina({required int min, required int max, bool obavezno = true}) {
    return (String? value) {
      final v = value?.trim() ?? '';
      if (v.isEmpty) return obavezno ? 'Polje je obavezno.' : null;
      if (v.length < min || v.length > max) {
        return 'Unesite između $min i $max znakova (trenutno ${v.length}).';
      }
      return null;
    };
  }

  static String? email(String? value) {
    final v = value?.trim() ?? '';
    if (v.isEmpty) return 'Email je obavezan.';
    final regex = RegExp(r'^[\w.+-]+@[\w-]+(\.[\w-]+)+$');
    if (!regex.hasMatch(v)) return 'Unesite validan email, npr. ime@domena.com';
    return null;
  }

  static String? telefon(String? value) {
    final v = value?.trim() ?? '';
    if (v.isEmpty) return 'Telefon je obavezan.';
    if (!RegExp(r'^\+?\d{6,15}$').hasMatch(v)) {
      return 'Unesite samo cifre (6-15), npr. 061123456 ili +38761123456';
    }
    return null;
  }

  static String? jmbg(String? value) {
    final v = value?.trim() ?? '';
    if (v.isEmpty) return 'JMBG je obavezan.';
    if (!RegExp(r'^\d{13}$').hasMatch(v)) return 'JMBG mora imati tačno 13 cifara.';
    return null;
  }

  static String? ziroRacun(String? value) {
    final v = value?.trim() ?? '';
    if (v.isEmpty) return 'Broj žiro računa je obavezan.';
    if (!RegExp(r'^\d{13,16}$').hasMatch(v)) {
      return 'Unesite validan broj transakcijskog računa (13-16 cifara, bez razmaka).';
    }
    return null;
  }

  static String? Function(String?) cijeliBroj({required int min, required int max}) {
    return (String? value) {
      final v = value?.trim() ?? '';
      if (v.isEmpty) return 'Polje je obavezno.';
      final broj = int.tryParse(v);
      if (broj == null) return 'Unesite cijeli broj (npr. 25).';
      if (broj < min || broj > max) return 'Unesite broj između $min i $max.';
      return null;
    };
  }

  static String? Function(String?) decimalniBroj({double min = 0.01, double max = 1000000}) {
    return (String? value) {
      final v = value?.trim().replaceAll(',', '.') ?? '';
      if (v.isEmpty) return 'Polje je obavezno.';
      final broj = double.tryParse(v);
      if (broj == null) return 'Unesite broj, npr. 150 ili 149.90';
      if (broj < min || broj > max) return 'Unesite iznos između ${min.toStringAsFixed(2)} i ${max.toStringAsFixed(0)}.';
      return null;
    };
  }

  static String? lozinka(String? value, {bool obavezna = true}) {
    final v = value ?? '';
    if (v.isEmpty) return obavezna ? 'Lozinka je obavezna.' : null;
    if (v.length < 4) return 'Lozinka mora imati najmanje 4 znaka.';
    return null;
  }

  static String? dropdown(Object? value, String naziv) {
    if (value == null) return 'Odaberite $naziv.';
    return null;
  }

  static double parseDouble(String text) => double.parse(text.trim().replaceAll(',', '.'));
}
