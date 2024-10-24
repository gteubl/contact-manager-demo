using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContactManagerDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name", "Provincia" },
                values: new object[,]
                {
                    { new Guid("2715bbd6-b877-4d92-87f1-fc5c9a003d72"), "Napoli", "NA" },
                    { new Guid("57ecb213-4180-40de-bf53-dc37c74c25bb"), "Torino", "TO" },
                    { new Guid("59fe6a19-1672-4d63-885e-02d167e42648"), "Bologna", "BO" },
                    { new Guid("60f2228a-5c90-4eec-b206-d5192c057519"), "Genova", "GE" },
                    { new Guid("7998bdb8-d0cb-42e5-b5e0-bb4d7519cf74"), "Verona", "VR" },
                    { new Guid("92fb889b-4b6e-4021-9b02-6de5e786d408"), "Firenze", "FI" },
                    { new Guid("9cc69728-ad13-43dd-a93b-3d733ed9e7cb"), "Milano", "MI" },
                    { new Guid("b4f4e3de-b40f-4749-8484-82e5b072a08a"), "Venezia", "VE" },
                    { new Guid("c85b5b8f-5b67-4a2a-8f93-79f751a72c7e"), "Brescia", "BS" },
                    { new Guid("e52cacbe-bcb7-4ee9-bcd3-6c7758bbcf64"), "Roma", "RM" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "BirthDate", "CityId", "Email", "FirstName", "Gender", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("1773734c-5d93-48d4-8c96-6ad7e1f16d7c"), new DateTime(1960, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("9cc69728-ad13-43dd-a93b-3d733ed9e7cb"), "mario.rossi@example.com", "Mario", 1, "Rossi", "+39 340 123 4567" },
                    { new Guid("3e67d961-0d7a-4070-b2a6-3bc5ef2e5e5c"), new DateTime(1967, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("92fb889b-4b6e-4021-9b02-6de5e786d408"), "roberto.baggio@example.com", "Roberto", 1, "Baggio", "+39 320 678 1234" },
                    { new Guid("79da87ef-ad11-405f-9b95-0b1f597b3d92"), new DateTime(1977, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("60f2228a-5c90-4eec-b206-d5192c057519"), "luca.toni@example.com", "Luca", 1, "Toni", "+39 331 987 6543" },
                    { new Guid("842adc90-87d2-42f7-9c1d-18b268ad0417"), new DateTime(1974, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e52cacbe-bcb7-4ee9-bcd3-6c7758bbcf64"), "giovanna.mezzogiorno@example.com", "Giovanna", 2, "Mezzogiorno", "+39 392 456 7890" },
                    { new Guid("e682b5d5-a9e3-4745-b394-1081b640e4e5"), new DateTime(1934, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2715bbd6-b877-4d92-87f1-fc5c9a003d72"), "sophia.loren@example.com", "Sophia", 2, "Loren", "+39 349 234 5678" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CityId",
                table: "Contacts",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
