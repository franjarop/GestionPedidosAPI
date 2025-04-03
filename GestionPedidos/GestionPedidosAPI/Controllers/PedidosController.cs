using Aplication.Commands;
using Aplication.Queries;
using Domain.Dtos;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (request.User == "admin" && request.Password == "1234")
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, request.User)
        };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mYjw7TSj83z@NrpWmLQvKx5uE9AczGd3"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized();
        }


        /// <summary>
        /// Metodo que permite registrar un pedido
        /// </summary>
        /// <param name="Crear pedido"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("Registrar")]
        public async Task<ActionResult> CrearPedido([FromBody] CreatePedidoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { result });
        }

        /// <summary>
        /// Funcion que permite retornar todos los pedidos registrados
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Obtener")]
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
        [AllowAnonymous]
        [HttpGet("Obtenerporid/{id}" )]
        public async Task<ActionResult> ObtenerPedidosByID(int id)
        {
            var result = await _mediator.Send(new GetPedidoByIdQuerie { Id = id });
            return Ok(result);
        }

        [Authorize]
        [HttpPut("Actualizar")]
        public async Task<ActionResult> ActualizarEstadoPedido([FromBody] UpdatePedidoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { mensaje = "Pedido actualizado" });
        }

        [Authorize]
        [HttpDelete("Eliminarporid/{id}")]
        public async Task<ActionResult> DeletePedido(int id)
        {
            var result = await _mediator.Send(new DeletePedidoCommand { Id = id });
            return Ok(new { mensaje = "Pedido eliminado" });
        }
    }
}
