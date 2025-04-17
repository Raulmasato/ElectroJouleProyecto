using System;
using ElectroJoule.ENTITY;
using ElectroJoule.DAL;
using ElectroJoule.SERVICES;

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
                // Simulación de guardado
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