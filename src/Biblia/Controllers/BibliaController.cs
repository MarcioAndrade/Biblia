using System;
using Biblia.App.DTO;
using Biblia.App.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Biblia.Repositorio.Excecoes;
using System.Linq;

namespace Biblia.Controllers
{
    /// <summary>
    /// API fornece recursos de consulta a livros, versículos e dados estatísticos em seis versões diferentes em português
    /// </summary>
    [Route("v1")]
    [ApiController]
    public class BibliaController : ControllerBase
    {
        private IBibliaApp _bibliaApp { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bibliaApp">Parâmetro inserido por injeção de dependência. Pode ser usada para mockar os dados de consulta para teste</param>
        public BibliaController(IBibliaApp bibliaApp)
        {
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
            catch (BibliaException)
            {
                // Logar Excecao

                return new StatusCodeResult(500);
            }
            catch (Exception)
            {
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
            catch (BibliaException)
            {
                // Logar Excecao

                return StatusCode(500, "Falha ao consultar lista de caixinha de promessas");
            }
            catch (Exception)
            {
                return StatusCode(500, "Falha ao consultar lista de caixinha de promessas");
            }
        }

        /// <summary>
        /// Caixinha de promessas retorna os versículos cadastrados
        /// </summary>
        /// <returns>Versículo</returns>
        [HttpPost("caixinha-de-promessas")]
        public async Task<IActionResult> CadastrarCaixinhaPromessasAsync([FromBody]IEnumerable<CadastroCaixinhaPromessaRequest> cadastroCaixinhasPromessa)
        {
            try
            {
                var lista = await _bibliaApp.CadastrarCaixinhaDePromessasAsync(cadastroCaixinhasPromessa);

                return Created("", lista);
            }
            catch (BibliaException)
            {
                // Logar Excecao

                return StatusCode(500, "Falha ao consultar lista de caixinha de promessas");
            }
            catch (Exception)
            {
                return StatusCode(500, "Falha ao consultar lista de caixinha de promessas");
            }
        }

        /// <summary>
        /// Recurso retorna os livros do antigo e novo testamento
        /// </summary>
        /// <returns>Lista com identificador e a descrição dos livros</returns>
        [HttpGet("livros")]
        public async Task<IEnumerable<LivroViewModel>> ObterLivrosAsync([FromQuery] int? testamentoId)
        {
            try
            {
                return await _bibliaApp.LivrosAsync(testamentoId);
            }
            catch (BibliaException)
            {
                // Logar Excecao

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Recurso retorna as versões bíblicas disponíveis na API
        /// </summary>
        /// <returns>Lista com identificador e a descrição da versão</returns>
        [HttpGet("versoes")]
        public async Task<IEnumerable<VersaoViewModel>> VersoesAsync()
        {
            try
            {
                return await _bibliaApp.VersoesAsync();
            }
            catch (BibliaException)
            {
                // Logar Excecao

                return null;
            }
            catch (Exception)
            {
                throw;
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
        public async Task<VersiculoViewModel> ObterVersiculoAsync(int versaoId, int livroId, int capitulo, int numero)
        {
            try
            {
                return await _bibliaApp.ObterVersiculoAsync(versaoId, livroId, capitulo, numero);
            }
            catch (BibliaException)
            {
                // Logar Excecao

                return null;
            }
            catch (Exception)
            {
                throw;
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
        public async Task<IEnumerable<VersiculoViewModel>> ObterVersiculosAsync(int versaoId, int livroId, int capitulo)
        {
            try
            {
                return await _bibliaApp.ObterVersiculosAsync(versaoId, livroId, capitulo);
            }
            catch (BibliaException)
            {
                // Logar Excecao

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Recurso retorna o resumo contando capítulos e versículos por livro. 
        /// Pode ser filtrado por testamento através da proriedade testamentoId, sendo 1 - Para o antigo testamento e 2 - Para o novo testamento
        /// Pode ser filtrado por livro através da propriedade livroId, passando o id do livro que se deseja o resumo
        /// </summary>
        /// <returns>Lista com identificador e a descrição dos livros</returns>
        [HttpGet("Resumo/versao/{versaoId}")]
        public async Task<IEnumerable<ResumoViewModel>> ObterResumoLivrosAsync(int versaoId, [FromQuery] int? testamentoId, [FromQuery] int? livroId)
        {
            try
            {
                return await _bibliaApp.ObterResumoLivrosAsync(versaoId, testamentoId, livroId);
            }
            catch (BibliaException)
            {
                // Logar Excecao

                return null;
            }
            catch (Exception)
            {
                throw;
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
        public async Task<QuantidadeVersiculosCapitulo> ObterQuantidadeVersiculosNoCapituloAsync(int versaoId, int livroId, int capitulo)
        {
            try
            {
                return await _bibliaApp.ObterQuantidadeVersiculosNoCapituloAsync(versaoId, livroId, capitulo);
            }
            catch (BibliaException)
            {
                // Logar Excecao

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
