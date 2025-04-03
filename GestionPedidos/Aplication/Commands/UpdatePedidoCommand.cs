using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands
{
    public class UpdatePedidoCommand : IRequest<bool>
    {
        public int id { get; set; }
        public EstadoPedido statusId { get; set; }
    }
}
