using Aplication.Commands;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestionPedidosAPI.Controllers
{
    [ApiController]
    [Route("api/crearpedido")]
    public class PedidosController : Controller
    {
        private readonly IMediator _mediator;
        public PedidosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CrearPedido([FromBody] CreatePedidoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { result });
        }
    }
}
