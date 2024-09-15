using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaDeControleDeCurriculo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curriculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PretensaoSalarial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CargoPretendido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormacaoAcademica = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienciasProfissionais = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Idiomas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curriculos");
        }
    }
}
