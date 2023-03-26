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

public class DPUnidadeMedidaRepository : IUnidadeMedidaRepository
{
    private readonly IProvedorDados _provedorDados;

    public DPUnidadeMedidaRepository(IProvedorDados provedorDados)
    {
        _provedorDados = provedorDados;
    }

    public async Task<bool> AdicionarAsync(UnidadeMedida unidadeMedida)
    {
        const string query = 
        @"
            INSERT INTO [TB_UNIDADE_MEDIDA](DESCRICAO)
            VALUES(@UnidadeMedidaDescricao);
        ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, unidadeMedida);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> AtualizarAsync(UnidadeMedida unidadeMedida)
    {
        const string query =
        @"
            UPDATE [TB_UNIDADE_MEDIDA] 
            SET [DESCRICAO] = @UnidadeMedidaDescricao
            WHERE ID_UNIDADE_MEDIDA = @UnidadeMedidaId;
        ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, unidadeMedida);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<List<UnidadeMedida>> ListarTodosAsync()
    {
        const string query =
        @"
            SELECT 
                 [ID_UNIDADE_MEDIDA] AS UnidadeMedidaId
                ,[DESCRICAO] AS UnidadeMedidaDescricao
            FROM 
                [TB_UNIDADE_MEDIDA]
        ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        var result = await conexao.QueryAsync<UnidadeMedida>(query);
        return result.AsList();
    }

    public async Task<bool> RemoveAsync(int unidadeMedidaId)
    {
        const string query =
        @"
           DELETE FROM [TB_UNIDADE_MEDIDA] WHERE [ID_UNIDADE_MEDIDA] = @unidadeMedidaId
        ";

        var parametros = new DynamicParameters();
        parametros.Add("@unidadeMedidaId", unidadeMedidaId, DbType.Int32, ParameterDirection.Input);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, parametros);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }
}