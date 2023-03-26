using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Dtos;

public class LocalizacaoEditarDto
{
    [Required(ErrorMessage = "O campo LocalizacaoId é obrigatório")]
    [Range(1, 9999, ErrorMessage = "Valor minímo de 1 e maxímo de 9999")]
    public int LocalizacaoId { get; set; }

    [Required(ErrorMessage = "O campo Andar é obrigatório")]
    [StringLength(maximumLength: 3, MinimumLength = 1, ErrorMessage = "Maxímo de 3 e minímo de 1 caractere" )]
    public string? Andar { get; set; }

    [Required(ErrorMessage = "O campo Corredor é obrigatório")]
    [Range(1, 20, ErrorMessage = "Valor minímo de 1 e maxímo de 9999")]
    public int Corredor { get; set; }

    [Required(ErrorMessage = "O campo Lado é obrigatório")]
    public char? Lado { get; set; }

    [Required(ErrorMessage = "O campo Prateleira é obrigatório")]
    [Range(1, 20, ErrorMessage = "Valor minímo de 1 e maxímo de 9999")]
    public int Prateleira { get; set; }

    [Required(ErrorMessage = "O campo Vao é obrigatório")]
    [Range(1, 100, ErrorMessage = "Valor minímo de 1 e maxímo de 9999")]
    public int Vao { get; set; }

    public int? ProdutoId { get; set; }
}