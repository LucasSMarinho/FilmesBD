using FilmesBD.WebAPI.slnx.BdContextFilme;
using FilmesBD.WebAPI.slnx.Interfaces;
using FilmesBD.WebAPI.slnx.Repositories;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//adiciona o contexto do banco de dados (exemplo com SQL Server)

builder.Services.AddDbContext<FilmeContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionContext")));

//adiciona o repositório ao container de injeção de dependencia

builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();

//adiciona o serviço de Controllers

builder.Services.AddControllers();

var app = builder.Build();

//adiciona o mapeamento de Controllers
app.MapControllers();

app.Run();
