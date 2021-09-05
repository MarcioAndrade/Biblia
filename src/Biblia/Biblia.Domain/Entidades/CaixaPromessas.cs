namespace Biblia.Domain.Entidades
{
    public class CaixaPromessas
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public int CapituloId { get; set; }
        public int NumeroVersiculo { get; set; }
    }
}
