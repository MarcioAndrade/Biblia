using Biblia.App.Servicos;
using Biblia.Domain.Entidades;
using Biblia.Domain.Enum;
using Biblia.Repositorio;
using Moq;

namespace Biblia.App.Tests;

public class BibliaAppTests
{
    [Fact]
    public async Task LivrosAsync_MapeiaLivrosRetornadosPeloRepositorio()
    {
        var livros = new[]
        {
            new Livro
            {
                Id = 1,
                Nome = "Gênesis",
                TestamentoId = TestamentoEnum.ANTIGO,
                Posicao = 1
            }
        };
        var repositorio = new Mock<IBibliaRepository>();
        repositorio
            .Setup(repository => repository.ListarLivrosAsync(1))
            .ReturnsAsync(livros);
        var servico = new BibliaApp(repositorio.Object);

        var resultado = (await servico.LivrosAsync(1)).ToArray();

        var livro = Assert.Single(resultado);
        Assert.Equal(livros[0].Id, livro.Id);
        Assert.Equal(livros[0].Nome, livro.Nome);
        Assert.Equal((int)livros[0].TestamentoId, livro.TestamentoId);
        Assert.Equal(livros[0].Posicao, livro.Posicao);
        repositorio.Verify(repository => repository.ListarLivrosAsync(1), Times.Once);
    }
}
