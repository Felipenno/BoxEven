using System.ComponentModel.DataAnnotations;

namespace BE.Domain.Dtos;

public class UnidadeMedidaDto
{
    public int? UnidadeMedidaId { get; set; }
    public string? Descricao { get; set; }
}