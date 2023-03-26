using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Interfaces;
using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces.Service;

namespace BE.Service;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ILocalizacaoRepository _localizacaoRepository;
    private readonly IArquivosService _arquivosService;

    public ProdutoService(IProdutoRepository produtoRepository, ILocalizacaoRepository localizacaoRepository, IArquivosService arquivosService)
    {
        _produtoRepository = produtoRepository;
        _localizacaoRepository = localizacaoRepository;
        _arquivosService = arquivosService;
    }

    public async Task<ResultadoModel> ListarProdutosAsync(int itensPagina, int pagina, bool? status, int? id, string? nome)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var produtoDtoList = new List<ProdutoListarDto>();

        var produtos = await _produtoRepository.ListarTodosAsync(itensPagina, pagina, status, id, nome);
        if (produtos != null)
        {
            foreach (Produto item in produtos)
            {
                string imagemBase64 = await _arquivosService.RecuperarImagemProduto(item.ImagemNome, item.ImagemTipo);

                var produtoDto = new ProdutoListarDto()
                {
                    ProdutoId = item.ProdutoId,
                    Ativo = item.Ativo,
                    Quantidade = item.Quantidade,
                    Preco = item.Preco,
                    Nome = item.Nome,
                    Imagem = imagemBase64,
                    CodigoBarras = item.CodigoBarras,
                    Criacao = item.Criacao,
                    Atualizacao = item.Atualizacao,
                    UnidadeMedida = new UnidadeMedidaDto()
                    {
                        UnidadeMedidaId = item.UnidadeMedida.UnidadeMedidaId,
                        Descricao = item.UnidadeMedida.Descricao
                    }
                };

                if (item.Localizacoes != null && item.Localizacoes.Count > 0 && item.Localizacoes[0] != null)
                {
                    produtoDto.Localizacoes = new List<LocalizacaoEnderecoMontadoDto>();

                    foreach (var localizacao in item.Localizacoes)
                    {
                        produtoDto.Localizacoes.Add(new LocalizacaoEnderecoMontadoDto()
                        {
                            LocalizacaoId = localizacao.LocalizacaoId,
                            Endereco = localizacao.MontarEndereco()
                        });
                    }
                }

                produtoDtoList.Add(produtoDto);
            }
        }

        resultadoModel.Sucesso = true;
        resultadoModel.Mensagem = "";
        resultadoModel.Objeto = produtoDtoList;
        return resultadoModel;
    }

    public async Task<ResultadoModel> ListarProdutosDesalocadosAsync()
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var produtoDtoList = new List<ProdutoListarDto>();

        var produtos = await _produtoRepository.ListarTodosDesalocadosAsync();
        if (produtos != null)
        {
            foreach (Produto item in produtos)
            {
                string imagemBase64 = await _arquivosService.RecuperarImagemProduto(item.ImagemNome, item.ImagemTipo);

                var produtoDto = new ProdutoListarDto()
                {
                    ProdutoId = item.ProdutoId,
                    Ativo = item.Ativo,
                    Quantidade = item.Quantidade,
                    Preco = item.Preco,
                    Nome = item.Nome,
                    Imagem = imagemBase64,
                    CodigoBarras = item.CodigoBarras,
                    Criacao = item.Criacao,
                    Atualizacao = item.Atualizacao,
                    UnidadeMedida = new UnidadeMedidaDto()
                    {
                        UnidadeMedidaId = item.UnidadeMedida.UnidadeMedidaId,
                        Descricao = item.UnidadeMedida.Descricao
                    }
                };

                if (item.Localizacoes != null && item.Localizacoes.Count > 0 && item.Localizacoes[0] != null)
                {
                    produtoDto.Localizacoes = new List<LocalizacaoEnderecoMontadoDto>();

                    foreach (var localizacao in item.Localizacoes)
                    {
                        produtoDto.Localizacoes.Add(new LocalizacaoEnderecoMontadoDto()
                        {
                            LocalizacaoId = localizacao.LocalizacaoId,
                            Endereco = localizacao.MontarEndereco()
                        });
                    }
                }

                produtoDtoList.Add(produtoDto);
            }
        }

        resultadoModel.Sucesso = true;
        resultadoModel.Mensagem = "";
        resultadoModel.Objeto = produtoDtoList;
        return resultadoModel;
    }

    public async Task<ResultadoModel> ListarPorIdAsync(int produtoId)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        ProdutoListarDto produtoDto = null;

        var produto = await _produtoRepository.ListarPorIdAsync(produtoId);
        if (produto != null)
        {
            string imagemBase64 = await _arquivosService.RecuperarImagemProduto(produto.ImagemNome, produto.ImagemTipo);

            produtoDto = new ProdutoListarDto();
            produtoDto.ProdutoId = produto.ProdutoId;
            produtoDto.Ativo = produto.Ativo;
            produtoDto.Quantidade = produto.Quantidade;
            produtoDto.Preco = produto.Preco;
            produtoDto.Nome = produto.Nome;
            produtoDto.Imagem = imagemBase64;
            produtoDto.CodigoBarras = produto.CodigoBarras;
            produtoDto.Criacao = produto.Criacao;
            produtoDto.Atualizacao = produto.Atualizacao;
            produtoDto.UnidadeMedida = new UnidadeMedidaDto()
            {
                UnidadeMedidaId = produto.UnidadeMedida.UnidadeMedidaId,
                Descricao = produto.UnidadeMedida.Descricao
            };

            if (produto.Localizacoes != null && produto.Localizacoes.Count > 0 && produto.Localizacoes[0] != null)
            {
                produtoDto.Localizacoes = new List<LocalizacaoEnderecoMontadoDto>();

                foreach (var localizacao in produto.Localizacoes)
                {
                    produtoDto.Localizacoes.Add(new LocalizacaoEnderecoMontadoDto()
                    {
                        LocalizacaoId = localizacao.LocalizacaoId,
                        Endereco = localizacao.MontarEndereco()
                    });
                }
            }
        }

        resultadoModel.Sucesso = true;
        resultadoModel.Mensagem = "";
        resultadoModel.Objeto = produtoDto;
        return resultadoModel;
    }

    public async Task<ResultadoModel> CriarProdutoAsync(ProdutoCriarDto produtoDto)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        Produto produto;
        bool concluido;

        if (!string.IsNullOrEmpty(produtoDto.Imagem))
        {
            string tipoImagemDados = produtoDto.Imagem.Split(',').First() + ",";
            string tipoImagem = "." + tipoImagemDados.Split(';').First().Split('/').Last();
            string base64Imagem = produtoDto.Imagem.Split("base64,").Last();
            string nomeProduto = new string(produtoDto.Nome.Take(10).ToArray()).ToLower().Replace(" ", "");
            string nomeImagem = $"{nomeProduto}{DateTime.Now:yymmssfff}{tipoImagem}";
            string caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", nomeImagem);

            produto = new Produto(produtoDto.Ativo, produtoDto.Quantidade, produtoDto.Preco, produtoDto.Nome, tipoImagemDados, nomeImagem, produtoDto.CodigoBarras, DateTime.Now, DateTime.Now, produtoDto.UnidadeMedidaId);

            concluido = await _produtoRepository.AdicionarAsync(produto);

            if (concluido)
            {
                _arquivosService.CriarImagemProduto(caminhoImagem, base64Imagem);

                resultadoModel.Sucesso = true;
                resultadoModel.Mensagem = "Produto criado com sucesso.";
                resultadoModel.Objeto = produtoDto.Nome;
                return resultadoModel;
            }
            else
            {
                resultadoModel.Sucesso = false;
                resultadoModel.Mensagem = "Não foi possível adicionar o produto";
                return resultadoModel;
            }
        }

        produto = new Produto(produtoDto.Ativo, produtoDto.Quantidade, produtoDto.Preco, produtoDto.Nome, null, null, produtoDto.CodigoBarras, DateTime.Now, DateTime.Now, produtoDto.UnidadeMedidaId);

        concluido = await _produtoRepository.AdicionarAsync(produto);
        if (concluido)
        {
            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "Produto criado com sucesso.";
            resultadoModel.Objeto = produtoDto.Nome;
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Não foi possível adicionar o produto";
            return resultadoModel;
        }
    }

    public async Task<ResultadoModel> AtualizarProdutoAsync(ProdutoEditarDto produtoDto, int id)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var produtoExistente = await _produtoRepository.ListarPorIdAsync(id);
        if (produtoExistente == null || produtoExistente.ProdutoId != produtoDto.ProdutoId)
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Produto invalido.";
            return resultadoModel;
        }

        Produto produto;
        bool concluido;

        if (!string.IsNullOrEmpty(produtoDto.Imagem))
        {
            string tipoImagemDados = produtoDto.Imagem.Split(',').First() + ",";
            string tipoImagem = "." + tipoImagemDados.Split(';').First().Split('/').Last();
            string base64Imagem = produtoDto.Imagem.Split("base64,").Last();
            string nomeProduto = new string(produtoDto.Nome.Take(10).ToArray()).ToLower().Replace(" ", "");
            string nomeImagem = $"{nomeProduto}{DateTime.Now:yymmssfff}{tipoImagem}";
            string caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", nomeImagem);

            produto = new Produto(produtoDto.ProdutoId, produtoDto.Ativo, produtoDto.Quantidade, produtoDto.Preco, produtoDto.Nome, tipoImagemDados, nomeImagem, produtoDto.CodigoBarras, DateTime.Now, produtoDto.UnidadeMedidaId);

            concluido = await _produtoRepository.AtualizarAsync(produto);

            if (concluido)
            {
                _arquivosService.TrocarImagemProduto(caminhoImagem, base64Imagem, produtoExistente.ImagemNome);

                if (produtoExistente.Localizacoes != null || produtoDto.Localizacoes != null)
                {
                    var localizacoesIO = produtoExistente.TrocarLocalizacoes(produtoDto.Localizacoes);

                    if (localizacoesIO.LocalizacoesAdicionadas != null)
                    {
                        await _localizacaoRepository.AlocarProdutoAsync(localizacoesIO.LocalizacoesAdicionadas, produtoDto.ProdutoId);
                    }

                    if (localizacoesIO.LocalizacoesRemovidas != null)
                    {
                        await _localizacaoRepository.DesalocarProdutoAsync(localizacoesIO.LocalizacoesRemovidas);
                    }
                }

                resultadoModel.Sucesso = true;
                resultadoModel.Mensagem = "Produto atualizado.";
                return resultadoModel;
            }
            else
            {
                resultadoModel.Sucesso = false;
                resultadoModel.Mensagem = "Erro ao atualizar produto.";
                return resultadoModel;
            }
        }

        produto = new Produto(produtoDto.ProdutoId, produtoDto.Ativo, produtoDto.Quantidade, produtoDto.Preco, produtoDto.Nome, null, null, produtoDto.CodigoBarras, DateTime.Now, produtoDto.UnidadeMedidaId);

        concluido = await _produtoRepository.AtualizarAsync(produto);
        if (concluido)
        {
            if (!(string.IsNullOrEmpty(produtoExistente.ImagemNome) || string.IsNullOrEmpty(produtoExistente.ImagemTipo)) && string.IsNullOrEmpty(produtoDto.Imagem))
            {
                _arquivosService.RemoverImagemProduto(produtoExistente.ImagemNome);
            }

            if (produtoExistente.Localizacoes != null || produtoDto.Localizacoes != null)
            {
                var localizacoesIO = produtoExistente.TrocarLocalizacoes(produtoDto.Localizacoes);

                if (localizacoesIO.LocalizacoesAdicionadas != null)
                {
                    await _localizacaoRepository.AlocarProdutoAsync(localizacoesIO.LocalizacoesAdicionadas, produtoDto.ProdutoId);
                }

                if (localizacoesIO.LocalizacoesRemovidas != null)
                {
                    await _localizacaoRepository.DesalocarProdutoAsync(localizacoesIO.LocalizacoesRemovidas);
                }
            }

            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "Produto atualizado.";
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Erro ao atualizar produto.";
            return resultadoModel;
        }
    }
}