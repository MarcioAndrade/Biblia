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
            const int versaoId = 3;

            var qtdCaixaPromessa = await _versiculoRepository.ObterQuantidadeCaixaPromessasAsync();
            var caixaPromessaId = new Random(DateTime.Now.Millisecond).Next(1, qtdCaixaPromessa);

            var versiculosCaixaPromessas = await _versiculoRepository.ObterVersiculosDaCaixaPromessasAsync(caixaPromessaId); //45

            var livroId = versiculosCaixaPromessas.Max(x => x.LivroId);
            var capituloId = versiculosCaixaPromessas.Max(x => x.CapituloId);
            var numerosVersiculos = versiculosCaixaPromessas.Select(x => x.NumeroVersiculo);

            var livro = await _versiculoRepository.ObterLivroAsync(livroId);
            var versiculos = await _versiculoRepository.ObterVersiculosAsync(versaoId, livroId, capituloId, numerosVersiculos);

            var referencia = $"{livro.Nome} {capituloId}:{versiculos.Min(x => x.Numero)}";
            var texto = string.Join(" ", versiculos.Select(x => x.Texto));

            if (versiculos.Count() > 1)
                referencia += $"-{versiculos.Max(x => x.Numero)}";

            return new CaixaPromessaViewModel { Id = caixaPromessaId, Referencia = referencia, Texto = texto, ContinuarLendo = $@"/v1/versao/{versaoId}/livro/{livroId}/capitulo/{capituloId}/versiculos" };
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

        public async Task<IEnumerable<CaixaPromessaViewModel>> ListarCaixinhaDePromessasAsync(int? livroId, int? capituloId, int? versiculoId)
        {
            var retorno = new List<CaixaPromessaViewModel>();

            var versosDaCaixinha = await _versiculoRepository.ObterCaixinhaDePromessaAsync(livroId, capituloId, versiculoId);

            foreach (var item in versosDaCaixinha)
                retorno.Add(new CaixaPromessaViewModel { Id = item.id, Referencia = item.livro + " " + item.capitulo + ":" + item.versiculo, Texto = item.texto });

            return retorno;
        }

        public async Task<IEnumerable<CaixaPromessaViewModel>> CadastrarCaixinhaDePromessasAsync(IEnumerable<CadastroCaixinhaPromessaRequest> cadastrosCaixinhasPromessas)
        {
            var tasks = new List<Task>();
            var retorno = new List<CaixaPromessaViewModel>();

            var id = await _versiculoRepository.ObterQuantidadeCaixaPromessasAsync();

            foreach (var item in cadastrosCaixinhasPromessas)
                tasks.Add(_versiculoRepository.CadastrarCaixinhaDePromessaAsync(id + 1, item.LivroId, item.CapituloId, item.VersiculoId));

            await Task.WhenAll(tasks);

            var retornosDynamic = new List<dynamic>();
            foreach (var item in cadastrosCaixinhasPromessas)
                retornosDynamic.AddRange(await _versiculoRepository.ObterCaixinhaDePromessaAsync(item.LivroId, item.CapituloId, item.VersiculoId));

            foreach (var item in retornosDynamic)
                retorno.Add(new CaixaPromessaViewModel { Id = item.id, Referencia = item.livro + " " + item.capitulo + ":" + item.versiculo, Texto = item.texto });

            return retorno;
        }

        public async Task<IEnumerable<VersiculoViewModel>> ObterPorTexto(string texto)
        {
            var versiculos = await _versiculoRepository.ObterVersiculosAsync(texto);

            return Mapper.Map<IEnumerable<VersiculoViewModel>>(versiculos);
        }
    }
}
