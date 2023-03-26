using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Infra.EfCore.Context;
using Microsoft.EntityFrameworkCore;

namespace BE.Infra.EfCore.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly BoxEvenContext _boxEvenContext;

    public ProdutoRepository(BoxEvenContext boxEvenContext)
    {
        _boxEvenContext = boxEvenContext;
    }

    public async Task<bool> AdicionarAsync(Produto produto)
    {
        await _boxEvenContext.Produtos.AddAsync(produto);

        int linhasAfetadas = await _boxEvenContext.SaveChangesAsync();
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> AlterarQuantidade(int quantidade, int id, DateTime dataAtualizacao)
    {
        var produto = new Produto(id, quantidade, dataAtualizacao);

        _boxEvenContext.Entry(produto).Property(p => new { p.Quantidade, p.Atualizacao }).IsModified = true;

        int linhasAfetadas = await _boxEvenContext.SaveChangesAsync();
        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> AtualizarAsync(Produto produto)
    {
        int linhasAfetadas = await _boxEvenContext.Database.ExecuteSqlInterpolatedAsync(
                              $@"
                                UPDATE [BD_BOXEVEN].[dbo].[TB_PRODUTO] 
                                SET 
                                   [FK_IDUNIDADE_MEDIDA] = IsNull({produto.UnidadeMedidaId}, [FK_IDUNIDADE_MEDIDA])
                                  ,[ATIVO] =  IsNull({produto.Ativo}, [ATIVO])
                                  ,[QUANTIDADE] = IsNull({produto.Quantidade}, [QUANTIDADE])
                                  ,[PRECO] = IsNull({produto.Preco}, [PRECO])
                                  ,[NOME] = IsNull({produto.Nome}, [DESCRICAO])
                                  ,[IMAGEM_TIPO] = IsNull({produto.ImagemTipo}, [IMAGEM_TIPO])
                                  ,[IMAGEM_NOME] = IsNull({produto.ImagemNome}, [IMAGEM_NOME])
                                  ,[CODIGO_BARRAS] = IsNull({produto.CodigoBarras}, [CODIGO_BARRAS])
                                  ,[DATA_ATUALIZACAO] = IsNull({produto.Atualizacao}, [DATA_ATUALIZACAO])
                                WHERE ID_PRODUTO = {produto.ProdutoId};
                              ");

        if (linhasAfetadas <= 0)
        {
            return false;
        }

        return true;

        //=================================================================
        //var prodAtual = await ListarPorIdAsync(produto.ProdutoId);

        //var pprodModificado = new Produto(
        //    produto.ProdutoId,
        //    produto.Ativo ?? prodAtual.Ativo,
        //    produto.Quantidade ?? prodAtual.Quantidade,
        //    produto.Preco ?? prodAtual.Preco,
        //    produto.ProdutoDescricao ?? prodAtual.ProdutoDescricao,
        //    produto.Imagem ?? prodAtual.Imagem,
        //    produto.CodigoBarras ?? prodAtual.CodigoBarras,
        //    produto.Criacao ?? prodAtual.Criacao,
        //    produto.Atualizacao ?? prodAtual.Atualizacao,
        //    produto.UnidadeMedidaId ?? prodAtual.UnidadeMedidaId
        //    );

        //_boxEvenContext.Produtos.Update(pprodModificado);


        //============================================================
        //_boxEvenContext.Entry(produto).State = EntityState.Modified;
        //var produtoEntry = _boxEvenContext.Entry(produto);

        //if (produto.Ativo == null)
        //{
        //    produtoEntry.Property(x => x.Ativo).IsModified = false;
        //}

        //if (produto.Quantidade == null)
        //{
        //    produtoEntry.Property(x => x.Quantidade).IsModified = false;
        //}

        //if (produto.Preco == null)
        //{
        //    produtoEntry.Property(x => x.Preco).IsModified = false;
        //}

        //if (produto.ProdutoDescricao == null)
        //{
        //    produtoEntry.Property(x => x.ProdutoDescricao).IsModified = false;
        //}

        //if (produto.Imagem == null)
        //{
        //    produtoEntry.Property(x => x.Imagem).IsModified = false;
        //}

        //if (produto.CodigoBarras == null)
        //{
        //    produtoEntry.Property(x => x.CodigoBarras).IsModified = false;
        //}

        //if (produto.Criacao == null)
        //{
        //    produtoEntry.Property(x => x.Criacao).IsModified = false;
        //}

        //if (produto.Atualizacao == null)
        //{
        //    produtoEntry.Property(x => x.Atualizacao).IsModified = false;
        //}

        //if (produto.UnidadeMedidaId == null)
        //{
        //    produtoEntry.Property(x => x.UnidadeMedidaId).IsModified = false;
        //}

        //======================================================

        //dbSet.Attach(entity);
        //dbContext.Entry(entity).State = EntityState.Modified;

        //var entry = dbContext.Entry(entity);

        //Type type = typeof(TEntity);
        //PropertyInfo[] properties = type.GetProperties();
        //foreach (PropertyInfo property in properties)
        //{
        //    if (property.GetValue(entity, null) == null)
        //    {
        //        entry.Property(property.Name).IsModified = false;
        //    }
        //}
    }

    public async Task<Produto> ListarPorIdAsync(int id)
    {
        return await _boxEvenContext.Produtos.Where(x => x.ProdutoId == id).Include(x => x.UnidadeMedida).Include(x => x.Localizacoes).AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<List<Produto>> ListarTodosAsync(int itensPagina, int pagina, bool? status, int? id, string? nomeProduto)
    {
        var produtoQuery = _boxEvenContext.Produtos.AsQueryable();
        if (status != null)
        {
            return await produtoQuery.Where(x => x.Ativo == status).Include(x => x.UnidadeMedida).Include(x => x.Localizacoes).AsNoTracking().ToListAsync();
        }

        return await produtoQuery.Include(x => x.UnidadeMedida).Include(x => x.Localizacoes).AsNoTracking().Skip((pagina - 1) * itensPagina).Take(itensPagina).ToListAsync();
    }

    public Task<List<Produto>> ListarTodosDesalocadosAsync()
    {
        throw new NotImplementedException();
    }
}