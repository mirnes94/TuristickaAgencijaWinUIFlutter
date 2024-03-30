import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:flutter_stripe/flutter_stripe.dart';
import 'package:http/http.dart' as http;
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/models/Uplate.dart';
import 'package:turisticka_agencija_mobile/providers/uplate_provider.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';

import '../models/Korisnici.dart';
import '../providers/korisnici_provider.dart';

class Uplata extends StatefulWidget {
  static const String routeName = "/uplata";
  const Uplata({Key? key}) : super(key: key);

  @override
  State<Uplata> createState() => _UplataState();
}

class _UplataState extends State<Uplata> {
  TextEditingController rezervacijaController = new TextEditingController();
  TextEditingController iznosUplateController = new TextEditingController();

  KorisniciProvider? _korisniciProvider = null;
  UplateProvider? _uplateProvider = null;

  @override
  initState() {
    super.initState();
    Stripe.publishableKey =
        "pk_test_51LaG1nEUjDocQuVTZ3rZmqN8oXwIeX2JyRUhHyBadwIZatCfvXw5a1qTv4QOG5uQmZqP5zvcNpPWtcy9rbz7Bzmb00Hu193O8s";
    Stripe.merchantIdentifier = "merchant.com.turistickaagencija";
    Stripe.instance.applySettings();
    print("Stripe:${Stripe.merchantIdentifier} - ${Stripe.publishableKey}");
    _korisniciProvider = context.read<KorisniciProvider>();
    _uplateProvider = context.read<UplateProvider>();
  }

  Map<String, dynamic>? paymentIntent;
  final uplataKey = GlobalKey();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Stripe Payment'),
      ),
      body: Center(
        child: Padding(
          padding: EdgeInsets.all(60),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Column(
                children: const [
                  Image(
                      width: 100,
                      height: 100,
                      image: AssetImage("assets/tourist_agency_icon.jpg")),
                  SizedBox(
                    width: 30,
                  ),
                  Text(
                    "Online placanje",
                    style: TextStyle(fontSize: 20),
                  )
                ],
              ),
              SizedBox(
                height: 20,
              ),
              TextField(
                controller: rezervacijaController,
                decoration: InputDecoration(
                    border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(30)),
                    hintText: "Broj rezervacije"),
              ),
              SizedBox(
                height: 20,
              ),
              TextField(
                controller: iznosUplateController,
                decoration: InputDecoration(
                    border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(30)),
                    hintText: "Iznos uplate"),
              ),
              SizedBox(
                height: 20,
              ),
              Container(
                height: 50,
                width: 300,
                decoration: BoxDecoration(
                    color: Colors.amber[500],
                    borderRadius: BorderRadius.circular(30)),
                child: TextButton(
                    onPressed: () async {
                      await makePayment(iznosUplateController.text);
                      await addUplata(int.parse(rezervacijaController.text),
                          int.parse(iznosUplateController.text));
                      Navigator.push(
                          context,
                          MaterialPageRoute(
                              builder: (context) => Uplata(key: uplataKey)));
                      iznosUplateController.text = "";
                      rezervacijaController.text = "";
                    },
                    child: const Text(
                      "Izvrsi uplatu",
                      style: TextStyle(color: Colors.white, fontSize: 20),
                    )),
              )
            ],
          ),
        ),
      ),
    );
  }

  Future<void> makePayment(String iznosUplate) async {
    try {
      paymentIntent = await createPaymentIntent(iznosUplate, 'USD');
      //Payment sheet
      await Stripe.instance.initPaymentSheet(
          paymentSheetParameters: SetupPaymentSheetParameters(
              paymentIntentClientSecret: paymentIntent!['client_secret'],
              merchantCountryCode: 'US',
              applePay: false,
              googlePay: false,
              style: ThemeMode.dark,
              testEnv: true,
              //googlePay: const PaymentSheetGooglePay(testEnv: true,currencyCode:"USD",merchantCountryCode: "123"),
              merchantDisplayName: 'Visa'));
      print("payment sheet created");
      //display payment sheet
      displayPaymentSheet();

      print("after payment sheet presented");
    } catch (e, s) {
      print('exception:$e$s');
    }
  }

  createPaymentIntent(String amount, String currency) async {
    try {
      Map<String, dynamic> body = {
        'amount': calculateAmount(amount),
        'currency': currency,
        'payment_method_types[]': 'card',
      };

      var SECRET_KEY =
          "sk_test_51LaG1nEUjDocQuVTydoHwvFVuCTO3KaRZIM3qAyC4AoN9vfLVzKivTl1DqXnuOlJYlCTCrQbWFe9GP9XuxbeGiIh00oPL85urB";
      var response = await http.post(
          Uri.parse('https://api.stripe.com/v1/payment_intents'),
          body: body,
          headers: {
            'Authorization': 'Bearer $SECRET_KEY',
            'Content-Type': 'application/x-www-form-urlencoded'
          });
      print('Payment Intent Body ->>>${response.body.toString()}');
      return jsonDecode(response.body);
    } catch (e) {
      print('err charging user:${e.toString()}');
    }
  }

  calculateAmount(String amount) {
    final calculatedAmount = (int.parse(amount)) * 100;
    return calculatedAmount.toString();
  }

  Future<void> displayPaymentSheet() async {
    try {
      await Stripe.instance.presentPaymentSheet().then((value) {
        showDialog(
            context: context,
            builder: (_) => AlertDialog(
                  content: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      Row(
                        children: const [
                          Icon(
                            Icons.check_circle,
                            color: Colors.green,
                          ),
                          Text(("Payment Successfull")),
                        ],
                      ),
                    ],
                  ),
                ));

        paymentIntent = null;
      }).onError((error, stackTrace) {
        print('Error is:--->$error $stackTrace');
      });
    } on StripeException catch (e) {
      print('Error is:---> $e');
      showDialog(
          context: context,
          builder: (_) => const AlertDialog(
                content: Text("Cancelled"),
              ));
    } catch (e) {
      print('$e');
    }
  }

  Future<Uplate?> addUplata(int brojRezervacije, int iznosUplate) async {
    int id = 0;

    var korisnici = await _korisniciProvider?.get(null);
    var korisniciList = korisnici!.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList) {
      if (user?.korisnickoIme
              .toString()
              .compareTo(Authorization.username.toString()) ==
          0) {
        id = user!.id;
      }
    }

    if (id != 0) {
      Map<String, dynamic> body = {
        "datum": DateTime.now().toString(),
        "iznos": iznosUplate,
        "rezervacijaId": brojRezervacije,
        "korisnikId": id
      };

      return await _uplateProvider?.insert(body);
    }
  }
}
