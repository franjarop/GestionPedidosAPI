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
    public class UpdatePedidoCommandHandler : IRequestHandler<UpdatePedidoCommand, bool>
    {
        public IPedidoRepository _pedidoRepository { get; set; }
        public UpdatePedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<bool> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
        {

            List<Pedido> lstPedidos = await _pedidoRepository.GetPedidos();
            var pedido = lstPedidos.Where(x => x.Id == request.id).FirstOrDefault();
            if (pedido != null)
            {
                if (pedido.Status == EstadoPedido.Entregado)
                    throw new InvalidOperationException($"El pedido no puedo ser actualizado por que ya fue entregado");
                if (pedido.Status == EstadoPedido.Cancelado)
                    throw new InvalidOperationException($"El pedido no puedo ser actualizado por que ya fue cancelado");


                if (!permiteActualizar(pedido.Status, (EstadoPedido)request.statusId))
                    throw new InvalidOperationException($"No se puede cambiar de '{pedido.Status}' a '{request.statusId}'");
                else
                {
                    HistorialEstado historial = new HistorialEstado();
                    historial.OrderId = request.id;
                    historial.PreviousStatus = pedido.Status;
                    historial.NewStatus = (EstadoPedido)request.statusId;
                    historial.ChangedAt = DateTime.Now;
                    await _pedidoRepository.CreateHistoryPedido(historial);

                    pedido.Status = (EstadoPedido)request.statusId;
                    pedido.UpdatedAt = DateTime.Now;
                    await _pedidoRepository.UpdatePedido(pedido);
                }
                return true;
            }
            else
                throw new NotFoundException($"No se encontró un pedido con ID {request.id}");

        }

        private bool permiteActualizar(EstadoPedido estadoActual, EstadoPedido nuevoEstado)
        {
            if (estadoActual == EstadoPedido.Pendiente)
            {
                return nuevoEstado == EstadoPedido.Procesando || nuevoEstado == EstadoPedido.Cancelado;
            }
            else if (estadoActual == EstadoPedido.Procesando)
            {
                return nuevoEstado == EstadoPedido.Enviado || nuevoEstado == EstadoPedido.Cancelado;
            }
            else if (estadoActual == EstadoPedido.Enviado)
            {
                return nuevoEstado == EstadoPedido.Entregado || nuevoEstado == EstadoPedido.Cancelado;
            }
            else
                return false;
        }
    }
}
