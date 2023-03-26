using BE.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BE.Infra;
public class ProvedorDados : IProvedorDados
{
    private readonly IConfiguration _config;

    public ProvedorDados(IConfiguration configuration)
    {
        _config = configuration;
    }

    public SqlConnection BoxEvenConexao()
    {
        return new SqlConnection(StringConexao());
    }

    public string StringConexao()
    {
        return _config.GetConnectionString("SqlServerConnection");
    }
}