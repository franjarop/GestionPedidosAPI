using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public EstadoPedido Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<HistorialEstado> HistorialEstados { get; set; } = new List<HistorialEstado>();
    }

    public enum EstadoPedido
    {
        Pendiente,
        Procesando,
        Enviado,
        Entregado,
        Cancelado
    }
}
