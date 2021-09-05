using Dapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using Biblia.Domain.Entidades;
using System.Collections.Generic;
using Biblia.Repositorio.Excecoes;
using Microsoft.Extensions.Configuration;

namespace Biblia.Repositorio
{
    public class BibliaRepository : RepoBase, IBibliaRepository
    {
        public BibliaRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<int> ObterQuantidadeCapitulosDoLivroAsync(int id)
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

                    return await Conexao.QueryFirstOrDefaultAsync<int>(query, new { cid = id });
                }
                catch (Exception ex)
                {
                    throw new ObterQuantidadeCapitulosDoLivroException(ex.Message);
                }
            }
        }

        public async Task<int> ObterQuantidadeVersiculosNoCapituloDoLivroAsync(int livro, int capitulo)
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

                    return await Conexao.QueryFirstOrDefaultAsync<int>(query, new { lid = livro, cid = capitulo });
                }
                catch (Exception ex)
                {
                    throw new ObterQuantidadeVersiculosNoCapituloDoLivroException(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<Livro>> ListarLivrosAsync(int? testamento)
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
                    if (testamento.HasValue)
                    {
                        query += $@"WHERE 
										testamentoId = {testamento}
									";
                    }

                    return await Conexao.QueryAsync<Livro>(query);

                }
                catch (Exception ex)
                {
                    throw new ListarLivrosException(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<Resumo>> ListarResumosLivrosAsync(int versao, int? testamento, int? livro)
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

                    if (testamento.HasValue)
                    {
                        query += $@"        AND t.Id = {testamento} 
									";
                    }

                    if (livro.HasValue)
                    {
                        query += $@"        AND l.Id = {livro} 
									";
                    }

                    query += $@"        GROUP BY  
											t.Id, 
											t.nome, 
											l.Id, 
											l.nome, 
											l.posicao
								";

                    return await Conexao.QueryAsync<Resumo>(query, new { vid = versao });
                }
                catch (Exception ex)
                {
                    throw new ListarResumosLivrosException(ex.Message);
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
                    throw new ListarVersoesException(ex.Message);
                }
            }
        }

        public async Task<Versiculo> ObterVersiculoAsync(int versao, int livro, int capitulo, int numero)
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

                    using (var result = await Conexao.QueryMultipleAsync(query, new { vid = versao, lid = livro, cid = capitulo, nid = numero }))
                    {
                        var versiculo = result.Read<Versiculo>().First();
                        versiculo.Livro = result.Read<Livro>().Single();

                        return versiculo;
                    }

                }
                catch (Exception ex)
                {
                    throw new ObterVersiculoException(ex.Message);
                }
            }
        }

        public async Task<dynamic> ObterQuantidadeVersiculosNoCapituloAsync(int versao, int livro, int capitulo)
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"
												SELECT     
													COUNT(v.Id) AS Versiculos, 
													v.versaoId AS Versao, 
													v.livroId AS Livro, 
													v.capitulo AS Capitulo
												FROM 
													Versiculos v
												WHERE 
													v.versaoId = @vid
													AND v.livroId = @lid
													AND v.capitulo = @c
												GROUP BY
													v.versaoId, v.livroId, v.capitulo
											";

                    return await Conexao.QueryFirstOrDefaultAsync<dynamic>(query, new { vid = versao, lid = livro, c = capitulo });

                }
                catch (Exception ex)
                {
                    throw new ObterQuantidadeVersiculosNoCapituloException(ex.Message);
                }
            }
        }

        public async Task<int> ObterQuantidadeCaixaPromessasAsync()
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"
												SELECT     
													MAX(id)
												FROM 
													CaixaPromessas c
											";

                    return await Conexao.QueryFirstOrDefaultAsync<int>(query);

                }
                catch (Exception ex)
                {
                    throw new ObterQuantidadeCaixaPromessasException(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<CaixaPromessas>> ObterVersiculosDaCaixaPromessasAsync(int caixaPromessa)
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"
												SELECT
													id, 
													livroId, 
													capituloId, 
													numeroVersiculo
												FROM 
													CaixaPromessas v
												WHERE 
													v.id = @caixaPromessa
										  ";

                    return await Conexao.QueryAsync<CaixaPromessas>(query, new { caixaPromessa });

                }
                catch (Exception ex)
                {
                    throw new ObterVersiculosDaCaixaPromessasException(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<Versiculo>> ObterVersiculosAsync(int versao, int livro, int capitulo, IEnumerable<int> numeros)
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
									AND numero IN @nid;
								
								SELECT 
									id, nome, testamentoId, posicao 
								FROM 
									Livros 
								WHERE 
									id = @lid;
										  ";

                    return await Conexao.QueryAsync<Versiculo>(query, new { vid = versao, lid = livro, cid = capitulo, nid = numeros });

                }
                catch (Exception ex)
                {
                    throw new ObterVersiculosException(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<Versiculo>> ObterVersiculosAsync(int versaoId, int livroId, int capitulo)
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
                                ORDER BY
                                    capitulo, numero;
								
								SELECT 
									id, nome, testamentoId, posicao 
								FROM 
									Livros 
								WHERE 
									id = @lid;
							";

                    using (var result = await Conexao.QueryMultipleAsync(query, new { vid = versaoId, lid = livroId, cid = capitulo }))
                    {
                        var versiculos = result.Read<Versiculo>();
                        var livro = result.Read<Livro>().Single();

                        foreach (var verso in versiculos)
                            verso.Livro = livro;

                        return versiculos;
                    }
                }
                catch (Exception ex)
                {
                    throw new ObterVersiculoException(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<Versiculo>> ObterVersiculosAsync(string texto)
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"
                                                SELECT 
	                                                v.id, 
	                                                v.versaoId,
	                                                v.capitulo, 
	                                                v.numero, 
	                                                v.texto,
                                                    l.id,
	                                                l.nome, 
	                                                l.testamentoId,
	                                                l.posicao
                                                FROM 
	                                                Livros l 
                                                INNER JOIN 
	                                                Versiculos v ON l.id = v.livroId 
                                                WHERE 
	                                                UPPER(v.texto) LIKE @txt
								            ";

                    var likeTrick = $"%{texto.ToUpperInvariant()}%";
                    return await Conexao.QueryAsync<Versiculo, Livro, Versiculo>(query, 
                        map: (versiculo, livro) =>
                        {
                            versiculo.Livro = livro;
                            return versiculo;
                        },
                        new { txt = likeTrick },
                    splitOn: "id,id");
                }
                catch (Exception ex)
                {
                    throw new ObterVersiculoException(ex.Message);
                }
            }
        }

        public async Task<Livro> ObterLivroAsync(int livro)
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"
												SELECT 
													id, 
													testamentoId,
													posicao,
													nome
												FROM 
													Livros
												WHERE 
													id =  @lid;
										  ";

                    return await Conexao.QueryFirstOrDefaultAsync<Livro>(query, new { lid = livro });

                }
                catch (Exception ex)
                {
                    throw new ObterLivroException(ex.Message);
                }
            }
        }

        public async Task<IEnumerable<dynamic>> ObterCaixinhaDePromessaAsync(int? livro, int? capitulo, int? versiculo)
        {
            using (Conexao)
            {
                try
                {
                    var query = @"
									SELECT 
										c.id, 
										l.nome as livro, 
										v.capitulo, 
										v.numero as versiculo, 
										v.texto
									FROM 
										CaixaPromessas c
									INNER JOIN 
										Livros l ON c.livroId = l.id
									INNER JOIN 
										Versiculos v ON l.id = v.livroId AND v.capitulo = c.capituloId AND v.numero = c.numeroVersiculo
									WHERE 
										versaoId = @versaoId
								";

                    var dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("versaoId", 5);

                    if (livro.HasValue)
                    {
                        dynamicParameters.Add("livroId", livro);
                        query += " AND c.livroId = @livroId ";
                    }

                    if (capitulo.HasValue)
                    {
                        dynamicParameters.Add("capituloId", capitulo);
                        query += " AND c.capituloId = @capituloId ";
                    }

                    if (versiculo.HasValue)
                    {
                        dynamicParameters.Add("versiculoId", versiculo);
                        query += " AND c.numeroVersiculo = @versiculoId ";
                    }

                    query += " ORDER BY 2, 3, 4 ";

                    return await Conexao.QueryAsync<dynamic>(query, dynamicParameters);

                }
                catch (Exception ex)
                {
                    throw new ObterCaixinhaDePromessaException(ex.Message);
                }
            }
        }

        public async Task CadastrarCaixinhaDePromessaAsync(int id, int livroId, int capituloId, int versiculoId)
        {
            using (Conexao)
            {
                try
                {
                    const string query = @"												
												INSERT INTO 
													CaixaPromessas (id, livroId, capituloId, numeroVersiculo)
												VALUES
													(@id, @livroId, @capituloId, @versiculoId)												
										  ";

                    await Conexao.ExecuteAsync(query, new { id, livroId, capituloId, versiculoId });

                }
                catch (Exception ex)
                {
                    throw new CadastrarCaixinhaDePromessaException(ex.Message);
                }
            }
        }
    }
}
