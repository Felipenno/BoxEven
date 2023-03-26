using BE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Dtos;

public class PedidoDto
{
    public string? Id { get; set; }
    public int Numero { get; set; }
    public string Vendedor { get; set; }
    public StatusPedido Status { get; set; }
    public DateTime Criacao { get; set; }
    public DateTime Conclusao { get; set; }
    public List<ProdutoListarDto> Produtos { get; set; }
}
