using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TuristickaAgencija.WebAPI.Migrations
{
    public partial class updated_uplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uplate_Rezervacija_RezervacijaId",
                table: "Uplate");

            migrationBuilder.AlterColumn<int>(
                name: "RezervacijaId",
                table: "Uplate",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikId",
                table: "Uplate",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(5247));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(7638));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(7864));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(7881));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(7889));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(7904));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(7912));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(7920));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(7928));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1,
                column: "DatumIzmjene",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 327, DateTimeKind.Local).AddTicks(7853));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "KorisnikUlogaId",
                keyValue: 2,
                column: "DatumIzmjene",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 335, DateTimeKind.Local).AddTicks(1089));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "KorisnikUlogaId",
                keyValue: 3,
                column: "DatumIzmjene",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 335, DateTimeKind.Local).AddTicks(1198));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(3627));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(6159));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(6340));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(6355));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(6364));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(6374));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(6383));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(6392));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(6399));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(6407));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 348, DateTimeKind.Local).AddTicks(8098));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(829));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(943));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(958));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(966));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(974));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(982));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(990));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(997));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(1005));

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 11, 8, 36, 9, 342, DateTimeKind.Local).AddTicks(9069), new DateTime(2022, 9, 11, 8, 36, 9, 342, DateTimeKind.Local).AddTicks(8250) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 11, 8, 36, 9, 343, DateTimeKind.Local).AddTicks(8203), new DateTime(2022, 9, 11, 8, 36, 9, 343, DateTimeKind.Local).AddTicks(8142) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 11, 8, 36, 9, 344, DateTimeKind.Local).AddTicks(1671), new DateTime(2022, 9, 11, 8, 36, 9, 344, DateTimeKind.Local).AddTicks(1636) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 11, 8, 36, 9, 344, DateTimeKind.Local).AddTicks(4682), new DateTime(2022, 9, 11, 8, 36, 9, 344, DateTimeKind.Local).AddTicks(4649) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 11, 8, 36, 9, 344, DateTimeKind.Local).AddTicks(7775), new DateTime(2022, 9, 11, 8, 36, 9, 344, DateTimeKind.Local).AddTicks(7744) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 11, 8, 36, 9, 345, DateTimeKind.Local).AddTicks(929), new DateTime(2022, 9, 11, 8, 36, 9, 345, DateTimeKind.Local).AddTicks(897) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 11, 8, 36, 9, 345, DateTimeKind.Local).AddTicks(3846), new DateTime(2022, 9, 11, 8, 36, 9, 345, DateTimeKind.Local).AddTicks(3815) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 11, 8, 36, 9, 345, DateTimeKind.Local).AddTicks(6946), new DateTime(2022, 9, 11, 8, 36, 9, 345, DateTimeKind.Local).AddTicks(6915) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 11, 8, 36, 9, 345, DateTimeKind.Local).AddTicks(9915), new DateTime(2022, 9, 11, 8, 36, 9, 345, DateTimeKind.Local).AddTicks(9883) });

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 1,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 346, DateTimeKind.Local).AddTicks(8782));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 2,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(3295));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 3,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(3405));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 4,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 5,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(3428));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 6,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(3437));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 7,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(3447));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 8,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(3456));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 9,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(3466));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 10,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 347, DateTimeKind.Local).AddTicks(3477));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 349, DateTimeKind.Local).AddTicks(2670));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 350, DateTimeKind.Local).AddTicks(913));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 350, DateTimeKind.Local).AddTicks(1004));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 350, DateTimeKind.Local).AddTicks(1015));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 350, DateTimeKind.Local).AddTicks(1024));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 350, DateTimeKind.Local).AddTicks(1031));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 350, DateTimeKind.Local).AddTicks(1039));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 350, DateTimeKind.Local).AddTicks(1047));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 350, DateTimeKind.Local).AddTicks(1055));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 9, 11, 8, 36, 9, 350, DateTimeKind.Local).AddTicks(1063));

            migrationBuilder.CreateIndex(
                name: "IX_Uplate_KorisnikId",
                table: "Uplate",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uplate_Korisnici_KorisnikId",
                table: "Uplate",
                column: "KorisnikId",
                principalTable: "Korisnici",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Uplate_Rezervacija_RezervacijaId",
                table: "Uplate",
                column: "RezervacijaId",
                principalTable: "Rezervacija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uplate_Korisnici_KorisnikId",
                table: "Uplate");

            migrationBuilder.DropForeignKey(
                name: "FK_Uplate_Rezervacija_RezervacijaId",
                table: "Uplate");

            migrationBuilder.DropIndex(
                name: "IX_Uplate_KorisnikId",
                table: "Uplate");

            migrationBuilder.AlterColumn<int>(
                name: "RezervacijaId",
                table: "Uplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "KorisnikId",
                table: "Uplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 746, DateTimeKind.Local).AddTicks(5378));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 747, DateTimeKind.Local).AddTicks(964));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 747, DateTimeKind.Local).AddTicks(1084));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 747, DateTimeKind.Local).AddTicks(1096));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 747, DateTimeKind.Local).AddTicks(1110));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 747, DateTimeKind.Local).AddTicks(1120));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 747, DateTimeKind.Local).AddTicks(1127));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 747, DateTimeKind.Local).AddTicks(1134));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 747, DateTimeKind.Local).AddTicks(1141));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 747, DateTimeKind.Local).AddTicks(1148));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1,
                column: "DatumIzmjene",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 706, DateTimeKind.Local).AddTicks(5609));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "KorisnikUlogaId",
                keyValue: 2,
                column: "DatumIzmjene",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 719, DateTimeKind.Local).AddTicks(2332));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "KorisnikUlogaId",
                keyValue: 3,
                column: "DatumIzmjene",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 719, DateTimeKind.Local).AddTicks(2553));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(3379));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(8942));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(9208));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(9230));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(9237));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(9244));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(9253));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(9267));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 748, DateTimeKind.Local).AddTicks(9274));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 749, DateTimeKind.Local).AddTicks(3339));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 751, DateTimeKind.Local).AddTicks(5820));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 751, DateTimeKind.Local).AddTicks(6178));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 751, DateTimeKind.Local).AddTicks(6195));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 751, DateTimeKind.Local).AddTicks(6210));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 751, DateTimeKind.Local).AddTicks(6222));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 751, DateTimeKind.Local).AddTicks(6235));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 751, DateTimeKind.Local).AddTicks(6247));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 751, DateTimeKind.Local).AddTicks(6259));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 751, DateTimeKind.Local).AddTicks(6274));

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 738, DateTimeKind.Local).AddTicks(3462), new DateTime(2022, 9, 9, 21, 1, 9, 738, DateTimeKind.Local).AddTicks(1950) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 739, DateTimeKind.Local).AddTicks(8002), new DateTime(2022, 9, 9, 21, 1, 9, 739, DateTimeKind.Local).AddTicks(7893) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 740, DateTimeKind.Local).AddTicks(2863), new DateTime(2022, 9, 9, 21, 1, 9, 740, DateTimeKind.Local).AddTicks(2811) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 740, DateTimeKind.Local).AddTicks(6298), new DateTime(2022, 9, 9, 21, 1, 9, 740, DateTimeKind.Local).AddTicks(6254) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 741, DateTimeKind.Local).AddTicks(4730), new DateTime(2022, 9, 9, 21, 1, 9, 741, DateTimeKind.Local).AddTicks(3964) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 742, DateTimeKind.Local).AddTicks(5566), new DateTime(2022, 9, 9, 21, 1, 9, 742, DateTimeKind.Local).AddTicks(5458) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 742, DateTimeKind.Local).AddTicks(9447), new DateTime(2022, 9, 9, 21, 1, 9, 742, DateTimeKind.Local).AddTicks(9397) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 743, DateTimeKind.Local).AddTicks(3383), new DateTime(2022, 9, 9, 21, 1, 9, 743, DateTimeKind.Local).AddTicks(3339) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 743, DateTimeKind.Local).AddTicks(6562), new DateTime(2022, 9, 9, 21, 1, 9, 743, DateTimeKind.Local).AddTicks(6527) });

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 1,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(872));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 2,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(9526));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 3,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(9678));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 4,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(9690));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 5,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(9699));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 6,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(9707));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 7,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(9715));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 8,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(9723));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 9,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(9732));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 10,
                column: "DatumRezervacije",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 745, DateTimeKind.Local).AddTicks(9740));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 752, DateTimeKind.Local).AddTicks(5763));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2032));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2132));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2144));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2151));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2163));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2170));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2177));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2184));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2191));

            migrationBuilder.AddForeignKey(
                name: "FK_Uplate_Rezervacija_RezervacijaId",
                table: "Uplate",
                column: "RezervacijaId",
                principalTable: "Rezervacija",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
