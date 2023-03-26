namespace BE.Domain.Entities;

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
    public int? UnidadeMedidaId { get; private set; }
    public UnidadeMedida? UnidadeMedida { get; private set; }

    public List<Localizacao>? Localizacoes { get; private set; }



    public Produto(int produtoId, bool? ativo, int? quantidade, decimal? preco, string? nome, string? imagemTipo, string? imagemNome, string? codigoBarras, DateTime? atualizacao, int? unidadeMedidaId) //atualizar
    {
        ProdutoId = produtoId;
        Ativo = ativo;
        Quantidade = quantidade;
        Preco = preco;
        Nome = nome;
        ImagemTipo = imagemTipo;
        ImagemNome = imagemNome;
        CodigoBarras = codigoBarras;
        Atualizacao = atualizacao;
        UnidadeMedidaId = unidadeMedidaId;
    }

    public Produto(int produtoId, bool? ativo, int? quantidade, decimal? preco, string? nome, string? imagemTipo, string? imagemNome, string? codigoBarras, DateTime? criacao, DateTime? atualizacao, UnidadeMedida? unidadeMedida = null, List<Localizacao>? localizacoes = null) //listar
    {
        ProdutoId = produtoId;
        Ativo = ativo;
        Quantidade = quantidade;
        Preco = preco;
        Nome = nome;
        ImagemTipo = imagemTipo;
        ImagemNome = imagemNome;
        CodigoBarras = codigoBarras;
        Atualizacao = atualizacao;
        UnidadeMedida = unidadeMedida;
        Criacao = criacao;
        Localizacoes = localizacoes;
    }

    public Produto(int produtoId, bool? ativo, int? quantidade, decimal? preco, string? nome, string? imagemTipo, string? imagemNome, string? codigoBarras, DateTime? criacao, DateTime? atualizacao) //listar dapper
    {
        ProdutoId = produtoId;
        Ativo = ativo;
        Quantidade = quantidade;
        Preco = preco;
        Nome = nome;
        ImagemTipo = imagemTipo;
        ImagemNome = imagemNome;
        CodigoBarras = codigoBarras;
        Atualizacao = atualizacao;
        Criacao = criacao;
    }

    public Produto(bool? ativo, int? quantidade, decimal? preco, string? nome, string? imagemTipo, string? imagemNome, string? codigoBarras, DateTime? criacao, DateTime? atualizacao, int? unidadeMedidaId)//adicionar
    {
        Ativo = ativo;
        Quantidade = quantidade;
        Preco = preco;
        Nome = nome;
        ImagemTipo = imagemTipo;
        ImagemNome = imagemNome;
        CodigoBarras = codigoBarras;
        Criacao = criacao;
        Atualizacao = atualizacao;
        UnidadeMedidaId = unidadeMedidaId;
    }

    public Produto(int produtoId, int? quantidade, DateTime? atualizacao)//alterar quantidade
    {
        ProdutoId = produtoId;
        Quantidade = quantidade;
        Atualizacao = atualizacao;
    }

    public LocalizacoesIO TrocarLocalizacoes(List<int> localizacaoIds)
    {
        LocalizacoesIO localizacoesIO = new LocalizacoesIO();
        HashSet<int> novaLocalizacoes = null;
        HashSet<int> localizacoesAtuais = null;

        if (localizacaoIds != null && localizacaoIds.Count > 0 && Localizacoes == null || Localizacoes.Count < 1)
        {
            novaLocalizacoes = localizacaoIds.ToHashSet();
            localizacoesIO.LocalizacoesAdicionadas = novaLocalizacoes.ToList();

            return localizacoesIO;
        }

        if(Localizacoes != null && Localizacoes.Count > 0 && localizacaoIds == null || localizacaoIds.Count < 1)
        {
            localizacoesAtuais = new HashSet<int>();

            foreach (var local in Localizacoes)
            {
                localizacoesAtuais.Add(local.LocalizacaoId);
            }

            localizacoesIO.LocalizacoesRemovidas = localizacoesAtuais.ToList();

            return localizacoesIO;
        }
   
        novaLocalizacoes = localizacaoIds.ToHashSet();
        localizacoesAtuais = new HashSet<int>();

        foreach(var local in Localizacoes)
        {
            localizacoesAtuais.Add(local.LocalizacaoId);
        }

        novaLocalizacoes.ExceptWith(localizacoesAtuais);
        localizacoesIO.LocalizacoesAdicionadas = novaLocalizacoes.ToList();

        novaLocalizacoes = localizacaoIds.ToHashSet();

        localizacoesAtuais.ExceptWith(novaLocalizacoes);
        localizacoesIO.LocalizacoesRemovidas = localizacoesAtuais.ToList();

        return localizacoesIO;
    }
}
