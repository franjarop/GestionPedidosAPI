using Aplication.Exceptions;
using Aplication.Interfaces.infraestrcuture;
using Domain.Dtos;
using Domain.Models;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;
        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddPedido(Pedido pedido)
        {
            try
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return pedido.Id;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocurrio un error al registrar el pedido", ex);
            }

        }

        public async Task CreateHistoryPedido(HistorialEstado historialPedido)
        {
            try
            {
                _context.Add(historialPedido);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocurrio un error al registrar el historial del pedido", ex);
            }

        }

        public async Task<List<Pedido>> GetPedidos()
        {
            try
            {
                var pedidos = await _context.Pedidos.ToListAsync();
                return pedidos;
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocurrio un error al obtener los pedidos", ex);
            }

        }

        public async Task UpdatePedido(Pedido pedido)
        {
            try
            {
                _context.Update(pedido);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocurrio un error al actualizar el pedido", ex);
            }

        }
    }
}
