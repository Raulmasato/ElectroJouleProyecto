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
        public List<ItemPedido> Items { get; set; } = new List<ItemPedido>();
    }

}