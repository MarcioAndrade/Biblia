namespace Biblia.App.DTO
{
    public class VersiculoViewModel
    {
        public int Id { get; set; }
        public int VersaoId { get; set; }
        public LivroViewModel Livro { get; set; }
        public int Capitulo { get; set; }
        public int Numero { get; set; }
        public string Texto { get; set; }
    }
}
