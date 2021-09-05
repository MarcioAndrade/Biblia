using System;

namespace Biblia.Repositorio.Excecoes
{
    public class CadastrarCaixinhaDePromessaException : Exception
    {
        public CadastrarCaixinhaDePromessaException(string mensagem) : base(mensagem)
        {
        }
    }
}
