using BEV.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace BEV.Domain.Entities;

public class Pedido
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public int Numero { get; private set; }
    public string Vendedor { get; private set; }
    public StatusPedido Status { get; private set; }
    public DateTime Criacao { get; private set; }
    public DateTime Conclusao { get; private set; }
    public List<Produto> Produtos { get; private set; }

    public Pedido(string? id, int numero, string vendedor, StatusPedido status, DateTime criacao, DateTime conclusao, List<Produto> produtos)
    {
        Id = id;
        Numero = numero;
        Vendedor = vendedor;
        Status = status;
        Criacao = criacao;
        Conclusao = conclusao;
        Produtos = produtos;
    }

    public Pedido(int numero, string vendedor, StatusPedido status, DateTime criacao, List<Produto> produtos)
    {
        Numero = numero;
        Vendedor = vendedor;
        Status = status;
        Criacao = criacao;
        Produtos = produtos;
    }
}