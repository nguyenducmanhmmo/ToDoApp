using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToDo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateDate", "Name", "Password", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb"), new DateTime(2023, 10, 10, 12, 56, 40, 625, DateTimeKind.Utc).AddTicks(7941), "user01", "user01", null },
                    { new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc"), new DateTime(2023, 10, 10, 12, 56, 40, 625, DateTimeKind.Utc).AddTicks(7949), "user02", "user02", null }
                });

            migrationBuilder.InsertData(
                table: "ToDo",
                columns: new[] { "Id", "CreateDate", "CreationDate", "Title", "UpdateDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfa"), null, new DateTime(2023, 10, 10, 12, 56, 40, 625, DateTimeKind.Utc).AddTicks(4552), "Milk", null, new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb") },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfb"), null, new DateTime(2023, 10, 10, 12, 56, 40, 625, DateTimeKind.Utc).AddTicks(4559), "Dog food", null, new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfb") },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfc"), null, new DateTime(2023, 10, 10, 12, 56, 40, 625, DateTimeKind.Utc).AddTicks(4561), "Kubernetes", null, new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc") },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfd"), null, new DateTime(2023, 10, 10, 12, 56, 40, 625, DateTimeKind.Utc).AddTicks(4563), "New Relic", null, new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc") },
                    { new Guid("8c2196e4-9d06-4574-a212-d4bdef0a4bfe"), null, new DateTime(2023, 10, 10, 12, 56, 40, 625, DateTimeKind.Utc).AddTicks(4565), "Azure Databases", null, new Guid("7c2196e4-9d06-4574-a212-d4bdef0a4bfc") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDo_UserId",
                table: "ToDo",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDo");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
