using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Biblia.Repositorio
{
    public class BibliaRepository : RepoBase, IBibliaRepository
    {
        public BibliaRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<int> ObterQuantidadeCapitulosDoLivroAsync(int livroId)
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"
                                SELECT 
                                    MAX(capitulo) 
                                FROM 
                                    Versiculos WHERE livroId = @cid";

                    return await Conexao.QueryFirstOrDefaultAsync<int>(query, new { cid = livroId });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<int> ObterQuantidadeVersiculosNoCapituloDoLivroAsync(int idLivro, int idCapitulo)
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"
                                SELECT 
                                    MAX(numero) 
                                FROM 
                                    Versiculos 
                                WHERE 
                                    livroId = @lid AND capitulo = @cid";

                    return await Conexao.QueryFirstOrDefaultAsync<int>(query, new { lid = idLivro, cid = idCapitulo });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Livro>> ListarLivrosAsync(int? testamentoId)
        {
            using (Conexao)
            {
                try
                {
                    var query = $@"
                                    SELECT 
                                        id, testamentoId, posicao, nome 
                                    FROM 
                                        Livros
                                ";
                    if (testamentoId.HasValue)
                    {
                        query += $@"WHERE 
                                        testamentoId = {testamentoId}
                                    ";
                    }

                    return await Conexao.QueryAsync<Livro>(query);

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<dynamic>> ListarResumosLivrosAsync(int versaoId, int? testamentoId, int? livroId)
        {
            using (Conexao)
            {
                try
                {
                    var query = $@"
                                        SELECT 
                                            t.Id AS TestamentoId, 
                                            t.nome AS Testamento, 
                                            l.Id AS LivroId, 
                                            l.nome AS Livro, 
                                            l.posicao AS Posicao, 
                                            COUNT(DISTINCT(v.capitulo)) AS Capitulos, 
                                            COUNT(v.Id) AS Versiculos
                                        FROM 
                                	        Versiculos v
                                        INNER JOIN 
                                	        Livros l ON v.livroId = l.Id
                                        INNER JOIN 
                                	        Testamentos t ON l.testamentoId = t.Id
                                        WHERE 
                                	        v.versaoId = @vid 
                                    ";

                    if (testamentoId.HasValue)
                    {
                        query += $@"        AND t.Id = {testamentoId} 
                                    ";
                    }

                    if (livroId.HasValue)
                    {
                        query += $@"        AND l.Id = {livroId} 
                                    ";
                    }

                    query += $@"        GROUP BY  
                                            t.Id, 
                                            t.nome, 
                                	        l.Id, 
                                            l.nome, 
                                            l.posicao
                                ";

                    return await Conexao.QueryAsync(query, new { vid = versaoId });
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<Versao>> ListarVersoesAsync()
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"
                                SELECT 
                                    id,
                                    nome
                                FROM 
                                    Versoes
                                ORDER BY
                                    id
                            ";

                    return await Conexao.QueryAsync<Versao>(query);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<Versiculo> ObterVersiculoAsync(int versaoId, int livroId, int capitulo, int numero)
        {
            using (Conexao)
            {
                try
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

                    using (var result = await Conexao.QueryMultipleAsync(query, new { vid = versaoId, lid = livroId, cid = capitulo, nid = numero }))
                    {
                        var versiculo = result.Read<Versiculo>().First();
                        versiculo.Livro = result.Read<Livro>().Single();

                        return versiculo;
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public async Task<int> ObterQuantidadeVersiculosNoCapituloAsync(int versaoId, int livroId, int capitulo)
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"
                                                SELECT     
                                                    COUNT(v.Id) AS versiculos
                                                FROM 
                                                    Versiculos v
                                                WHERE 
                                                    v.versaoId = @vid
                                                    AND v.livroId = @lid
                                                    AND v.capitulo = @c
                                            ";

                    return await Conexao.QueryFirstOrDefaultAsync<int>(query, new { vid = versaoId, lid = livroId, c = capitulo });

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
