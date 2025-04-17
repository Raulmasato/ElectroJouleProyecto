using System;
using System.IO;

namespace ElectroJoule.SERVICES
{
    public static class LogService
    {
        private static string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bitacora.log");

        public static void RegistrarEvento(string mensaje)
        {
            File.AppendAllText(ruta, $"[INFO] {DateTime.Now}: {mensaje}\n");
        }

        public static void RegistrarError(string mensaje, Exception ex)
        {
            File.AppendAllText(ruta, $"[ERROR] {DateTime.Now}: {mensaje} - {ex.Message}\n");
        }
    }
}