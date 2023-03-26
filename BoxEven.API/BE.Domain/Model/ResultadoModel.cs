namespace BE.Domain.Entities;

public class ResultadoModel
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public object? Objeto { get; set; }

    public ResultadoModel()
    {
        Sucesso = false;
        Mensagem = "";
        Objeto = null;
    }
}