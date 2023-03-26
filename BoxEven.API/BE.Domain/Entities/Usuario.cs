namespace BE.Domain.Entities;

public class Usuario
{
    public Guid UsuarioId { get; private set; }
    public string Apelido { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }

    public Usuario(Guid usuarioId, string apelido, string nome, string email, string senha)
    {
        UsuarioId = usuarioId;
        Apelido = apelido;
        Nome = nome;
        Email = email;
        Senha = senha;
    }

    public Usuario(Guid usuarioId, string apelido, string nome, string email)
    {
        UsuarioId = usuarioId;
        Apelido = apelido;
        Nome = nome;
        Email = email;
    }

    public Usuario(string apelido, string senha)
    {
        Apelido = apelido;
        Senha = senha;
    }
}