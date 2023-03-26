using BE.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Service;

public interface IAuthenticationService
{
    string GerarToken(UsuarioDto usuario);
}
