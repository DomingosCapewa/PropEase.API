using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropEase.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCoreFieldsToImovel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Alugado",
                table: "Imoveis",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Imoveis",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "Imoveis",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alugado",
                table: "Imoveis");

            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Imoveis");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Imoveis");
        }
    }
}
