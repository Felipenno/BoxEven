using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Infra.Dapper;

public class DPLocalizacaoRepository : ILocalizacaoRepository
{
    private readonly IProvedorDados _provedorDados;

    public DPLocalizacaoRepository(IProvedorDados provedorDados)
    {
        _provedorDados = provedorDados;
    }

    public async Task<bool> AdicionarAsync(Localizacao localizacao)
    {
        const string query =
        @"
            INSERT INTO TB_LOCALIZACAO(FK_IDPRODUTO, ANDAR, CORREDOR, LADO, PRATELEIRA, VAO) 
            VALUES(@ProdutoId, @Andar, @Corredor, @Lado, @Prateleira, @Vao);
        ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, localizacao);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> AtualizarAsync(Localizacao localizacao)
    {
        const string query =
        @"
            UPDATE TB_LOCALIZACAO 
            SET 
                 [FK_IDPRODUTO] = @ProdutoId
                ,[ANDAR] = @Andar
                ,[CORREDOR] = @Corredor
                ,[LADO] = @Lado
                ,[PRATELEIRA] = @Prateleira
                ,[VAO] = @Vao
            WHERE ID_LOCALIZACAO = @LocalizacaoId;
        ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, localizacao);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<Localizacao> ListarPorIdAsync(int id)
    {
        string query =
        @"
            SELECT 
                [ID_LOCALIZACAO] AS LocalizacaoId
               ,[ANDAR]
               ,[CORREDOR]
               ,[LADO]
               ,[PRATELEIRA]
               ,[VAO]
               ,[ID_PRODUTO] AS ProdutoId
               ,[ATIVO]
               ,[QUANTIDADE]
               ,[PRECO]
               ,[NOME]
               ,[IMAGEM_TIPO] AS ImagemTipo
               ,[IMAGEM_NOME] AS ImagemNome
               ,[CODIGO_BARRAS] AS CodigoBarras
               ,[DATA_CRIACAO] AS Criacao
               ,[DATA_ATUALIZACAO] AS Atualizacao
               ,[ID_UNIDADE_MEDIDA] AS UnidadeMedidaId
               ,[DESCRICAO]
            FROM [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO]
            LEFT JOIN TB_PRODUTO ON  ID_PRODUTO = FK_IDPRODUTO
            LEFT JOIN TB_UNIDADE_MEDIDA ON FK_IDUNIDADE_MEDIDA = ID_UNIDADE_MEDIDA
            WHERE ID_LOCALIZACAO = @ID_LOCALIZACAO;
        ";

        var parametros = new DynamicParameters();
        parametros.Add("@ID_LOCALIZACAO", id, DbType.Int32, ParameterDirection.Input);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        var resultado = await conexao.QueryAsync<Localizacao, Produto, UnidadeMedida, Localizacao>(query, (ll, pr, um) =>
        {
            Produto produto;
            Localizacao localizacao;

            if (pr != null)
            {
                produto = new Produto(pr.ProdutoId, pr.Ativo, pr.Quantidade, pr.Preco, pr.Nome, pr.ImagemTipo, pr.ImagemNome, pr.CodigoBarras, pr.Criacao, pr.Atualizacao, um);
                localizacao = new Localizacao(ll.LocalizacaoId, ll.Andar, ll.Corredor, ll.Lado, ll.Prateleira, ll.Vao,produto.ProdutoId, produto);

                return localizacao;
            }

            localizacao = new Localizacao(ll.LocalizacaoId, ll.Andar, ll.Corredor, ll.Lado, ll.Prateleira, ll.Vao);

            return localizacao;

        }, parametros, splitOn: "LocalizacaoId, ProdutoId, UnidadeMedidaId");

        return resultado.Distinct().SingleOrDefault();
    }

    public async Task<List<Localizacao>> ListarTodosAsync(int? produtoId, string? andar, int? corredor, char? lado, int? prateleira)
    {
        string query =
        @"
            SELECT 
                [ID_LOCALIZACAO] AS LocalizacaoId
               ,[ANDAR]
               ,[CORREDOR]
               ,[LADO]
               ,[PRATELEIRA]
               ,[VAO]
               ,[ID_PRODUTO] AS ProdutoId
               ,[ATIVO]
               ,[QUANTIDADE]
               ,[PRECO]
               ,[NOME]
               ,[IMAGEM_TIPO] AS ImagemTipo
               ,[IMAGEM_NOME] AS ImagemNome
               ,[CODIGO_BARRAS] AS CodigoBarras
               ,[DATA_CRIACAO] AS Criacao
               ,[DATA_ATUALIZACAO] AS Atualizacao
               ,[ID_UNIDADE_MEDIDA] AS UnidadeMedidaId
               ,[DESCRICAO]
            FROM [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO]
            LEFT JOIN TB_PRODUTO ON ID_PRODUTO = FK_IDPRODUTO
            LEFT JOIN TB_UNIDADE_MEDIDA ON FK_IDUNIDADE_MEDIDA = ID_UNIDADE_MEDIDA
        ";

        var parametros = new DynamicParameters();
        string queryParams = null;

        if (produtoId != null)
        {
            query += "WHERE [FK_IDPRODUTO] = @ID_PRODUTO;";
            parametros.Add("@ID_PRODUTO", produtoId, DbType.Int32, ParameterDirection.Input);
        }
        else
        {
            if (andar != null)
            {
                if(queryParams == null)
                {
                    queryParams += "WHERE [ANDAR] = @ANDAR";
                }
                else
                {
                    queryParams += " AND [ANDAR] = @ANDAR";
                }

                parametros.Add("@ANDAR", andar, DbType.String, ParameterDirection.Input);
            }

            if (corredor != null)
            {
                if (queryParams == null)
                {
                    queryParams = "WHERE [CORREDOR] = @CORREDOR";
                }
                else
                {
                    queryParams += " AND [CORREDOR] = @CORREDOR";
                }

                parametros.Add("@CORREDOR", corredor, DbType.Int32, ParameterDirection.Input);
            }

            if (lado != null)
            {
                if (queryParams == null)
                {
                    queryParams = "WHERE [LADO] = @LADO";
                }
                else
                {
                    queryParams += " AND [LADO] = @LADO";
                }

                parametros.Add("@LADO", lado, DbType.String, ParameterDirection.Input);
            }

            if (prateleira != null)
            {
                if (queryParams == null)
                {
                    queryParams = "WHERE [PRATELEIRA] = @PRATELEIRA";
                }
                else
                {
                    queryParams += " AND [PRATELEIRA] = @PRATELEIRA";
                }

                parametros.Add("@PRATELEIRA", prateleira, DbType.Int32, ParameterDirection.Input);
            }

            if (queryParams != null)
            {
                query += queryParams;
            }
        }

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        var resultado = await conexao.QueryAsync<Localizacao, Produto, UnidadeMedida, Localizacao>(query, (ll, pr, um) =>
        {
            Produto produto;
            Localizacao localizacao;

            if (pr != null)
            {
                produto = new Produto(pr.ProdutoId, pr.Ativo, pr.Quantidade, pr.Preco, pr.Nome, pr.ImagemTipo, pr.ImagemNome, pr.CodigoBarras, pr.Criacao, pr.Atualizacao, um);
                localizacao = new Localizacao(ll.LocalizacaoId, ll.Andar, ll.Corredor, ll.Lado, ll.Prateleira, ll.Vao, produto.ProdutoId, produto);

                return localizacao;
            }

            localizacao = new Localizacao(ll.LocalizacaoId, ll.Andar, ll.Corredor, ll.Lado, ll.Prateleira, ll.Vao);

            return localizacao;

        }, parametros, splitOn: "LocalizacaoId, ProdutoId, UnidadeMedidaId");

        return resultado.Distinct().AsList();
    }

    public async Task<List<Localizacao>> ListarTodosDisponiveisAsync()
    {
        string query =
       @"
            SELECT 
                [ID_LOCALIZACAO] AS LocalizacaoId
                ,[ANDAR]
                ,[CORREDOR]
                ,[LADO]
                ,[PRATELEIRA]
                ,[VAO]
            FROM [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO]
            WHERE [FK_IDPRODUTO] IS NULL;
        ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        var resultado = await conexao.QueryAsync<Localizacao>(query);
        
        return resultado.Distinct().AsList();
    }

    public async Task<bool> RemoveAsync(int localizacaoId)
    {
        const string query =
        @"
           DELETE FROM TB_LOCALIZACAO WHERE ID_LOCALIZACAO = @ID_LOCALIZACAO
        ";

        var parametros = new DynamicParameters();
        parametros.Add("@ID_LOCALIZACAO", localizacaoId, DbType.Int32, ParameterDirection.Input);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, parametros);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> VerificarDisponibilidadeAsync(int localizacaoId)
    {
        string query =
        @"
            SELECT 
                [ID_LOCALIZACAO] AS LocalizacaoId
                ,[ANDAR]
                ,[CORREDOR]
                ,[LADO]
                ,[PRATELEIRA]
                ,[VAO]
            FROM [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO]
            WHERE [ID_LOCALIZACAO] = @ID_LOCALIZACAO AND [FK_IDPRODUTO] IS NULL;
        ";

        var parametros = new DynamicParameters();
        parametros.Add("@ID_LOCALIZACAO", localizacaoId, DbType.Int32, ParameterDirection.Input);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        var resultado = await conexao.QueryFirstOrDefaultAsync<Localizacao>(query, parametros);
        if(resultado != null)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> VerificarExistenciaAsync(Localizacao localizacao)
    {
        string query =
        @"
           SELECT 
                [ID_LOCALIZACAO] AS LocalizacaoId
                ,[ANDAR]
                ,[CORREDOR]
                ,[LADO]
                ,[PRATELEIRA]
                ,[VAO]
            FROM [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO]
            WHERE [ANDAR] = @ANDAR
            AND [CORREDOR] = @CORREDOR
            AND [LADO] = @LADO
            AND [PRATELEIRA] = @PRATELEIRA
            AND [VAO] = @VAO
        ";

        var parametros = new DynamicParameters();
        parametros.Add("@ANDAR", localizacao.Andar, DbType.String, ParameterDirection.Input);
        parametros.Add("@CORREDOR", localizacao.Corredor, DbType.Int32, ParameterDirection.Input);
        parametros.Add("@LADO", localizacao.Lado, DbType.String, ParameterDirection.Input);
        parametros.Add("@PRATELEIRA", localizacao.Prateleira, DbType.Int32, ParameterDirection.Input);
        parametros.Add("@VAO", localizacao.Vao, DbType.Int32, ParameterDirection.Input);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        var resultado = await conexao.QueryFirstOrDefaultAsync<Localizacao>(query, parametros);
        if (resultado != null)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> ProdutoLimiteAlocacaoAtingidoAsync(int? produtoId)
    {
        string query =
        @"
            SELECT COUNT(*) FROM [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO] WHERE FK_IDPRODUTO = @PRODUTOID;
        ";

        var parametros = new DynamicParameters();
        parametros.Add("@PRODUTOID", produtoId, DbType.Int32, ParameterDirection.Input);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        var resultado = await conexao.QueryFirstOrDefaultAsync<int>(query, parametros);
        if (resultado >= 3)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> AlocarProdutoAsync(List<int> localizacoesId, int produtoId)
    {
       string query =
       @"
            UPDATE TB_LOCALIZACAO 
            SET 
                 [FK_IDPRODUTO] = @PRODUTO_ID
            WHERE ID_LOCALIZACAO IN @LOCALIZACOES_ID;
        ";

        var parametros = new DynamicParameters();
        parametros.Add("@PRODUTO_ID", produtoId, DbType.Int32, ParameterDirection.Input);
        parametros.Add("@LOCALIZACOES_ID", localizacoesId);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, parametros);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> DesalocarProdutoAsync(List<int> localizacoesId)
    {
       string query =
       @"
            UPDATE TB_LOCALIZACAO 
            SET 
                 [FK_IDPRODUTO] = NULL
            WHERE ID_LOCALIZACAO IN @LOCALIZACOES_ID;
       ";

        var parametros = new DynamicParameters();
        parametros.Add("@LOCALIZACOES_ID", localizacoesId);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, parametros);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }
}