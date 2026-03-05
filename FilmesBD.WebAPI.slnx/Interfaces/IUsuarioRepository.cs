using FilmesBD.WebAPI.slnx.Models;

namespace FilmesBD.WebAPI.slnx.Interfaces
{
    public interface IUsuarioRepository
    {
        void Cadastrar(Usuario novoUsuario);
        Usuario BuscarPorId(Guid id);
        Usuario BuscarPorEmailESenha(string email, string senha);
    }
}
