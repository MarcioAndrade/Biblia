using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Biblia.Repositorio
{
    public class RepoBase
    {
        private IConfiguration _configuracoes;

        public RepoBase(IConfiguration config)
        {
            _configuracoes = config;
        }

        public MySqlConnection Conexao
        {
            get
            {
                return new MySqlConnection(_configuracoes.GetConnectionString("BibliaConn"));
            }
        }
    }
}
