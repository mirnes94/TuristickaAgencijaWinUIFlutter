using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TuristickaAgencija.WebAPI.Migrations
{
    public partial class UpdatedUplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Uplate",
                nullable: true);

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
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 752, DateTimeKind.Local).AddTicks(5763), 1 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2032), 2 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2132), 3 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2144), 4 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2151), 5 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2163), 6 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2170), 3 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2177), 4 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2184), 5 });

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Datum", "KorisnikId" },
                values: new object[] { new DateTime(2022, 9, 9, 21, 1, 9, 753, DateTimeKind.Local).AddTicks(2191), 6 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Uplate");

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(8669));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 779, DateTimeKind.Local).AddTicks(5687));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 779, DateTimeKind.Local).AddTicks(5938));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 779, DateTimeKind.Local).AddTicks(5964));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 779, DateTimeKind.Local).AddTicks(5997));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 779, DateTimeKind.Local).AddTicks(6031));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 779, DateTimeKind.Local).AddTicks(6050));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 779, DateTimeKind.Local).AddTicks(6060));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 779, DateTimeKind.Local).AddTicks(6070));

            migrationBuilder.UpdateData(
                table: "Komentar",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 779, DateTimeKind.Local).AddTicks(6081));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "KorisnikUlogaId",
                keyValue: 1,
                column: "DatumIzmjene",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 750, DateTimeKind.Local).AddTicks(7313));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "KorisnikUlogaId",
                keyValue: 2,
                column: "DatumIzmjene",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 759, DateTimeKind.Local).AddTicks(9488));

            migrationBuilder.UpdateData(
                table: "KorisniciUloge",
                keyColumn: "KorisnikUlogaId",
                keyValue: 3,
                column: "DatumIzmjene",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 759, DateTimeKind.Local).AddTicks(9714));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 780, DateTimeKind.Local).AddTicks(6960));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(1570));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(1892));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(1917));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(1934));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(1947));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(1963));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(1981));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(1995));

            migrationBuilder.UpdateData(
                table: "Obavijesti",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(2009));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(5210));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(9309));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(9507));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(9525));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(9538));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(9551));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(9563));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(9575));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(9586));

            migrationBuilder.UpdateData(
                table: "Ocjene",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 781, DateTimeKind.Local).AddTicks(9598));

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 5, 23, 15, 9, 28, 772, DateTimeKind.Local).AddTicks(504), new DateTime(2022, 5, 23, 15, 9, 28, 771, DateTimeKind.Local).AddTicks(9369) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 5, 23, 15, 9, 28, 773, DateTimeKind.Local).AddTicks(4282), new DateTime(2022, 5, 23, 15, 9, 28, 773, DateTimeKind.Local).AddTicks(4036) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 5, 23, 15, 9, 28, 773, DateTimeKind.Local).AddTicks(9597), new DateTime(2022, 5, 23, 15, 9, 28, 773, DateTimeKind.Local).AddTicks(9537) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 5, 23, 15, 9, 28, 774, DateTimeKind.Local).AddTicks(3434), new DateTime(2022, 5, 23, 15, 9, 28, 774, DateTimeKind.Local).AddTicks(3380) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 5, 23, 15, 9, 28, 774, DateTimeKind.Local).AddTicks(6976), new DateTime(2022, 5, 23, 15, 9, 28, 774, DateTimeKind.Local).AddTicks(6918) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 5, 23, 15, 9, 28, 775, DateTimeKind.Local).AddTicks(453), new DateTime(2022, 5, 23, 15, 9, 28, 775, DateTimeKind.Local).AddTicks(407) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 5, 23, 15, 9, 28, 775, DateTimeKind.Local).AddTicks(3326), new DateTime(2022, 5, 23, 15, 9, 28, 775, DateTimeKind.Local).AddTicks(3290) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 5, 23, 15, 9, 28, 775, DateTimeKind.Local).AddTicks(7025), new DateTime(2022, 5, 23, 15, 9, 28, 775, DateTimeKind.Local).AddTicks(6968) });

            migrationBuilder.UpdateData(
                table: "Putovanja",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "DatumDolaska", "DatumPolaska" },
                values: new object[] { new DateTime(2022, 5, 23, 15, 9, 28, 776, DateTimeKind.Local).AddTicks(1390), new DateTime(2022, 5, 23, 15, 9, 28, 776, DateTimeKind.Local).AddTicks(1326) });

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 1,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 777, DateTimeKind.Local).AddTicks(6333));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 2,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(3901));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 3,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(4118));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 4,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(4145));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 5,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(4161));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 6,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(4177));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 7,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(4193));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 8,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(4207));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 9,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(4465));

            migrationBuilder.UpdateData(
                table: "Rezervacija",
                keyColumn: "Id",
                keyValue: 10,
                column: "DatumRezervacije",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 778, DateTimeKind.Local).AddTicks(4498));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 1,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 782, DateTimeKind.Local).AddTicks(2112));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 2,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 783, DateTimeKind.Local).AddTicks(255));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 3,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 783, DateTimeKind.Local).AddTicks(432));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 4,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 783, DateTimeKind.Local).AddTicks(449));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 5,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 783, DateTimeKind.Local).AddTicks(460));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 6,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 783, DateTimeKind.Local).AddTicks(472));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 7,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 783, DateTimeKind.Local).AddTicks(486));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 8,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 783, DateTimeKind.Local).AddTicks(498));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 9,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 783, DateTimeKind.Local).AddTicks(512));

            migrationBuilder.UpdateData(
                table: "Uplate",
                keyColumn: "Id",
                keyValue: 10,
                column: "Datum",
                value: new DateTime(2022, 5, 23, 15, 9, 28, 783, DateTimeKind.Local).AddTicks(524));
        }
    }
}
