using System;
using System.IO;

namespace ConcesionarioWEBFORM1111.Model.Utilities
{
    public static class Logger
    {
        private static readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "application_log.txt");

        public static void LogError(string message, Exception ex = null)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine($"Date : {DateTime.Now.ToString()}");
                    writer.WriteLine($"Message : {message}");
                    if (ex != null)
                    {
                        writer.WriteLine($"Exception : {ex.Message}");
                        writer.WriteLine($"StackTrace : {ex.StackTrace}");
                    }
                    writer.WriteLine("-----------------------------------------------------------------------------");
                }
            }
            catch
            {
                // Si el logger falla, no queremos crashear la aplicaciÃ³n.
            }
        }

        public static void LogInfo(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[INFO] {DateTime.Now.ToString()}: {message}");
                }
            }
            catch
            {
            }
        }
    }
}

