using System;
using Biblia.Repositorio;
using Biblia.App.Interfaces;
using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using System.Collections.Generic;

namespace Biblia.App.Servicos
{
    public class BibliaApp : IBibliaApp
    {
        private VersiculoRepository _versiculoRepository { get; }

        public BibliaApp(VersiculoRepository versiculoRepository)
        {
            _versiculoRepository = versiculoRepository;
        }

        public async Task<Versiculo> CaixinhaDePromessasAsync()
        {
            // 66 livros .:. Antigo Testamento = 39; Novo Testamento = 27

            var versao = new Random(DateTime.Now.Millisecond).Next(0, 5);
            var livro = new Random(DateTime.Now.Millisecond).Next(1, 66);

            var capitulosDoLivro = await _versiculoRepository.CapitulosDoLivroAsync(livro);
            var capitulo = new Random(DateTime.Now.Millisecond).Next(1, capitulosDoLivro);

            var versiculosNoCapitulo = await _versiculoRepository.VersiculosNoCapituloDoLivroAsync(livro, capitulo);
            var numeroVersiculo = new Random(DateTime.Now.Millisecond).Next(1, versiculosNoCapitulo);

            var versiculo = await _versiculoRepository.ObterAsync(versao, livro, capitulo, numeroVersiculo);

            return versiculo;
        }

        public async Task<IEnumerable<Livro>> LivrosAsync()
        {
            return await _versiculoRepository.ListarTodosAsync();
        }

        public async Task<IEnumerable<Versao>> VersoesAsync()
        {
            return await _versiculoRepository.VersoesAsync();
        }

        public async Task<Versiculo> ObterVersiculoAsync(int versaoId, int livroId, int capitulo, int numero)
        {
            return await _versiculoRepository.ObterAsync(versaoId, livroId, capitulo, numero);
        }
    }
}
