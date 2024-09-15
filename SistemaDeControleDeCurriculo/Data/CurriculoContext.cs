using Microsoft.EntityFrameworkCore;
using CurriculoMVC.Models;
using System.Collections.Generic;

namespace CurriculoMVC.Data
{
    public class CurriculoContext : DbContext
    {
        /*
        Script para criação da tabela Curriculo:

        CREATE TABLE Curriculos (
            Id INT PRIMARY KEY IDENTITY,
            Nome NVARCHAR(100),
            CPF NVARCHAR(14),
            Endereco NVARCHAR(200),
            Telefone NVARCHAR(15),
            Email NVARCHAR(100),
            PretensaoSalarial DECIMAL(18, 2),
            CargoPretendido NVARCHAR(100),
            FormacaoAcademica NVARCHAR(MAX),
            ExperienciasProfissionais NVARCHAR(MAX),
            Idiomas NVARCHAR(MAX)
        );
        */

        public CurriculoContext(DbContextOptions<CurriculoContext> options) : base(options) { }

        public DbSet<Curriculo> Curriculos { get; set; }
    }
}