using Biblia.Domain.Entidades;

namespace Biblia.Domain.Tests;

public class VersiculoTests
{
    [Fact]
    public void ToString_FormataReferenciaETextoDoVersiculo()
    {
        var versiculo = new Versiculo
        {
            Livro = new Livro { Nome = "Salmos" },
            Capitulo = 23,
            Numero = 1,
            Texto = "O Senhor é meu pastor"
        };

        var resultado = versiculo.ToString();

        Assert.Equal("Salmos 23.1: \"O Senhor é meu pastor\"", resultado);
    }

    [Fact]
    public void ToString_FormataVersiculoSemLivro()
    {
        var versiculo = new Versiculo
        {
            Capitulo = 23,
            Numero = 1,
            Texto = "O Senhor é meu pastor"
        };

        var resultado = versiculo.ToString();

        Assert.Equal(" 23.1: \"O Senhor é meu pastor\"", resultado);
    }
}
