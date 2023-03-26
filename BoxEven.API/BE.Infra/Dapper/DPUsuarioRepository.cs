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

public class DPUsuarioRepository : IUsuarioRepository
{
    private readonly IProvedorDados _provedorDados;

    public DPUsuarioRepository(IProvedorDados provedorDados)
    {
        _provedorDados = provedorDados;
    }

    public async Task<bool> AdicionarAsync(Usuario usuario)
    {
        const string query =
        @"
            INSERT INTO [TB_USUARIO](ID_USUARIO, APELIDO, NOME_COMPLETO, EMAIL, SENHA)
            VALUES(@UsuarioId, @Apelido, @Nome, @Email, @Senha);
        ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, usuario);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public Task<bool> AlterarSenha(string senha)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AtualizarAsync(Usuario usuario)
    {
        const string query =
        @"
            UPDATE [TB_USUARIO]
            SET
                 APELIDO = @Apelido
                ,NOME_COMPLETO = @Nome
                ,EMAIL = @Email
                ,SENHA = @Senha
            WHERE 
                ID_USUARIO = UsuarioId;
        ";

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        int linhasAfetadas = await conexao.ExecuteAsync(query, usuario);
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public Task<bool> ConfirmarCadastro()
    {
        throw new NotImplementedException();
    }

    public async Task<Usuario> ListarPorIdAsync(Guid usuarioId)
    {
        const string query =
        @"
            SELECT 
                 [ID_USUARIO] AS UsuarioId
                ,[APELIDO] AS Apelido
                ,[NOME_COMPLETO] AS Nome
                ,[EMAIL] AS Email
            FROM [TB_USUARIO]
            WHERE ID_USUARIO = @usuarioId;
        ";

        var parametros = new DynamicParameters();
        parametros.Add("@usuarioId", usuarioId, DbType.Guid, ParameterDirection.Input);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        return await conexao.QueryFirstAsync<Usuario>(query, parametros); 
    }

    public async Task<Usuario> Login(string apelido, string senha)
    {
        const string query =
        @"
            SELECT 
                 [ID_USUARIO] AS UsuarioId
                ,[APELIDO] AS Apelido
                ,[NOME_COMPLETO] AS Nome
                ,[EMAIL] AS Email
            FROM [TB_USUARIO]
            WHERE [APELIDO] = @apelido AND [SENHA] = @senha;
        ";

        var parametros = new DynamicParameters();
        parametros.Add("@apelido", apelido, DbType.String, ParameterDirection.Input);
        parametros.Add("@senha", senha, DbType.String, ParameterDirection.Input);

        using IDbConnection conexao = _provedorDados.BoxEvenConexao();
        return await conexao.QueryFirstOrDefaultAsync<Usuario>(query, parametros);
    }

    public Task<bool> VerificarCodigo(string codigo)
    {
        throw new NotImplementedException();
    }
}