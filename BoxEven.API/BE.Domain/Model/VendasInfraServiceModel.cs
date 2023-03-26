using BE.Domain.Entities;
using BE.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Model;

public class VendasInfraServiceModel
{
    public string? Id { get; set; }
    public int Numero { get; set; }
    public string Vendedor { get; set; }
    public StatusPedido Status { get; set; }
    public DateTime Criacao { get; set; }
    public DateTime Conclusao { get; set; }
    public List<ProdutoModel> Produtos { get; set; }

    public static List<Pedido> ToPedidoEntityList(List<VendasInfraServiceModel> pedidoModel)
    {
        var pedidos = new List<Pedido>();

        if (pedidoModel != null && pedidoModel.Count > 0)
        foreach (var pm in pedidoModel)
        {
            var produtos = new List<Produto>();

            foreach (var p in pm.Produtos)
            {
                var localizacoes = new List<Localizacao>();

                if (p.Localizacoes != null && p.Localizacoes.Count > 0)
                foreach (var lm in p.Localizacoes)
                {
                    localizacoes.Add(new Localizacao(lm.LocalizacaoId, lm.Andar, lm.Corredor, lm.Lado, lm.Prateleira, lm.Vao));
                }

                produtos.Add(new Produto(p.ProdutoId, p.Ativo, p.Quantidade, p.Preco, p.Nome, p.ImagemTipo, p.ImagemNome, p.CodigoBarras, p.Criacao, p.Atualizacao, new UnidadeMedida(p.UnidadeMedida.UnidadeMedidaId, p.UnidadeMedida.Descricao), localizacoes));
            }

            pedidos.Add(new Pedido(pm.Id, pm.Numero, pm.Vendedor, pm.Status, pm.Criacao, pm.Conclusao, produtos));
        }

        return pedidos;
    }
}

public class ProdutoModel
{
    public int ProdutoId { get; set; }
    public bool? Ativo { get; set; }
    public int? Quantidade { get; set; }
    public decimal? Preco { get; set; }
    public string? Nome { get; set; }
    public string? ImagemTipo { get; set; }
    public string? ImagemNome { get; set; }
    public string? CodigoBarras { get; set; }
    public DateTime? Criacao { get; set; }
    public DateTime? Atualizacao { get; set; }
    public UnidadeMedidaModel? UnidadeMedida { get; set; }

    public List<LocalizacaoModel>? Localizacoes { get; set; }
}

public class LocalizacaoModel
{
    public int LocalizacaoId { get; set; }
    public string Andar { get; set; }
    public int Corredor { get; set; }
    public char Lado { get; set; }
    public int Prateleira { get; set; }
    public int Vao { get; set; }
}

public class UnidadeMedidaModel
{
    public int? UnidadeMedidaId { get; set; }
    public string? Descricao { get; set; }
}
