using Domain.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.infraestrcuture
{
    public interface IPedidoRepository
    {
        Task<int> AddPedido(Pedido pedido);
        Task<List<Pedido>> GetPedidos();
        Task UpdatePedido(Pedido pedido);
        Task CreateHistoryPedido(HistorialEstado historialPedido);

    }
}
