using Microsoft.Data.SqlClient;

namespace BE.Domain.Interfaces.Repository;

public interface IProvedorDados
{
    SqlConnection BoxEvenConexao();
    string StringConexao();
}