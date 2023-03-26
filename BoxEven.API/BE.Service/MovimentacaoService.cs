using AutoMapper;
using BE.Domain.Dtos;
using BE.Domain.Entities;
using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Service;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IMovimentacaoRepository _movimentacaoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IProdutoRepository _produtoRepository;

    public MovimentacaoService(IMovimentacaoRepository movimentacaoRepository, IUsuarioRepository usuarioRepository, IProdutoRepository produtoRepository)
    {
        _movimentacaoRepository = movimentacaoRepository;
        _usuarioRepository = usuarioRepository;
        _produtoRepository = produtoRepository;
    }

    public async Task<ResultadoModel> ListarMovimentacaoesAsync()
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var movimentacaoDtoList = new List<MovimentacaoDto>();

        var movimentacoes = await _movimentacaoRepository.ListarTodosAsync();
        if (movimentacoes != null)
        {
            foreach (var mo in movimentacoes)
            {
                var movimentacaoDto = new MovimentacaoDto()
                {
                    MovimentacaoId = mo.MovimentacaoId,
                    Quantidade = mo.Quantidade,
                    Justificativa = mo.Justificativa,
                    DataOperacao = mo.DataOperacao,
                    Tipo = mo.Tipo,
                    Produto = $"{mo.Produto.ProdutoId} - {mo.Produto.Nome}",
                    Usuario = $"{mo.Usuario.Nome} ({mo.Usuario.Apelido})"
                };

                movimentacaoDtoList.Add(movimentacaoDto);
            }
        }

        if(movimentacaoDtoList.Count > 0)
        {
            resultadoModel.Sucesso = true;
            resultadoModel.Mensagem = "";
            resultadoModel.Objeto = movimentacaoDtoList;
            return resultadoModel;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Nenhum item encontrado.";
            return resultadoModel;
        }

    }

    public Task<ResultadoModel> ListarPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ResultadoModel> CriarMovimentacaoAsync(MovimentacaoCriarDto moDto)
    {
        ResultadoModel resultadoModel = new ResultadoModel();

        var produto = await _produtoRepository.ListarPorIdAsync(moDto.ProdutoId);
        if(produto == null) 
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Produto não encontrado.";
            return resultadoModel;
        }

        var usuario = await _usuarioRepository.ListarPorIdAsync(moDto.UsuarioId);
        if (usuario == null) 
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Usuário inválido.";
            return resultadoModel;
        }

        int quantidadeAlterada = produto.Quantidade.Value;
        if(moDto.Tipo == "ENTRADA")
        {
            quantidadeAlterada += moDto.Quantidade;
        }
        else if(moDto.Tipo == "SAIDA")
        {
            if(quantidadeAlterada < moDto.Quantidade)
            {
                resultadoModel.Sucesso = false;
                resultadoModel.Mensagem = "Quantidade do produto passada é maior que a disponível.";
                return resultadoModel;
            }

            quantidadeAlterada -= moDto.Quantidade;
        }
        else
        {
            resultadoModel.Sucesso = false;
            resultadoModel.Mensagem = "Tipo de movimentação invalido";
            return resultadoModel;
        }

        var produtoAlterado = await _produtoRepository.AlterarQuantidade(quantidadeAlterada, produto.ProdutoId, DateTime.Now);
        if(produtoAlterado)
        {
            var movimentacao = new Movimentacao(moDto.Quantidade, moDto.Justificativa, DateTime.Now, moDto.Tipo, produto, usuario);

            var conluido = await _movimentacaoRepository.AdicionarAsync(movimentacao);
            if (conluido)
            {
                resultadoModel.Sucesso = true;
                resultadoModel.Mensagem = "Movimentação criada.";
                return resultadoModel;
            }
        }

        resultadoModel.Sucesso = false;
        resultadoModel.Mensagem = "Não foi possível criar a movimentação.";
        return resultadoModel;

    }
}