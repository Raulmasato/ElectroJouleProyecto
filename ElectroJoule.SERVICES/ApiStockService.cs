using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ElectroJoule.SERVICES
{
    public static class ApiStockService
    {
        private static readonly HttpClient client = new();

        public static async Task EnviarStockAsync(string codigoProducto, int stockActual)
        {
            var json = JsonSerializer.Serialize(new { codigo = codigoProducto, stock = stockActual });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("https://electrojoule.com/api/stock", content);
                if (!response.IsSuccessStatusCode)
                    LogService.RegistrarError($"Falló envío de stock para {codigoProducto}", new Exception(response.StatusCode.ToString()));
            }
            catch (Exception ex)
            {
                LogService.RegistrarError("Error al enviar stock", ex);
            }
        }
    }
}