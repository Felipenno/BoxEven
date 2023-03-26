using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Dtos;

public class UsuarioRegistroDto
{
    [Required]
    [StringLength(maximumLength: 40, MinimumLength = 10, ErrorMessage = "Maxímo de 40 e minímo de 10 caractere")]
    public string Nome { get; set; }
    [Required]
    [EmailAddress]
    [StringLength(maximumLength: 40, MinimumLength = 15, ErrorMessage = "Maxímo de 3 e minímo de 1 caractere")]
    public string Email { get; set; }
    [Required]
    [StringLength(maximumLength: 40, MinimumLength = 8, ErrorMessage = "Maxímo de 3 e minímo de 1 caractere")]
    public string Senha { get; set; }
}
