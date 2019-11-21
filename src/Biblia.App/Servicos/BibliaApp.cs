using System;
using Biblia.Repositorio;
using Biblia.App.Interfaces;
using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using System.Collections.Generic;
using Biblia.App.DTO;

namespace Biblia.App.Servicos
{
    public class BibliaApp : ServiceBase, IBibliaApp
    {
        private VersiculoRepository _versiculoRepository { get; }

        public BibliaApp(VersiculoRepository versiculoRepository)
        {
            _versiculoRepository = versiculoRepository;
        }

        public async Task<VersiculoViewModel> CaixinhaDePromessasAsync()
        {
            // 66 livros .:. Antigo Testamento = 39; Novo Testamento = 27

            var versao = new Random(DateTime.Now.Millisecond).Next(0, 5);
            var livro = new Random(DateTime.Now.Millisecond).Next(1, 66);

            var capitulosDoLivro = await _versiculoRepository.CapitulosDoLivroAsync(livro);
            var capitulo = new Random(DateTime.Now.Millisecond).Next(1, capitulosDoLivro);

            var versiculosNoCapitulo = await _versiculoRepository.VersiculosNoCapituloDoLivroAsync(livro, capitulo);
            var numeroVersiculo = new Random(DateTime.Now.Millisecond).Next(1, versiculosNoCapitulo);

            var versiculo = await _versiculoRepository.ObterAsync(versao, livro, capitulo, numeroVersiculo);

            return Mapper.Map< VersiculoViewModel>(versiculo);
        }

        public async Task<IEnumerable<LivroViewModel>> LivrosAsync()
        {
            var livros = await _versiculoRepository.ListarTodosAsync();

            return Mapper.Map<IEnumerable<LivroViewModel>>(livros);
        }

        public async Task<IEnumerable<VersaoViewModel>> VersoesAsync()
        {
            var versoes = await _versiculoRepository.VersoesAsync();
            return Mapper.Map<IEnumerable<VersaoViewModel>>(versoes);
        }

        public async Task<VersiculoViewModel> ObterVersiculoAsync(int versaoId, int livroId, int capitulo, int numero)
        {
            var versiculo = await _versiculoRepository.ObterAsync(versaoId, livroId, capitulo, numero);
            return Mapper.Map<VersiculoViewModel>(versiculo);
        }

        public async Task<IEnumerable<Resumo>> ObterResumoLivrosAsync(int versaoId)
        {
            return await _versiculoRepository.ObterResumoLivrosAsync(versaoId);
        }
    }
}
