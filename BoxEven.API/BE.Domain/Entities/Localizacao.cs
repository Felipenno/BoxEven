namespace BE.Domain.Entities;

public class Localizacao
{
    public int LocalizacaoId { get; private set; }
    public string Andar { get; private set; }
    public int Corredor { get; private set; }
    public char Lado { get; private set; }
    public int Prateleira { get; private set; }
    public int Vao { get; private set; }
    public int? ProdutoId { get; private set; }
    public Produto? Produto { get; private set; }

    public Localizacao(int localizacaoId, string andar, int corredor, char lado, int prateleira, int vao, int? produtoId = null, Produto? produto = null)
    {
        LocalizacaoId = localizacaoId;
        Andar = andar;
        Corredor = corredor;
        Lado = lado;
        Prateleira = prateleira;
        Vao = vao;
        ProdutoId = produtoId;
        Produto = produto;
    }

    public Localizacao(string andar, int corredor, char lado, int prateleira, int vao, int? produtoId = null, Produto? produto = null)
    {
        Andar = andar;
        Corredor = corredor;
        Lado = lado;
        Prateleira = prateleira;
        Vao = vao;
        ProdutoId = produtoId;
        Produto = produto;
    }

    public Localizacao(int localizacaoId, string andar, int corredor, char lado, int prateleira, int vao)
    {
        LocalizacaoId = localizacaoId;
        Andar = andar;
        Corredor = corredor;
        Lado = lado;
        Prateleira = prateleira;
        Vao = vao;
    }

    public Localizacao(int localizacaoId, int? produtoId, string andar, int corredor, char lado, int prateleira, int vao)
    {
        LocalizacaoId = localizacaoId;
        Andar = andar;
        Corredor = corredor;
        Lado = lado;
        Prateleira = prateleira;
        Vao = vao;
        ProdutoId = produtoId;

    }

    public string MontarEndereco()
    {
        return $"{Andar.Trim()} - {Corredor} - {Lado} - {Prateleira} - {Vao}";
    }

    public bool EnderecoIgual(Localizacao localizacao)
    {
        if (
                LocalizacaoId == localizacao.LocalizacaoId
                && Andar == localizacao.Andar
                && Corredor == localizacao.Corredor
                && Lado == localizacao.Lado
                && Prateleira == localizacao.Prateleira
                && Vao == localizacao.Vao
            )
            return true;
        else return false;
    }
}

