using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries
{
    public class GetPedidoByIdQuerie : IRequest<PedidoResponse>
    {
        public int Id { get; set; }
    }
}
