using Microsoft.EntityFrameworkCore.Migrations;

namespace Canary.DbMigration.Migrations
{
    public partial class LS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW LSUser
                    AS
                    SELECT * FROM [LS_v13].[A-Peer_DB].dbo.tbl_User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW LSUser");
        }
    }
}
