using BE.Domain.Enum;

namespace BE.Domain.Entities;

public class Movimentacao
{
    public int MovimentacaoId { get; private set; }
    public int Quantidade { get; private set; }
    public string Justificativa { get; private set; }
    public DateTime DataOperacao { get; private set; }
    public string Tipo { get; private set; }
    public Produto Produto { get; private set; }
    public Usuario Usuario { get; private set; }

    public Movimentacao(int movimentacaoId, int quantidade, string justificativa, DateTime dataOperacao, string tipo, Produto produto, Usuario usuario)
    {
        MovimentacaoId = movimentacaoId;
        Quantidade = quantidade;
        Justificativa = justificativa;
        DataOperacao = dataOperacao;
        Tipo = tipo;
        Produto = produto;
        Usuario = usuario;
    }

    public Movimentacao(int quantidade, string justificativa, DateTime dataOperacao, string tipo, Produto produto, Usuario usuario)
    {
        Quantidade = quantidade;
        Justificativa = justificativa;
        DataOperacao = dataOperacao;
        Tipo = tipo;
        Produto = produto;
        Usuario = usuario;
    }

    public Movimentacao(int movimentacaoId, int quantidade, string justificativa, DateTime dataOperacao, string tipo)
    {
        MovimentacaoId = movimentacaoId;
        Quantidade = quantidade;
        Justificativa = justificativa;
        DataOperacao = dataOperacao;
        Tipo = tipo;
    }
}