using System;
using Biblia.Repositorio;
using Biblia.App.Interfaces;
using Biblia.Domain.Entidades;
using System.Collections.Generic;

namespace Biblia.App
{
    public class BibliaApp : IBibliaApp
    {
        private VersiculoRepository _versiculoRepository { get; }

        public BibliaApp(VersiculoRepository versiculoRepository)
        {
            _versiculoRepository = versiculoRepository;
        }

        public Versiculo CaixinhaDePromessas()
        {
            // 66 livros .:. Antigo Testamento = 39; Novo Testamento = 27

            var versao = new Random(DateTime.Now.Millisecond).Next(0, 5);
            var livro = new Random(DateTime.Now.Millisecond).Next(1, 66);
            var capitulo = new Random(DateTime.Now.Millisecond).Next(1, _versiculoRepository.CapitulosDoLivro(livro));
            var numeroVersiculo = new Random(DateTime.Now.Millisecond).Next(1, _versiculoRepository.VersiculosNoCapituloDoLivro(livro, capitulo));

            var versiculo = _versiculoRepository.Obter(versao, livro, capitulo, numeroVersiculo);

            if (versiculo == null)
                return null;

            return versiculo;
        }

        public IEnumerable<Livro> Livros()
        {
            return _versiculoRepository.ListarTodos();
        }

        public IEnumerable<Versao> Versoes()
        {
            return _versiculoRepository.Versoes();
        }

        public Versiculo ObterVersiculo(int versaoId, int livroId, int capitulo, int numero)
        {
            return _versiculoRepository.Obter(versaoId, livroId, capitulo, numero);
        }
    }
}
