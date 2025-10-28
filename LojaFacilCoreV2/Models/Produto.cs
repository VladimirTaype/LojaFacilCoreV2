using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace LojaFacilCoreV2.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a categoria")]
        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty;

        // 🔹 Define tipo decimal no banco e garante compatibilidade com vírgula
        [Required(ErrorMessage = "O preço é obrigatório")]
        [Column(TypeName = "decimal(10,2)")]
        [Display(Name = "Preço")]
        [Range(0.01, 9999.99, ErrorMessage = "O preço deve ser maior que zero")]
        public decimal Preco { get; set; }

        [Display(Name = "Quantidade em Estoque")]
        public int Estoque { get; set; }
    }
}
