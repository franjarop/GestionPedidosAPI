using Domain.Dtos;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Queries
{
    public class GetPedidoQuerie : IRequest<List<PedidoResponse>>
    {
    }
}
