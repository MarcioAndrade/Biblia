using Moq;
using Xunit;
using System.Linq;
using Biblia.Domain.Enum;
using Biblia.Repositorio;
using Biblia.App.Servicos;
using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using System.Collections.Generic;

namespace Biblia.App.Testes
{
    public class BibliaAppTeste
    {
        Mock<IBibliaRepository> MockBibliaRepository = new Mock<IBibliaRepository>();

        [Fact(DisplayName = "LivrosAsync retorna livros com sucesso")]
        public async Task LivrosAsync_Retorna_Livros_Com_Sucesso()
        {
            var livros = new List<Livro> 
            {
                new Livro
                {
                    Id = Faker.RandomNumber.Next(1, 99999),
                    Nome = Faker.Name.First(),
                    Posicao = Faker.RandomNumber.Next(1, 99),
                    TestamentoId = (TestamentoEnum)Faker.RandomNumber.Next(1, 2)
                } 
            };

            MockBibliaRepository.Setup(x => x.ListarLivrosAsync(It.IsAny<int>()))
                .ReturnsAsync(livros);

            var app = new BibliaApp(MockBibliaRepository.Object);

            var result = await app.LivrosAsync(0);

            Assert.NotNull(result);
            Assert.Equal(livros.First().Id, result.First().Id);
            Assert.Equal(livros.First().Nome, result.First().Nome);
            Assert.Equal(livros.First().Posicao, result.First().Posicao);
            Assert.Equal((int)livros.First().TestamentoId, result.First().TestamentoId);
        }
    }
}
