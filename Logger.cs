using System;

namespace Engine
{
    public enum Log { Error, Warning, Info }
    public static class Logger{
        private static ConsoleColor getColor(Log type){
            switch (type){
                case Log.Error: return ConsoleColor.Red;
                case Log.Warning: return ConsoleColor.Yellow;
                case Log.Info: return ConsoleColor.Green;
                default: return ConsoleColor.Black;
            }
        }
        public static void Console(Log type, string message){
            System.Console.ForegroundColor = getColor(type);
            System.Console.Write("[" + type.ToString() + "]: ");
            System.Console.ResetColor();
            System.Console.WriteLine(message);
        }
        public static void DebugLog(Log type, string message){
            System.Diagnostics.Debug.Print("[" + type.ToString() + "]: " + message);
        }
    }
}