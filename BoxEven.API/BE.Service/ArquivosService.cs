using BE.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Service
{
    public class ArquivosService : IArquivosService
    {
        public void CriarImagemProduto(string caminhoImagem, string base64Imagem)
        {
            byte[] imagemBytes = Convert.FromBase64String(base64Imagem);

            using (var fileStream = new FileStream(caminhoImagem, FileMode.CreateNew))
            {
                for (int i = 0; i < imagemBytes.Length; i++)
                {
                    fileStream.WriteByte(imagemBytes[i]);
                }
            }
        }

        public async Task<string> RecuperarImagemProduto(string imagemNome, string imagemTipo)
        {
            string imagemBase64 = null;

            if (!(string.IsNullOrEmpty(imagemNome) || string.IsNullOrEmpty(imagemTipo)))
            {
                string caminhoImagem = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", imagemNome);

                if (File.Exists(caminhoImagem))
                {
                    byte[] imgBytes = await File.ReadAllBytesAsync(caminhoImagem);

                    imagemBase64 = imagemTipo + Convert.ToBase64String(imgBytes);

                    //using (var fileStream = new FileStream(caminhoImagem, FileMode.Open, FileAccess.Read))
                    //{
                    //    byte[] bytes = new byte[fileStream.Length];
                    //    int numBytesToRead = (int)fileStream.Length;
                    //    int numBytesRead = 0;
                    //    while (numBytesToRead > 0)
                    //    {
                    //        // Read may return anything from 0 to numBytesToRead.
                    //        int n = fileStream.Read(bytes, numBytesRead, numBytesToRead);

                    //        // Break when the end of the file is reached.
                    //        if (n == 0)
                    //            break;

                    //        numBytesRead += n;
                    //        numBytesToRead -= n;
                    //    }

                    //    imagemBase64 = metaDados + Convert.ToBase64String(bytes);
                    //}
                }
            }

            return imagemBase64;
        }

        public void RemoverImagemProduto(string imagemNome)
        {
            string caminhoImagemAnterior = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", imagemNome);

            if (File.Exists(caminhoImagemAnterior))
            {
                File.Delete(caminhoImagemAnterior);
            }
        }

        public void TrocarImagemProduto(string caminhoImagem, string base64Imagem, string imagemNomeExistente)
        {
            if (!string.IsNullOrEmpty(imagemNomeExistente))
            {
                string caminhoImagemAnterior = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", imagemNomeExistente);

                if (File.Exists(caminhoImagemAnterior))
                {
                    File.Delete(caminhoImagemAnterior);
                }
            }

            byte[] imagemBytes = Convert.FromBase64String(base64Imagem);

            using (var fileStream = new FileStream(caminhoImagem, FileMode.CreateNew))
            {
                for (int i = 0; i < imagemBytes.Length; i++)
                {
                    fileStream.WriteByte(imagemBytes[i]);
                }
            }
        }
    }
}
