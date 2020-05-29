using Biblia.App.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Biblia.App.Interfaces
{
    public interface IBibliaApp
    {
        Task<CaixaPromessaViewModel> CaixinhaDePromessasAsync();
        Task<IEnumerable<LivroViewModel>> LivrosAsync(int? testamentoId);
        Task<IEnumerable<VersaoViewModel>> VersoesAsync();
        Task<VersiculoViewModel> ObterVersiculoAsync(int versaoId, int livroId, int capitulo, int numero);
        Task<IEnumerable<ResumoViewModel>> ObterResumoLivrosAsync(int versaoId, int? testamentoId, int? livroId);
        Task<QuantidadeVersiculosCapitulo> ObterQuantidadeVersiculosNoCapituloAsync(int versaoId, int livroId, int capitulo);
    }
}
