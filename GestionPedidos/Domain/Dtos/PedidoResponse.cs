using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class PedidoResponse
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
        public string StatusDescription { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
