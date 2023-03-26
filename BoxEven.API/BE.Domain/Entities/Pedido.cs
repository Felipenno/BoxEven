using BE.Domain.Enum;

namespace BE.Domain.Entities;

public class Pedido
{
    public string? Id { get; private set; }
    public int Numero { get; private set; }
    public string Vendedor { get; private set; }
    public StatusPedido Status { get; private set; }
    public DateTime Criacao { get; private set; }
    public DateTime Conclusao { get; private set; }
    public List<Produto> Produtos { get; private set; }

    public Pedido() {}

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
}
