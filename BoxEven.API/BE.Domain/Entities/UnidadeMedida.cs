namespace BE.Domain.Entities;

public class UnidadeMedida
{
    public int? UnidadeMedidaId { get; private set; }
    public string? Descricao { get; private set; }

    public Produto? Produto { get; private set; }

    public UnidadeMedida(int? unidadeMedidaId, string? descricao)
    {
        UnidadeMedidaId = unidadeMedidaId;
        Descricao = descricao;
    }
}