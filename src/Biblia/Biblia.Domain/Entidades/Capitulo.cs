namespace Biblia.Domain.Entidades
{
    public class Capitulo : EntityBase
    {
        public Livro Livro { get; set; }
        public string Titulo { get; set; }
        public int Numero { get; set; }
    }
}
