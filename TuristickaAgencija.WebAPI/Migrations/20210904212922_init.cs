using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TuristickaAgencija.WebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefon = table.Column<string>(nullable: true),
                    KorisnickoIme = table.Column<string>(nullable: true),
                    LozinkaHash = table.Column<string>(nullable: true),
                    LozinkaSalt = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Smjestaj",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivSmjestaja = table.Column<string>(nullable: true),
                    OpisSmjestaja = table.Column<string>(nullable: true),
                    CijenaNocenja = table.Column<float>(nullable: false),
                    Tip_sobe = table.Column<string>(nullable: true),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smjestaj", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uloge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uloge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vodic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Kontakt = table.Column<string>(nullable: true),
                    JMBG = table.Column<string>(nullable: true),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vodic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gradovi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivGrada = table.Column<string>(nullable: true),
                    DrzavaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gradovi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gradovi_Drzava_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzava",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Obavijesti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true),
                    KorisnikId = table.Column<int>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijesti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obavijesti_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KorisniciUloge",
                columns: table => new
                {
                    KorisnikUlogaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    UlogaId = table.Column<int>(nullable: false),
                    DatumIzmjene = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciUloge", x => x.KorisnikUlogaId);
                    table.ForeignKey(
                        name: "FK_KorisniciUloge_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisniciUloge_Uloge_UlogaId",
                        column: x => x.UlogaId,
                        principalTable: "Uloge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Firma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    GradId = table.Column<int>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    BrojZiroracuna = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Firma_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prevoz",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaId = table.Column<int>(nullable: true),
                    TipPrevoza = table.Column<string>(nullable: true),
                    BrojMjesta = table.Column<int>(nullable: false),
                    CijenaPoMjestu = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prevoz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prevoz_Firma_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firma",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Putovanja",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivPutovanja = table.Column<string>(nullable: true),
                    OpisPutovanja = table.Column<string>(nullable: true),
                    CijenaPutovanja = table.Column<float>(nullable: false),
                    DatumPolaska = table.Column<DateTime>(nullable: false),
                    DatumDolaska = table.Column<DateTime>(nullable: false),
                    BrojMjesta = table.Column<int>(nullable: false),
                    GradId = table.Column<int>(nullable: true),
                    PrevozId = table.Column<int>(nullable: true),
                    SmjestajId = table.Column<int>(nullable: true),
                    Slika = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Putovanja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Putovanja_Gradovi_GradId",
                        column: x => x.GradId,
                        principalTable: "Gradovi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Putovanja_Prevoz_PrevozId",
                        column: x => x.PrevozId,
                        principalTable: "Prevoz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Putovanja_Smjestaj_SmjestajId",
                        column: x => x.SmjestajId,
                        principalTable: "Smjestaj",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Komentar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PutovanjeId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    Sadrzaj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Komentar_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Komentar_Putovanja_PutovanjeId",
                        column: x => x.PutovanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ListaZelja",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PutovanjeId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    Opis = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListaZelja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ListaZelja_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ListaZelja_Putovanja_PutovanjeId",
                        column: x => x.PutovanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocjene",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PutovanjeId = table.Column<int>(nullable: false),
                    KorisnikId = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    Ocjena = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocjene", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ocjene_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocjene_Putovanja_PutovanjeId",
                        column: x => x.PutovanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rezervacija",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    KorisnikId = table.Column<int>(nullable: true),
                    PutovanjeId = table.Column<int>(nullable: true),
                    DatumRezervacije = table.Column<DateTime>(nullable: false),
                    BrojOsoba = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Napomena = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rezervacija", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rezervacija_Putovanja_PutovanjeId",
                        column: x => x.PutovanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VodiciPutovanja",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VodicId = table.Column<int>(nullable: false),
                    PutovanjeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VodiciPutovanja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VodiciPutovanja_Putovanja_PutovanjeId",
                        column: x => x.PutovanjeId,
                        principalTable: "Putovanja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VodiciPutovanja_Vodic_VodicId",
                        column: x => x.VodicId,
                        principalTable: "Vodic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uplate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    Iznos = table.Column<double>(nullable: false),
                    RezervacijaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Uplate_Rezervacija_RezervacijaId",
                        column: x => x.RezervacijaId,
                        principalTable: "Rezervacija",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Drzava",
                columns: new[] { "Id", "Naziv" },
                values: new object[,]
                {
                    { 1, "Bosna i Hercegovina" },
                    { 2, "Srbija" },
                    { 3, "Hrvatska" },
                    { 4, "Crna Gora" },
                    { 5, "Holandija" },
                    { 6, "Turska" },
                    { 7, "Austrija" },
                    { 8, "Njemacka" },
                    { 9, "Francuska" },
                    { 10, "Spanija" }
                });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "Id", "Email", "Ime", "KorisnickoIme", "LozinkaHash", "LozinkaSalt", "Prezime", "Status", "Telefon" },
                values: new object[,]
                {
                    { 6, "kemo@mail.com", "Kemo", "kmujic", "Fcm514xjmNp/HVnC/upkWqW0G+0=", "YW55IGNhcm5hbCBwbGVhc3VyZQ==", "Mujic", true, "0601478851" },
                    { 4, "suljo@mail.com", "Suljo", "ssuljo", "Fcm514xjmNp/HVnC/upkWqW0G+0=", "YW55IGNhcm5hbCBwbGVhc3VyZQ==", "Suljic", true, "0601478951" },
                    { 5, "mujo@mail.com", "Mujo", "mmujo", "Fcm514xjmNp/HVnC/upkWqW0G+0=", "YW55IGNhcm5hbCBwbGVhc3VyZQ==", "Mujic", true, "0601478951" },
                    { 2, "amila@mail.com", "Amila", "tamila", "Fcm514xjmNp/HVnC/upkWqW0G+0=", "YW55IGNhcm5hbCBwbGVhc3VyZQ==", "Turkovic", true, "062479951" },
                    { 1, "admin@mail.com", "Mirnes", "tmirnes", "Fcm514xjmNp/HVnC/upkWqW0G+0=", "YW55IGNhcm5hbCBwbGVhc3VyZQ==", "Turkovic", true, "061478951" },
                    { 3, "meho@mail.com", "Meho", "mmeho", "Fcm514xjmNp/HVnC/upkWqW0G+0=", "YW55IGNhcm5hbCBwbGVhc3VyZQ==", "Mehic", true, "061478951" }
                });

            migrationBuilder.InsertData(
                table: "Smjestaj",
                columns: new[] { "Id", "CijenaNocenja", "NazivSmjestaja", "OpisSmjestaja", "Slika", "Tip_sobe" },
                values: new object[,]
                {
                    { 1, 100f, "Hotel", "Hotel sa 5 zvjezdica. Ukljucen dorucak", null, "trokrevetna" },
                    { 2, 60f, "Hotel", "Hotel sa 4 zvjezdica.Ukljucen dorucak", null, "dvokrevetna" },
                    { 3, 40f, "Hotel", "Hotel sa 4 zvjezdica", null, "dvkrevetna" }
                });

            migrationBuilder.InsertData(
                table: "Uloge",
                columns: new[] { "Id", "Naziv", "Opis" },
                values: new object[,]
                {
                    { 1, "Admin", "Administrator sistema" },
                    { 2, "Zaposleni", "Ljudi koji su zaposleni u firmi" }
                });

            migrationBuilder.InsertData(
                table: "Vodic",
                columns: new[] { "Id", "Ime", "JMBG", "Kontakt", "Prezime", "Slika" },
                values: new object[,]
                {
                    { 7, "Ime7", "8959866441278", "061499963", "Prezime7", null },
                    { 1, "Ime1", "1457896541278", "061475963", "Prezime1", null },
                    { 2, "Ime2", "1459896541278", "062475963", "Prezime2", null },
                    { 3, "Ime3", "8959896541278", "062495963", "Prezime3", null },
                    { 4, "Ime4", "8959896541277", "062495964", "Prezime4", null },
                    { 5, "Ime5", "8959866541278", "062495563", "Prezime5", null },
                    { 6, "Ime6", "8959866441278", "061495963", "Prezime6", null },
                    { 8, "Ime8", "1359866441278", "0601495963", "Prezime8", null }
                });

            migrationBuilder.InsertData(
                table: "Gradovi",
                columns: new[] { "Id", "DrzavaId", "NazivGrada" },
                values: new object[,]
                {
                    { 1, 1, "Sarajevo" },
                    { 2, 1, "Mostar" },
                    { 3, 2, "Beograd" },
                    { 4, 3, "Zagreb" },
                    { 5, 4, "Budva" },
                    { 6, 5, "Amsterdam" },
                    { 7, 6, "Istanbul" },
                    { 8, 7, "Bec" },
                    { 9, 8, "Minhen" },
                    { 10, 9, "Pariz" },
                    { 11, 10, "Madrid" }
                });

            migrationBuilder.InsertData(
                table: "KorisniciUloge",
                columns: new[] { "KorisnikUlogaId", "DatumIzmjene", "KorisnikId", "UlogaId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 9, 4, 23, 29, 20, 896, DateTimeKind.Local).AddTicks(3920), 1, 1 },
                    { 3, new DateTime(2021, 9, 4, 23, 29, 20, 909, DateTimeKind.Local).AddTicks(9347), 2, 2 },
                    { 2, new DateTime(2021, 9, 4, 23, 29, 20, 909, DateTimeKind.Local).AddTicks(9149), 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Obavijesti",
                columns: new[] { "Id", "Datum", "KorisnikId", "Naziv", "Sadrzaj" },
                values: new object[,]
                {
                    { 5, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(8400), 3, "Obavijest5", "Sadrzaj5" },
                    { 10, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(8463), 3, "Obavijest10", "Sadrzaj10" },
                    { 2, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(8077), 4, "Obavijest2", "Sadrzaj2" },
                    { 7, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(8426), 4, "Obavijest7", "Sadrzaj7" },
                    { 3, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(8364), 5, "Obavijest3", "Sadrzaj3" },
                    { 9, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(8451), 5, "Obavijest9", "Sadrzaj9" },
                    { 6, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(8413), 6, "Obavijest6", "Sadrzaj6" },
                    { 8, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(8439), 6, "Obavijest8", "Sadrzaj8" },
                    { 4, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(8387), 3, "Obavijest4", "Sadrzaj4" },
                    { 1, new DateTime(2021, 9, 4, 23, 29, 20, 921, DateTimeKind.Local).AddTicks(4709), 3, "Obavijest1", "Sadrzaj1" }
                });

            migrationBuilder.InsertData(
                table: "Firma",
                columns: new[] { "Id", "Adresa", "BrojZiroracuna", "GradId", "Naziv" },
                values: new object[] { 1, "Marsala Tita bb", "0014478857897", 1, "Centrotrans" });

            migrationBuilder.InsertData(
                table: "Firma",
                columns: new[] { "Id", "Adresa", "BrojZiroracuna", "GradId", "Naziv" },
                values: new object[] { 2, "Marsala Tita 28", "1475893177897", 2, "Autprevoz-Bus" });

            migrationBuilder.InsertData(
                table: "Prevoz",
                columns: new[] { "Id", "BrojMjesta", "CijenaPoMjestu", "FirmaId", "TipPrevoza" },
                values: new object[] { 1, 35, 12f, 1, "drumski" });

            migrationBuilder.InsertData(
                table: "Prevoz",
                columns: new[] { "Id", "BrojMjesta", "CijenaPoMjestu", "FirmaId", "TipPrevoza" },
                values: new object[] { 2, 33, 11f, 2, "drumski" });

            migrationBuilder.InsertData(
                table: "Putovanja",
                columns: new[] { "Id", "BrojMjesta", "CijenaPutovanja", "DatumDolaska", "DatumPolaska", "GradId", "NazivPutovanja", "OpisPutovanja", "PrevozId", "Slika", "SmjestajId" },
                values: new object[,]
                {
                    { 1, 30, 200f, new DateTime(2021, 9, 4, 23, 29, 20, 917, DateTimeKind.Local).AddTicks(7004), new DateTime(2021, 9, 4, 23, 29, 20, 917, DateTimeKind.Local).AddTicks(5752), 3, "Putovanje1", "Putovanjeee1", 1, null, 3 },
                    { 4, 33, 220f, new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(3982), new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(3966), 6, "Putovanje4", "Putovanjeee4", 1, null, 2 },
                    { 8, 35, 320f, new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4131), new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4121), 10, "Putovanje8", "Putovanjeee8", 1, null, 2 },
                    { 9, 35, 350f, new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4163), new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4154), 11, "Putovanje9", "Putovanjeee9", 1, null, 3 },
                    { 2, 35, 150f, new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(3624), new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(3523), 4, "Putovanje2", "Putovanjeee2", 2, null, 2 },
                    { 3, 33, 250f, new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(3936), new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(3908), 5, "Putovanje3", "Putovanjeee3", 2, null, 1 },
                    { 5, 35, 300f, new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4018), new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4004), 6, "Putovanje5", "Putovanjeee5", 2, null, 1 },
                    { 6, 35, 180f, new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4056), new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4042), 3, "Putovanje6", "Putovanjeee6", 2, null, 3 },
                    { 7, 35, 320f, new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4102), new DateTime(2021, 9, 4, 23, 29, 20, 918, DateTimeKind.Local).AddTicks(4086), 7, "Putovanje7", "Putovanjeee7", 2, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Komentar",
                columns: new[] { "Id", "Datum", "KorisnikId", "PutovanjeId", "Sadrzaj" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(4328), 3, 1, "Komentar1" },
                    { 10, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(7818), 3, 9, "Komentar10" },
                    { 9, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(7805), 5, 9, "Komentar9" },
                    { 4, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(7740), 3, 2, "Komentar4" },
                    { 5, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(7752), 3, 3, "Komentar5" },
                    { 8, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(7792), 6, 8, "Komentar8" },
                    { 7, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(7779), 4, 8, "Komentar7" },
                    { 3, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(7722), 5, 2, "Komentar3" },
                    { 6, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(7766), 6, 5, "Komentar6" },
                    { 2, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(7588), 4, 1, "Komentar2" }
                });

            migrationBuilder.InsertData(
                table: "ListaZelja",
                columns: new[] { "Id", "KorisnikId", "Opis", "PutovanjeId" },
                values: new object[,]
                {
                    { 5, 3, "Opis5", 3 },
                    { 6, 6, "Opis6", 5 },
                    { 10, 3, "Opis10", 9 },
                    { 8, 6, "Opis8", 8 },
                    { 4, 3, "Opis4", 2 },
                    { 3, 5, "Opis3", 2 },
                    { 2, 4, "Opis2", 1 },
                    { 1, 3, "Opis1", 1 },
                    { 9, 5, "Opis9", 9 },
                    { 7, 4, "Opis7", 8 }
                });

            migrationBuilder.InsertData(
                table: "Ocjene",
                columns: new[] { "Id", "Datum", "KorisnikId", "Ocjena", "PutovanjeId" },
                values: new object[,]
                {
                    { 6, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(4118), 6, 4, 5 },
                    { 4, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(4093), 3, 5, 2 },
                    { 3, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(4078), 5, 4, 2 },
                    { 5, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(4106), 3, 5, 3 },
                    { 10, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(4165), 3, 5, 9 },
                    { 9, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(4154), 5, 4, 9 },
                    { 8, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(4142), 6, 5, 8 },
                    { 7, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(4129), 4, 5, 8 },
                    { 1, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(789), 3, 4, 1 },
                    { 2, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(3948), 4, 5, 1 }
                });

            migrationBuilder.InsertData(
                table: "Rezervacija",
                columns: new[] { "Id", "BrojOsoba", "DatumRezervacije", "Ime", "KorisnikId", "Napomena", "PutovanjeId", "Status" },
                values: new object[,]
                {
                    { 9, 3, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(1626), "Rezervacija9", 4, "Napomena8", 7, "Rezervisano" },
                    { 4, 3, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(1551), "Rezervacija4", 3, "Napomena4", 6, "Rezervisano" },
                    { 6, 2, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(1581), "Rezervacija6", 6, "Napomena6", 5, "Rezervisano" },
                    { 10, 5, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(1640), "Rezervacija10", 6, "Napomena9", 8, "Rezervisano" },
                    { 8, 3, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(1612), "Rezervacija8", 3, "Napomena8", 8, "Rezervisano" },
                    { 5, 2, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(1566), "Rezervacija5", 4, "Napomena5", 5, "Rezervisano" },
                    { 3, 2, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(1529), "Rezervacija3", 5, "Napomena3", 5, "Rezervisano" },
                    { 7, 2, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(1597), "Rezervacija7", 4, "Napomena7", 6, "Rezervisano" },
                    { 2, 3, new DateTime(2021, 9, 4, 23, 29, 20, 920, DateTimeKind.Local).AddTicks(1351), "Rezervacija2", 4, "Napomena2", 3, "Rezervisano" },
                    { 1, 2, new DateTime(2021, 9, 4, 23, 29, 20, 919, DateTimeKind.Local).AddTicks(4133), "Rezervacija1", 3, "Napomena1", 1, "Rezervisano" }
                });

            migrationBuilder.InsertData(
                table: "VodiciPutovanja",
                columns: new[] { "Id", "PutovanjeId", "VodicId" },
                values: new object[,]
                {
                    { 6, 5, 6 },
                    { 1, 1, 3 },
                    { 2, 1, 4 },
                    { 4, 2, 3 },
                    { 3, 2, 5 },
                    { 7, 8, 4 },
                    { 8, 8, 6 },
                    { 10, 9, 3 },
                    { 5, 3, 3 },
                    { 9, 9, 5 }
                });

            migrationBuilder.InsertData(
                table: "Uplate",
                columns: new[] { "Id", "Datum", "Iznos", "RezervacijaId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 9, 4, 23, 29, 20, 922, DateTimeKind.Local).AddTicks(6439), 100.0, 1 },
                    { 8, new DateTime(2021, 9, 4, 23, 29, 20, 923, DateTimeKind.Local).AddTicks(4827), 150.0, 8 },
                    { 10, new DateTime(2021, 9, 4, 23, 29, 20, 923, DateTimeKind.Local).AddTicks(4851), 170.0, 10 },
                    { 2, new DateTime(2021, 9, 4, 23, 29, 20, 923, DateTimeKind.Local).AddTicks(4604), 120.0, 2 },
                    { 3, new DateTime(2021, 9, 4, 23, 29, 20, 923, DateTimeKind.Local).AddTicks(4754), 150.0, 3 },
                    { 5, new DateTime(2021, 9, 4, 23, 29, 20, 923, DateTimeKind.Local).AddTicks(4790), 150.0, 5 },
                    { 6, new DateTime(2021, 9, 4, 23, 29, 20, 923, DateTimeKind.Local).AddTicks(4802), 150.0, 6 },
                    { 4, new DateTime(2021, 9, 4, 23, 29, 20, 923, DateTimeKind.Local).AddTicks(4776), 120.0, 4 },
                    { 7, new DateTime(2021, 9, 4, 23, 29, 20, 923, DateTimeKind.Local).AddTicks(4815), 180.0, 7 },
                    { 9, new DateTime(2021, 9, 4, 23, 29, 20, 923, DateTimeKind.Local).AddTicks(4839), 180.0, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Firma_GradId",
                table: "Firma",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Gradovi_DrzavaId",
                table: "Gradovi",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_KorisnikId",
                table: "Komentar",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_PutovanjeId",
                table: "Komentar",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUloge_KorisnikId",
                table: "KorisniciUloge",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciUloge_UlogaId",
                table: "KorisniciUloge",
                column: "UlogaId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaZelja_KorisnikId",
                table: "ListaZelja",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_ListaZelja_PutovanjeId",
                table: "ListaZelja",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Obavijesti_KorisnikId",
                table: "Obavijesti",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjene_KorisnikId",
                table: "Ocjene",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocjene_PutovanjeId",
                table: "Ocjene",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prevoz_FirmaId",
                table: "Prevoz",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_GradId",
                table: "Putovanja",
                column: "GradId");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_PrevozId",
                table: "Putovanja",
                column: "PrevozId");

            migrationBuilder.CreateIndex(
                name: "IX_Putovanja_SmjestajId",
                table: "Putovanja",
                column: "SmjestajId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_KorisnikId",
                table: "Rezervacija",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Rezervacija_PutovanjeId",
                table: "Rezervacija",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_RezervacijaId",
                table: "Uplate",
                column: "RezervacijaId");

            migrationBuilder.CreateIndex(
                name: "IX_VodiciPutovanja_PutovanjeId",
                table: "VodiciPutovanja",
                column: "PutovanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_VodiciPutovanja_VodicId",
                table: "VodiciPutovanja",
                column: "VodicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentar");

            migrationBuilder.DropTable(
                name: "KorisniciUloge");

            migrationBuilder.DropTable(
                name: "ListaZelja");

            migrationBuilder.DropTable(
                name: "Obavijesti");

            migrationBuilder.DropTable(
                name: "Ocjene");

            migrationBuilder.DropTable(
                name: "Uplate");

            migrationBuilder.DropTable(
                name: "VodiciPutovanja");

            migrationBuilder.DropTable(
                name: "Uloge");

            migrationBuilder.DropTable(
                name: "Rezervacija");

            migrationBuilder.DropTable(
                name: "Vodic");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Putovanja");

            migrationBuilder.DropTable(
                name: "Prevoz");

            migrationBuilder.DropTable(
                name: "Smjestaj");

            migrationBuilder.DropTable(
                name: "Firma");

            migrationBuilder.DropTable(
                name: "Gradovi");

            migrationBuilder.DropTable(
                name: "Drzava");
        }
    }
}
