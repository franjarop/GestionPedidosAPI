using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands
{
    public class DeletePedidoCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
