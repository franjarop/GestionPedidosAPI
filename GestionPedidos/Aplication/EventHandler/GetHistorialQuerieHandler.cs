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
    public class GetHistorialQuerieHandler : IRequestHandler<GetHistorialQuerie, List<HistorialEstado>>
    {
        public IPedidoRepository _pedidoRepository { get; set; }
        public GetHistorialQuerieHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<HistorialEstado>> Handle(GetHistorialQuerie request, CancellationToken cancellationToken)
        {
            List<HistorialEstado> lstHistorialPedidos = await _pedidoRepository.GetHistoryPedidos();
            lstHistorialPedidos = lstHistorialPedidos.OrderBy(p => p.OrderId).ToList();
            return lstHistorialPedidos;
        }
    }
}
