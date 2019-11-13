using Biblia.App.Interfaces;
using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biblia.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class VersiculosController : ControllerBase
    {
        private IBibliaApp _bibliaApp { get; }

        public VersiculosController(IBibliaApp bibliaApp)
        {
            _bibliaApp = bibliaApp;
        }

        /// <summary>
        /// Caixinha de promessas retorna de forma aleatória um versículo bíblico
        /// </summary>
        /// <returns>Versículo</returns>
        [HttpGet("CaixinhaPromessas")]
        public async Task<ActionResult<Versiculo>> CaixinhaPromessasAsync()
        {
            return await _bibliaApp.CaixinhaDePromessasAsync();
        }

        /// <summary>
        /// Recurso retorna os livros do antigo e novo testamento
        /// </summary>
        /// <returns>Lista com identificador e a descrição dos livros</returns>
        [HttpGet("livros")]
        public async Task<IEnumerable<Livro>> ObterLivrosAsync()
        {
            return await _bibliaApp.LivrosAsync();
        }

        /// <summary>
        /// Recurso retorna as versões disponíveis
        /// </summary>
        /// <returns>Lista com identificador e a descrição da versão</returns>
        [HttpGet("versoes")]
        public async Task<IEnumerable<Versao>> VersoesAsync()
        {
            return await _bibliaApp.VersoesAsync();
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
        public async Task<Versiculo> VersiculoAsync(int versaoId, int livroId, int capitulo, int numero)
        {
            return await _bibliaApp.ObterVersiculoAsync(versaoId, livroId, capitulo, numero);
        }
    }
}
