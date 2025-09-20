using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Todo.Minimalist.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "IsDone", "Title" },
                values: new object[,]
                {
                    { new Guid("a1e1c1d1-0000-0000-0000-000000000001"), false, "Aprender .NET 10" },
                    { new Guid("a1e1c1d1-0000-0000-0000-000000000002"), true, "Criar API Minimalista" },
                    { new Guid("a1e1c1d1-0000-0000-0000-000000000003"), false, "Testar Seed Data" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("a1e1c1d1-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("a1e1c1d1-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: new Guid("a1e1c1d1-0000-0000-0000-000000000003"));

            migrationBuilder.InsertData(
                table: "TodoItems",
                columns: new[] { "Id", "IsDone", "Title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), false, "Aprender .NET 10" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, "Criar API Minimalista" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), false, "Testar Seed Data" }
                });
        }
    }
}
