

namespace FilmesBD.WebAPI.slnx.DTO;

public class FilmeDTO
{
    public string? Nome { get; set; }

    public IFormFile? Imagem { get; set; }

    public Guid? IdGenero { get; set; }
}
