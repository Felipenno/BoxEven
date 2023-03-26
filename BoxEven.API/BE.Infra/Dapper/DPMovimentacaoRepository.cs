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

public class DPMovimentacaoRepository : IMovimentacaoRepository
{
    private readonly IProvedorDados _provedorDados;

    public DPMovimentacaoRepository(IProvedorDados provedorDados)
    {
        _provedorDados = provedorDados;
    }

    public async Task<bool> AdicionarAsync(Movimentacao movimentacao)
    {
        const string query =
        @"
            INSERT INTO TB_MOVIMENTACAO([FK_IDPRODUTO], [FK_IDUSUARIO], [TIPO], [QUANTIDADE], [JUSTIFICATIVA], [DATA_OPERACAO])
            VALUES(@PRODUTOID, @USUARIOID, @TIPO, @QUANTIDADE, @JUSTIFICATIVA, @DATA_OPERACAO);
        ";

        using (IDbConnection conexao = _provedorDados.BoxEvenConexao())
        {
            var parametros = new DynamicParameters();
            parametros.Add("@PRODUTOID", movimentacao.Produto.ProdutoId, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@USUARIOID", movimentacao.Usuario.UsuarioId, DbType.Guid, ParameterDirection.Input);
            parametros.Add("@TIPO", movimentacao.Tipo, DbType.String, ParameterDirection.Input);
            parametros.Add("@QUANTIDADE", movimentacao.Quantidade, DbType.Int32, ParameterDirection.Input);
            parametros.Add("@JUSTIFICATIVA", movimentacao.Justificativa, DbType.String, ParameterDirection.Input);
            parametros.Add("@DATA_OPERACAO", movimentacao.DataOperacao, DbType.DateTime, ParameterDirection.Input);

            int linhasAfetadas = await conexao.ExecuteAsync(query, parametros);
            if (linhasAfetadas <= 0)
            {
                return false;
            }

            return true;
        }
    }

    public Task<Movimentacao> ListarPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Movimentacao>> ListarTodosAsync()
    {
        string query =
        @"
            SELECT 
               [ID_MOVIMENTACAO] AS MovimentacaoId
              ,MO.[QUANTIDADE] AS Quantidade
              ,[JUSTIFICATIVA] AS Justificativa
              ,[DATA_OPERACAO] AS DataOperacao
              ,[TIPO] AS Tipo
              ,[ID_PRODUTO] AS ProdutoId
              ,[ATIVO] AS Ativo
              ,PR.[QUANTIDADE] AS Quantidade
              ,[PRECO] AS Preco
              ,[NOME] AS Nome
              ,[IMAGEM_TIPO] AS ImagemTipo
              ,[IMAGEM_NOME] AS ImagemNome
              ,[CODIGO_BARRAS] AS CodigoBarras
              ,[DATA_CRIACAO] AS Criacao
              ,[DATA_ATUALIZACAO] AS Atualizacao
              ,[ID_UNIDADE_MEDIDA] AS UnidadeMedidaId
              ,[DESCRICAO] AS Descricao
              ,[ID_USUARIO] AS UsuarioId
              ,[APELIDO] AS Apelido
              ,[NOME_COMPLETO] AS Nome
              ,[EMAIL] AS Email
            FROM [BD_BOXEVEN].[dbo].[TB_MOVIMENTACAO] AS MO
            INNER JOIN [TB_PRODUTO] AS PR ON ID_PRODUTO = FK_IDPRODUTO
            INNER JOIN [TB_UNIDADE_MEDIDA] ON ID_UNIDADE_MEDIDA = PR.FK_IDUNIDADE_MEDIDA
            INNER JOIN [TB_USUARIO] ON ID_USUARIO = FK_IDUSUARIO;
       ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        var resultado = await conexao.QueryAsync<Movimentacao, Produto, UnidadeMedida, Usuario, Movimentacao>(query, (mo, pr, um, user) =>
        {
            var produto = new Produto(pr.ProdutoId, pr.Ativo, pr.Quantidade, pr.Preco, pr.Nome, pr.ImagemTipo, pr.ImagemNome, pr.CodigoBarras, pr.Criacao, pr.Atualizacao, um);
            return new Movimentacao(mo.MovimentacaoId, mo.Quantidade, mo.Justificativa, mo.DataOperacao, mo.Tipo, produto, user);

        }, splitOn: "MovimentacaoId, ProdutoId, UnidadeMedidaId, UsuarioId");

        return resultado.Distinct().AsList();
    }
}