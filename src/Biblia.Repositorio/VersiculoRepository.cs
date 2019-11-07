using Dapper;
using System.Linq;
using Biblia.Domain.Entidades;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Biblia.Repositorio
{
    public class VersiculoRepository : RepoBase, IVersiculoRepository
    {
        public VersiculoRepository(IConfiguration config) : base(config)
        {
        }

        public int CapitulosDoLivro(int id)
        {
            using (Conexao)
            {
                const string query = @"
                                SELECT 
                                    MAX(capitulo) 
                                FROM 
                                    Versiculos WHERE livroId = @cid";

                return Conexao.QueryFirstOrDefault<int>(query, new { cid = id });
            }
        }

        public int VersiculosNoCapituloDoLivro(int idLivro, int idCapitulo)
        {
            using (Conexao)
            {
                const string query = @"
                                SELECT 
                                    MAX(numero) 
                                FROM 
                                    Versiculos 
                                WHERE 
                                    livroId = @lid AND capitulo = @cid";

                return Conexao.QueryFirstOrDefault<int>(query, new { lid = idLivro, cid = idCapitulo });
            }
        }

        public IEnumerable<Livro> ListarTodos()
        {
            using (Conexao)
            {
                const string query = @"
                                SELECT 
                                    id, testamentoId, posicao, nome 
                                FROM 
                                    Livros
                            ";

                return Conexao.Query<Livro>(query);
            }
        }

        public IEnumerable<Versao> Versoes()
        {
            using (Conexao)
            {
                const string query = @"
                                SELECT 
                                    id,
                                    nome
                                FROM 
                                    Versoes
                            ";

                return Conexao.Query<Versao>(query);
            }
        }

        public Versiculo Obter(int versaoId, int livroId, int capitulo, int numero)
        {
            using (Conexao)
            {
                const string query = @"
                                SELECT 
                                    id, versaoId, capitulo, numero, texto 
                                FROM 
                                    Versiculos 
                                WHERE 
                                    versaoId = @vid 
                                    AND livroId = @lid 
                                    AND capitulo = @cid 
                                    AND numero = @nid;
                                
                                SELECT 
                                    id, nome, testamentoId, posicao 
                                FROM 
                                    Livros 
                                WHERE 
                                    id = @lid;
                            ";

                using (var result = Conexao.QueryMultiple(query, new { vid = versaoId, lid = livroId, cid = capitulo, nid = numero }))
                {
                    var versiculo = result.Read<Versiculo>().First();
                    versiculo.Livro = result.Read<Livro>().Single();

                    return versiculo;
                }
            }
        }
    }
}
