using Microsoft.EntityFrameworkCore.Migrations;

namespace LojaVirtual.Migrations
{
    public partial class NewsletterEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_newsletterEmails",
                table: "newsletterEmails");

            migrationBuilder.RenameTable(
                name: "newsletterEmails",
                newName: "NewsletterEmails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsletterEmails",
                table: "NewsletterEmails",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsletterEmails",
                table: "NewsletterEmails");

            migrationBuilder.RenameTable(
                name: "NewsletterEmails",
                newName: "newsletterEmails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_newsletterEmails",
                table: "newsletterEmails",
                column: "Id");
        }
    }
}
