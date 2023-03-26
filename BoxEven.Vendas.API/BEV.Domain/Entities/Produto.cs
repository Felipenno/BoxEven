namespace BEV.Domain.Entities;

public class Produto
{
    public int ProdutoId { get; private set; }
    public bool? Ativo { get; private set; }
    public int? Quantidade { get; private set; }
    public decimal? Preco { get; private set; }
    public string? Nome { get; private set; }
    public string? ImagemTipo { get; private set; }
    public string? ImagemNome { get; private set; }
    public string? CodigoBarras { get; private set; }
    public DateTime? Criacao { get; private set; }
    public DateTime? Atualizacao { get; private set; }
    public UnidadeMedida? UnidadeMedida { get; private set; }

    public List<Localizacao>? Localizacoes { get; private set; }

    public Produto(int produtoId, bool? ativo, int? quantidade, decimal? preco, string? nome, string? imagemTipo, string? imagemNome, string? codigoBarras, DateTime? criacao, DateTime? atualizacao, UnidadeMedida? unidadeMedida, List<Localizacao>? localizacoes)
    {
        ProdutoId = produtoId;
        Ativo = ativo;
        Quantidade = quantidade;
        Preco = preco;
        Nome = nome;
        ImagemTipo = imagemTipo;
        ImagemNome = imagemNome;
        CodigoBarras = codigoBarras;
        Criacao = criacao;
        Atualizacao = atualizacao;
        UnidadeMedida = unidadeMedida;
        Localizacoes = localizacoes;
    }

    public Produto(int produtoId, bool? ativo, int? quantidade, decimal? preco, string? nome, string? imagemTipo, string? imagemNome, string? codigoBarras, DateTime? criacao, DateTime? atualizacao)
    {
        ProdutoId = produtoId;
        Ativo = ativo;
        Quantidade = quantidade;
        Preco = preco;
        Nome = nome;
        ImagemTipo = imagemTipo;
        ImagemNome = imagemNome;
        CodigoBarras = codigoBarras;
        Criacao = criacao;
        Atualizacao = atualizacao;
    }

    public void DefinirQuantidadeAleatorioPedido()
    {
        var quantidade = new Random().Next(1, Quantidade.Value);

        Quantidade = quantidade;
    }
}