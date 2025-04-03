using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<HistorialEstado> HistorialEstados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.HistorialEstados)
                .WithOne(h => h.Pedido)
                .HasForeignKey(h => h.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
