using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class HistorialEstado
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public EstadoPedido PreviosStatus { get; set; }
        public EstadoPedido NewStatus { get; set; }
        public DateTime ChangeAt { get; set; }

        public Pedido Pedido { get; set; }
    }
}
