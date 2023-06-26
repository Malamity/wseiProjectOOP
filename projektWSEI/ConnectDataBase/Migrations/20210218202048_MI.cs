using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDataBase.Migrations
{
    public partial class MI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employers",
                columns: table => new
                {
                    Employer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employer_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Employer_last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Employment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Username = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employers", x => x.Employer_id);
                });
            
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Item_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Item_Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Item_ID);
                });

            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    Performance_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Performance_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Performace_visit_cost = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    Performance_Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.Performance_id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPerformance",
                columns: table => new
                {
                    ItemsItem_ID = table.Column<int>(type: "int", nullable: false),
                    Performance_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPerformance", x => new { x.ItemsItem_ID, x.Performance_id });
                    table.ForeignKey(
                        name: "FK_ItemsPerformance_Items_ItemsItem_ID",
                        column: x => x.ItemsItem_ID,
                        principalTable: "Items",
                        principalColumn: "Item_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemsPerformance_Performance_Performance_id",
                        column: x => x.Performance_id,
                        principalTable: "Performance",
                        principalColumn: "Performance_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Performance_staff",
                columns: table => new
                {
                    Performance_staff_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Performance_id = table.Column<int>(type: "int", nullable: false),
                    Employer_id = table.Column<int>(type: "int", nullable: false),
                    Performance_id1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance_staff", x => x.Performance_staff_id);
                    table.ForeignKey(
                        name: "FK_Performance_staff_Employers_Employer_id",
                        column: x => x.Employer_id,
                        principalTable: "Employers",
                        principalColumn: "Employer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Performance_staff_Performance_Performance_id1",
                        column: x => x.Performance_id1,
                        principalTable: "Performance",
                        principalColumn: "Performance_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employers",
                columns: new[] { "Employer_id", "Employer_last_name", "Employer_name", "Employment_date", "Password", "Username" },
                values: new object[] { 1, "Heliak", "Mikolaj", new DateTime(2021, 2, 18, 21, 20, 48, 309, DateTimeKind.Local).AddTicks(1115), "123", "menager" });

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPerformance_Performance_id",
                table: "ItemsPerformance",
                column: "Performance_id");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_staff_Employer_id",
                table: "Performance_staff",
                column: "Employer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_staff_Performance_id1",
                table: "Performance_staff",
                column: "Performance_id1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemsPerformance");

            migrationBuilder.DropTable(
                name: "Performance_staff");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Employers");

            migrationBuilder.DropTable(
                name: "Performance");
        }
    }
}
