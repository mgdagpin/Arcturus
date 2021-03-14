using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Arcturus.DbMigration.Migrations
{
    public partial class BaseDoNotDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 150, nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(maxLength: 150, nullable: false),
                    FullName = table.Column<string>(nullable: true, computedColumnSql: "CONCAT(LastName, ', ', FirstName, ISNULL(' ' + SUBSTRING(MiddleName,1,1) + '.', ''))"),
                    Gender = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Authors_Users_ID",
                        column: x => x.ID,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                schema: "dbo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Content = table.Column<string>(maxLength: 250, nullable: false),
                    PublishedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlogPosts_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalSchema: "dbo",
                        principalTable: "Authors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "ID", "FirstName", "Gender", "LastName", "MiddleName" },
                values: new object[] { -1, "Mar", "Male", "Dagpin", "" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Authors",
                column: "ID",
                value: -1);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_AuthorID",
                schema: "dbo",
                table: "BlogPosts",
                column: "AuthorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPosts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Authors",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");
        }
    }
}
