using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using System.Data;
using Dapper;

namespace BE.Infra.Dapper;

public class DPProdutoRepository : IProdutoRepository
{
    private readonly IProvedorDados _provedorDados;

    public DPProdutoRepository(IProvedorDados provedorDados)
    {
        _provedorDados = provedorDados;
    }

    public async Task<bool> AdicionarAsync(Produto produto)
    {
        const string query = @"INSERT INTO TB_PRODUTO(FK_IDUNIDADE_MEDIDA, ATIVO, QUANTIDADE, PRECO, NOME, IMAGEM_NOME, IMAGEM_TIPO, CODIGO_BARRAS, DATA_CRIACAO, DATA_ATUALIZACAO) 
                               VALUES(@UNIDADE_MEDIDAID, @ATIVO, @QUANTIDADE, @PRECO, @NOME, @IMAGEM_NOME, @IMAGEM_TIPO, @CODIGO_BARRAS, @DATA_CRIACAO, @DATA_ATUALIZACAO);";

        using (IDbConnection conexao = _provedorDados.BoxEvenConexao())
        {
            var parametros = new DynamicParameters();
            parametros.Add("@UNIDADE_MEDIDAID", produto.UnidadeMedidaId, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@ATIVO", produto.Ativo, DbType.Boolean, ParameterDirection.Input);
            parametros.Add("@QUANTIDADE", produto.Quantidade, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@PRECO", produto.Preco, DbType.Decimal, ParameterDirection.Input);
            parametros.Add("@NOME", produto.Nome, DbType.String, ParameterDirection.Input);
            parametros.Add("@IMAGEM_NOME", produto.ImagemNome, DbType.String, ParameterDirection.Input);
            parametros.Add("@IMAGEM_TIPO", produto.ImagemTipo, DbType.String, ParameterDirection.Input);
            parametros.Add("@CODIGO_BARRAS", produto.CodigoBarras, DbType.String, ParameterDirection.Input);
            parametros.Add("@DATA_CRIACAO", produto.Criacao, DbType.DateTime, ParameterDirection.Input);
            parametros.Add("@DATA_ATUALIZACAO", produto.Atualizacao, DbType.DateTime, ParameterDirection.Input);

            int linhasAfetadas = await conexao.ExecuteAsync(query, parametros);
            if (linhasAfetadas <= 0)
            {
                return false;
            }

            return true;
        }
    }

    public async Task<bool> AlterarQuantidade(int quantidade, int id, DateTime dataAtualizacao)
    {
        const string query = @"
                                UPDATE [BD_BOXEVEN].[dbo].[TB_PRODUTO] 
                                SET 
                                   [QUANTIDADE] = @QUANTIDADE
                                  ,[DATA_ATUALIZACAO] = @DATA_ATUALIZACAO
                                WHERE ID_PRODUTO = @ID_PRODUTO;
                              ";

        using (IDbConnection conexao = _provedorDados.BoxEvenConexao())
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ID_PRODUTO", id, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@QUANTIDADE", quantidade, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@DATA_ATUALIZACAO", dataAtualizacao, DbType.DateTime, ParameterDirection.Input);

            int linhasAfetadas = await conexao.ExecuteAsync(query, parametros);
            if (linhasAfetadas <= 0)
            {
                return false;
            }

            return true;
        }
    }

    public async Task<bool> AtualizarAsync(Produto produto)
    {
        const string query = @"
                                UPDATE [BD_BOXEVEN].[dbo].[TB_PRODUTO] 
                                SET 
                                   [FK_IDUNIDADE_MEDIDA] = IsNull(@UNIDADE_MEDIDAID, [FK_IDUNIDADE_MEDIDA])
                                  ,[ATIVO] =  IsNull(@ATIVO, [ATIVO])
                                  ,[PRECO] = IsNull(@PRECO, [PRECO])
                                  ,[NOME] = IsNull(@NOME, [NOME])
                                  ,[IMAGEM_TIPO] = @IMAGEM_TIPO
                                  ,[IMAGEM_NOME] = @IMAGEM_NOME
                                  ,[CODIGO_BARRAS] = @CODIGO_BARRAS
                                  ,[DATA_ATUALIZACAO] = @DATA_ATUALIZACAO
                                WHERE ID_PRODUTO = @ID_PRODUTO;
                              ";

        using (IDbConnection conexao = _provedorDados.BoxEvenConexao())
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ID_PRODUTO", produto.ProdutoId, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@UNIDADE_MEDIDAID", produto.UnidadeMedidaId, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@ATIVO", produto.Ativo, DbType.Boolean, ParameterDirection.Input);
            parametros.Add("@PRECO", produto.Preco, DbType.Decimal, ParameterDirection.Input);
            parametros.Add("@NOME", produto.Nome, DbType.String, ParameterDirection.Input);
            parametros.Add("@IMAGEM_TIPO", produto.ImagemTipo, DbType.String, ParameterDirection.Input);
            parametros.Add("@IMAGEM_NOME", produto.ImagemNome, DbType.String, ParameterDirection.Input);
            parametros.Add("@CODIGO_BARRAS", produto.CodigoBarras, DbType.String, ParameterDirection.Input);
            parametros.Add("@DATA_ATUALIZACAO", produto.Atualizacao, DbType.DateTime, ParameterDirection.Input);

            int linhasAfetadas = await conexao.ExecuteAsync(query, parametros);
            if (linhasAfetadas <= 0)
            {
                return false;
            }

            return true;
        }
    }

    public async Task<Produto> ListarPorIdAsync(int id)
    {
        string query = @"SELECT
                            ID_PRODUTO AS ProdutoId
                            ,[ATIVO] AS Ativo
                            ,[QUANTIDADE] AS Quantidade
                            ,[PRECO] AS Preco
                            ,[NOME] AS Nome
                            ,[IMAGEM_TIPO] AS ImagemTipo
                            ,[IMAGEM_NOME] AS ImagemNome
                            ,[CODIGO_BARRAS] AS CodigoBarras
                            ,[DATA_CRIACAO] AS Criacao
                            ,[DATA_ATUALIZACAO] AS Atualizacao
                            ,ID_UNIDADE_MEDIDA AS UnidadeMedidaId
                            ,[DESCRICAO] AS Descricao
                            ,ID_LOCALIZACAO AS LocalizacaoId
                            ,[ANDAR] AS Andar
                            ,[CORREDOR] AS Corredor
                            ,[LADO] AS Lado
                            ,[PRATELEIRA] AS Prateleira
                            ,[VAO] AS Vao
                          FROM [BD_BOXEVEN].[dbo].[TB_PRODUTO] WITH (NOLOCK)
                          LEFT JOIN [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO] ON(ID_PRODUTO = FK_IDPRODUTO)
                          LEFT JOIN [BD_BOXEVEN].[dbo].[TB_UNIDADE_MEDIDA] ON(ID_UNIDADE_MEDIDA = FK_IDUNIDADE_MEDIDA)
                          WHERE ID_PRODUTO = @ID_PRODUTO;";

        using (IDbConnection conexao = _provedorDados.BoxEvenConexao())
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ID_PRODUTO", id, DbType.Int32, ParameterDirection.Input);

            Produto? produto = null;

            var resultado = await conexao.QueryAsync<Produto, UnidadeMedida, Localizacao, Produto>(query, (pr, um, ll) =>
            {
                if (produto == null || produto.ProdutoId != pr.ProdutoId)
                {
                    produto = new Produto(pr.ProdutoId, pr.Ativo, pr.Quantidade, pr.Preco, pr.Nome, pr.ImagemTipo, pr.ImagemNome, pr.CodigoBarras, pr.Criacao, pr.Atualizacao, um, new List<Localizacao>());
                    if(ll != null)
                    {
                        produto.Localizacoes.Add(ll);
                    }
                }
                else
                {
                    if (ll != null)
                    {
                        produto.Localizacoes.Add(ll);
                    }
                }

                return produto;

            }, parametros, splitOn: "ProdutoId, UnidadeMedidaId, LocalizacaoId");

            return resultado.Distinct().SingleOrDefault();
        }
    }

    public async Task<List<Produto>> ListarTodosAsync(int itensPagina, int pagina, bool? status, int? id, string? nome)
    {
        using (IDbConnection conexao = _provedorDados.BoxEvenConexao())
        {
            //var produtoDictionary = new Dictionary<int, Produto>();

            //var resultado = await conexao.QueryAsync<Produto, UnidadeMedida, Localizacao, Produto>(query, (produto, unidadeMedida, localizacao) =>
            // {
            //     if (!produtoDictionary.TryGetValue(produto.ProdutoId, out Produto produtoOut))
            //     {
            //         produtoOut = produto;
            //         produtoOut.UnidadeMedida = unidadeMedida;
            //         produtoOut.Localizacoes = produtoOut.Localizacoes ?? new List<Localizacao>();
            //         produtoDictionary.Add(produtoOut.ProdutoId, produtoOut);
            //     }

            //     produtoOut.Localizacoes.Add(localizacao);
            //     return produtoOut;

            // }, splitOn: "ProdutoId, UnidadeMedidaId, LocalizacaoId");

            //return resultado.Distinct().AsList(); como isso funciona??????????????????
            string subQuery =
            @"
                SELECT [FK_IDUNIDADE_MEDIDA]
		              ,[ID_PRODUTO]
                      ,[ATIVO]
                      ,[QUANTIDADE]
                      ,[PRECO]
                      ,[NOME]
                      ,[IMAGEM_TIPO]
                      ,[IMAGEM_NOME]
                      ,[CODIGO_BARRAS]
                      ,[DATA_CRIACAO]
                      ,[DATA_ATUALIZACAO]
	            FROM [BD_BOXEVEN].[dbo].[TB_PRODUTO] WITH (NOLOCK)
            ";

            DynamicParameters parametro = new DynamicParameters();
            
            if (!(string.IsNullOrEmpty(nome) || string.IsNullOrWhiteSpace(nome) || nome.Length < 3))
            {
                subQuery += " WHERE [NOME] LIKE @NOME + '%' ";
                parametro.Add("@NOME", nome, DbType.String, ParameterDirection.Input);

                if (status != null)
                {
                    subQuery += " AND [ATIVO] = @STATUS";
                    parametro.Add("@STATUS", status, DbType.Boolean, ParameterDirection.Input);
                }    
            }
            else if(id > 0)
            {
                subQuery += " WHERE ID_PRODUTO = @ID_PRODUTO";
                parametro.Add("@ID_PRODUTO", id, DbType.Int32, ParameterDirection.Input);
            }
            else if (status != null)
            {
                subQuery += " WHERE [ATIVO] = @STATUS";
                parametro.Add("@STATUS", status, DbType.Boolean, ParameterDirection.Input);
            }

            subQuery += " ORDER BY ID_PRODUTO ASC OFFSET @PAGINA ROWS FETCH NEXT @ITENSPAGINA ROWS ONLY ";
            parametro.Add("@ITENSPAGINA", itensPagina, DbType.Int32, ParameterDirection.Input);
            parametro.Add("@PAGINA", (pagina - 1) * itensPagina, DbType.Int32, ParameterDirection.Input);

            string query =
            $@"
                SELECT [ID_PRODUTO] AS ProdutoId
                      ,[ATIVO] AS Ativo
                      ,[QUANTIDADE] AS Quantidade
                      ,[PRECO] AS Preco
                      ,[NOME] AS Nome
                      ,[IMAGEM_TIPO] AS ImagemTipo
                      ,[IMAGEM_NOME] AS ImagemNome
                      ,[CODIGO_BARRAS] AS CodigoBarras
                      ,[DATA_CRIACAO] AS Criacao
                      ,[DATA_ATUALIZACAO] AS Atualizacao
                      ,[ID_UNIDADE_MEDIDA] AS UnidadeMedidaId
                      ,[DESCRICAO] AS Descricao
                      ,[ID_LOCALIZACAO] AS LocalizacaoId
	                  ,[ANDAR] AS Andar
                      ,[CORREDOR] AS Corredor
                      ,[LADO] AS Lado
                      ,[PRATELEIRA] AS Prateleira
                      ,[VAO] AS Vao
                FROM
                (
	                {subQuery}
                ) AS P
                LEFT JOIN [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO] ON(ID_PRODUTO = FK_IDPRODUTO)
                LEFT JOIN [BD_BOXEVEN].[dbo].[TB_UNIDADE_MEDIDA] ON(ID_UNIDADE_MEDIDA = FK_IDUNIDADE_MEDIDA);
            ";

            Produto? produto = null;

            var resultado = await conexao.QueryAsync<Produto, UnidadeMedida, Localizacao, Produto>(query, (pr, um, ll) =>
            {
                if (produto == null || produto.ProdutoId != pr.ProdutoId)
                {
                    produto = new Produto(pr.ProdutoId, pr.Ativo, pr.Quantidade, pr.Preco, pr.Nome, pr.ImagemTipo, pr.ImagemNome, pr.CodigoBarras, pr.Criacao, pr.Atualizacao, um, new List<Localizacao>());
                    produto.Localizacoes.Add(ll);
                }
                else
                {
                    produto.Localizacoes.Add(ll);
                }

                return produto;

            }, parametro, splitOn: "ProdutoId, UnidadeMedidaId, LocalizacaoId");

            return resultado.Distinct().AsList();
        }
    }

    public async Task<List<Produto>> ListarTodosDesalocadosAsync()
    {
        string query = @"
            SELECT
               ID_PRODUTO AS ProdutoId
               ,[ATIVO] AS Ativo
               ,[QUANTIDADE] AS Quantidade
               ,[PRECO] AS Preco
               ,[NOME] AS Nome
               ,[IMAGEM_TIPO] AS ImagemTipo
               ,[IMAGEM_NOME] AS ImagemNome
               ,[CODIGO_BARRAS] AS CodigoBarras
               ,[DATA_CRIACAO] AS Criacao
               ,[DATA_ATUALIZACAO] AS Atualizacao
               ,ID_UNIDADE_MEDIDA AS UnidadeMedidaId
               ,[DESCRICAO] AS Descricao
               ,ID_LOCALIZACAO AS LocalizacaoId
               ,[ANDAR] AS Andar
               ,[CORREDOR] AS Corredor
               ,[LADO] AS Lado
               ,[PRATELEIRA] AS Prateleira
               ,[VAO] AS Vao
            FROM [BD_BOXEVEN].[dbo].[TB_PRODUTO] WITH (NOLOCK)
            LEFT JOIN [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO] ON(ID_PRODUTO = FK_IDPRODUTO)
            LEFT JOIN [BD_BOXEVEN].[dbo].[TB_UNIDADE_MEDIDA] ON(ID_UNIDADE_MEDIDA = FK_IDUNIDADE_MEDIDA)
            WHERE FK_IDPRODUTO IS NULL;
        ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        Produto? produto = null;

        var resultado = await conexao.QueryAsync<Produto, UnidadeMedida, Localizacao, Produto>(query, (pr, um, ll) =>
        {
            if (produto == null || produto.ProdutoId != pr.ProdutoId)
            {
                produto = new Produto(pr.ProdutoId, pr.Ativo, pr.Quantidade, pr.Preco, pr.Nome, pr.ImagemTipo, pr.ImagemNome, pr.CodigoBarras, pr.Criacao, pr.Atualizacao, um, new List<Localizacao>());
                produto.Localizacoes.Add(ll);
            }
            else
            {
                produto.Localizacoes.Add(ll);
            }

            return produto;

        }, splitOn: "ProdutoId, UnidadeMedidaId, LocalizacaoId");

        return resultado.Distinct().AsList();
    }
}