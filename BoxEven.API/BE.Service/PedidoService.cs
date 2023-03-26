using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Enum;
using BE.Domain.Interfaces;
using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Service;

public class PedidoService : IPedidoService
{
    private readonly IVendasInfraServices _vendasContext;
    private readonly IArquivosService _arquivosService;
    private readonly IMovimentacaoService _movimentacoService;

    public PedidoService(IVendasInfraServices vendasContext, IArquivosService arquivosService, IMovimentacaoService movimentacoService)
    {
        _arquivosService = arquivosService;
        _vendasContext = vendasContext;
        _movimentacoService = movimentacoService;
    }

    public async Task<ResultadoModel> AtualizarStatus(PedidoAlterarStatusDto pedido)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        if (pedido.StatusPedido == StatusPedido.Concluido)
        {
            if (pedido == null || pedido.Produtos == null || pedido.Produtos.Count <= 0)
            {
                resultadoModel.Sucesso = false;
                resultadoModel.Mensagem = "Pedido com informações incompletas.";
                return resultadoModel;
            }

            var movimentacaoDto = new MovimentacaoCriarDto()
            {
                UsuarioId = pedido.IdUsuario,
                Justificativa = $"Saída automática: {pedido.NumeroPedido}",
                Tipo = "SAIDA"
            };

            bool concluido = false;

            foreach (var item in pedido.Produtos)
            {
                movimentacaoDto.ProdutoId = item.IdProduto;
                movimentacaoDto.Quantidade = item.Quantidade;
                var resultado = await _movimentacoService.CriarMovimentacaoAsync(movimentacaoDto);
                concluido = resultado.Sucesso;
            }

            if (concluido)
            {
                await _vendasContext.AlterarStatusPedidoAsync(pedido.IdPedido, pedido.StatusPedido);

                resultadoModel.Sucesso = true;
                resultadoModel.Mensagem = "Pedido Conluido.";
                return resultadoModel;
            }
        }

        if (pedido.StatusPedido == StatusPedido.Cancelado)
        {
            if (pedido == null)
            {
                resultadoModel.Sucesso = false;
                resultadoModel.Mensagem = "Pedido com informações incompletas.";
                return resultadoModel;
            }

            await _vendasContext.AlterarStatusPedidoAsync(pedido.IdPedido, pedido.StatusPedido);
            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "Pedido Cancelado.";
            return resultadoModel;
        }

        resultadoModel.Sucesso = false;
        resultadoModel.Mensagem = "Não foi possível pocessar o pedido.";
        return resultadoModel;
    }

    public async Task<ResultadoModel> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var pedidosDto = new List<PedidoDto>();

        var pedidos = await _vendasContext.ListarPedidosPorFiltroAsync(status, conclusao);
        if (pedidos != null && pedidos.Count > 0)
        {
            foreach (var pedido in pedidos)
            {
                var pedidoDto = new PedidoDto
                {
                    Id = pedido.Id,
                    Numero = pedido.Numero,
                    Vendedor = pedido.Vendedor,
                    Status = pedido.Status,
                    Criacao = pedido.Criacao,
                    Conclusao = pedido.Conclusao,
                    Produtos = new List<ProdutoListarDto>()
                };

                foreach (var produto in pedido.Produtos)
                {
                    var produtoDto = new ProdutoListarDto()
                    {
                        ProdutoId = produto.ProdutoId,
                        Ativo = produto.Ativo,
                        Nome = produto.Nome,
                        Quantidade = produto.Quantidade,
                        Preco = produto.Preco,
                        CodigoBarras = produto.CodigoBarras,
                        Imagem = await _arquivosService.RecuperarImagemProduto(produto.ImagemNome, produto.ImagemTipo),
                        Criacao = produto.Criacao,
                        Atualizacao = produto.Atualizacao,
                        UnidadeMedida = new UnidadeMedidaDto() { UnidadeMedidaId = produto.UnidadeMedida.UnidadeMedidaId, Descricao = produto.UnidadeMedida.Descricao },
                        Localizacoes = new List<LocalizacaoEnderecoMontadoDto>()
                    };

                    if (produto != null && produto.Localizacoes != null)
                    {
                        foreach (var local in produto.Localizacoes)
                        {
                            var localizacaoDto = new LocalizacaoEnderecoMontadoDto()
                            {
                                LocalizacaoId = local.LocalizacaoId,
                                Endereco = local.MontarEndereco()
                            };

                            produtoDto.Localizacoes.Add(localizacaoDto);
                        }
                    }
                        

                    pedidoDto.Produtos.Add(produtoDto);
                }

                pedidosDto.Add(pedidoDto);
            }
        }

        resultadoModel.Sucesso = true;
        resultadoModel.Mensagem = "";
        resultadoModel.Objeto = pedidosDto;
        return resultadoModel;
    }

    public async Task<ResultadoModel> ListasPedidosSeparacao()
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var pedidosDto = new List<PedidoDto>();

        var pedidos = await _vendasContext.ListarPedidos();

        if (pedidos != null && pedidos.Count > 0)
        {
            foreach (var pedido in pedidos)
            {
                var pedidoDto = new PedidoDto
                {
                    Id = pedido.Id,
                    Numero = pedido.Numero,
                    Vendedor = pedido.Vendedor,
                    Status = pedido.Status,
                    Criacao = pedido.Criacao,
                    Conclusao = pedido.Conclusao,
                    Produtos = new List<ProdutoListarDto>()
                };

                foreach (var produto in pedido.Produtos)
                {
                    var produtoDto = new ProdutoListarDto()
                    {
                        ProdutoId = produto.ProdutoId,
                        Ativo = produto.Ativo,
                        Nome = produto.Nome,
                        Quantidade = produto.Quantidade,
                        Preco = produto.Preco,
                        CodigoBarras = produto.CodigoBarras,
                        Imagem = await _arquivosService.RecuperarImagemProduto(produto.ImagemNome, produto.ImagemTipo),
                        Criacao = produto.Criacao,
                        Atualizacao = produto.Atualizacao,
                        UnidadeMedida = new UnidadeMedidaDto() { UnidadeMedidaId = produto.UnidadeMedida.UnidadeMedidaId, Descricao = produto.UnidadeMedida.Descricao },
                        Localizacoes = new List<LocalizacaoEnderecoMontadoDto>()
                    };

                    if (produto != null && produto.Localizacoes != null)
                    {
                        foreach (var local in produto.Localizacoes)
                        {
                            var localizacaoDto = new LocalizacaoEnderecoMontadoDto()
                            {
                                LocalizacaoId = local.LocalizacaoId,
                                Endereco = local.MontarEndereco()
                            };

                            produtoDto.Localizacoes.Add(localizacaoDto);
                        }
                    }
                        

                    pedidoDto.Produtos.Add(produtoDto);
                }

                pedidosDto.Add(pedidoDto);
            }
        }

        resultadoModel.Sucesso = true;
        resultadoModel.Mensagem = "";
        resultadoModel.Objeto = pedidosDto;
        return resultadoModel;
    }
}
