using Aplication.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionPedidosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HistorialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("Obtener")]
        public async Task<ActionResult> ObtenerHistorialPedidos()
        {
            var result = await _mediator.Send(new GetHistorialQuerie());
            return Ok(new { result });
        }
    }
}
