using FilmesBD.WebAPI.slnx.Models;

namespace FilmesBD.WebAPI.slnx.Interfaces;

public interface IFilmeRepository
{
    void Cadastrar(Filme novoFilme);
    void AtualizarIdCorpo(Filme filmeAtualizado);

    void AutalizarIdUrl(Guid id, Filme filmeAtualizado);

    List<Filme> Listar();

    void Deletar(Guid id);

    Filme BuscarPorId(Guid id);
}
