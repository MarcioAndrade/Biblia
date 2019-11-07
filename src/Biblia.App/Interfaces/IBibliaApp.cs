using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using System.Collections.Generic;

namespace Biblia.App.Interfaces
{
    public interface IBibliaApp
    {
        Task<Versiculo> CaixinhaDePromessasAsync();
        Task<IEnumerable<Livro>> LivrosAsync();
        Task<IEnumerable<Versao>> VersoesAsync();
        Task<Versiculo> ObterVersiculoAsync(int versaoId, int livroId, int capitulo, int numero);
    }
}
