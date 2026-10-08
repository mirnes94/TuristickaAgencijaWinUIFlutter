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

  runApp(
    MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => AuthProvider()),
        ChangeNotifierProvider(create: (_) => DrzavaProvider()),
        ChangeNotifierProvider(create: (_) => GradProvider()),
        ChangeNotifierProvider(create: (_) => FirmaProvider()),
        ChangeNotifierProvider(create: (_) => PrevozProvider()),
        ChangeNotifierProvider(create: (_) => SmjestajProvider()),
        ChangeNotifierProvider(create: (_) => UlogaProvider()),
        ChangeNotifierProvider(create: (_) => VodicProvider()),
        ChangeNotifierProvider(create: (_) => KorisnikProvider()),
        ChangeNotifierProvider(create: (_) => PutovanjeProvider()),
        ChangeNotifierProvider(create: (_) => RezervacijaProvider()),
        ChangeNotifierProvider(create: (_) => UplataProvider()),
        ChangeNotifierProvider(create: (_) => KomentarProvider()),
        ChangeNotifierProvider(create: (_) => OcjenaProvider()),
        ChangeNotifierProvider(create: (_) => ObavijestProvider()),
        Provider(create: (_) => IzvjestajProvider()),
        Provider(create: (_) => PreporukaProvider()),
      ],
      child: const TuristickaAgencijaAdminApp(),
    ),
  );
}

class TuristickaAgencijaAdminApp extends StatelessWidget {
  const TuristickaAgencijaAdminApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Turistička agencija - Administracija',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: const Color(0xFF1565C0)),
        useMaterial3: true,
        inputDecorationTheme: const InputDecorationTheme(border: OutlineInputBorder()),
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
