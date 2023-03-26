namespace BEV.Domain.Model;

public class DatabaseSettings
{
    public string SqlServerConnection { get; set; } = null!;
    public string MongoDBConnection { get; set; } = null!;
    public string MongoDBDatabaseName { get; set; } = null!;
    public string PedidoCollectionName { get; set; } = null!;
}