using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectroJoule.ENTITY
{
    public class NotaPedido
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string Email { get; set; }
        public DateTime Fecha { get; set; }
        public List<ItemPedido> Items { get; set; } = new();
    }

    public class ItemPedido
    {
        public string CodigoProducto { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }

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