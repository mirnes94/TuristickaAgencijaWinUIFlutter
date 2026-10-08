import 'package:flutter_test/flutter_test.dart';
import 'package:turisticka_agencija_admin/utils/validators.dart';

void main() {
  test('Validatori vracaju jasne poruke', () {
    expect(Validators.required(''), isNotNull);
    expect(Validators.email('ime@domena.com'), isNull);
    expect(Validators.email('pogresan-email'), isNotNull);
    expect(Validators.telefon('061123456'), isNull);
    expect(Validators.telefon('06a'), isNotNull);
  });
}
