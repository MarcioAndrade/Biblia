using Biblia.Domain.Enum;

namespace Biblia.Domain.Entidades
{
    public class Livro : EntityBase
    {        
        public string Nome { get; set; }
        public TestamentoEnum TestamentoId { get; set; }
        public int Posicao { get; set; }
    }
}
