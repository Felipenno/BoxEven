namespace BEV.Domain.Entities;

public class Localizacao
{
    public int LocalizacaoId { get; private set; }
    public string Andar { get; private set; }
    public int Corredor { get; private set; }
    public char Lado { get; private set; }
    public int Prateleira { get; private set; }
    public int Vao { get; private set; }

    public Localizacao(int localizacaoId, string andar, int corredor, char lado, int prateleira, int vao)
    {
        LocalizacaoId = localizacaoId;
        Andar = andar;
        Corredor = corredor;
        Lado = lado;
        Prateleira = prateleira;
        Vao = vao;
    }
}