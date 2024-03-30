import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter_stripe/flutter_stripe.dart';
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/models/ListaZelja.dart';
import 'package:turisticka_agencija_mobile/models/Obavijesti.dart';
import 'package:turisticka_agencija_mobile/models/Ocjene.dart';
import 'package:turisticka_agencija_mobile/models/Prevoz.dart';
import 'package:turisticka_agencija_mobile/models/Recommender.dart';
import 'package:turisticka_agencija_mobile/models/Rezervacija.dart';
import 'package:turisticka_agencija_mobile/models/Uloga.dart';
import 'package:turisticka_agencija_mobile/models/Uplate.dart';
import 'package:turisticka_agencija_mobile/models/Vodic.dart';
import 'package:turisticka_agencija_mobile/models/VodiciPutovanja.dart';
//import 'package:shared_preferences/shared_preferences.dart';
import 'package:turisticka_agencija_mobile/pages/Favoriti.dart';
import 'package:turisticka_agencija_mobile/pages/Home.dart';
import 'package:turisticka_agencija_mobile/pages/ListaUplata.dart';
import 'package:turisticka_agencija_mobile/pages/Login.dart';
import 'package:turisticka_agencija_mobile/pages/ObavijestiPage.dart';
import 'package:turisticka_agencija_mobile/pages/Profile.dart';
import 'package:turisticka_agencija_mobile/pages/PutovanjaPage.dart';
import 'package:turisticka_agencija_mobile/pages/Uplata.dart';
import 'package:turisticka_agencija_mobile/providers/authenticate_provider.dart';
import 'package:turisticka_agencija_mobile/providers/firma_provider.dart';
import 'package:turisticka_agencija_mobile/providers/gradovi_provider.dart';
import 'package:turisticka_agencija_mobile/providers/komentar_model_provider.dart';
import 'package:turisticka_agencija_mobile/providers/komentar_provider.dart';
import 'package:turisticka_agencija_mobile/providers/korisnici_provider.dart';
import 'package:turisticka_agencija_mobile/providers/korisnici_uloge_provider.dart';
import 'package:turisticka_agencija_mobile/providers/lista_zelja_provider.dart';
import 'package:turisticka_agencija_mobile/providers/obavijesti_provider.dart';
import 'package:turisticka_agencija_mobile/providers/ocjene_provider.dart';
import 'package:turisticka_agencija_mobile/providers/prevoz_provider.dart';
import 'package:turisticka_agencija_mobile/providers/putovanja_provider.dart';
import 'package:turisticka_agencija_mobile/providers/recommender_provider.dart';
import 'package:turisticka_agencija_mobile/providers/rezervacija_provider.dart';
import 'package:turisticka_agencija_mobile/providers/smjestaj_provider.dart';
import 'package:turisticka_agencija_mobile/providers/uloga_provider.dart';
import 'package:turisticka_agencija_mobile/providers/uloge_provider.dart';
import 'package:turisticka_agencija_mobile/providers/uplate_provider.dart';
import 'package:turisticka_agencija_mobile/providers/vodic_provider.dart';
import 'package:turisticka_agencija_mobile/providers/vodici_putovanja.dart';

import 'pages/Loading.dart';
import 'pages/RezervacijaDetaljiPage.dart';
import 'pages/RezervacijaPage.dart';
import 'providers/drzava_provider.dart';


Future<void> main() async {
  //initializeLocator();

  WidgetsFlutterBinding.ensureInitialized();
  Stripe.publishableKey =
      "pk_test_51LaG1nEUjDocQuVTZ3rZmqN8oXwIeX2JyRUhHyBadwIZatCfvXw5a1qTv4QOG5uQmZqP5zvcNpPWtcy9rbz7Bzmb00Hu193O8s";
  //Stripe.stripeAccountId = "acct_1LaG1nEUjDocQuVTt";
  Stripe.merchantIdentifier = "merchant.com.turistickaagencija";
  Stripe.instance.applySettings();
  print("Stripe:${Stripe.merchantIdentifier} - ${Stripe.publishableKey}");
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({Key? key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MultiProvider(providers: [
      ChangeNotifierProvider(create: (context) => DrzavaProvider()),
      ChangeNotifierProvider(create: (context) => FirmaProvider()),
      ChangeNotifierProvider(create: (context) => GradoviProvider()),
      ChangeNotifierProvider(create: (context) => KomentarModelProvider()),
      ChangeNotifierProvider(create: (context) => KomentarProvider()),
      ChangeNotifierProvider(create: (context) => KorisniciProvider()),
      ChangeNotifierProvider(create: (context) => KorisniciUlogeProvider()),
      ChangeNotifierProvider(create: (context) => ListaZeljaProvider()),
      ChangeNotifierProvider(create: (context) => ObavijestiProvider()),
      ChangeNotifierProvider(create: (context) => OcjeneProvider()),
      ChangeNotifierProvider(create: (context) => PrevozProvider()),
      ChangeNotifierProvider(create: (context) => PutovanjaProvider()),
      ChangeNotifierProvider(create: (context) => RecommenderProvider()),
      ChangeNotifierProvider(create: (context) => RezervacijaProvider()),
      ChangeNotifierProvider(create: (context) => SmjestajProvider()),
      ChangeNotifierProvider(create: (context) => UlogaProvider()),
      ChangeNotifierProvider(create: (context) => UlogeProvider()),
      ChangeNotifierProvider(create: (context) => UplateProvider()),
      ChangeNotifierProvider(create: (context) => VodicProvider()),
      ChangeNotifierProvider(create: (context) => VodiciPutovanjaProvider()),
      ChangeNotifierProvider(create: (context) => AuthenticateProvider())
    ],
    child:MaterialApp(
      title: 'Turisticka Agencija',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: Login(),
      //initial route login
      onGenerateRoute: (settings) {
        if (settings.name == Loading.routeName) {
          return MaterialPageRoute(
              builder: ((context) => Loading()));
        } else if (settings.name == Home.routeName) {
          return MaterialPageRoute(
              builder: ((context) => Home()));
        } else if (settings.name == Profile.routeName){
          return MaterialPageRoute(
              builder: ((context) => Profile()));
        }
        else if (settings.name == PutovanjaPage.routeName){
          return MaterialPageRoute(
              builder: ((context) => PutovanjaPage()));
        }
        else if (settings.name == ObavijestiPage.routeName){
          return MaterialPageRoute(
              builder: ((context) => ObavijestiPage()));
        }
        else if (settings.name == RezervacijaPage.routeName){
          return MaterialPageRoute(
              builder: ((context) => RezervacijaPage()));
        }
        else if (settings.name == ListaUplata.routeName){
          return MaterialPageRoute(
              builder: ((context) => ListaUplata()));
        }
        else if (settings.name == Uplata.routeName){
          return MaterialPageRoute(
              builder: ((context) => Uplata()));
        }
        else if (settings.name == Favoriti.routeName){
          return MaterialPageRoute(
              builder: ((context) => Favoriti()));
        }

      },
    ));
  }
}
