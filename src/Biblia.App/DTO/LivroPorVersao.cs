namespace Biblia.App.DTO
{
    public class LivroPorVersao
    {
        public int Id { get; set; }
        public Testamento Testamento { get; set; }
        public Livro Livro { get; set; }
    }
}
