using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient; //esse é o novo - system.data é o antigo
using System.Data;

namespace BE.Infra.ADO;

public class ADOProdutoRepository : ADOHelper, IProdutoRepository
{
    private readonly IProvedorDados _provedorDados;

    public ADOProdutoRepository(IProvedorDados provedorDados)
    {
        _provedorDados = provedorDados;
    }

    public async Task<bool> AdicionarAsync(Produto produto)
    {
        const string query = @"INSERT INTO TB_PRODUTO(FK_IDUNIDADE_MEDIDA, ATIVO, QUANTIDADE, PRECO, NOME, IMAGEM_TIPO, IMAGEM_NOME, CODIGO_BARRAS, DATA_CRIACAO, DATA_ATUALIZACAO) 
                               VALUES(@UNIDADE_MEDIDAID, @ATIVO, @QUANTIDADE, @PRECO, @NOME, @IMAGEM_TIPO, @IMAGEM_NOME, @CODIGO_BARRAS, @DATA_CRIACAO, @DATA_ATUALIZACAO);";

        using (SqlConnection conexao = _provedorDados.BoxEvenConexao())
        using (SqlCommand cmd = new SqlCommand(query, conexao))
        {
            cmd.Parameters.Add("@UNIDADE_MEDIDAID", SqlDbType.Int).Value = produto.UnidadeMedidaId;
            cmd.Parameters.Add("@ATIVO", SqlDbType.Bit).Value = produto.Ativo;
            cmd.Parameters.Add("@QUANTIDADE", SqlDbType.Int).Value = produto.Quantidade;
            cmd.Parameters.Add("@PRECO", SqlDbType.Decimal).Value = produto.Preco;
            cmd.Parameters.Add("@DESCRICAO", SqlDbType.VarChar).Value = produto.Nome;
            cmd.Parameters.Add("@IMAGEM_TIPO", SqlDbType.VarChar).Value = produto.ImagemTipo;
            cmd.Parameters.Add("@IMAGEM_NOME", SqlDbType.VarChar).Value = produto.ImagemNome;
            cmd.Parameters.Add("@CODIGO_BARRAS", SqlDbType.VarChar).Value = produto.CodigoBarras;
            cmd.Parameters.Add("@DATA_CRIACAO", SqlDbType.DateTime).Value = produto.Criacao;
            cmd.Parameters.Add("@DATA_ATUALIZACAO", SqlDbType.DateTime).Value = produto.Atualizacao;

            await conexao.OpenAsync();
            int linhasAfetadas = cmd.ExecuteNonQuery();
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
                                  [QUANTIDADE] = IsNull(@QUANTIDADE, [QUANTIDADE])
                                 ,[DATA_ATUALIZACAO] = @DATA_ATUALIZACAO
                                WHERE ID_PRODUTO = @ID;";

        using (SqlConnection conexao = _provedorDados.BoxEvenConexao())
        using (SqlCommand cmd = new SqlCommand(query, conexao))
        {
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@QUANTIDADE", SqlDbType.Int).Value = quantidade;
            cmd.Parameters.Add("@DATA_ATUALIZACAO", SqlDbType.DateTime).Value = dataAtualizacao;

            await conexao.OpenAsync();
            int linhasAfetadas = cmd.ExecuteNonQuery();
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
                                SET [FK_IDUNIDADE_MEDIDA] = IsNull(@UNIDADE_MEDIDAID, [FK_IDUNIDADE_MEDIDA])
                                  ,[ATIVO] =  IsNull(@ATIVO, [ATIVO])
                                  ,[QUANTIDADE] = IsNull(@QUANTIDADE, [QUANTIDADE])
                                  ,[PRECO] = IsNull(@PRECO, [PRECO])
                                  ,[NOME] = IsNull(@NOME, [NOME])
                                  ,[IMAGEM_TIPO] = IsNull(@IMAGEM_TIPO, [IMAGEM_TIPO])
                                  ,[IMAGEM_NOME] = IsNull(@IMAGEM_NOME, [IMAGEM_NOME])
                                  ,[CODIGO_BARRAS] = IsNull(@CODIGO_BARRAS, [CODIGO_BARRAS])
                                  ,[DATA_ATUALIZACAO] = @DATA_ATUALIZACAO
                                WHERE ID_PRODUTO = @ID;";

        using (SqlConnection conexao = _provedorDados.BoxEvenConexao())
        using (SqlCommand cmd = new SqlCommand(query, conexao))
        {
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = produto.ProdutoId;
            cmd.Parameters.Add("@UNIDADE_MEDIDAID", SqlDbType.Int).Value = produto.UnidadeMedidaId == null ? DBNull.Value : produto.UnidadeMedidaId;
            cmd.Parameters.Add("@ATIVO", SqlDbType.Bit).Value = produto.Ativo == null ? DBNull.Value : produto.Ativo;
            cmd.Parameters.Add("@QUANTIDADE", SqlDbType.Int).Value = produto.Quantidade == null ? DBNull.Value : produto.Quantidade;
            cmd.Parameters.Add("@PRECO", SqlDbType.Decimal).Value = produto.Preco == null ? DBNull.Value : produto.Preco;
            cmd.Parameters.Add("@NOME", SqlDbType.VarChar).Value = produto.Nome == null ? DBNull.Value : produto.Nome;
            cmd.Parameters.Add("@IMAGEM_TIPO", SqlDbType.VarChar).Value = produto.ImagemTipo == null ? DBNull.Value : produto.ImagemTipo;
            cmd.Parameters.Add("@IMAGEM_NOME", SqlDbType.VarChar).Value = produto.ImagemNome == null ? DBNull.Value : produto.ImagemNome;
            cmd.Parameters.Add("@CODIGO_BARRAS", SqlDbType.VarChar).Value = produto.CodigoBarras == null ? DBNull.Value : produto.CodigoBarras;
            cmd.Parameters.Add("@DATA_ATUALIZACAO", SqlDbType.DateTime).Value = produto.Atualizacao == null ? DBNull.Value : produto.Atualizacao;

            await conexao.OpenAsync();
            int linhasAfetadas = cmd.ExecuteNonQuery();
            if (linhasAfetadas <= 0)
            {
                return false;
            }

            return true;
        }
    }

    public async Task<Produto> ListarPorIdAsync(int id)
    {
        const string query = @"SELECT
                                ID_PRODUTO
                                ,[ATIVO]
                                ,[QUANTIDADE]
                                ,[PRECO]
                                ,[NOME]
                                ,[IMAGEM_TIPO]
                                ,[IMAGEM_NOME]
                                ,[CODIGO_BARRAS]
                                ,[DATA_CRIACAO]
                                ,[DATA_ATUALIZACAO]
                                ,[ID_UNIDADE_MEDIDA]
                                ,[DESCRICAO]
                                ,[ID_LOCALIZACAO]
                                ,[FK_IDPRODUTO]
                                ,[ANDAR]
                                ,[CORREDOR]
                                ,[LADO]
                                ,[PRATELEIRA]
                                ,[VAO]
                              FROM [BD_BOXEVEN].[dbo].[TB_PRODUTO] WITH (NOLOCK)
                              LEFT JOIN [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO] ON(ID_PRODUTO = FK_IDPRODUTO)
                              LEFT JOIN [BD_BOXEVEN].[dbo].[TB_UNIDADE_MEDIDA] ON(ID_UNIDADE_MEDIDA = FK_IDUNIDADE_MEDIDA)
                              WHERE ID_PRODUTO = @ID_PRODUTO;";

        using var conexao = _provedorDados.BoxEvenConexao();
        await conexao.OpenAsync();

        using SqlCommand cmd = new SqlCommand(query, conexao);
        cmd.Parameters.Add("@ID_PRODUTO", SqlDbType.Int).Value = id;

        using SqlDataReader rd = await cmd.ExecuteReaderAsync();

        Produto produto = null;
        while (await rd.ReadAsync())
        {
            var produtoId = GetValidValue<int>(rd, "ID_PRODUTO");
            var ativo = GetValidValue<bool>(rd, "ATIVO");
            var quantidade = GetValidValue<int>(rd, "QUANTIDADE");
            var preco = GetValidValue<decimal>(rd, "PRECO");
            var nome = GetValidValue<string>(rd, "NOME");
            var imagemTipo = GetValidValue<string>(rd, "IMAGEM_TIPO");
            var imagemNome = GetValidValue<string>(rd, "IMAGEM_NOME");
            var codigoBarras = GetValidValue<string>(rd, "CODIGO_BARRAS");
            var dataCriacao = GetValidValue<DateTime>(rd, "DATA_CRIACAO");
            var dataAtualizacao = GetValidValue<DateTime>(rd, "DATA_ATUALIZACAO");

            var unidadeMedidaId = GetValidValue<int>(rd, "ID_UNIDADE_MEDIDA");
            var descricao = GetValidValue<string>(rd, "DESCRICAO");

            var fkIdProduto = GetValidValue<int>(rd, "FK_IDPRODUTO");
            var localizacaoId = GetValidValue<int>(rd, "ID_LOCALIZACAO");
            var andar = GetValidValue<string>(rd, "ANDAR");
            var corredor = GetValidValue<int>(rd, "CORREDOR");
            var lado = GetValidValue<char>(rd, "LADO");
            var prateleira = GetValidValue<int>(rd, "PRATELEIRA");
            var vao = GetValidValue<int>(rd, "VAO");

            if (produto == null)
            {
                var unidadeMedida = new UnidadeMedida(unidadeMedidaId, descricao);

                var localizacao = new Localizacao(localizacaoId, andar, corredor, lado, prateleira, vao);

                var localizacoes = new List<Localizacao>();
                if (fkIdProduto == produtoId)
                {
                    localizacoes.Add(localizacao);
                }

                produto = new Produto(produtoId, ativo, quantidade, preco, nome, imagemTipo, imagemNome, codigoBarras, dataCriacao, dataAtualizacao, unidadeMedida, localizacoes);

            }
            else
            {
                if (fkIdProduto == produto.ProdutoId)
                {
                    var localizacao = new Localizacao(localizacaoId, andar, corredor, lado, prateleira, vao);

                    produto.Localizacoes.Add(localizacao);
                }
            }
        }

        return produto;
    }


    public async Task<List<Produto>> ListarTodosAsync(int itensPagina, int pagina, bool? status, int? id, string? nomeProduto)
    {
         string query = @"SELECT
                                ID_PRODUTO
                                ,[ATIVO]
                                ,[QUANTIDADE]
                                ,[PRECO]
                                ,[NOME]
                                ,[IMAGEM_TIPO]
                                ,[IMAGEM_NOME]
                                ,[CODIGO_BARRAS]
                                ,[DATA_CRIACAO]
                                ,[DATA_ATUALIZACAO]
                                ,[ID_UNIDADE_MEDIDA]
                                ,[DESCRICAO]
                                ,[ID_LOCALIZACAO]
                                ,[FK_IDPRODUTO]
                                ,[ANDAR]
                                ,[CORREDOR]
                                ,[LADO]
                                ,[PRATELEIRA]
                                ,[VAO]
                              FROM [BD_BOXEVEN].[dbo].[TB_PRODUTO] WITH (NOLOCK)
                              LEFT JOIN [BD_BOXEVEN].[dbo].[TB_LOCALIZACAO] ON(ID_PRODUTO = FK_IDPRODUTO)
                              LEFT JOIN [BD_BOXEVEN].[dbo].[TB_UNIDADE_MEDIDA] ON(ID_UNIDADE_MEDIDA = FK_IDUNIDADE_MEDIDA)
                              --ORDER BY ID_PRODUTO;";

        using (var conexao = _provedorDados.BoxEvenConexao())
        {
            await conexao.OpenAsync();

            using (SqlCommand cmd = new SqlCommand(query, conexao))
            {
                if (status != null)
                {
                    query += " WHERE [ATIVO] = @STATUS;";
                    cmd.Parameters.Add("@ID_PRODUTO", SqlDbType.Bit).Value = status;
                }

                using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                {
                    List<Produto> produtos = new List<Produto>();

                    while (await rd.ReadAsync())
                    {
                        var produtoId = GetValidValue<int>(rd, "ID_PRODUTO");
                        var ativo = GetValidValue<bool>(rd, "ATIVO");
                        var quantidade = GetValidValue<int>(rd, "QUANTIDADE");
                        var preco = GetValidValue<decimal>(rd, "PRECO");
                        var nome = GetValidValue<string>(rd, "NOME");
                        var imagemTipo = GetValidValue<string>(rd, "IMAGEM_TIPO");
                        var imagemNome = GetValidValue<string>(rd, "IMAGEM_NOME");
                        var codigoBarras = GetValidValue<string>(rd, "CODIGO_BARRAS");
                        var dataCriacao = GetValidValue<DateTime>(rd, "DATA_CRIACAO");
                        var dataAtualizacao = GetValidValue<DateTime>(rd, "DATA_ATUALIZACAO");

                        var unidadeMedidaId = GetValidValue<int>(rd, "ID_UNIDADE_MEDIDA");
                        var descricao = GetValidValue<string>(rd, "DESCRICAO");

                        var fkIdProduto = GetValidValue<int>(rd, "FK_IDPRODUTO");
                        var localizacaoId = GetValidValue<int>(rd, "ID_LOCALIZACAO");
                        var andar = GetValidValue<string>(rd, "ANDAR");
                        var corredor = GetValidValue<int>(rd, "CORREDOR");
                        var lado = GetValidValue<char>(rd, "LADO");
                        var prateleira = GetValidValue<int>(rd, "PRATELEIRA");
                        var vao = GetValidValue<int>(rd, "VAO");

                        var produto = produtos.Where(p => p.ProdutoId == produtoId).FirstOrDefault();
                        if (produto == null)
                        {
                            var unidadeMedida = new UnidadeMedida(unidadeMedidaId, descricao);

                            var localizacao = new Localizacao(localizacaoId, andar, corredor, lado, prateleira, vao);

                            var localizacoes = new List<Localizacao>();
                            if (fkIdProduto == produtoId)
                            {
                                localizacoes.Add(localizacao);
                            }

                            produto = new Produto(produtoId, ativo, quantidade, preco, nome, imagemTipo, imagemNome, codigoBarras, dataCriacao, dataAtualizacao, unidadeMedida, localizacoes);

                            produtos.Add(produto);
                        }
                        else
                        {
                            if (fkIdProduto == produto.ProdutoId)
                            {
                                var localizacao = new Localizacao(localizacaoId, andar, corredor, lado, prateleira, vao);

                                produto.Localizacoes.Add(localizacao); //por referencia
                            }
                        }
                    }

                    return produtos;

                }
            }
        }
    }

    public Task<List<Produto>> ListarTodosDesalocadosAsync()
    {
        throw new NotImplementedException();
    }
}