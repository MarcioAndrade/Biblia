using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using System.Collections.Generic;

namespace Biblia.Repositorio
{
    public interface IBibliaRepository
    {
        Task<IEnumerable<Livro>> ListarLivrosAsync(int? testamento);
        Task<IEnumerable<Versao>> ListarVersoesAsync();
        Task<int> ObterQuantidadeCapitulosDoLivroAsync(int id);
        Task<int> ObterQuantidadeVersiculosNoCapituloDoLivroAsync(int livro, int capitulo);
        Task<Versiculo> ObterVersiculoAsync(int versao, int livro, int capitulo, int numero);
        Task<IEnumerable<Resumo>> ListarResumosLivrosAsync(int versao, int? testamento, int? livro);
        Task<dynamic> ObterQuantidadeVersiculosNoCapituloAsync(int versao, int livro, int capitulo);
        Task<int> ObterQuantidadeCaixaPromessasAsync();
        Task<IEnumerable<CaixaPromessas>> ObterVersiculosDaCaixaPromessasAsync(int caixaPromessa);
        Task<IEnumerable<Versiculo>> ObterVersiculosAsync(int versao, int livro, int capitulo);
        Task<IEnumerable<Versiculo>> ObterVersiculosAsync(int versao, int livro, int capitulo, IEnumerable<int> numeros);
        Task<Livro> ObterLivroAsync(int livro);
    }
}
