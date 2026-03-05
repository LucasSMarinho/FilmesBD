using FilmesBD.WebAPI.slnx.DTO;
using FilmesBD.WebAPI.slnx.Interfaces;
using FilmesBD.WebAPI.slnx.Models;
using FilmesBD.WebAPI.slnx.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FilmesBD.WebAPI.slnx.Controllers;

[Route("api/[controller]")]
[ApiController]

public class FilmeController : ControllerBase
{
    private readonly IFilmeRepository _filmeRepository;

    public FilmeController(IFilmeRepository filmeRepository)
    {
        _filmeRepository = filmeRepository;
    }

    [HttpGet("{id}")]

    public IActionResult GetById(Guid id)
    {
        try
        {
            return Ok(_filmeRepository.BuscarPorId(id));
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    //[Authorize]
    [HttpGet]

    public IActionResult Get()
    {
        try
        {
            return Ok(_filmeRepository.Listar());
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpPost]

    public async Task<IActionResult> Post([FromForm] FilmeDTO filme)
    {
        if (string.IsNullOrEmpty(filme.Nome))
            return BadRequest("É obrigatorio que o filme tenha nome e gênero");

        Filme novoFilme = new Filme();

        if (filme.Imagem != null && filme.Imagem.Length != 0)
        {

            var extensao = Path.GetExtension(filme.Imagem.FileName);

            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";

            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            //Garante que a pasta exista
            if(!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using(var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await filme.Imagem.CopyToAsync(stream);
            }

            novoFilme.Imagem = nomeArquivo;

        }

        novoFilme.IdGenero = filme.IdGenero.ToString();
        novoFilme.Titulo = filme.Nome;

        try
        {
            _filmeRepository.Cadastrar(novoFilme);
            return StatusCode(201);
        }
        catch (Exception e)
        {
            {
                return BadRequest(e.Message);
            }
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, FilmeDTO filmeAtualizado)
    {

        var filmeBuscado = _filmeRepository.BuscarPorId(id);
        if (filmeBuscado == null)
            return NotFound("Filme não buscado");

        if (!string.IsNullOrEmpty(filmeAtualizado.Nome))
            filmeBuscado.Titulo = filmeAtualizado.Nome;

        if (filmeAtualizado.IdGenero != null && filmeBuscado.IdGenero != filmeAtualizado.IdGenero.ToString())
            filmeBuscado.IdGenero = filmeAtualizado.IdGenero.ToString();

        if (filmeAtualizado.Imagem != null && filmeAtualizado.Imagem.Length != 0)
        {
            var pastaRelativa = "wwwroot/imagens";
            var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

            //Deleta arquivo antigo

            if(!String.IsNullOrEmpty(filmeBuscado.Imagem))
            {
                var caminhoAntigo = Path.Combine(caminhoPasta, filmeBuscado.Imagem);
                if (System.IO.File.Exists(caminhoAntigo))
                    System.IO.File.Delete(caminhoAntigo);
            }

            var extensao = Path.GetExtension(filmeAtualizado.Imagem.FileName);
            var nomeArquivo = $"{Guid.NewGuid()}{extensao}";


            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

            using(var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await filmeAtualizado.Imagem.CopyToAsync(stream);
            }

            filmeBuscado.Imagem = nomeArquivo;
        }

            try
        {
            _filmeRepository.AutalizarIdUrl(id,filmeBuscado);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }


    [HttpPut]

    public IActionResult PutBody(Filme filmeAtualizado)
    {
        try
        {
            _filmeRepository.AtualizarIdCorpo(filmeAtualizado);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {

        var filmeBuscado = _filmeRepository.BuscarPorId(id);

        if (filmeBuscado == null)
            return NotFound("Filme não encontrado");

        var pastaRelativa = "wwwroot/imagens";
        var caminhoPasta = Path.Combine(Directory.GetCurrentDirectory(), pastaRelativa);

        //Deleta o arquivo

        if(!String.IsNullOrEmpty(filmeBuscado.Imagem))
        {
            var caminho = Path.Combine(caminhoPasta, filmeBuscado.Imagem);

            if (System.IO.File.Exists(caminho))
                System.IO.File.Delete(caminho);
        }

        try
        {
            _filmeRepository.Deletar(id);
            return NoContent();
        }
        catch (Exception erro)
        {
            return BadRequest(erro.Message);
        }
    }

}
