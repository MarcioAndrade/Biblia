using Moq;
using Xunit;
using Biblia.Controllers;
using Biblia.App.Interfaces;
using System.Threading.Tasks;

namespace Biblia.Teste
{
    public class BibliaControllerTeste
    {
        readonly Mock<IBibliaApp> MockBibliaApp = new Mock<IBibliaApp>();

        [Fact]
        public async Task CaixinhaPromessasAsunc_Teste()
        {
            var controller = new BibliaController(MockBibliaApp.Object);

            //var response = await controller.CaixinhaPromessasAsync();
        }
    }
}
