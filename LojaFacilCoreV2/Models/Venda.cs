using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LojaFacilCoreV2.Models
{
    public class Venda
    {
        public int Id { get; set; }

        [Display(Name = "Data da Venda")]
        public DateTime DataVenda { get; set; } = DateTime.Now;

        [Display(Name = "Total (R$)")]
        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        [Display(Name = "Vendedor")]
        [Required]
        public string UsuarioNome { get; set; } = string.Empty;

        // Relação 1:N com Itens
        public List<ItemVenda> Itens { get; set; } = new();
    }
}
