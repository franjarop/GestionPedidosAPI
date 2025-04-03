using Aplication.Interfaces.infraestrcuture;
using Domain.Models;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
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
            _context.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido.Id;
        }
    }
}
