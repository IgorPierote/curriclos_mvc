using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurriculoMVC.Models
{
    public class Curriculo
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string CPF { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PretensaoSalarial { get; set; }

        public string CargoPretendido { get; set; }

        public string FormacaoAcademica { get; set; }

        public string ExperienciasProfissionais { get; set; }

        public string Idiomas { get; set; }
    }
}