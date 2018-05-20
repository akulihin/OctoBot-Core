
using System;
using System.IO;
using System.Threading.Tasks;
using Discord;

namespace  OctoBot.Handeling
{
    internal static class Logger
    {
        static Logger()
        {
           
            if (!File.Exists("runtime.log")) File.Create("OctoDataBase/runtime.log");
        }

        internal static Task Log(LogMessage logMessage)
        {
            var color = SeverityToConsoleColor(logMessage.Severity);
            var message = $"[{logMessage.Source}] {logMessage.Message}";
            Log(message, color);
            return Task.CompletedTask;
        }

        internal static void Log(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{DateTime.Now.ToLongTimeString()} - {message}");
            Console.ResetColor();

            File.AppendAllText("runtime.log", $"\n{DateTime.Now.ToLongTimeString()} - {message}");
        }

        internal static void ClearLog()
        {
            File.WriteAllText("runtime.log", "");
        }

        private static ConsoleColor SeverityToConsoleColor(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Critical:
                    return ConsoleColor.Red;
                case LogSeverity.Debug:
                    return ConsoleColor.Green;
                case LogSeverity.Error:
                    return ConsoleColor.Red;
                case LogSeverity.Info:
                    return ConsoleColor.Cyan;
                case LogSeverity.Verbose:
                    return ConsoleColor.White;
                case LogSeverity.Warning:
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}