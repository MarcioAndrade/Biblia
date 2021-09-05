namespace Biblia.Domain.Entidades
{
    public class Versiculo : EntityBase
    {
        public int VersaoId { get; set; }
        public Livro Livro { get; set; }
        public int Capitulo { get; set; }
        public int Numero { get; set; }
        public string Texto { get; set; }

        public override string ToString()
        {
            return $"{Livro?.Nome} {Capitulo}.{Numero}: \"{Texto}\"";
        }
    }
}
