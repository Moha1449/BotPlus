using System;
using System.IO;
using System.Threading.Tasks;

namespace BusinessLayer.ErrorHandler
{
    internal abstract class clsErrorLogger
    {
        private static string _ErrorLoggerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logger.txt");

        internal async static Task LogErrorAsync(string detail)
        {
            if (!File.Exists(_ErrorLoggerPath))
            {
                File.Create(_ErrorLoggerPath);
            }

            using (StreamWriter Writer = File.AppendText(_ErrorLoggerPath))
            {
                await Writer.WriteLineAsync($"[{DateTime.Now}] " + detail);
            }
        }

    }
}
