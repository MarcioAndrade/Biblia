using Biblia.Domain.Entidades;
using System.Collections.Generic;

namespace Biblia.App.Interfaces
{
    public interface IBibliaApp
    {
        Versiculo CaixinhaDePromessas();
        IEnumerable<Livro> Livros();
        IEnumerable<Versao> Versoes();
        Versiculo ObterVersiculo(int versaoId, int livroId, int capitulo, int numero);
    }
}
