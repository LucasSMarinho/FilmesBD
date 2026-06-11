using FilmesBD.WebAPI.slnx.DTO;
using FilmesBD.WebAPI.slnx.Interfaces;
using FilmesBD.WebAPI.slnx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FilmesBD.WebAPI.slnx.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{

    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]

    public IActionResult Post(UsuarioDTO usuario)
    {
        try
        {

            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome!,
                Senha = usuario.Senha!,
                Email = usuario.email!
            };

            _usuarioRepository.Cadastrar(novoUsuario);

            return StatusCode(201, novoUsuario);
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

}
