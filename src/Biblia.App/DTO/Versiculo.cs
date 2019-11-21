namespace Biblia.App.DTO
{
    public class Versiculo
    {
        public int Id { get; set; }
        public int VersaoId { get; set; }
        public Livro Livro { get; set; }
        public int Capitulo { get; set; }
        public int Numero { get; set; }
        public string Texto { get; set; }
    }
}
