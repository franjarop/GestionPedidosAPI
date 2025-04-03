using Aplication.Commands;
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
    public class GetPedidoQuerieHandler : IRequestHandler<GetPedidoQuerie, List<PedidoResponse>>
    {
        public IPedidoRepository _pedidoRepository { get; set; }

        public GetPedidoQuerieHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }
        public async Task<List<PedidoResponse>> Handle(GetPedidoQuerie request, CancellationToken cancellationToken)
        {
            List<Pedido> lstPedidos = await _pedidoRepository.GetPedidos();
            List<PedidoResponse> lstPedidosResponse = new List<PedidoResponse>();
            foreach (Pedido pedido in lstPedidos)
            {
                PedidoResponse pedidoResponse = new PedidoResponse();
                pedidoResponse.Id = pedido.Id;
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
                lstPedidosResponse.Add(pedidoResponse);
            }
            return lstPedidosResponse;
        }
    }
}
