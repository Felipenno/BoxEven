using BEV.Domain.Entities;
using BEV.Domain.Enum;
using BEV.Domain.Interfaces.Repository;
using BEV.Domain.Interfaces.Service;
using System.Timers;

namespace BEV.Service;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public async Task AlterarStatusPedidoAsync(string id, StatusPedido status)
    {
        await _pedidoRepository.AlterarStatusPedidoAsync(id, status);
    }

    public async Task GerarPedidosAsync(object source, ElapsedEventArgs e)
    {
        var produtos = await _pedidoRepository.ListarProdutosAsync();

        var produtosSelecionados = new List<Produto>();

        var rnd = new Random();

        int numeroDeProdutos = rnd.Next(1, 4);

        for (int i = 0; i < numeroDeProdutos; i++)
        {
            var produtoIndex = rnd.Next(0, produtos.Length - 1);

            produtosSelecionados.Add(produtos[produtoIndex]);
        }

        produtosSelecionados.ForEach(p => p.DefinirQuantidadeAleatorioPedido());

        var pedino = new Pedido(GerarNumeroAleatorio(), GerarNomeVendedorAleatorio(), StatusPedido.Novo, DateTime.Now, produtosSelecionados);

        await _pedidoRepository.InserirPedidoAsync(pedino);
    }

    public async Task<List<Pedido>> ListarNovosPedidosAsync()
    {
        return await _pedidoRepository.ListarNovosPedidosAsync();
    }

    public async Task<List<Pedido>> ListarPedidosPorFiltroAsync(StatusPedido status, DateTime conclusao)
    {
        return await _pedidoRepository.ListarPedidosPorFiltroAsync(status, conclusao);
    }

    private string GerarNomeVendedorAleatorio()
    {
        string[] nomes = new string[]
        {
            "Maria Rosa",
            "Jose carlos",
            "Thiago Macedo",
            "Michael Tomas",
            "Mario Machado",
            "Tim Korez",
            "Julia Mota",
        };

        int index = new Random().Next(0, nomes.Length - 1);

        return nomes[index];
    }

    private static int GerarNumeroAleatorio()
    {
        return new Random().Next(100000, 999999);
    }
}