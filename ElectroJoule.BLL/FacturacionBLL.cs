using System;


namespace ElectroJoule.BLL
{
    public class FacturacionBLL
    {
        public static Factura GenerarFacturaDesdeNota(NotaPedido nota, int numeroFactura)
        {
            return new Factura
            {
                NumeroFactura = numeroFactura,
                Cliente = nota.Cliente,
                Email = nota.Email,
                Fecha = DateTime.Now,
                Items = nota.Items
            };
        }

        public static bool GuardarFactura(Factura factura)
        {
            try
            {
                // Aquí iría DAL para guardar la factura si se desea
                LogService.RegistrarEvento($"Factura {factura.NumeroFactura} generada.");
                return true;
            }
            catch (Exception ex)
            {
                LogService.RegistrarError("Error al guardar factura", ex);
                return false;
            }
        }
    }
}