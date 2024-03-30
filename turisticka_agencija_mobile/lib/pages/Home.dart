import 'package:flutter/material.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';

import 'Login.dart';

class Home extends StatefulWidget {
  static const String routeName = "/home";
  const Home({Key? key}) : super(key: key);

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text("Sidemenu"),
      ),
      drawer: Drawer(
        child: ListView(
          children: [
            DrawerHeader(
              child: Text("Turisticka Agencija"),
              decoration: BoxDecoration(color: Colors.blue),
            ),
            ListTile(
              title: Text("Profil korisnika"),
              onTap: () {
                Navigator.of(context).pushNamed('/profile');
              },
            ),
            ListTile(
              title: Text("Putovanja"),
              onTap: () {
                Navigator.of(context).pushNamed('/putovanjapage');
              },
            ),
            ListTile(
              title: Text("Obavijesti"),
              onTap: () {
                Navigator.of(context).pushNamed('/obavijestipage');
              },
            ),
            ListTile(
              title: Text("Rezervacije"),
              onTap: () {
                Navigator.of(context).pushNamed('/rezervacijapage');
              },
            ),
            ListTile(
              title: Text("Lista uplata"),
              onTap: () {
                Navigator.of(context).pushNamed('/listauplata');
              },
            ),
            ListTile(
              title: Text("Uplata"),
              onTap: () {
                Navigator.of(context).pushNamed('/uplata');
              },
            ),
            ListTile(
              title: Text("Favoriti"),
              onTap: () {
                Navigator.of(context).pushNamed('/favoriti');
              },
            ),
            ListTile(
              title: Text("Log out"),
              onTap: () {
                if (Authorization.username.isNotEmpty &&
                    Authorization.password.isNotEmpty) {
                  Authorization.username = "";
                  Authorization.password = "";
                  Navigator.push(context,
                      MaterialPageRoute(builder: (context) => Login()));
                }
              },
            ),
          ],
        ),
      ),
    );
  }
}
