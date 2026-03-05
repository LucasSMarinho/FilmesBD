using FilmesBD.WebAPI.slnx.DTO;
using FilmesBD.WebAPI.slnx.Interfaces;
using FilmesBD.WebAPI.slnx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


//Por segurança criamos o Token


namespace FilmesBD.WebAPI.slnx.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    public IActionResult Login(LoginDTO loginDTO)
    {
        try
        {
            Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmailESenha(loginDTO.email!, loginDTO.Senha!);

            if (usuarioBuscado == null)
            {
                return NotFound("Email ou Senha inválidos");
            }

            //Caso encontre o usuário, prosseguir oara criação do token

            //1° - Definir as informações(claims) que serão fornecidas no token

            var claims = new[]
            {
                //Formato da claim
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario),
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email!)

                //existe a possibilidade de criar uma claim personalizada
                //new Claim("Claim Personalizada", "Valor da claim personalizada" 

            };

            //2° - Definir chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev"));

            //3° - Definir as credenciais do token (HEADER)

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //4° - Gerar o token

            var token = new JwtSecurityToken
            (
                //emissor do token
                issuer: "api_filmes",

                //destinatário do token
                audience: "api_filmes",

                //dados definidos nas claims
                claims: claims,

                //tempo de expiração do token
                expires: DateTime.Now.AddMinutes(5),

                //credenciais do token
                signingCredentials: creds

            );

            //5° retornar o token criado
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token)});
        }
        catch (Exception error)
        {

            return BadRequest(error.Message);
        }
    }
}
