using BE.Domain.Interfaces.Repository;
using BE.Domain.Interfaces;
using BE.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using BE.Domain.Entities;
using BE.Domain.Dtos;
using System.Reflection;

namespace BE.Test.Services
{
    public class ProdutoServiceTest
    {
        private ProdutoService _produtoService;
        private Mock<IProdutoRepository> _produtoRepositoryMock;
        private Mock<ILocalizacaoRepository> _localizacaoRepositoryMock;
        private Mock<IArquivosService> _arquivosServiceMock;

        public ProdutoServiceTest()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _localizacaoRepositoryMock = new Mock<ILocalizacaoRepository>();
            _arquivosServiceMock = new Mock<IArquivosService>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object, _localizacaoRepositoryMock.Object, _arquivosServiceMock.Object);
        }

        [Fact]
        public void ListarProdutos()
        {

        }

        [Fact]
        public async void ListarProdutosDesalocados()
        {
            //Arrange - preparação, objestos de uso

            var produtos = new List<Produto>()
            {
                new Produto(3, true, 55, 10.55M, "produto", "", "", "", DateTime.Now, DateTime.Now, new UnidadeMedida(4, "medida"), null)
            };

            var produtoDtoList = new List<ProdutoListarDto>()
            {
                new ProdutoListarDto
                {
                   ProdutoId = 3,
                    Ativo = true,
                    Quantidade = 55,
                    Preco = 10.55M,
                    Nome = "produto",
                    Imagem = "",
                    CodigoBarras = "",
                    Criacao = DateTime.Now,
                    Atualizacao = DateTime.Now,
                    UnidadeMedida = new UnidadeMedidaDto()
                    {
                        UnidadeMedidaId = 4,
                        Descricao = "medida"
                    }
                }
            };

            var resultadoModel = new ResultadoModel()
            {
                Mensagem = "",
                Sucesso = true,
                Objeto = produtoDtoList
                
            };

            _produtoRepositoryMock.Setup(x => x.ListarTodosDesalocadosAsync()).Returns(Task.FromResult(produtos));
            _arquivosServiceMock.Setup(x => x.RecuperarImagemProduto("", "")).Returns(Task.FromResult(""));

            //Act - Ação
            var resultModel = await _produtoService.ListarProdutosDesalocadosAsync();
            var listProdutos = resultadoModel.Objeto as List<ProdutoListarDto>;

            //Assert - verificação da condição, validação
            Assert.NotNull(resultModel);

            Assert.NotNull(listProdutos);
            Assert.Equal(produtoDtoList[0].ProdutoId, listProdutos[0].ProdutoId);

            _produtoRepositoryMock.Verify(x => x.ListarTodosDesalocadosAsync(), Times.Once());
            _arquivosServiceMock.Verify(x => x.RecuperarImagemProduto("", ""), Times.Once());
        }

        [Fact]
        public void ListarPorId()
        {

        }

        [Fact]
        public void CriarProduto()
        {

        }

        [Fact]
        public void AtualizarProduto()
        {

        }

    }
}