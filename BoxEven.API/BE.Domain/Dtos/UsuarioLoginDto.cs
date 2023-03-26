using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Dtos;

public class UsuarioLoginDto
{
    [Required]
    [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "Maxímo de 10 e minímo de 10 caractere")]
    public string Apelido { get; set; }
    [Required]
    [StringLength(maximumLength: 40, MinimumLength = 8, ErrorMessage = "Maxímo de 3 e minímo de 1 caractere")]
    public string Senha { get; set; }
}
