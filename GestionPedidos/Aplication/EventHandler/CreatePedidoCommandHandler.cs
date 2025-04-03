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
    public class CreatePedidoCommandHandler : IRequestHandler<CreatePedidoCommand, int>
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
           var id = await _pedidoRepository.AddPedido(pedido);
            
            return id;
        }
    }
}
