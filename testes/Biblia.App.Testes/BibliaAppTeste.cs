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
        readonly Mock<IBibliaRepository> MockBibliaRepository = new Mock<IBibliaRepository>();

        [Fact(DisplayName = "CaixinhaDePromessasAsync retorna com um versículo com sucesso")]
        public async Task CaixinhaDePromessasAsync_Retorna_Promessa_Com_Um_Versiculo_Com_Sucesso()
        {
            MockBibliaRepository.Setup(x => x.ObterQuantidadeCaixaPromessasAsync())
                .ReturnsAsync(Faker.RandomNumber.Next(1, 100));

            var caixaPromessas = new List<CaixaPromessas>
            {
                new CaixaPromessas
                {
                    CapituloId = 8,
                    Id = Faker.RandomNumber.Next(1, 100),
                    LivroId = Faker.RandomNumber.Next(1, 100),
                    NumeroVersiculo = 28
                }
            };
            MockBibliaRepository.Setup(x => x.ObterVersiculosDaCaixaPromessasAsync(It.IsAny<int>()))
                .ReturnsAsync(caixaPromessas);

            var livro = new Livro
            {
                Id = Faker.RandomNumber.Next(1, 100),
                Nome = "Romanos",
                Posicao = Faker.RandomNumber.Next(1, 50),
                TestamentoId = TestamentoEnum.NOVO
            };
            MockBibliaRepository.Setup(x => x.ObterLivroAsync(It.IsAny<int>()))
                .ReturnsAsync(livro);

            const string texto = @"Sabemos que Deus age em todas as coisas para o bem daqueles que o amam, 
                                dos que foram chamados de acordo com o seu propósito.";

            var versiculos = new List<Versiculo>
            {
                new Versiculo
                {
                    Capitulo = 8,
                    Id = Faker.RandomNumber.Next(1, 100),
                    Livro = livro,
                    Numero = 28,
                    Texto = texto,
                    VersaoId = Faker.RandomNumber.Next(1, 10),
                }
            };
            MockBibliaRepository.Setup(x => x.ObterVersiculosAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(versiculos);

            var app = new BibliaApp(MockBibliaRepository.Object);

            var resposta = await app.CaixinhaDePromessasAsync();

            Assert.NotNull(resposta);
            Assert.Equal(texto, resposta.Texto);
            Assert.Equal("Romanos 8:28", resposta.Referencia);
        }

        [Fact(DisplayName = "CaixinhaDePromessasAsync retorna com dois versículos com sucesso")]
        public async Task CaixinhaDePromessasAsync_Retorna_Promessa_Com_Dois_Versiculo_Com_Sucesso()
        {
            MockBibliaRepository.Setup(x => x.ObterQuantidadeCaixaPromessasAsync())
                .ReturnsAsync(Faker.RandomNumber.Next(1, 100));

            var caixaPromessas = new List<CaixaPromessas>
            {
                new CaixaPromessas
                {
                    CapituloId = 27,
                    Id = Faker.RandomNumber.Next(1, 100),
                    LivroId = Faker.RandomNumber.Next(1, 100),
                    NumeroVersiculo = 5
                },
                new CaixaPromessas
                {
                    CapituloId = 27,
                    Id = Faker.RandomNumber.Next(1, 100),
                    LivroId = Faker.RandomNumber.Next(1, 100),
                    NumeroVersiculo = 6
                }
            };
            MockBibliaRepository.Setup(x => x.ObterVersiculosDaCaixaPromessasAsync(It.IsAny<int>()))
                .ReturnsAsync(caixaPromessas);

            var livro = new Livro
            {
                Id = Faker.RandomNumber.Next(1, 100),
                Nome = "Provérbios",
                Posicao = Faker.RandomNumber.Next(1, 50),
                TestamentoId = TestamentoEnum.NOVO
            };
            MockBibliaRepository.Setup(x => x.ObterLivroAsync(It.IsAny<int>()))
                .ReturnsAsync(livro);

            var texto = new string[2]{ "Melhor é a repreensão aberta do que o amor encoberto.",
                                       "Fiéis são as feridas dum amigo; mas os beijos dum inimigo são enganosos." };

            var versiculos = new List<Versiculo>
            {
                new Versiculo
                {
                    Capitulo = 27,
                    Id = Faker.RandomNumber.Next(1, 100),
                    Livro = livro,
                    Numero = 5,
                    Texto = texto[0],
                    VersaoId = Faker.RandomNumber.Next(1, 10),
                },
                new Versiculo
                {
                    Capitulo = 27,
                    Id = Faker.RandomNumber.Next(1, 100),
                    Livro = livro,
                    Numero = 6,
                    Texto = texto[1],
                    VersaoId = Faker.RandomNumber.Next(1, 10),
                }
            };
            MockBibliaRepository.Setup(x => x.ObterVersiculosAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IEnumerable<int>>()))
                .ReturnsAsync(versiculos);

            var app = new BibliaApp(MockBibliaRepository.Object);

            var resposta = await app.CaixinhaDePromessasAsync();

            Assert.NotNull(resposta);
            Assert.Equal(string.Join(" ", texto), resposta.Texto);
            Assert.Equal("Provérbios 27:5-6", resposta.Referencia);
        }

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

            var resposta = await app.LivrosAsync(0);

            Assert.NotNull(resposta);
            Assert.Equal(livros.First().Id, resposta.First().Id);
            Assert.Equal(livros.First().Nome, resposta.First().Nome);
            Assert.Equal(livros.First().Posicao, resposta.First().Posicao);
            Assert.Equal((int)livros.First().TestamentoId, resposta.First().TestamentoId);
        }

        [Fact(DisplayName = "VersoesAsync retorna versões com sucesso")]
        public async Task VersoesAsync_Retorna_Versoes_Com_Sucesso()
        {
            var versoes = new List<Versao>
            {
                new Versao
                {
                    Id = Faker.RandomNumber.Next(1, 99999),
                    Nome = Faker.Name.First()
                }
            };

            MockBibliaRepository.Setup(x => x.ListarVersoesAsync())
                .ReturnsAsync(versoes);

            var app = new BibliaApp(MockBibliaRepository.Object);

            var resposta = await app.VersoesAsync();

            Assert.NotNull(resposta);
            Assert.Equal(versoes.First().Id, resposta.First().Id);
            Assert.Equal(versoes.First().Nome, resposta.First().Nome);
        }

        [Fact(DisplayName = "ObterVersiculoAsync retorna versículo com sucesso")]
        public async Task ObterVersiculoAsync_Retorna_Versiculo_Com_Sucesso()
        {
            var versiculo = new Versiculo
            {
                Capitulo = Faker.RandomNumber.Next(1, 30),
                Id = Faker.RandomNumber.Next(1, 100),
                Numero = Faker.RandomNumber.Next(1, 120),
                Texto = Faker.Lorem.Paragraph(1),
                VersaoId = Faker.RandomNumber.Next(1, 10)
            };

            MockBibliaRepository.Setup(x => x.ObterVersiculoAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(versiculo);

            var app = new BibliaApp(MockBibliaRepository.Object);

            var resposta = await app.ObterVersiculoAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            Assert.NotNull(resposta);
            Assert.Equal(versiculo.Capitulo, resposta.Capitulo);
            Assert.Equal(versiculo.Id, resposta.Id);
            Assert.Equal(versiculo.Numero, resposta.Numero);
            Assert.Equal(versiculo.Texto, resposta.Texto);
            Assert.Equal(versiculo.VersaoId, resposta.VersaoId);
        }

        [Fact(DisplayName = "ObterResumoLivrosAsync retorna versões com sucesso")]
        public async Task ObterResumoLivrosAsync_Retorna_Versoes_Com_Sucesso()
        {
            var resumos = new List<Resumo>
            {
                new Resumo
                {
                    TestamentoId = Faker.RandomNumber.Next(1, 30),
                    Testamento = Faker.Name.First(),
                    LivroId = Faker.RandomNumber.Next(1, 30),
                    Livro = Faker.Name.First(),
                    Posicao = Faker.RandomNumber.Next(1, 100),
                    Capitulos = Faker.RandomNumber.Next(1, 120),
                    Versiculos = Faker.RandomNumber.Next(1, 10)
                }
            };

            MockBibliaRepository.Setup(x => x.ListarResumosLivrosAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(resumos);

            var app = new BibliaApp(MockBibliaRepository.Object);

            var resposta = await app.ObterResumoLivrosAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            Assert.NotNull(resposta);
            Assert.Equal(resumos.First().TestamentoId, resposta.First().TestamentoId);
            Assert.Equal(resumos.First().Testamento, resposta.First().Testamento);
            Assert.Equal(resumos.First().LivroId, resposta.First().LivroId);
            Assert.Equal(resumos.First().Livro, resposta.First().Livro);
            Assert.Equal(resumos.First().Posicao, resposta.First().Posicao);
            Assert.Equal(resumos.First().Capitulos, resposta.First().Capitulos);
            Assert.Equal(resumos.First().Versiculos, resposta.First().Versiculos);
        }
        
        [Fact(DisplayName = "ObterQuantidadeVersiculosNoCapituloAsync retorna quantidade de versículos no capítulo com sucesso")]
        public async Task ObterQuantidadeVersiculosNoCapituloAsync_Retorna_Quantidade_De_Versiculos_No_Capitulo_Com_Sucesso()
        {
            var resumos = new List<Resumo>
            {
                new Resumo
                {
                    TestamentoId = Faker.RandomNumber.Next(1, 30),
                    Testamento = Faker.Name.First(),
                    LivroId = Faker.RandomNumber.Next(1, 30),
                    Livro = Faker.Name.First(),
                    Posicao = Faker.RandomNumber.Next(1, 100),
                    Capitulos = Faker.RandomNumber.Next(1, 120),
                    Versiculos = Faker.RandomNumber.Next(1, 10)
                }
            };

            MockBibliaRepository.Setup(x => x.ObterQuantidadeVersiculosNoCapituloAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(resumos);

            var app = new BibliaApp(MockBibliaRepository.Object);

            var resposta = await app.ObterResumoLivrosAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());

            Assert.NotNull(resposta);
        }
    }
}
