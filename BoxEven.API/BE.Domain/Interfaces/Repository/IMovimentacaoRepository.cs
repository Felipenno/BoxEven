using BE.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Domain.Interfaces.Repository;

public interface IMovimentacaoRepository
{
    Task<bool> AdicionarAsync(Movimentacao movimentacao);
    Task<Movimentacao> ListarPorIdAsync(int id);
    Task<List<Movimentacao>> ListarTodosAsync();
}
