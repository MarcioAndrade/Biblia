using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using System.Collections.Generic;

namespace Biblia.Repositorio
{
    public interface IVersiculoRepository
    {
        Task<IEnumerable<Livro>> ListarTodosAsync();
        Task<IEnumerable<Versao>> VersoesAsync();
        Task<int> CapitulosDoLivroAsync(int id);
        Task<int> VersiculosNoCapituloDoLivroAsync(int idLivro, int idCapitulo);
        Task<Versiculo> ObterAsync(int versao, int livro, int capitulo, int numero);
    }
}
