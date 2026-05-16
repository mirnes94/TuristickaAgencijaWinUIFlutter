import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/Gradovi.dart';
import '../providers/gradovi_provider.dart';

class PutovanjaDodajPage extends StatefulWidget {
  static const String routeName = "/putovanjadodajpage";

  @override
  _PutovanjaDodajPageState createState() => _PutovanjaDodajPageState();
}

class _PutovanjaDodajPageState extends State<PutovanjaDodajPage> {
  final _formKey = GlobalKey<FormState>();

  final ScrollController _scrollController = ScrollController();

  final TextEditingController nazivPutovanjaController =
      TextEditingController();
  final TextEditingController opisPutovanjaController = TextEditingController();
  final TextEditingController brojMjestaController = TextEditingController();
  final TextEditingController cijenaPutovanjaController =
      TextEditingController();
  final TextEditingController datumPolaskaController = TextEditingController();
  final TextEditingController datumDolaskaController = TextEditingController();

  GradoviProvider? _gradoviProvider;
  Gradovi? _selectedGrad;
  List<Gradovi?> _gradoviList = [];

  @override
  void initState() {
    super.initState();
    _gradoviProvider = context.read<GradoviProvider>();
    loadGradovi();
  }

  @override
  void dispose() {
    _scrollController.dispose();
    nazivPutovanjaController.dispose();
    opisPutovanjaController.dispose();
    brojMjestaController.dispose();
    cijenaPutovanjaController.dispose();
    datumPolaskaController.dispose();
    datumDolaskaController.dispose();
    super.dispose();
  }

  Future<void> loadGradovi() async {
    var gradovi = await _gradoviProvider?.get(null);
    var gradoviList =
        gradovi!.map((i) => _gradoviProvider?.fromJson(i)).toList();
    setState(() {
      _gradoviList = gradoviList;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: const Text('Dodaj putovanje'),
        ),
        body: Scrollbar(
          controller: _scrollController,
          thumbVisibility: true,
          child: SingleChildScrollView(
            controller: _scrollController,
            child: Padding(
              padding: const EdgeInsets.all(20),
              child: Form(
                key: _formKey,
                child: Column(
                  children: [
                    TextFormField(
                      controller: nazivPutovanjaController,
                      decoration: InputDecoration(
                        labelText: 'Naziv putovanja',
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(20)),
                      ),
                      validator: (value) => value == null || value.isEmpty
                          ? 'Unesite naziv putovanja'
                          : null,
                    ),
                    SizedBox(height: 20),
                    TextFormField(
                      controller: opisPutovanjaController,
                      keyboardType: TextInputType.multiline,
                      maxLines: 8,
                      decoration: InputDecoration(
                        labelText: 'Opis putovanja',
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(20)),
                      ),
                      validator: (value) => value == null || value.isEmpty
                          ? 'Unesite opis putovanja'
                          : null,
                    ),
                    SizedBox(height: 20),
                    TextFormField(
                      controller: cijenaPutovanjaController,
                      keyboardType: TextInputType.number,
                      decoration: InputDecoration(
                        labelText: 'Cijena putovanja',
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(20)),
                      ),
                      validator: (value) {
                        if (value == null || value.isEmpty)
                          return 'Unesite cijenu putovanja';
                        if (double.tryParse(value) == null)
                          return 'Unesite ispravnu cijenu';
                        return null;
                      },
                    ),
                    SizedBox(height: 20),
                    TextFormField(
                      controller: brojMjestaController,
                      keyboardType: TextInputType.number,
                      decoration: InputDecoration(
                        labelText: 'Broj mjesta',
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(20)),
                      ),
                      validator: (value) {
                        if (value == null || value.isEmpty)
                          return 'Unesite broj mjesta';
                        if (int.tryParse(value) == null)
                          return 'Unesite ispravan broj';
                        return null;
                      },
                    ),
                    SizedBox(height: 20),
                    DropdownButtonFormField<Gradovi>(
                      decoration: InputDecoration(
                        labelText: 'Odaberite grad',
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(20)),
                      ),
                      isExpanded: true,
                      value: _selectedGrad,
                      items: _gradoviList.map((Gradovi? grad) {
                        return DropdownMenuItem<Gradovi>(
                          value: grad,
                          child: Text(grad?.nazivGrada ?? ''),
                        );
                      }).toList(),
                      onChanged: (Gradovi? newValue) {
                        setState(() {
                          _selectedGrad = newValue;
                        });
                      },
                      validator: (value) =>
                          value == null ? 'Molimo odaberite grad' : null,
                    ),
                    SizedBox(height: 20),
                    TextField(
                      controller: datumPolaskaController,
                      readOnly: true,
                      decoration: InputDecoration(
                        labelText: "Datum polaska",
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(20)),
                        suffixIcon: Icon(Icons.calendar_today),
                      ),
                      onTap: () async {
                        DateTime? pickedDate = await showDatePicker(
                          context: context,
                          initialDate: DateTime.now(),
                          firstDate: DateTime(2020),
                          lastDate: DateTime(2100),
                        );
                        if (pickedDate != null) {
                          String formattedDate =
                              "${pickedDate.day}.${pickedDate.month}.${pickedDate.year}";
                          datumPolaskaController.text = formattedDate;
                        }
                      },
                    ),
                    SizedBox(height: 20),
                    TextField(
                      controller: datumDolaskaController,
                      readOnly: true,
                      decoration: InputDecoration(
                        labelText: "Datum dolaska",
                        border: OutlineInputBorder(
                            borderRadius: BorderRadius.circular(20)),
                        suffixIcon: Icon(Icons.calendar_today),
                      ),
                      onTap: () async {
                        DateTime? pickedDate = await showDatePicker(
                          context: context,
                          initialDate: DateTime.now(),
                          firstDate: DateTime(2020),
                          lastDate: DateTime(2100),
                        );
                        if (pickedDate != null) {
                          String formattedDate =
                              "${pickedDate.day}.${pickedDate.month}.${pickedDate.year}";
                          datumDolaskaController.text = formattedDate;
                        }
                      },
                    ),
                    SizedBox(height: 30),
                    Container(
                      height: 50,
                      width: 300,
                      decoration: BoxDecoration(
                        color: Colors.amber[500],
                        borderRadius: BorderRadius.circular(30),
                      ),
                      child: TextButton(
                        onPressed: () {
                          if (_formKey.currentState!.validate()) {
                            print('Naziv: ${nazivPutovanjaController.text}');
                            print('Opis: ${opisPutovanjaController.text}');
                            print('Cijena: ${cijenaPutovanjaController.text}');
                            print('Broj mjesta: ${brojMjestaController.text}');
                            print(
                                'Datum polaska: ${datumPolaskaController.text}');
                            print(
                                'Datum dolaska: ${datumDolaskaController.text}');
                            print('Grad: ${_selectedGrad?.nazivGrada}');

                            ScaffoldMessenger.of(context).showSnackBar(
                              SnackBar(
                                  content: Text('Putovanje uspješno dodano!')),
                            );

                            nazivPutovanjaController.clear();
                            opisPutovanjaController.clear();
                            cijenaPutovanjaController.clear();
                            brojMjestaController.clear();
                            datumPolaskaController.clear();
                            datumDolaskaController.clear();
                            setState(() {
                              _selectedGrad = null;
                            });
                          }
                        },
                        child: const Text(
                          "Dodaj putovanje",
                          style: TextStyle(color: Colors.white, fontSize: 20),
                        ),
                      ),
                    ),
                  ],
                ),
              ),
            ),
          ),
        ));
  }
}
