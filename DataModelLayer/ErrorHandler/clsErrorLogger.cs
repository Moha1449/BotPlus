using System;
using System.IO;

namespace DataModelLayer.ErrorHandler
{
    public static class clsErrorLogger
    {
        private static readonly object _Key = new object();

        private static string _ErrorLoggerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logger.txt");

        public static void LogError(string detail)
        {
            lock (_Key)
            {
                using (StreamWriter Writer = File.AppendText(_ErrorLoggerPath))
                {
                    Writer.WriteLine($"[{DateTime.Now}] " + detail);
                }
            }
        }
    }
}