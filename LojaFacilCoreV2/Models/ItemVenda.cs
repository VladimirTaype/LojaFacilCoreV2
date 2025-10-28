using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaFacilCoreV2.Models
{
    public class ItemVenda
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "Selecione um produto.")]
        public int ProdutoId { get; set; }

        [ForeignKey(nameof(ProdutoId))]
        public Produto? Produto { get; set; }

        [Display(Name = "Quantidade")]
        [Range(1, int.MaxValue, ErrorMessage = "Informe ao menos 1 unidade.")]
        public int Quantidade { get; set; }

        // 👇 ADICIONE ESTA LINHA
        [Display(Name = "Preço Unitário (R$)")]
        [Range(0.01, 999999.99, ErrorMessage = "O preço unitário deve ser maior que zero.")]
        public decimal PrecoUnitario { get; set; }

        // 👇 PODE MANTER O SUBTOTAL, ELE É OPCIONAL
        [Display(Name = "Subtotal (R$)")]
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }

        // FK para Venda
        public int VendaId { get; set; }
        public Venda? Venda { get; set; }
    }
}
