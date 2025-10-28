using System.ComponentModel.DataAnnotations;

namespace LojaFacilCoreV2.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(255)]
        public string SenhaHash { get; set; }

        [Required(ErrorMessage = "O tipo de usuário é obrigatório")]
        [Display(Name = "Tipo de Usuário")]
        public string TipoUsuario { get; set; } // "Gerente" ou "Cliente"
    }
}
