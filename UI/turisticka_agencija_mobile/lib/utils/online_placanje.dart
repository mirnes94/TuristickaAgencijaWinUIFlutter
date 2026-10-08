import 'package:flutter_stripe/flutter_stripe.dart';

import '../providers/api_client.dart';
import '../providers/auth_provider.dart';
import 'api_exception.dart';

/// Online placanje preko Stripe-a.
/// 1) API kreira PaymentIntent (tajni Stripe kljuc je samo u .env konfiguraciji servera),
/// 2) aplikacija prikazuje Stripe Payment Sheet,
/// 3) nakon uspjesnog placanja uplata se evidentira na API-ju, koji kod Stripe-a provjerava da je placanje uspjelo.
class OnlinePlacanje {
  /// Vraca true ako je placanje uspjesno izvrseno i evidentirano, false ako je korisnik odustao.
  static Future<bool> platiRezervaciju({required int rezervacijaId, required double iznos}) async {
    final iznosZaokruzen = double.parse(iznos.toStringAsFixed(2));

    final intent = await ApiClient.post('api/Uplate/PaymentIntent', {
      'rezervacijaId': rezervacijaId,
      'iznos': iznosZaokruzen,
    }) as Map<String, dynamic>;

    final publishableKey = (intent['publishableKey'] as String?) ?? '';
    if (publishableKey.isEmpty) {
      throw ApiException('Online plaćanje nije konfigurisano na serveru.');
    }

    Stripe.publishableKey = publishableKey;
    await Stripe.instance.applySettings();
    await Stripe.instance.initPaymentSheet(
      paymentSheetParameters: SetupPaymentSheetParameters(
        paymentIntentClientSecret: intent['clientSecret'] as String,
        merchantDisplayName: 'Turistička agencija',
      ),
    );

    try {
      await Stripe.instance.presentPaymentSheet();
    } on StripeException catch (e) {
      if (e.error.code == FailureCode.Canceled) return false;
      throw ApiException(e.error.localizedMessage ?? 'Plaćanje nije uspjelo.');
    }

    await ApiClient.post('api/Uplate', {
      'rezervacijaId': rezervacijaId,
      'iznos': iznosZaokruzen,
      'korisnikId': AuthProvider.korisnikId,
      'stripePaymentIntentId': intent['paymentIntentId'],
    });
    return true;
  }
}
