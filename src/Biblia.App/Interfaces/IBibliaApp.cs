using Biblia.App.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Biblia.App.Interfaces
{
    public interface IBibliaApp
    {
        Task<VersiculoViewModel> CaixinhaDePromessasAsync();
        Task<IEnumerable<LivroViewModel>> LivrosAsync();
        Task<IEnumerable<VersaoViewModel>> VersoesAsync();
        Task<VersiculoViewModel> ObterVersiculoAsync(int versaoId, int livroId, int capitulo, int numero);
        Task<IEnumerable<ResumoViewModel>> ObterResumoLivrosAsync(int versaoId);
    }
}
