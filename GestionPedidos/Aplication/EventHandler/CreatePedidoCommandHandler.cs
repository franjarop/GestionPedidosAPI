using Aplication.Commands;
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
    internal class CreatePedidoCommandHandler : IRequestHandler<CreatePedidoCommand, int>
    {
        public IPedidoRepository _pedidoRepository { get; set; }
        public CreatePedidoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<int> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
        {
            var pedido = new Pedido
            {
                CustomerId = request.CustomerId,
                TotalAmount = request.TotalAmount,
                Status = EstadoPedido.Pendiente,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _pedidoRepository.AddPedido(pedido);
            
            return pedido.Id;
        }
    }
}
