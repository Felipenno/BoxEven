using BEV.Domain.Entities;
using BEV.Domain.Enum;
using BEV.Domain.Interfaces.Repository;
using BEV.Domain.Model;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Data;

namespace BEV.Infra.SqlServer;

public class PedidoRepository : IPedidoRepository
{
    private readonly IOptions<DatabaseSettings> _databaseSettings;
    private readonly IMongoCollection<Pedido> _pedidosCollection;

    public PedidoRepository(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings;

        var mongoCliente = new MongoClient(_databaseSettings.Value.MongoDBConnection);
        var mongoDatabase = mongoCliente.GetDatabase(_databaseSettings.Value.MongoDBDatabaseName);
        _pedidosCollection = mongoDatabase.GetCollection<Pedido>(_databaseSettings.Value.PedidoCollectionName);
    }

    public async Task AlterarStatusPedidoAsync(string id, StatusPedido status)
    {
        var filter = Builders<Pedido>.Filter.Eq(x => x.Id, id);
        var update = Builders<Pedido>.Update.Set(p => p.Status, status).Set(p => p.Conclusao, DateTime.Now);
        await _pedidosCollection.UpdateOneAsync(filter, update);
    }

    public async Task InserirPedidoAsync(Pedido pedido)
    {
        await _pedidosCollection.InsertOneAsync(pedido);
    }

    public async Task<List<Pedido>> ListarNovosPedidosAsync()
    {
       return await _pedidosCollection.Find(x => x.Status == StatusPedido.Novo).ToListAsync();
    }

    public async Task<List<Pedido>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao)
    {
        return await _pedidosCollection.Find(x => x.Status == status && x.Conclusao >= conclusao).ToListAsync();
    }

    public async Task<Produto[]> ListarProdutosAsync()
    {
        string query = 
        @"
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
        ";

        using (IDbConnection conexao = new SqlConnection(_databaseSettings.Value.SqlServerConnection))
        {
            Produto? produto = null;

            var resultado = await conexao.QueryAsync<Produto, UnidadeMedida, Localizacao, Produto>(query, (pr, um, ll) =>
            {
                 if (produto == null || produto.ProdutoId != pr.ProdutoId)
                {
                    produto = new Produto(pr.ProdutoId, pr.Ativo, pr.Quantidade, pr.Preco, pr.Nome, pr.ImagemTipo, pr.ImagemNome, pr.CodigoBarras, pr.Criacao, pr.Atualizacao, um, new List<Localizacao>());
                     if (ll != null)
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

            }, splitOn: "ProdutoId, UnidadeMedidaId, LocalizacaoId");

            return resultado.Distinct().ToArray();
        }
    }
}