using Biblia.App.Interfaces;
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
        public ActionResult<Versiculo> CaixinhaPromessas()
        {
            return _bibliaApp.CaixinhaDePromessas();
        }

        [HttpGet]
        [Route("api/livros")]
        public IEnumerable<Livro> ObterLivros()
        {
            return _bibliaApp.Livros();
        }

        [HttpGet("api/versoes")]
        public IEnumerable<Versao> Versoes()
        {
            return _bibliaApp.Versoes();
        }

        [HttpGet("api/versao/{versaoId}/livro/{livroId}/capitulo/{capitulo}/numero/{numero}")]
        public Versiculo Versiculo(int versaoId, int livroId, int capitulo, int numero)
        {
            return _bibliaApp.ObterVersiculo(versaoId, livroId, capitulo, numero);
        }
    }
}
