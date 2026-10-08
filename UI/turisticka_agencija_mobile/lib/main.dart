import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:intl/intl.dart';
import 'package:provider/provider.dart';

import 'providers/auth_provider.dart';
import 'providers/providers.dart';
import 'screens/login_screen.dart';

Future<void> main() async {
  WidgetsFlutterBinding.ensureInitialized();
  Intl.defaultLocale = 'bs';
  await initializeDateFormatting('bs');
  // Stripe publishable key se ne cuva u aplikaciji - dobija se sa API-ja prije svakog placanja.

  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => AuthProvider()),
        ChangeNotifierProvider(create: (_) => GradProvider()),
        ChangeNotifierProvider(create: (_) => KorisnikProvider()),
        ChangeNotifierProvider(create: (_) => PutovanjeProvider()),
        ChangeNotifierProvider(create: (_) => RezervacijaProvider()),
        ChangeNotifierProvider(create: (_) => UplataProvider()),
        ChangeNotifierProvider(create: (_) => KomentarProvider()),
        ChangeNotifierProvider(create: (_) => OcjenaProvider()),
        ChangeNotifierProvider(create: (_) => ListaZeljaProvider()),
        ChangeNotifierProvider(create: (_) => ObavijestProvider()),
      ],
      child: const TuristickaAgencijaApp(),
    ),
  );
}

class TuristickaAgencijaApp extends StatelessWidget {
  const TuristickaAgencijaApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Turistička agencija',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: const Color(0xFF1565C0)),
        useMaterial3: true,
      ),
      locale: const Locale('bs'),
      supportedLocales: const [Locale('bs'), Locale('en')],
      localizationsDelegates: const [
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate,
      ],
      home: const LoginScreen(),
    );
  }
}
