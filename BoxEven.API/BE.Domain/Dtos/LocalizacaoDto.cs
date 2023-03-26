using BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Dtos;

public class LocalizacaoDto
{
    public int LocalizacaoId { get; set; } 
    public string? Andar { get; set; }
    public int Corredor { get; set; }
    public char? Lado { get; set; }
    public int Prateleira { get; set; }
    public int Vao { get; set; }
    public ProdutoListarDto? Produto { get; set; }
}