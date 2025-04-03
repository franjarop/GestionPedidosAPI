using Aplication.Exceptions;
using Aplication.Interfaces.infraestrcuture;
using Aplication.Queries;
using Domain.Dtos;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.EventHandler
{
    public class GetPedidoByIdQuerieHandler : IRequestHandler<GetPedidoByIdQuerie, PedidoResponse>
    {
        public IPedidoRepository _pedidoRepository { get; set; }
        public GetPedidoByIdQuerieHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

       
        public async Task<PedidoResponse?> Handle(GetPedidoByIdQuerie request, CancellationToken cancellationToken)
        {
            List<Pedido> lstPedidos = await _pedidoRepository.GetPedidos();
            Pedido pedido = lstPedidos.Where(x => x.Id == request.Id).FirstOrDefault();
            PedidoResponse pedidoResponse = new PedidoResponse();
            if (pedido != null)
            {
                pedidoResponse.Id = request.Id;
                pedidoResponse.CustomerID = pedido.CustomerId;
                pedidoResponse.TotalAmount = pedido.TotalAmount;
                switch (pedido.Status)
                {
                    case EstadoPedido.Pendiente:
                        pedidoResponse.StatusDescription = "Pendiente";
                        break;
                    case EstadoPedido.Procesando:
                        pedidoResponse.StatusDescription = "Procesando";
                        break;
                    case EstadoPedido.Enviado:
                        pedidoResponse.StatusDescription = "Enviado";
                        break;
                    case EstadoPedido.Entregado:
                        pedidoResponse.StatusDescription = "Entregado";
                        break;
                    case EstadoPedido.Cancelado:
                        pedidoResponse.StatusDescription = "Cancelado";
                        break;
                }
                pedidoResponse.CreatedAt = pedido.CreatedAt;
                return pedidoResponse;
            }
            else
                throw new NotFoundException($"No se encontró un pedido con ID {request.Id}");
        }
    }
}
