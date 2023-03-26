using BE.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Service.Cryptography;

public class CryptographyService : ICryptographyService
{
    private const string _salt = "Vwa4A/[FAfa170af-}&*#54ad$bgcxb@adw?A";

    public string EncriptarSenha(string passWord)
    {
        var valueBytes = KeyDerivation.Pbkdf2(
            password: passWord,
            salt: Encoding.UTF8.GetBytes(_salt),
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 256 / 8);

        return Convert.ToBase64String(valueBytes);
    } 

}
