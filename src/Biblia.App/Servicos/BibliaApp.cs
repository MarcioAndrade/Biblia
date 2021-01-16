using System;
using System.Linq;
using Biblia.App.DTO;
using Biblia.Repositorio;
using Biblia.App.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Biblia.App.Servicos
{
    public class BibliaApp : ServiceBase, IBibliaApp
    {
        private IBibliaRepository _versiculoRepository { get; }

        public BibliaApp(IBibliaRepository versiculoRepository)
        {
            _versiculoRepository = versiculoRepository;
        }

        public async Task<CaixaPromessaViewModel> CaixinhaDePromessasAsync()
        {
            var qtdCaixaPromessa = await _versiculoRepository.ObterQuantidadeCaixaPromessasAsync();
            var caixaPromessaId = new Random(DateTime.Now.Millisecond).Next(1, qtdCaixaPromessa);

            var versiculosCaixaPromessas = await _versiculoRepository.ObterVersiculosDaCaixaPromessasAsync(caixaPromessaId); //45

            var livroId = versiculosCaixaPromessas.Max(x => x.LivroId);
            var capituloId = versiculosCaixaPromessas.Max(x => x.CapituloId);
            var numerosVersiculos = versiculosCaixaPromessas.Select(x => x.NumeroVersiculo);

            var livro = await _versiculoRepository.ObterLivroAsync(livroId);
            var versiculos = await _versiculoRepository.ObterVersiculosAsync(5, livroId, capituloId, numerosVersiculos);

            var referencia = $"{livro.Nome} {capituloId}:{versiculos.Min(x => x.Numero)}";
            var texto = string.Join(" ", versiculos.Select(x => x.Texto));

            if (versiculos.Count() > 1)
                referencia += $"-{versiculos.Max(x => x.Numero)}";

            return new CaixaPromessaViewModel { Id = caixaPromessaId, Referencia = referencia, Texto = texto };
        }

        public async Task<IEnumerable<LivroViewModel>> LivrosAsync(int? testamento)
        {
            var livros = await _versiculoRepository.ListarLivrosAsync(testamento);

            return Mapper.Map<IEnumerable<LivroViewModel>>(livros);
        }

        public async Task<IEnumerable<VersaoViewModel>> VersoesAsync()
        {
            var versoes = await _versiculoRepository.ListarVersoesAsync();
            return Mapper.Map<IEnumerable<VersaoViewModel>>(versoes);
        }

        public async Task<VersiculoViewModel> ObterVersiculoAsync(int versao, int livro, int capitulo, int numero)
        {
            var versiculo = await _versiculoRepository.ObterVersiculoAsync(versao, livro, capitulo, numero);
            return Mapper.Map<VersiculoViewModel>(versiculo);
        }

        public async Task<IEnumerable<ResumoViewModel>> ObterResumoLivrosAsync(int versaoId, int? testamento, int? livro)
        {
            var resumoDynamic = await _versiculoRepository.ListarResumosLivrosAsync(versaoId, testamento, livro);
            return Mapper.Map<IEnumerable<ResumoViewModel>>(resumoDynamic);
        }

        public async Task<QuantidadeVersiculosCapitulo> ObterQuantidadeVersiculosNoCapituloAsync(int versao, int livro, int capitulo)
        {
            var quantidadeDynamic = await _versiculoRepository.ObterQuantidadeVersiculosNoCapituloAsync(versao, livro, capitulo);

            return Mapper.Map<QuantidadeVersiculosCapitulo>(quantidadeDynamic);
        }

        public async Task<IEnumerable<VersiculoViewModel>> ObterVersiculosAsync(int versao, int livro, int capitulo)
        {
            var versiculos = await _versiculoRepository.ObterVersiculosAsync(versao, livro, capitulo);
            return Mapper.Map<IEnumerable<VersiculoViewModel>>(versiculos);
        }
    }
}
