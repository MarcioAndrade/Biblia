using System;
using Biblia.App.DTO;
using Biblia.Repositorio;
using Biblia.App.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Biblia.App.Servicos
{
    public class BibliaApp : ServiceBase, IBibliaApp
    {
        private BibliaRepository _versiculoRepository { get; }

        public BibliaApp(BibliaRepository versiculoRepository)
        {
            _versiculoRepository = versiculoRepository;
        }

        public async Task<VersiculoViewModel> CaixinhaDePromessasAsync()
        {
            // 66 livros .:. Antigo Testamento = 39; Novo Testamento = 27
            var livro = new Random(DateTime.Now.Millisecond).Next(1, 66);

            var capitulosDoLivro = await _versiculoRepository.ObterQuantidadeCapitulosDoLivroAsync(livro);
            var capitulo = new Random(DateTime.Now.Millisecond).Next(1, capitulosDoLivro);

            var versiculosNoCapitulo = await _versiculoRepository.ObterQuantidadeVersiculosNoCapituloDoLivroAsync(livro, capitulo);
            var numeroVersiculo = new Random(DateTime.Now.Millisecond).Next(1, versiculosNoCapitulo);

            var versiculo = await _versiculoRepository.ObterVersiculoAsync(5, livro, capitulo, numeroVersiculo);

            return Mapper.Map<VersiculoViewModel>(versiculo);
        }

        public async Task<IEnumerable<LivroViewModel>> LivrosAsync(int? testamentoId)
        {
            var livros = await _versiculoRepository.ListarLivrosAsync(testamentoId);

            return Mapper.Map<IEnumerable<LivroViewModel>>(livros);
        }

        public async Task<IEnumerable<VersaoViewModel>> VersoesAsync()
        {
            var versoes = await _versiculoRepository.ListarVersoesAsync();
            return Mapper.Map<IEnumerable<VersaoViewModel>>(versoes);
        }

        public async Task<VersiculoViewModel> ObterVersiculoAsync(int versaoId, int livroId, int capitulo, int numero)
        {
            var versiculo = await _versiculoRepository.ObterVersiculoAsync(versaoId, livroId, capitulo, numero);
            return Mapper.Map<VersiculoViewModel>(versiculo);
        }

        public async Task<IEnumerable<ResumoViewModel>> ObterResumoLivrosAsync(int versaoId, int? testamentoId, int? livroId)
        {
            var resumoDynamic = await _versiculoRepository.ListarResumosLivrosAsync(versaoId, testamentoId, livroId);
            return Mapper.Map<IEnumerable<ResumoViewModel>>(resumoDynamic);
        }

        public async Task<QuantidadeVersiculosCapitulo> ObterQuantidadeVersiculosNoCapituloAsync(int versaoId, int livroId, int capitulo)
        {
            var quantidadeDynamic = await _versiculoRepository.ObterQuantidadeVersiculosNoCapituloAsync(versaoId, livroId, capitulo);

            return Mapper.Map<QuantidadeVersiculosCapitulo>(quantidadeDynamic);
        }
    }
}
