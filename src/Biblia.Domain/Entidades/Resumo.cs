namespace Biblia.Domain.Entidades
{
    public class Resumo
    {
        public int TestamentoId { get; set; }
        public string Testamento { get; set; }
        public int LivroId { get; set; }
        public string Livro { get; set; }
        public int Posicao { get; set; }
        public int Capitulos { get; set; }
        public int Versiculos { get; set; }
    }
}
