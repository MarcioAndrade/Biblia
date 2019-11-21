using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using System.Collections.Generic;

namespace Biblia.Repositorio
{
    public interface IVersiculoRepository
    {
        Task<IEnumerable<Livro>> ListarLivrosAsync(int? testamentoId);
        Task<IEnumerable<Versao>> ListarVersoesAsync();
        Task<int> ObterQuantidadeCapitulosDoLivroAsync(int id);
        Task<int> ObterQuantidadeVersiculosNoCapituloDoLivroAsync(int idLivro, int idCapitulo);
        Task<Versiculo> ObterVersiculoAsync(int versao, int livro, int capitulo, int numero);
        Task<IEnumerable<dynamic>> ListarResumosLivrosAsync(int versaoId, int? testamentoId, int? livroId);
        Task<int> ObterQuantidadeVersiculosNoCapituloAsync(int versaoId, int livroId, int capitulo);
    }
}
