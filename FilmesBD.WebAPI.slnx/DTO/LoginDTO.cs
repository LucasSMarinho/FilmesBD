using System.ComponentModel.DataAnnotations;

namespace FilmesBD.WebAPI.slnx.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O Email do usuario é obrigatorio")]
    public string? email {  get; set; }

    [Required(ErrorMessage = "A Senha do usuario é obrigatoria")]
    public string? Senha { get; set; }
}
