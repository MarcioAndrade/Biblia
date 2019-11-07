using Biblia.App.Interfaces;
using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Biblia.Controllers
{
    [ApiController]
    public class VersiculosController : ControllerBase
    {
        private IBibliaApp _bibliaApp { get; }

        public VersiculosController(IBibliaApp bibliaApp)
        {
            _bibliaApp = bibliaApp;
        }

        [HttpGet]
        [Route("api/CaixinhaPromessas")]
        public async Task<ActionResult<Versiculo>> CaixinhaPromessasAsync()
        {
            return await _bibliaApp.CaixinhaDePromessasAsync();
        }

        [HttpGet]
        [Route("api/livros")]
        public async Task<IEnumerable<Livro>> ObterLivrosAsync()
        {
            return await _bibliaApp.LivrosAsync();
        }

        [HttpGet("api/versoes")]
        public async Task<IEnumerable<Versao>> VersoesAsync()
        {
            return await _bibliaApp.VersoesAsync();
        }

        [HttpGet("api/versao/{versaoId}/livro/{livroId}/capitulo/{capitulo}/numero/{numero}")]
        public async Task<Versiculo> VersiculoAsync(int versaoId, int livroId, int capitulo, int numero)
        {
            return await _bibliaApp.ObterVersiculoAsync(versaoId, livroId, capitulo, numero);
        }
    }
}
