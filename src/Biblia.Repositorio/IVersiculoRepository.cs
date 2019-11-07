using Biblia.Domain.Entidades;
using System.Collections.Generic;

namespace Biblia.Repositorio
{
    public interface IVersiculoRepository
    {
        IEnumerable<Livro> ListarTodos();
        IEnumerable<Versao> Versoes();
        int CapitulosDoLivro(int id);
        int VersiculosNoCapituloDoLivro(int idLivro, int idCapitulo);
        Versiculo Obter(int versao, int livro, int capitulo, int numero);
    }
}
