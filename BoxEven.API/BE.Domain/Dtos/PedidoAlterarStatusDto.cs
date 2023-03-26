using BE.Domain.Enum;
using BE.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Dtos;

public class PedidoAlterarStatusDto
{
    [Required]
    [StringLength(maximumLength: 40, MinimumLength = 10, ErrorMessage = "Maxímo de 3 e minímo de 1 caractere")]
    public string IdPedido { get; set; }
    [Required]
    public StatusPedido StatusPedido { get; set; }
    public Guid IdUsuario { get; set; }
    public int QuantidadePedido { get; set; }
    public int NumeroPedido { get; set; }
    public List<ProdutoAlterarQuantidade>? Produtos { get; set; }
}
