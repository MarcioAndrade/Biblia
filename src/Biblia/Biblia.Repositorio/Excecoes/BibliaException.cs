using System;

namespace Biblia.Repositorio.Excecoes
{
    public class BibliaException : Exception
    {
        public BibliaException(string mensagem) : base(mensagem)    
        {
        }
    }
}
