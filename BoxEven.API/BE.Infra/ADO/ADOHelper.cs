using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace BE.Infra.ADO;

public class ADOHelper
{
    internal static dynamic? GetValidValue<T>(SqlDataReader dataReader, string coluna)
    {
        var ordinalColuna = dataReader.GetOrdinal(coluna);

        if (typeof(T).Equals(typeof(char)))
        {
            return dataReader.IsDBNull(ordinalColuna) ? default(T) : dataReader.GetString(coluna)[0];
        }

        return dataReader.IsDBNull(ordinalColuna) ? default(T) : dataReader.GetFieldValue<T>(coluna);
    }
}

