using Aplication.Commands;
using Aplication.Exceptions;
using Aplication.Interfaces.infraestrcuture;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.EventHandler
{
    public class DeletePedidoCommandHandler : IRequestHandler<DeletePedidoCommand, bool>
    {
        public IPedidoRepository _pedidoRepository { get; set; }
        public DeletePedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<bool> Handle(DeletePedidoCommand request, CancellationToken cancellationToken)
        {
            List<Pedido> lstPedidos =await _pedidoRepository.GetPedidos();
            var pedido = lstPedidos.Where(x => x.Id == request.Id).FirstOrDefault();
            
            if (pedido != null)
            {
                List<HistorialEstado> lstHistorialPedidos = await _pedidoRepository.GetHistoryPedidos();
                List<HistorialEstado> historialPedido = lstHistorialPedidos.Where(x => x.OrderId == pedido.Id).ToList();
                await _pedidoRepository.DeletePedidoHistory(historialPedido);
                await _pedidoRepository.DeletePedido(pedido);
                return true;
            }
            else
                throw new NotFoundException($"No se encontró un pedido con ID {request.Id}");


        }
    }
}
