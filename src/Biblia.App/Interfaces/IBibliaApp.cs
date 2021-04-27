using Biblia.App.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Biblia.App.Interfaces
{
    public interface IBibliaApp
    {
        Task<CaixaPromessaViewModel> CaixinhaDePromessasAsync();
        Task<IEnumerable<CaixaPromessaViewModel>> ListarCaixinhaDePromessasAsync(int? livroId, int? capituloId, int? versiculoId);
        Task<IEnumerable<CaixaPromessaViewModel>> CadastrarCaixinhaDePromessasAsync(IEnumerable<CadastroCaixinhaPromessaRequest> cadastrosCaixinhasPromessas);
        Task<IEnumerable<LivroViewModel>> LivrosAsync(int? testamento);
        Task<IEnumerable<VersaoViewModel>> VersoesAsync();
        Task<VersiculoViewModel> ObterVersiculoAsync(int versao, int livro, int capitulo, int numero);
        Task<IEnumerable<VersiculoViewModel>> ObterVersiculosAsync(int versao, int livro, int capitulo);
        Task<IEnumerable<ResumoViewModel>> ObterResumoLivrosAsync(int versaoId, int? testamento, int? livro);
        Task<QuantidadeVersiculosCapitulo> ObterQuantidadeVersiculosNoCapituloAsync(int versao, int livro, int capitulo);
    }
}
