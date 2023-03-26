using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Service;

public interface ICryptographyService
{
    string EncriptarSenha(string passWord);
}
