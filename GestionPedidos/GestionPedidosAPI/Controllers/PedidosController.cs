using Aplication.Commands;
using Aplication.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GestionPedidosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PedidosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Metodo que permite registrar un pedido
        /// </summary>
        /// <param name="Crear pedido"></param>
        /// <returns></returns>
        [HttpPost(Name ="Registrar pedido")]
        public async Task<ActionResult> CrearPedido([FromBody] CreatePedidoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { result });
        }

        /// <summary>
        /// Funcion que permite retornar todos los pedidos registrados
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name ="Obtener todos los pedidos")]
        public async Task<ActionResult> ObtenerPedidos()
        {
            var result = await _mediator.Send(new GetPedidoQuerie());
            return Ok(new { result });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name ="Obtener pedido por ID")]
        public async Task<ActionResult> ObtenerPedidosByID(int id)
        {
            var result = await _mediator.Send(new GetPedidoByIdQuerie { Id = id});
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarEstadoPedido([FromBody] UpdatePedidoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { mensaje = "Pedido actualizado" });
        }
    }
}
