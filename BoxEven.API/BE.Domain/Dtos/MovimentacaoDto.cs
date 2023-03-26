using BE.Domain.Enum;

namespace BE.Domain.Dtos;

public class MovimentacaoDto
{
    public int MovimentacaoId { get; set; }
    public int Quantidade { get; set; }
    public string Justificativa { get; set; }
    public DateTime DataOperacao { get; set; }
    public string Tipo { get; set; }
    public string Produto { get; set; }
    public string Usuario { get; set; }
}