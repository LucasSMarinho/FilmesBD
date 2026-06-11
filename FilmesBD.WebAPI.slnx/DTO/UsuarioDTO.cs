using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FilmesBD.WebAPI.slnx.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "O Email do usuario é obrigatorio")]
        public string? email { get; set; }

        [Required(ErrorMessage = "A Senha do usuario é obrigatoria")]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O Nome do usuario é obrigatorio")]
        public string? Nome { get; set; }
    }
}
