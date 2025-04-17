using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ElectroJoule.ENTITY;

namespace ElectroJoule.SERVICES
{
    public static class FacturaService
    {
        public static string GenerarPDF(Factura factura)
        {
            try
            {
                string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Factura_{factura.NumeroFactura}.pdf");

                using var fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
                var doc = new Document();
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                doc.Add(new Paragraph($"Factura Nº {factura.NumeroFactura}"));
                doc.Add(new Paragraph($"Cliente: {factura.Cliente}"));
                doc.Add(new Paragraph($"Fecha: {factura.Fecha:dd/MM/yyyy}"));
                doc.Add(new Paragraph(" "));

                PdfPTable tabla = new PdfPTable(4);
                tabla.AddCell("Código");
                tabla.AddCell("Descripción");
                tabla.AddCell("Cantidad");
                tabla.AddCell("Subtotal");

                foreach (var item in factura.Items)
                {
                    tabla.AddCell(item.CodigoProducto);
                    tabla.AddCell(item.Descripcion);
                    tabla.AddCell(item.Cantidad.ToString());
                    tabla.AddCell(item.Subtotal.ToString("C"));
                }

                doc.Add(tabla);
                doc.Add(new Paragraph(" "));
                doc.Add(new Paragraph($"Total: {factura.Total:C}"));

                doc.Close();
                return ruta;
            }
            catch (Exception ex)
            {
                LogService.RegistrarError("Error al generar PDF", ex);
                return null;
            }
        }

        public static bool EnviarFacturaPorMail(Factura factura, string rutaPDF)
        {
            try
            {
                var mensaje = new MailMessage("tucorreo@ejemplo.com", factura.Email)
                {
                    Subject = $"Factura Nº {factura.NumeroFactura}",
                    Body = "Adjuntamos su factura. ¡Gracias por su compra!"
                };

                mensaje.Attachments.Add(new Attachment(rutaPDF));

                var smtp = new SmtpClient("smtp.tucorreo.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("usuario", "contraseña"),
                    EnableSsl = true
                };

                smtp.Send(mensaje);
                LogService.RegistrarEvento($"Factura enviada a {factura.Email}");
                return true;
            }
            catch (Exception ex)
            {
                LogService.RegistrarError("Error al enviar email", ex);
                return false;
            }
        }
    }
}