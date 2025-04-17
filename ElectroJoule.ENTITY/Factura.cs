using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroJoule.ENTITY
{
    public class Factura
    {
        public int NumeroFactura { get; set; }
        public string Cliente { get; set; }
        public string Email { get; set; }
        public DateTime Fecha { get; set; }
        public List<ItemPedido> Items { get; set; } = new();
        public decimal Total => Items.Sum(x => x.Subtotal);
    }
}
