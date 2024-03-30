import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/pages/Uplata.dart';

import '../models/Korisnici.dart';
import '../models/Putovanja.dart';
import '../models/Rezervacija.dart';
import '../models/Uplate.dart';
import '../providers/korisnici_provider.dart';
import '../providers/rezervacija_provider.dart';
import '../providers/uplate_provider.dart';

import 'package:flutter_stripe/flutter_stripe.dart';
import 'package:http/http.dart' as http;

import '../utils/util.dart';

class RezervacijaDetaljiPage extends StatelessWidget {
  TextEditingController iznosUplateController = new TextEditingController();
  TextEditingController brojOsobaController = new TextEditingController();
  TextEditingController napomenaController = new TextEditingController();

  final Putovanja putovanje;
  RezervacijaProvider? _rezervacijaProvider = null;
  KorisniciProvider? _korisniciProvider = null;
  UplateProvider? _uplateProvider = null;

  RezervacijaDetaljiPage({required Key key, required this.putovanje})
      : super(key: key);

  Map<String, dynamic>? paymentIntent;
  final navigatorKey = GlobalKey();

  var ctx;
  @override
  Widget build(BuildContext context) {
    ctx = context;
    _rezervacijaProvider = context.read<RezervacijaProvider>();
    _korisniciProvider = context.read<KorisniciProvider>();
    _uplateProvider = context.read<UplateProvider>();
    return Scaffold(
      appBar: AppBar(
        title: const Text('Rezervacija detalji'),
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: EdgeInsets.all(60),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Column(
                children: [
                  SingleChildScrollView(
                    child: FutureBuilder<List<Korisnici?>>(
                      future: getKorisnik(),
                      builder: (BuildContext context,
                          AsyncSnapshot<List<Korisnici?>> snapshot) {
                        if (snapshot.connectionState ==
                            (ConnectionState.waiting)) {
                          return const Center(child: Text("Loading..."));
                        } else if (snapshot.hasError) {
                          return Center(
                            child: Text('($snapshot.error)'),
                          );
                        } else {
                          return korisnikWidget();
                        }
                      },
                    ),
                  ),
                  SizedBox(
                    width: 30,
                  ),
                ],
              ),
              Text(
                "Naziv:" + putovanje.nazivPutovanja,
                style: TextStyle(fontSize: 20),
              ),
              Text(
                "Opis:" + putovanje.opisPutovanja,
                style: TextStyle(fontSize: 20),
              ),
              Text(
                "Datum polaska:" +
                    putovanje.datumPolaska
                        .substring(0, putovanje.datumPolaska.indexOf("T")),
                style: TextStyle(fontSize: 20),
              ),
              Text(
                "Datum dolaska:" +
                    putovanje.datumDolaska
                        .substring(0, putovanje.datumDolaska.indexOf("T")),
                style: TextStyle(fontSize: 20),
              ),
              Text(
                "Broj  mjesta:" + putovanje.brojMjesta.toString(),
                style: TextStyle(fontSize: 20),
              ),
              Text(
                "Cijena:" + putovanje.cijenaPutovanja.toString(),
                style: TextStyle(fontSize: 20),
              ),
              SizedBox(
                height: 20,
              ),
              TextField(
                controller: brojOsobaController,
                decoration: InputDecoration(
                    border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(20)),
                    hintText: "Broj osoba"),
              ),
              SizedBox(
                height: 20,
              ),
              TextField(
                controller: iznosUplateController,
                decoration: InputDecoration(
                    border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(20)),
                    hintText: "Iznos uplate"),
              ),
              SizedBox(
                height: 20,
              ),
              TextField(
                controller: napomenaController,
                decoration: InputDecoration(
                    border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(20)),
                    hintText: "Napomena"),
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
                      await makePayment();
                      await addRezervacija(
                          int.parse(brojOsobaController.text),
                          napomenaController.text,
                          int.parse(iznosUplateController.text));
                      iznosUplateController.text = "";
                      brojOsobaController.text = "";
                      napomenaController.text = "";
                    },
                    child: const Text(
                      "Rezervisi",
                      style: TextStyle(color: Colors.white, fontSize: 20),
                    )),
              )
            ],
          ),
        ),
      ),
    );
  }

  Widget korisnikWidget() {
    return FutureBuilder<List<Korisnici?>>(
      future: getKorisnik(),
      builder: (BuildContext context, AsyncSnapshot<List<Korisnici?>> snapshot) {
        if (snapshot.connectionState == (ConnectionState.waiting)) {
          return const Center(child: Text("Loading..."));
        } else if (snapshot.hasError) {
          return Center(
            child: Text('($snapshot.error)'),
          );
        } else {
          return Center(
            child: Padding(
              padding: const EdgeInsets.all(15),
              child: Text(
                  '${snapshot.data?.first?.ime}\n${snapshot.data?.first?.prezime}\n${snapshot.data?.first?.email}\n${snapshot.data?.first?.telefon}',
                  style: TextStyle(fontSize: 20)),
            ),
          );
        }
      },
    );
  }

  Future makePayment() async {
    try {
      paymentIntent =
          await createPaymentIntent(iznosUplateController.text, 'USD');
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
            context: ctx as BuildContext,
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
          context: ctx,
          builder: (_) => const AlertDialog(
                content: Text("Cancelled"),
              ));
    } catch (e) {
      print('$e');
    }
  }

  Future<List<Korisnici?>> getKorisnik() async {
    Map<String, String>? queryParams;

    String ime = "";
    String prezime = "";

    var korisnici = await _korisniciProvider?.get(null);
    var korisniciList = korisnici!.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList) {
      if (user?.korisnickoIme == Authorization.username) {
        ime = user!.ime;
        prezime = user.prezime;
      }
    }
    if (ime.isNotEmpty && prezime.isNotEmpty) {
      queryParams = {'Ime': ime, 'Prezime': prezime};
    }

    var korisnik = await _korisniciProvider?.get(queryParams);
    return korisnik!.map((i) => _korisniciProvider?.fromJson(i)).toList();
  }

  Future addRezervacija(int brojOsoba, String napomena, int iznosUplate) async {
    int korisnikId = 0;
    String ime = "";
    Map<String, String>? queryParams;
    var korisnici = await _korisniciProvider?.get(null);
    var korisniciList = korisnici!.map((i) => _korisniciProvider?.fromJson(i)).toList();

    for (Korisnici? user in korisniciList) {
      if (user?.korisnickoIme
              .toString()
              .compareTo(Authorization.username.toString()) ==
          0) {
        korisnikId = user!.id;
        ime = "${user.ime} ${user.prezime}";
      }
    }

    Map<String, dynamic> bodyRezervacija = {
      "ime": ime,
      "korisnikId": korisnikId,
      "putovanjeId": putovanje.id,
      "datumRezervacije": DateTime.now().toString(),
      "brojOsoba": brojOsoba,
      "status": "Rezervisano",
      "napomena": napomena
    };
    var postRezervacija = await _rezervacijaProvider?.insert(bodyRezervacija);

    print("Rezervacija" + postRezervacija.toString());
    queryParams = {'KorisnikId': korisnikId.toString()};

    var getRezervacije = await _rezervacijaProvider?.get(queryParams);
    var getRezervacijeList =
        getRezervacije!.map((i) => _rezervacijaProvider?.fromJson(i)).toList();

    int rezervacijaId = -1;
    for (Rezervacija? rezervacija in getRezervacijeList) {
      if (rezervacija == getRezervacijeList.last)
        rezervacijaId = rezervacija!.id;
    }

    print("Rezervacija id" + rezervacijaId.toString());

    Map<String, dynamic> bodyUplata = {
      "datum": DateTime.now().toString(),
      "iznos": iznosUplate,
      "rezervacijaId": rezervacijaId,
      "korisnikId": korisnikId
    };
    await _uplateProvider?.insert(bodyUplata);
  }
}
