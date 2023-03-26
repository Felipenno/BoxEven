using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BE.Domain.Dtos;

public class ProdutoCriarDto
{
    [Required(ErrorMessage = "O campo Ativo é obrigatório")]
    public bool? Ativo { get; set; }

    [Required(ErrorMessage = "O campo Quantidade é obrigatório")]
    [Range(1, 9999, ErrorMessage = "Valor minímo de 1 e maxímo de 9999")]
    public int? Quantidade { get; set; }

    [Required(ErrorMessage = "O campo Preço é obrigatório")]
    [Range(1.0, 1000000, ErrorMessage = "Valor minímo de 1 e maxímo de 1.000.000.")]
    public decimal? Preco { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório")]
    [StringLength(maximumLength: 40, ErrorMessage = "Nome deve ter no maxímo 40 caracteres")]
    public string? Nome { get; set; }

    [StringLength(maximumLength: 70000, ErrorMessage = "Imagem deve ter no maxímo 70000 caracteres")]
    public string? Imagem { get; set; }

    [StringLength(maximumLength: 100, ErrorMessage = "CodigoBarras deve ter no maxímo 100 caracteres")]
    public string? CodigoBarras { get; set; }

    [Required(ErrorMessage = "O campo UnidadeMedidaId é obrigatório")]
    public int? UnidadeMedidaId { get; set; }
}