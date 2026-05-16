import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'package:turisticka_agencija_mobile/providers/authenticate_provider.dart';
import 'package:turisticka_agencija_mobile/utils/util.dart';

import '../providers/korisnici_provider.dart';

class Login extends StatefulWidget {
  const Login({Key? key}) : super(key: key);

  @override
  State<Login> createState() => _LoginState();
}

class _LoginState extends State<Login> {
  TextEditingController usernameController = new TextEditingController();
  TextEditingController passwordController = new TextEditingController();
  var result;
  var id = -1;
  bool _visible = false;
  KorisniciProvider? _korisniciProvider = null;
  AuthenticateProvider? _authenticateProvider = null;
  String  errorMessage="";

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    _korisniciProvider = context.read<KorisniciProvider>();
    _authenticateProvider = context.read<AuthenticateProvider>();
  }
  void getData() async {
    result = await _korisniciProvider?.get();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
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
                "Turisticka Agencija",
                style: TextStyle(fontSize: 20),
              )
            ],
          ),
          SizedBox(
            height: 20,
          ),
          TextField(
            controller: usernameController,
            decoration: InputDecoration(
                border:
                    OutlineInputBorder(borderRadius: BorderRadius.circular(30)),
                hintText: "Korisnicko ime"),
          ),
          SizedBox(
            height: 20,
          ),
          TextField(
            controller: passwordController,
            decoration: InputDecoration(
                border:
                    OutlineInputBorder(borderRadius: BorderRadius.circular(30)),
                hintText: "Password"),
            obscureText: true,
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
                  print(
                      usernameController.text + ' ' + passwordController.text);
                  Authorization.username = usernameController.text;
                  Authorization.password = passwordController.text;

                    String usernamePassword = (Authorization.username + "," +
                        Authorization.password).replaceAll(' ', '');

                    var response = await _authenticateProvider?.authenticate(
                        usernamePassword);
                    if(response == 522) {
                      setState(() {
                        if (_visible == false) {
                          _visible = !_visible;
                        }
                        usernameController.text = "";
                        passwordController.text = "";

                        errorMessage ="Connection timeout";
                      });
                    }else if (response == 200) {
                      Navigator.of(context).pushReplacementNamed('/home');
                      print(result);
                    } else if (response == 204) {
                      setState(() {
                        if (_visible == false) {
                          _visible = !_visible;
                        }
                        usernameController.text = "";
                        passwordController.text = "";

                        errorMessage ="Invalid username or password";
                      });
                    } else if(response == 400){
                      setState(() {
                        if (_visible == false) {
                          _visible = !_visible;
                        }
                        usernameController.text = "";
                        passwordController.text = "";

                        errorMessage ="Bad request";
                      });
                    }
                    else if(response == 401){
                      setState(() {
                        if (_visible == false) {
                          _visible = !_visible;
                        }
                        usernameController.text = "";
                        passwordController.text = "";

                        errorMessage ="Unauthorized";
                      });
                    }
                    else if(response == 403){
                      setState(() {
                        if (_visible == false) {
                          _visible = !_visible;
                        }
                        usernameController.text = "";
                        passwordController.text = "";

                        errorMessage ="Forbidden";
                      });
                    } else if(response == 404){
                      setState(() {
                        if (_visible == false) {
                          _visible = !_visible;
                        }
                        usernameController.text = "";
                        passwordController.text = "";

                        errorMessage ="Not found";
                      });
                    }
                    else if(response == 500){
                      setState(() {
                        if (_visible == false) {
                          _visible = !_visible;
                        }
                        usernameController.text = "";
                        passwordController.text = "";

                        errorMessage ="Internal service error";
                      });
                    }else if(response == 522){
                      setState(() {
                        if (_visible == false) {
                          _visible = !_visible;
                        }
                        usernameController.text = "";
                        passwordController.text = "";

                        errorMessage ="Connection timed out";
                      });
                    }else {
                      setState(() {
                        if (_visible == false) {
                          _visible = !_visible;
                        }
                        usernameController.text = "";
                        passwordController.text = "";

                        errorMessage ="Exception... handle this gracefully";
                      });
                    }
                  print("Response"+ response.toString());

                  //Navigator.of(context).pushReplacementNamed('/home');
                },
                child: const Text(
                  "Login",
                  style: TextStyle(color: Colors.white, fontSize: 20),
                )),
          ),
          Visibility(
            child: Text(
                errorMessage,
              style: TextStyle(color: Colors.red, fontSize: 15),
            ),
            visible: _visible,
          ),
        ],
      ),
    )));
  }

}
