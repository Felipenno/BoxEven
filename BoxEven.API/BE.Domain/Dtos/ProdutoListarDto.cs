namespace BE.Domain.Dtos;

public class ProdutoListarDto
{
    public int? ProdutoId { get; set; }
    public bool? Ativo { get; set; }
    public int? Quantidade { get; set; }
    public decimal? Preco { get; set; }
    public string? Nome { get; set; }
    public string? Imagem { get; set; }
    public string? CodigoBarras { get; set; }
    public DateTime? Criacao { get; set; }
    public DateTime? Atualizacao { get; set; }
    public UnidadeMedidaDto UnidadeMedida { get; set; }
    public List<LocalizacaoEnderecoMontadoDto>? Localizacoes { get; set; }
}