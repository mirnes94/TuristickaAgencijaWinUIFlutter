using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TuristickaAgencija.WebAPI.Database
{
    public partial class TuristickaAgencijaContext
    {
        public static byte[] ReadFile(string sPath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes 
            //to read from file.
            //In this case we want to read entire file. 
            //So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);

            return data;
        }
    
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Korisnici>().HasData(
                new WebAPI.Database.Korisnici()
                {
                    //password za sve korisnike je: 123456
                    Id = 1,
                    Ime = "Mirnes",
                    Prezime = "Turkovic",
                    Email = "admin@mail.com",
                    Telefon = "061478951",
                    KorisnickoIme = "tmirnes",
                    LozinkaHash = "Fcm514xjmNp/HVnC/upkWqW0G+0=",
                    LozinkaSalt = "YW55IGNhcm5hbCBwbGVhc3VyZQ==",
                    Status = true
                },
                new Korisnici
                {
                    Id = 2,
                    Ime = "Amila",
                    Prezime = "Turkovic",
                    Email = "amila@mail.com",
                    Telefon = "062479951",
                    KorisnickoIme = "tamila",
                    LozinkaHash = "Fcm514xjmNp/HVnC/upkWqW0G+0=",
                    LozinkaSalt = "YW55IGNhcm5hbCBwbGVhc3VyZQ==",
                    Status = true

                },
                 new Korisnici
                 {
                     Id = 3,
                     Ime = "Meho",
                     Prezime = "Mehic",
                     Email = "meho@mail.com",
                     Telefon = "061478951",
                     KorisnickoIme = "mmeho",
                     LozinkaHash = "Fcm514xjmNp/HVnC/upkWqW0G+0=",
                     LozinkaSalt = "YW55IGNhcm5hbCBwbGVhc3VyZQ==",
                     Status = true

                 },
                    new Korisnici
                    {
                        Id = 4,
                        Ime = "Suljo",
                        Prezime = "Suljic",
                        Email = "suljo@mail.com",
                        Telefon = "0601478951",
                        KorisnickoIme = "ssuljo",
                        LozinkaHash = "Fcm514xjmNp/HVnC/upkWqW0G+0=",
                        LozinkaSalt = "YW55IGNhcm5hbCBwbGVhc3VyZQ==",
                        Status = true

                    },
                       new Korisnici
                       {
                           Id = 5,
                           Ime = "Mujo",
                           Prezime = "Mujic",
                           Email = "mujo@mail.com",
                           Telefon = "0601478951",
                           KorisnickoIme = "mmujo",
                           LozinkaHash = "Fcm514xjmNp/HVnC/upkWqW0G+0=",
                           LozinkaSalt = "YW55IGNhcm5hbCBwbGVhc3VyZQ==",
                           Status = true

                       },
                         new Korisnici
                         {
                             Id = 6,
                             Ime = "Kemo",
                             Prezime = "Mujic",
                             Email = "kemo@mail.com",
                             Telefon = "0601478851",
                             KorisnickoIme = "kmujic",
                             LozinkaHash = "Fcm514xjmNp/HVnC/upkWqW0G+0=",
                             LozinkaSalt = "YW55IGNhcm5hbCBwbGVhc3VyZQ==",
                             Status = true

                         }
            );
            modelBuilder.Entity<Uloge>().HasData(
                new Uloge
                {
                    Id = 1, 
                    Naziv = "Admin",
                    Opis = "Administrator sistema"
                },
                 new Uloge
                 {
                     Id = 2,
                     Naziv = "Zaposleni",
                     Opis = "Ljudi koji su zaposleni u firmi"
                 }
                );
            modelBuilder.Entity<KorisniciUloge>().HasData(
            new KorisniciUloge
            {
                KorisnikUlogaId = 1,
                KorisnikId = 1,
                DatumIzmjene = DateTime.Now,
                UlogaId = 1
            },
               new KorisniciUloge
               {
                   KorisnikUlogaId = 2,
                   KorisnikId = 1,
                   DatumIzmjene = DateTime.Now,
                   UlogaId = 2
               },
                 new KorisniciUloge
                 {
                     KorisnikUlogaId = 3,
                     KorisnikId = 2,
                     DatumIzmjene = DateTime.Now,
                     UlogaId = 2
                 }
            );
            modelBuilder.Entity<Drzava>().HasData(
                new Drzava
                {
                    Id = 1,
                    Naziv = "Bosna i Hercegovina"
                },
                 new Drzava
                 {
                     Id = 2,
                     Naziv = "Srbija"
                 },
                 new Drzava
                 {
                     Id = 3,
                     Naziv = "Hrvatska"
                 },
                  new Drzava
                  {
                      Id = 4,
                      Naziv = "Crna Gora"
                  },
                   new Drzava
                   {
                       Id = 5,
                       Naziv = "Holandija"
                   },
                    new Drzava
                    {
                        Id = 6,
                        Naziv = "Turska"
                    },
                     new Drzava
                     {
                         Id = 7,
                         Naziv = "Austrija"
                     },
                      new Drzava
                      {
                          Id = 8,
                          Naziv = "Njemacka"
                      },
                         new Drzava
                         {
                             Id = 9,
                             Naziv = "Francuska"
                         },
                           new Drzava
                           {
                               Id = 10,
                               Naziv = "Spanija"
                           }
                );
            modelBuilder.Entity<Firma>().HasData(
            new Firma
            {
                Id = 1,
                Adresa = "Marsala Tita bb",
                BrojZiroracuna = "0014478857897",
                GradId = 1,
                Naziv = "Centrotrans"
            },
              new Firma
              {
                  Id = 2,
                  Adresa = "Marsala Tita 28",
                  BrojZiroracuna = "1475893177897",
                  GradId = 2,
                  Naziv = "Autprevoz-Bus"
              }
            );
            modelBuilder.Entity<Gradovi>().HasData(
                new Gradovi
                {
                    Id = 1,
                    NazivGrada = "Sarajevo",
                    DrzavaId = 1
                },
                 new Gradovi
                 {
                     Id = 2,
                     NazivGrada = "Mostar",
                     DrzavaId = 1
                 },
                  new Gradovi
                  {
                      Id = 3,
                      NazivGrada = "Beograd",
                      DrzavaId = 2
                  },
                    new Gradovi
                    {
                        Id = 4,
                        NazivGrada = "Zagreb",
                        DrzavaId = 3
                    },
                    new Gradovi
                    {
                        Id = 5,
                        NazivGrada = "Budva",
                        DrzavaId = 4
                    },
                      new Gradovi
                      {
                          Id = 6,
                          NazivGrada = "Amsterdam",
                          DrzavaId = 5
                      },
                         new Gradovi
                         {
                             Id = 7,
                             NazivGrada = "Istanbul",
                             DrzavaId = 6
                         },
                             new Gradovi
                             {
                                 Id = 8,
                                 NazivGrada = "Bec",
                                 DrzavaId = 7
                             },
                                  new Gradovi
                                  {
                                      Id = 9,
                                      NazivGrada = "Minhen",
                                      DrzavaId = 8
                                  },
                              new Gradovi
                              {
                                  Id = 10,
                                  NazivGrada = "Pariz",
                                  DrzavaId = 9
                              }, new Gradovi
                              {
                                  Id = 11,
                                  NazivGrada = "Madrid",
                                  DrzavaId = 10
                              }
                );
            modelBuilder.Entity<Prevoz>().HasData(
                new Prevoz
                {
                    Id = 1,
                    BrojMjesta = 35,
                    CijenaPoMjestu = 12,
                    FirmaId = 1,
                    TipPrevoza = "drumski"
                },
                  new Prevoz
                  {
                      Id = 2,
                      BrojMjesta = 33,
                      CijenaPoMjestu = 11,
                      FirmaId = 2,
                      TipPrevoza = "drumski"
                  }
                );
            modelBuilder.Entity<Smjestaj>().HasData(
                new Smjestaj
                {
                    Id = 1,
                    CijenaNocenja = 100,
                    NazivSmjestaja = "Hotel",
                    OpisSmjestaja = "Hotel sa 5 zvjezdica. Ukljucen dorucak",
                    Slika = ReadFile("TestImage/OIP1.jpg"),
                    TipSobe = "trokrevetna"

                },
                   new Smjestaj
                   {
                       Id = 2,
                       CijenaNocenja = 60,
                       NazivSmjestaja = "Hotel",
                       OpisSmjestaja = "Hotel sa 4 zvjezdica.Ukljucen dorucak",
                       Slika = ReadFile("TestImage/OIP1.jpg"),
                       TipSobe = "dvokrevetna"

                   },
                      new Smjestaj
                      {
                          Id = 3,
                          CijenaNocenja = 40,
                          NazivSmjestaja = "Hotel",
                          OpisSmjestaja = "Hotel sa 4 zvjezdica",
                          Slika = ReadFile("TestImage/OIP1.jpg"),
                          TipSobe = "dvkrevetna"

                      }
                );
            modelBuilder.Entity<Putovanja>().HasData(
                new Putovanja {
                    Id = 1,
                    NazivPutovanja = "Putovanje1",
                    OpisPutovanja = "Putovanjeee1",
                    CijenaPutovanja = 200,
                    DatumPolaska = DateTime.Now,
                    DatumDolaska = DateTime.Now,
                    BrojMjesta = 30,
                    GradId = 3,
                    PrevozId = 1,
                    SmjestajId = 3,
                    Slika = ReadFile("TestImage/OIP1.jpg")
                },
                  new Putovanja
                  {
                      Id = 2,
                      NazivPutovanja = "Putovanje2",
                      OpisPutovanja = "Putovanjeee2",
                      CijenaPutovanja = 150,
                      DatumPolaska = DateTime.Now,
                      DatumDolaska = DateTime.Now,
                      BrojMjesta = 35,
                      GradId = 4,
                      PrevozId = 2,
                      SmjestajId = 2,
                      Slika = ReadFile("TestImage/OIP1.jpg")
                  },
                     new Putovanja
                     {
                         Id = 3,
                         NazivPutovanja = "Putovanje3",
                         OpisPutovanja = "Putovanjeee3",
                         CijenaPutovanja = 250,
                         DatumPolaska = DateTime.Now,
                         DatumDolaska = DateTime.Now,
                         BrojMjesta = 33,
                         GradId = 5,
                         PrevozId = 2,
                         SmjestajId = 1,
                         Slika = ReadFile("TestImage/OIP1.jpg")
                     },
                     new Putovanja
                     {
                         Id = 4,
                         NazivPutovanja = "Putovanje4",
                         OpisPutovanja = "Putovanjeee4",
                         CijenaPutovanja = 220,
                         DatumPolaska = DateTime.Now,
                         DatumDolaska = DateTime.Now,
                         BrojMjesta = 33,
                         GradId = 6,
                         PrevozId = 1,
                         SmjestajId = 2,
                         Slika = ReadFile("TestImage/OIP1.jpg")
                     },
                     new Putovanja
                     {
                         Id = 5,
                         NazivPutovanja = "Putovanje5",
                         OpisPutovanja = "Putovanjeee5",
                         CijenaPutovanja = 300,
                         DatumPolaska = DateTime.Now,
                         DatumDolaska = DateTime.Now,
                         BrojMjesta = 35,
                         GradId = 6,
                         PrevozId = 2,
                         SmjestajId = 1,
                         Slika = ReadFile("TestImage/OIP1.jpg")
                     },
                      new Putovanja
                      {
                          Id = 6,
                          NazivPutovanja = "Putovanje6",
                          OpisPutovanja = "Putovanjeee6",
                          CijenaPutovanja = 180,
                          DatumPolaska = DateTime.Now,
                          DatumDolaska = DateTime.Now,
                          BrojMjesta = 35,
                          GradId = 3,
                          PrevozId = 2,
                          SmjestajId = 3,
                          Slika = ReadFile("TestImage/OIP1.jpg")
                      },
                       new Putovanja
                       {
                           Id = 7,
                           NazivPutovanja = "Putovanje7",
                           OpisPutovanja = "Putovanjeee7",
                           CijenaPutovanja = 320,
                           DatumPolaska = DateTime.Now,
                           DatumDolaska = DateTime.Now,
                           BrojMjesta = 35,
                           GradId = 7,
                           PrevozId = 2,
                           SmjestajId = 2,
                           Slika = ReadFile("TestImage/OIP1.jpg")
                       },
                        new Putovanja
                        {
                            Id = 8,
                            NazivPutovanja = "Putovanje8",
                            OpisPutovanja = "Putovanjeee8",
                            CijenaPutovanja = 320,
                            DatumPolaska = DateTime.Now,
                            DatumDolaska = DateTime.Now,
                            BrojMjesta = 35,
                            GradId = 10,
                            PrevozId = 1,
                            SmjestajId = 2,
                            Slika = ReadFile("TestImage/OIP1.jpg")
                        },
                          new Putovanja
                          {
                              Id = 9,
                              NazivPutovanja = "Putovanje9",
                              OpisPutovanja = "Putovanjeee9",
                              CijenaPutovanja = 350,
                              DatumPolaska = DateTime.Now,
                              DatumDolaska = DateTime.Now,
                              BrojMjesta = 35,
                              GradId = 11,
                              PrevozId = 1,
                              SmjestajId = 3,
                              Slika = ReadFile("TestImage/OIP1.jpg")
                          }

                );
            modelBuilder.Entity<Rezervacija>().HasData(
                new Rezervacija
                {
                    Id = 1,
                    Ime = "Rezervacija1",
                    DatumRezervacije = DateTime.Now,
                    BrojOsoba = 2,
                    KorisnikId = 3,
                    Napomena = "Napomena1",
                    PutovanjeId = 1,
                    Status = "Rezervisano"
                },
                 new Rezervacija
                 {
                     Id = 2,
                     Ime = "Rezervacija2",
                     DatumRezervacije = DateTime.Now,
                     BrojOsoba = 3,
                     KorisnikId = 4,
                     Napomena = "Napomena2",
                     PutovanjeId = 3,
                     Status = "Rezervisano"
                 },
                     new Rezervacija
                     {
                         Id = 3,
                         Ime = "Rezervacija3",
                         DatumRezervacije = DateTime.Now,
                         BrojOsoba = 2,
                         KorisnikId = 5,
                         Napomena = "Napomena3",
                         PutovanjeId = 5,
                         Status = "Rezervisano"
                     },
                       new Rezervacija
                       {
                           Id = 4,
                           Ime = "Rezervacija4",
                           DatumRezervacije = DateTime.Now,
                           BrojOsoba = 3,
                           KorisnikId = 3,
                           Napomena = "Napomena4",
                           PutovanjeId = 6,
                           Status = "Rezervisano"
                       },
                          new Rezervacija
                          {
                              Id = 5,
                              Ime = "Rezervacija5",
                              DatumRezervacije = DateTime.Now,
                              BrojOsoba = 2,
                              KorisnikId = 4,
                              Napomena = "Napomena5",
                              PutovanjeId = 5,
                              Status = "Rezervisano"
                          },
                            new Rezervacija
                            {
                                Id = 6,
                                Ime = "Rezervacija6",
                                DatumRezervacije = DateTime.Now,
                                BrojOsoba = 2,
                                KorisnikId = 6,
                                Napomena = "Napomena6",
                                PutovanjeId = 5,
                                Status = "Rezervisano"
                            },
                                new Rezervacija
                                {
                                    Id = 7,
                                    Ime = "Rezervacija7",
                                    DatumRezervacije = DateTime.Now,
                                    BrojOsoba = 2,
                                    KorisnikId = 4,
                                    Napomena = "Napomena7",
                                    PutovanjeId = 6,
                                    Status = "Rezervisano"
                                },
                                new Rezervacija
                                {
                                    Id = 8,
                                    Ime = "Rezervacija8",
                                    DatumRezervacije = DateTime.Now,
                                    BrojOsoba = 3,
                                    KorisnikId = 3,
                                    Napomena = "Napomena8",
                                    PutovanjeId = 8,
                                    Status = "Rezervisano"
                                },
                                  new Rezervacija
                                  {
                                      Id = 9,
                                      Ime = "Rezervacija9",
                                      DatumRezervacije = DateTime.Now,
                                      BrojOsoba = 3,
                                      KorisnikId = 4,
                                      Napomena = "Napomena8",
                                      PutovanjeId = 7,
                                      Status = "Rezervisano"
                                  },
                                      new Rezervacija
                                      {
                                          Id = 10,
                                          Ime = "Rezervacija10",
                                          DatumRezervacije = DateTime.Now,
                                          BrojOsoba = 5,
                                          KorisnikId = 6,
                                          Napomena = "Napomena9",
                                          PutovanjeId = 8,
                                          Status = "Rezervisano"
                                      }
                );
            modelBuilder.Entity<Komentar>().HasData(
               new Komentar
               {
                   Id = 1,
                   Datum = DateTime.Now,
                   KorisnikId = 3,
                   PutovanjeId = 1,
                   Sadrzaj = "Komentar1"
               },
                 new Komentar
                 {
                     Id = 2,
                     Datum = DateTime.Now,
                     KorisnikId = 4,
                     PutovanjeId = 1,
                     Sadrzaj = "Komentar2"
                 },
                   new Komentar
                   {
                       Id = 3,
                       Datum = DateTime.Now,
                       KorisnikId = 5,
                       PutovanjeId = 2,
                       Sadrzaj = "Komentar3"
                   },
                    new Komentar
                    {
                        Id = 4,
                        Datum = DateTime.Now,
                        KorisnikId = 3,
                        PutovanjeId = 2,
                        Sadrzaj = "Komentar4"
                    },
                       new Komentar
                       {
                           Id = 5,
                           Datum = DateTime.Now,
                           KorisnikId = 3,
                           PutovanjeId = 3,
                           Sadrzaj = "Komentar5"
                       },
                         new Komentar
                         {
                             Id = 6,
                             Datum = DateTime.Now,
                             KorisnikId = 6,
                             PutovanjeId = 5,
                             Sadrzaj = "Komentar6"
                         },
                           new Komentar
                           {
                               Id = 7,
                               Datum = DateTime.Now,
                               KorisnikId = 4,
                               PutovanjeId = 8,
                               Sadrzaj = "Komentar7"
                           },
                             new Komentar
                             {
                                 Id = 8,
                                 Datum = DateTime.Now,
                                 KorisnikId = 6,
                                 PutovanjeId = 8,
                                 Sadrzaj = "Komentar8"
                             },
                              new Komentar
                              {
                                  Id = 9,
                                  Datum = DateTime.Now,
                                  KorisnikId = 5,
                                  PutovanjeId = 9,
                                  Sadrzaj = "Komentar9"
                              },
                                 new Komentar
                                 {
                                     Id = 10,
                                     Datum = DateTime.Now,
                                     KorisnikId = 3,
                                     PutovanjeId = 9,
                                     Sadrzaj = "Komentar10"
                                 }

               );
            modelBuilder.Entity<ListaZelja>().HasData(
             new ListaZelja
             {
                 Id = 1,

                 KorisnikId = 3,
                 PutovanjeId = 1,
                 Opis = "Opis1"
             },
               new ListaZelja
               {
                   Id = 2,
                   KorisnikId = 4,
                   PutovanjeId = 1,
                   Opis = "Opis2"
               },
                 new ListaZelja
                 {
                     Id = 3,

                     KorisnikId = 5,
                     PutovanjeId = 2,
                     Opis = "Opis3"
                 },
                  new ListaZelja
                  {
                      Id = 4,

                      KorisnikId = 3,
                      PutovanjeId = 2,
                      Opis = "Opis4"
                  },
                     new ListaZelja
                     {
                         Id = 5,

                         KorisnikId = 3,
                         PutovanjeId = 3,
                         Opis = "Opis5"
                     },
                       new ListaZelja
                       {
                           Id = 6,

                           KorisnikId = 6,
                           PutovanjeId = 5,
                           Opis = "Opis6"
                       },
                         new ListaZelja
                         {
                             Id = 7,

                             KorisnikId = 4,
                             PutovanjeId = 8,
                             Opis = "Opis7"
                         },
                           new ListaZelja
                           {
                               Id = 8,

                               KorisnikId = 6,
                               PutovanjeId = 8,
                               Opis = "Opis8"
                           },
                            new ListaZelja
                            {
                                Id = 9,

                                KorisnikId = 5,
                                PutovanjeId = 9,
                                Opis = "Opis9"
                            },
                               new ListaZelja
                               {
                                   Id = 10,

                                   KorisnikId = 3,
                                   PutovanjeId = 9,
                                   Opis = "Opis10"
                               }

             );
            modelBuilder.Entity<Obavijesti>().HasData(
              new Obavijesti
              {
                  Id = 1,
                  Datum = DateTime.Now,
                  KorisnikId = 3,
                  Naziv = "Obavijest1",
                  Sadrzaj = "Sadrzaj1"
              },
                new Obavijesti
                {
                    Id = 2,
                    Datum = DateTime.Now,
                    KorisnikId = 4,
                    Naziv = "Obavijest2",
                    Sadrzaj = "Sadrzaj2"
                },
                  new Obavijesti
                  {
                      Id = 3,
                      Datum = DateTime.Now,
                      KorisnikId = 5,
                      Naziv = "Obavijest3",
                      Sadrzaj = "Sadrzaj3"
                  },
                   new Obavijesti
                   {
                       Id = 4,
                       Datum = DateTime.Now,
                       KorisnikId = 3,
                       Naziv = "Obavijest4",
                       Sadrzaj = "Sadrzaj4"
                   },
                      new Obavijesti
                      {
                          Id = 5,
                          Datum = DateTime.Now,
                          KorisnikId = 3,
                          Naziv = "Obavijest5",
                          Sadrzaj = "Sadrzaj5"
                      },
                        new Obavijesti
                        {
                            Id = 6,
                            Datum = DateTime.Now,
                            KorisnikId = 6,
                            Naziv = "Obavijest6",
                            Sadrzaj = "Sadrzaj6"
                        },
                          new Obavijesti
                          {
                              Id = 7,
                              Datum = DateTime.Now,
                              KorisnikId = 4,
                              Naziv = "Obavijest7",
                              Sadrzaj = "Sadrzaj7"
                          },
                            new Obavijesti
                            {
                                Id = 8,
                                Datum = DateTime.Now,
                                KorisnikId = 6,
                                Naziv = "Obavijest8",
                                Sadrzaj = "Sadrzaj8"
                            },
                             new Obavijesti
                             {
                                 Id = 9,
                                 Datum = DateTime.Now,
                                 KorisnikId = 5,
                                 Naziv = "Obavijest9",
                                 Sadrzaj = "Sadrzaj9"
                             },
                                new Obavijesti
                                {
                                    Id = 10,
                                    Datum = DateTime.Now,
                                    KorisnikId = 3,
                                    Naziv = "Obavijest10",
                                    Sadrzaj = "Sadrzaj10"
                                }

              );

            modelBuilder.Entity<Ocjene>().HasData(
              new Ocjene
              {
                  Id = 1,
                  Datum = DateTime.Now,
                  KorisnikId = 3,
                  PutovanjeId = 1,
                  Ocjena = 4
              },
                new Ocjene
                {
                    Id = 2,
                    Datum = DateTime.Now,
                    KorisnikId = 4,
                    PutovanjeId = 1,
                    Ocjena = 5
                },
                  new Ocjene
                  {
                      Id = 3,
                      Datum = DateTime.Now,
                      KorisnikId = 5,
                      PutovanjeId = 2,
                      Ocjena = 4
                  },
                   new Ocjene
                   {
                       Id = 4,
                       Datum = DateTime.Now,
                       KorisnikId = 3,
                       PutovanjeId = 2,
                       Ocjena = 5
                   },
                      new Ocjene
                      {
                          Id = 5,
                          Datum = DateTime.Now,
                          KorisnikId = 3,
                          PutovanjeId = 3,
                          Ocjena = 5
                      },
                        new Ocjene
                        {
                            Id = 6,
                            Datum = DateTime.Now,
                            KorisnikId = 6,
                            PutovanjeId = 5,
                            Ocjena = 4
                        },
                          new Ocjene
                          {
                              Id = 7,
                              Datum = DateTime.Now,
                              KorisnikId = 4,
                              PutovanjeId = 8,
                              Ocjena = 5
                          },
                            new Ocjene
                            {
                                Id = 8,
                                Datum = DateTime.Now,
                                KorisnikId = 6,
                                PutovanjeId = 8,
                                Ocjena = 5
                            },
                             new Ocjene
                             {
                                 Id = 9,
                                 Datum = DateTime.Now,
                                 KorisnikId = 5,
                                 PutovanjeId = 9,
                                 Ocjena = 4
                             },
                                new Ocjene
                                {
                                    Id = 10,
                                    Datum = DateTime.Now,
                                    KorisnikId = 3,
                                    PutovanjeId = 9,
                                    Ocjena = 5
                                }

              );
            modelBuilder.Entity<Uplate>().HasData(
              new Uplate { 
              Id=1,
              Datum=DateTime.Now,
              Iznos=100,
              RezervacijaId=1,
              KorisnikId =1,
              },
                new Uplate
                {
                    Id = 2,
                    Datum = DateTime.Now,
                    Iznos = 120,
                    RezervacijaId = 2,
                    KorisnikId =2
                },
                 new Uplate
                 {
                     Id = 3,
                     Datum = DateTime.Now,
                     Iznos = 150,
                     RezervacijaId = 3,
                     KorisnikId = 3
                 },
                  new Uplate
                  {
                      Id = 4,
                      Datum = DateTime.Now,
                      Iznos = 120,
                      RezervacijaId = 4,
                      KorisnikId = 4

                  },
                     new Uplate
                     {
                         Id = 5,
                         Datum = DateTime.Now,
                         Iznos = 150,
                         RezervacijaId = 5,
                         KorisnikId = 5
                     },
                        new Uplate
                        {
                            Id = 6,
                            Datum = DateTime.Now,
                            Iznos = 150,
                            RezervacijaId = 6,
                            KorisnikId = 6
                        },
                          new Uplate
                          {
                              Id = 7,
                              Datum = DateTime.Now,
                              Iznos = 180,
                              RezervacijaId = 7,
                              KorisnikId = 3
                          },
                            new Uplate
                            {
                                Id = 8,
                                Datum = DateTime.Now,
                                Iznos = 150,
                                RezervacijaId = 8,
                                KorisnikId = 4
                            },
                              new Uplate
                              {
                                  Id = 9,
                                  Datum = DateTime.Now,
                                  Iznos = 180,
                                  RezervacijaId = 9,
                                  KorisnikId = 5
                              },
                                new Uplate
                                {
                                    Id = 10,
                                    Datum = DateTime.Now,
                                    Iznos = 170,
                                    RezervacijaId = 10,
                                    KorisnikId = 6
                                }

              );
            modelBuilder.Entity<Vodic>().HasData(
             new Vodic
             {
                 Id=1,
                 Ime="Ime1",
                 Jmbg="1457896541278",
                 Kontakt="061475963",
                 Prezime="Prezime1",
                 Slika = ReadFile("TestImage/OIP1.jpg")
             },
               new Vodic
               {
                   Id = 2,
                   Ime = "Ime2",
                   Jmbg = "1459896541278",
                   Kontakt = "062475963",
                   Prezime = "Prezime2",
                   Slika = ReadFile("TestImage/OIP1.jpg")
               },
               new Vodic
               {
                   Id = 3,
                   Ime = "Ime3",
                   Jmbg = "8959896541278",
                   Kontakt = "062495963",
                   Prezime = "Prezime3",
                   Slika = ReadFile("TestImage/OIP1.jpg")
               },
                 new Vodic
                 {
                     Id = 4,
                     Ime = "Ime4",
                     Jmbg = "8959896541277",
                     Kontakt = "062495964",
                     Prezime = "Prezime4",
                     Slika = ReadFile("TestImage/OIP1.jpg")
                 },
                   new Vodic
                   {
                       Id = 5,
                       Ime = "Ime5",
                       Jmbg = "8959866541278",
                       Kontakt = "062495563",
                       Prezime = "Prezime5",
                       Slika = ReadFile("TestImage/OIP1.jpg")
                   },
                    new Vodic
                    {
                        Id = 6,
                        Ime = "Ime6",
                        Jmbg = "8959866441278",
                        Kontakt = "061495963",
                        Prezime = "Prezime6",
                        Slika = ReadFile("TestImage/OIP1.jpg")
                    },
                       new Vodic
                       {
                           Id = 7,
                           Ime = "Ime7",
                           Jmbg = "8959866441278",
                           Kontakt = "061499963",
                           Prezime = "Prezime7",
                           Slika = ReadFile("TestImage/OIP1.jpg")
                       },
                          new Vodic
                          {
                              Id = 8,
                              Ime = "Ime8",
                              Jmbg = "1359866441278",
                              Kontakt = "0601495963",
                              Prezime = "Prezime8",
                              Slika = ReadFile("TestImage/OIP1.jpg")
                          }
             );

            modelBuilder.Entity<VodiciPutovanja>().HasData(
              new VodiciPutovanja
              {
                  Id = 1,
                 
                  VodicId = 3,
                  PutovanjeId = 1,
                 
              },
                new VodiciPutovanja
                {
                    Id = 2,
                  
                    VodicId = 4,
                    PutovanjeId = 1,
                   
                },
                  new VodiciPutovanja
                  {
                      Id = 3,
                     
                      VodicId = 5,
                      PutovanjeId = 2,
                     
                  },
                   new VodiciPutovanja
                   {
                       Id = 4,
                      
                       VodicId = 3,
                       PutovanjeId = 2,
                      
                   },
                      new VodiciPutovanja
                      {
                          Id = 5,
                        
                          VodicId = 3,
                          PutovanjeId = 3,
                        
                      },
                        new VodiciPutovanja
                        {
                            Id = 6,
                           
                            VodicId = 6,
                            PutovanjeId = 5,
                            
                        },
                          new VodiciPutovanja
                          {
                              Id = 7,
                             
                              VodicId = 4,
                              PutovanjeId = 8,
                              
                          },
                            new VodiciPutovanja
                            {
                                Id = 8,
                               
                                VodicId = 6,
                                PutovanjeId = 8,
                               
                            },
                             new VodiciPutovanja
                             {
                                 Id = 9,
                               
                                 VodicId = 5,
                                 PutovanjeId = 9,
                               
                             },
                                new VodiciPutovanja
                                {
                                    Id = 10,
                                   
                                    VodicId = 3,
                                    PutovanjeId = 9,
                                   
                                }

              );
        }
    }
}
