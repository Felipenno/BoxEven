using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces.Service;
using System.Text.RegularExpressions;

namespace BE.Service;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ICryptographyService _cryptographyService;
    private readonly IAuthenticationService _authenticationService;

    public UsuarioService(IUsuarioRepository usuarioRepository, ICryptographyService cryptographyService, IAuthenticationService authenticationService)
    {
        _usuarioRepository = usuarioRepository;
        _cryptographyService = cryptographyService;
        _authenticationService = authenticationService;
    }

    public async Task<ResultadoModel> CadastrarUsuarioAsync(UsuarioRegistroDto usuarioDto)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var apelido = ObterStringSemAcentosECaracteresEspeciais(usuarioDto.Nome);

        var usuario = new Usuario(Guid.NewGuid(), apelido, usuarioDto.Nome, usuarioDto.Email, _cryptographyService.EncriptarSenha(usuarioDto.Senha));
        var concluido = await _usuarioRepository.AdicionarAsync(usuario);
        if (concluido)
        {
            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "Usuário criado.";
            resultadoModel.Objeto = apelido;
            return resultadoModel;
        }
    
        resultadoModel.Sucesso = false;
        resultadoModel.Mensagem = "Erro ao cadastrar usuário.";
        return resultadoModel;
    }

    public async Task<ResultadoModel> LoginAsync(string apelido, string senha)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var usuario = await _usuarioRepository.Login(apelido, _cryptographyService.EncriptarSenha(senha));
        if(usuario != null)
        {
            var usuarioDto = new UsuarioDto()
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha,
                Apelido = usuario.Apelido,
                UsuarioId = usuario.UsuarioId
            };

            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "";
            resultadoModel.Objeto = new { Token = _authenticationService.GerarToken(usuarioDto) };
            return resultadoModel;
        }

        resultadoModel.Sucesso = false;
        resultadoModel.Mensagem = "Dados incorretos";
        return resultadoModel;
    }

    public Task<ResultadoModel> ConfirmarCadastro()
    {
        throw new NotImplementedException();
    }

    private static string ObterStringSemAcentosECaracteresEspeciais(string str)
    {
        //Troca os caracteres acentuados por não acentuados
        string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
        string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

        for (int i = 0; i < acentos.Length; i++)
        {
            str = str.Replace(acentos[i], semAcento[i]);
        }

        //Troca os caracteres especiais da string por ""
        string[] caracteresEspeciais = { "¹", "²", "³", "£", "¢", "¬", "º", "¨", "\"", "'", ".", ",", "-", ":", "(", ")", "ª", "|", "\\\\", "°", "_", "@", "#", "!", "$", "%", "&", "*", ";", "/", "<", ">", "?", "[", "]", "{", "}", "=", "+", "§", "´", "`", "^", "~" };

        for (int i = 0; i < caracteresEspeciais.Length; i++)
        {
            str = str.Replace(caracteresEspeciais[i], "");
        }

        //Troca os caracteres especiais da string por " "
        str = Regex.Replace(str, @"[^\w\.@-]", " ", RegexOptions.None, TimeSpan.FromSeconds(1.5));

        var numero = new Random().Next(100, 999);

        str = str.ToLower().Replace(" ", "");

        str = new string(str.Take(7).ToArray()) + numero;

        return str;

    }
}
