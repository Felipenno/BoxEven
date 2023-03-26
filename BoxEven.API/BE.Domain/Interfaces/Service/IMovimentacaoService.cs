using BE.Domain.Dtos;
using BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Service;

public interface IMovimentacaoService
{
    Task<ResultadoModel> CriarMovimentacaoAsync(MovimentacaoCriarDto movimentacao);
    Task<ResultadoModel> ListarPorIdAsync(int id);
    Task<ResultadoModel> ListarMovimentacaoesAsync();
}
