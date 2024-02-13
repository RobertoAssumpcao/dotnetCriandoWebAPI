using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnetCriandoWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class capacidadeCinema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacidade",
                table: "Cinema",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacidade",
                table: "Cinema");
        }
    }
}
