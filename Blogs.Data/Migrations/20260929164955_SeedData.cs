using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogs.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
       table: "Users",
       columns: new[] { "Username", "CreatedAt" },
       values: new object[,]
       {
            { "john doe", new DateTime(2026, 7, 10, 10, 0, 0) },
            { "jane smith", new DateTime(2026, 8, 5, 11, 30, 0) },
            { "alex wilson", new DateTime(2026, 9, 1, 9, 15, 0) },
            { "maria garcia", new DateTime(2026, 9, 10, 14, 0, 0) }
       });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
