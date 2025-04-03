using Aplication.Commands;
using Aplication.Queries;
using Domain.Dtos;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        private readonly SettingsRequestDto _settings;

        public PedidosController(IMediator mediator, IOptions<SettingsRequestDto> settings)
        {
            _mediator = mediator;
            _settings = settings.Value;
        }

        /// <summary>
        /// Inicia sesión y genera un token JWT para autenticación.
        /// </summary>
        /// <param name="request">Credenciales del usuario</param>
        /// <returns>retorna un token si las credenciales son correctas</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (request.User == _settings.UserToken && request.Password == _settings.PasswordToken)
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, request.User)
        };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
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
        /// Registra un pedido. Nota:Requiere autenticación.
        /// </summary>
        /// <param name="command">Datos del pedido</param>
        /// <returns>ID del pedido creado</returns>
        [Authorize]
        [HttpPost("Registrar")]
        public async Task<ActionResult> CrearPedido([FromBody] CreatePedidoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { result });
        }

        /// <summary>
        /// Obtiene todos los pedidos registrados en el sistema.
        /// </summary>
        /// <returns>Lista de pedidos</returns>
        [AllowAnonymous]
        [HttpGet("Obtener")]
        public async Task<ActionResult> ObtenerPedidos()
        {
            var result = await _mediator.Send(new GetPedidoQuerie());
            return Ok(new { result });
        }

        /// <summary>
        /// Obtiene un pedido específico por su ID.
        /// </summary>
        /// <param name="id">ID del pedido</param>
        /// <returns>Datos del pedido</returns>
        [AllowAnonymous]
        [HttpGet("Obtenerporid/{id}" )]
        public async Task<ActionResult> ObtenerPedidosByID(int id)
        {
            var result = await _mediator.Send(new GetPedidoByIdQuerie { Id = id });
            return Ok(result);
        }

        /// <summary>
        /// Actualiza el estado de un pedido. Nota:Requiere autenticación.
        /// </summary>
        /// <param name="command">Nuevo estado y ID del pedido</param>
        /// <returns>Mensaje de confirmación</returns>
        [Authorize]
        [HttpPut("Actualizar")]
        public async Task<ActionResult> ActualizarEstadoPedido([FromBody] UpdatePedidoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { mensaje = "Pedido actualizado" });
        }

        /// <summary>
        /// Elimina un pedido por su ID. Nota:Requiere autenticación.
        /// </summary>
        /// <param name="id">ID del pedido a eliminar</param>
        /// <returns>Mensaje de confirmación</returns>
        [Authorize]
        [HttpDelete("Eliminarporid/{id}")]
        public async Task<ActionResult> DeletePedido(int id)
        {
            var result = await _mediator.Send(new DeletePedidoCommand { Id = id });
            return Ok(new { mensaje = "Pedido eliminado" });
        }
    }
}
