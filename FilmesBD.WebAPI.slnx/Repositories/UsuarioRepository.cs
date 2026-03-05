using FilmesBD.WebAPI.slnx.BdContextFilme;
using FilmesBD.WebAPI.slnx.Interfaces;
using FilmesBD.WebAPI.slnx.Models;
using FilmesBD.WebAPI.slnx.Utils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FilmesBD.WebAPI.slnx.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly FilmeContext _context;
    
    public UsuarioRepository( FilmeContext context)
    {
        _context = context;
    }

    public Usuario BuscarPorEmailESenha(string email, string senha)
    {
        try
        {
            Usuario usuarioBuscado = _context.Usuarios.FirstOrDefault(u => u.Email == email)!;

            if (usuarioBuscado != null)
            {
                bool confere = Criptografia.compararHash(senha, usuarioBuscado.Senha!);

                if (confere)
                {
                    return usuarioBuscado;
                }
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public Usuario BuscarPorId(Guid id)
    {
        try
        {
            Usuario usuarioBuscado = _context.Usuarios.Find(id.ToString())!;

            if (usuarioBuscado != null)
            {
                return usuarioBuscado;
            }

            return null!;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void Cadastrar(Usuario novoUsuario)
    {
        try
        {
            novoUsuario.IdUsuario = Guid.NewGuid().ToString();
            novoUsuario.Senha = Criptografia.GerarHash(novoUsuario.Senha)!;

            _context.Usuarios.Add(novoUsuario);
            _context.SaveChanges();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
