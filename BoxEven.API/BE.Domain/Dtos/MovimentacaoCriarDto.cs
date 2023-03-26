using BE.Domain.Enum;

namespace BE.Domain.Dtos;

public class MovimentacaoCriarDto
{
    public int Quantidade { get; set; }
    public string Justificativa { get; set; }
    public string Tipo { get; set; }
    public int ProdutoId { get; set; }
    public Guid UsuarioId { get; set; }
}