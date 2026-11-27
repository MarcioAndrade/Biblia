using Biblia.API.Controllers;
using Biblia.App.DTO;
using Biblia.App.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Biblia.API.Tests;

public class BibliaControllerTests
{
    [Fact]
    public async Task ListarCaixinhaPromessasAsync_RetornaNoContentQuandoNaoHaPromessas()
    {
        var bibliaApp = new Mock<IBibliaApp>();
        bibliaApp
            .Setup(app => app.ListarCaixinhaDePromessasAsync(null, null, null))
            .ReturnsAsync(Array.Empty<CaixaPromessaViewModel>());
        var controller = new BibliaController(
            NullLogger<BibliaController>.Instance,
            bibliaApp.Object);

        var resultado = await controller.ListarCaixinhaPromessasAsync(null, null, null);

        Assert.IsType<NoContentResult>(resultado);
    }

    [Fact]
    public async Task ListarCaixinhaPromessasAsync_RetornaOkQuandoHaPromessas()
    {
        var promessas = new[]
        {
            new CaixaPromessaViewModel { Id = 1, Referencia = "Salmos 23:1", Texto = "O Senhor é meu pastor." }
        };
        var bibliaApp = new Mock<IBibliaApp>();
        bibliaApp
            .Setup(app => app.ListarCaixinhaDePromessasAsync(null, null, null))
            .ReturnsAsync(promessas);
        var controller = new BibliaController(
            NullLogger<BibliaController>.Instance,
            bibliaApp.Object);

        var resultado = await controller.ListarCaixinhaPromessasAsync(null, null, null);

        var okResult = Assert.IsType<OkObjectResult>(resultado);
        Assert.Same(promessas, okResult.Value);
    }
}
