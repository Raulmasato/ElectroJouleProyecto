using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ElectroJoule.ENTITY;


namespace ElectroJoule.DAL
{
    public class NotaPedidoDAL
    {
        public static NotaPedido ObtenerPorId(int id)
        {
            try
            {
                NotaPedido nota = null;

                using var conn = new SqlConnection(ConexionBD.Cadena);
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM NotaPedido WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nota = new NotaPedido
                    {
                        Id = id,
                        Cliente = reader["Cliente"].ToString(),
                        Email = reader["Email"].ToString(),
                        Fecha = (DateTime)reader["Fecha"]
                    };
                }
                reader.Close();

                if (nota != null)
                {
                    nota.Items = ObtenerItems(id, conn);
                }

                return nota;
            }
            catch (Exception ex)
            {
                LogService.RegistrarError("Error al obtener nota de pedido", ex);
                return null;
            }
        }

        private static List<ItemPedido> ObtenerItems(int notaId, SqlConnection conn)
        {
            var items = new List<ItemPedido>();
            var cmd = new SqlCommand("SELECT * FROM ItemPedido WHERE NotaPedidoId = @id", conn);
            cmd.Parameters.AddWithValue("@id", notaId);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new ItemPedido
                {
                    CodigoProducto = reader["CodigoProducto"].ToString(),
                    Descripcion = reader["Descripcion"].ToString(),
                    Cantidad = (int)reader["Cantidad"],
                    PrecioUnitario = (decimal)reader["PrecioUnitario"]
                });
            }
            reader.Close();
            return items;
        }
    }

    public static class ConexionBD
    {
        public static string Cadena => "Server=MI_SERVIDOR;Database=ElectroJoule;Trusted_Connection=True;";
    }
}