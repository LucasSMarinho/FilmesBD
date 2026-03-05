using FilmesBD.WebAPI.slnx.BdContextFilme;
using FilmesBD.WebAPI.slnx.Interfaces;
using FilmesBD.WebAPI.slnx.Models;

namespace FilmesBD.WebAPI.slnx.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly FilmeContext _context;

        public FilmeRepository(FilmeContext context)
        {
            _context = context;
        }

        public void AtualizarIdCorpo(Filme filmeAtualizado)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(filmeAtualizado.IdFilme)!;

                if (filmeBuscado != null)
                {
                    filmeBuscado.Titulo = filmeAtualizado.Titulo;
                    filmeBuscado.IdGenero = filmeAtualizado.IdGenero;
                }

                _context.Filmes.Update(filmeBuscado!);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AutalizarIdUrl(Guid id, Filme filmeAtualizado)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(id.ToString())!;
                if (filmeBuscado != null)
                {
                    filmeBuscado.Titulo = filmeAtualizado.Titulo;
                    filmeBuscado.IdGenero = filmeAtualizado.IdGenero;
                }

                _context.Filmes.Update(filmeBuscado!);
                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Filme BuscarPorId(Guid id)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(id.ToString())!;
                return filmeBuscado;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Cadastrar(Filme novoFilme)
        {
            try
            {
                novoFilme.IdFilme = Guid.NewGuid().ToString();

                _context.Filmes.Add(novoFilme);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                Filme filmeBuscado = _context.Filmes.Find(id.ToString())!;

                if(filmeBuscado != null)
                {
                    _context.Filmes.Remove(filmeBuscado);

                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Filme> Listar()
        {
            try
            {
                List<Filme> listaFilmes = _context.Filmes.ToList();

                return listaFilmes;
            }
            catch
            {
                throw;
            }
        }
    }
}
