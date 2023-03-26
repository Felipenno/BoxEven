using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces
{
    public interface IArquivosService
    {
        Task<string> RecuperarImagemProduto(string imagemNome, string imagemTipo);
        void RemoverImagemProduto(string imagemNome);
        void TrocarImagemProduto(string caminhoImagem, string base64Imagem , string imagemNomeExistente);
        void CriarImagemProduto(string caminhoImagem, string base64Imagem);
    }
}
