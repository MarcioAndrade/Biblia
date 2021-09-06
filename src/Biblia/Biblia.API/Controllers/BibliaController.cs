using Biblia.App.DTO;
using Biblia.App.Interfaces;
using Biblia.Repositorio.Excecoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblia.API.Controllers
{
    [ApiController]
    [Route("v1")]
    public class BibliaController : ControllerBase
    {
        private readonly ILogger<BibliaController> _logger;
        private IBibliaApp _bibliaApp { get; }

        public BibliaController(ILogger<BibliaController> logger, IBibliaApp bibliaApp)
        {
            _logger = logger;
            _bibliaApp = bibliaApp;
        }

        /// <summary>
        /// Caixinha de promessas retorna de forma aleatória um versículo bíblico
        /// </summary>
        /// <returns>Versículo</returns>
        [HttpGet("CaixinhaPromessas")]
        public async Task<ActionResult<CaixaPromessaViewModel>> CaixinhaPromessasAsync()
        {
            try
            {
                return await _bibliaApp.CaixinhaDePromessasAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Caixinha de promessas retorna os versículos cadastrados
        /// </summary>
        /// <returns>Versículo</returns>
        [HttpGet("caixinha-de-promessas/listar")]
        public async Task<IActionResult> ListarCaixinhaPromessasAsync([FromQuery] int? livroId, [FromQuery] int? capituloId, [FromQuery] int? versiculoId)
        {
            try
            {
                var lista = await _bibliaApp.ListarCaixinhaDePromessasAsync(livroId, capituloId, versiculoId);

                if (lista == null || !lista.Any())
                    return NoContent();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Caixinha de promessas retorna os versículos cadastrados
        /// </summary>
        /// <returns>Versículo</returns>
        [HttpPost("caixinha-de-promessas")]
        public async Task<IActionResult> CadastrarCaixinhaPromessasAsync([FromBody] IEnumerable<CadastroCaixinhaPromessaRequest> cadastroCaixinhasPromessa)
        {
            try
            {
                var lista = await _bibliaApp.CadastrarCaixinhaDePromessasAsync(cadastroCaixinhasPromessa);

                return Created("", lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Recurso retorna os livros do antigo e novo testamento
        /// </summary>
        /// <returns>Lista com identificador e a descrição dos livros</returns>
        [HttpGet("livros")]
        public async Task<IActionResult> ObterLivrosAsync([FromQuery] int? testamentoId)
        {
            try
            {
                return Ok(await _bibliaApp.LivrosAsync(testamentoId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Recurso retorna as versões bíblicas disponíveis na API
        /// </summary>
        /// <returns>Lista com identificador e a descrição da versão</returns>
        [HttpGet("versoes")]
        public async Task<IActionResult> VersoesAsync()
        {
            try
            {
                return Ok(await _bibliaApp.VersoesAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Recurso retorna um versículo dada uma versão, livro, capítulo e o número do versículo
        /// </summary>
        /// <param name="versaoId">Versão de 1 a 6. Pode ser obtida pelo recurso api/versoes</param>
        /// <param name="livroId">Número identificador do livro. Pode ser obtido pelo recurso api/livros</param>
        /// <param name="capitulo">Capítulo do livro</param>
        /// <param name="numero">Versículo</param>
        /// <returns>Versículo buscado. Caso não encontre o versículo com os parâmetros passados retorna vazio</returns>
        [HttpGet("versao/{versaoId}/livro/{livroId}/capitulo/{capitulo}/numero/{numero}")]
        public async Task<IActionResult> ObterVersiculoAsync(int versaoId, int livroId, int capitulo, int numero)
        {
            try
            {
                return Ok(await _bibliaApp.ObterVersiculoAsync(versaoId, livroId, capitulo, numero));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Recurso retorna os versículos do capítulo de uma versão e livro
        /// </summary>
        /// <param name="versaoId">Versão de 1 a 6. Pode ser obtida pelo recurso api/versoes</param>
        /// <param name="livroId">Número identificador do livro. Pode ser obtido pelo recurso api/livros</param>
        /// <param name="capitulo">Capítulo do livro</param>
        /// <returns>Versículos buscado. Caso não encontre o capítulo do livro com os parâmetros passados retorna vazio</returns>
        [HttpGet("versao/{versaoId}/livro/{livroId}/capitulo/{capitulo}/versiculos")]
        public async Task<IActionResult> ObterVersiculosAsync(int versaoId, int livroId, int capitulo)
        {
            try
            {
                return Ok(await _bibliaApp.ObterVersiculosAsync(versaoId, livroId, capitulo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Recurso retorna o resumo contando capítulos e versículos por livro. 
        /// Pode ser filtrado por testamento através da proriedade testamentoId, sendo 1 - Para o antigo testamento e 2 - Para o novo testamento
        /// Pode ser filtrado por livro através da propriedade livroId, passando o id do livro que se deseja o resumo
        /// </summary>
        /// <returns>Lista com identificador e a descrição dos livros</returns>
        [HttpGet("Resumo/versao/{versaoId}")]
        public async Task<IActionResult> ObterResumoLivrosAsync(int versaoId, [FromQuery] int? testamentoId, [FromQuery] int? livroId)
        {
            try
            {
                return Ok(await _bibliaApp.ObterResumoLivrosAsync(versaoId, testamentoId, livroId));
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Recurso retorna a quantidade de versículos em um capítulo de um livro de uma versão
        /// </summary>
        /// <param name="versaoId">Versão de 1 a 6. Pode ser obtida pelo recurso api/versoes</param>
        /// <param name="livroId">Número identificador do livro. Pode ser obtido pelo recurso api/livros</param>
        /// <param name="capitulo">Capítulo do livro que deseja consultar</param>
        /// <returns>Quantidade de versículos em capítulo</returns>
        [HttpGet("Versao/{versaoId}/Livro/{livroId}/Capitulo/{capitulo}/quantidade-versiculos")]
        public async Task<IActionResult> ObterQuantidadeVersiculosNoCapituloAsync(int versaoId, int livroId, int capitulo)
        {
            try
            {
                return Ok(await _bibliaApp.ObterQuantidadeVersiculosNoCapituloAsync(versaoId, livroId, capitulo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Recurso retorna versículos que contenham o texto passado como parâmetro
        /// </summary>
        /// <param name="texto">Texto usado para busca</param>
        /// <returns>Versículos que contenham a ocorrência do texto</returns>
        [HttpGet("buscar/{texto}")]
        public async Task<IActionResult> ObterVersiculosPorTextoAsync(string texto)
        {
            try
            {
                return Ok(await _bibliaApp.ObterPorTexto(texto));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Source, ex.Message, ex.StackTrace);
                return new StatusCodeResult(500);
            }
        }
    }
}
