namespace CLITextTable.UI
{
    internal static class CliPrinter
    {

        public static void WriteLine(string line, ConsoleColor Color = ConsoleColor.White, ConsoleColor BackgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = Color;
            Console.BackgroundColor = BackgroundColor;
            Console.WriteLine(line);
        }
        public static void Write(string line, ConsoleColor Color = ConsoleColor.White, ConsoleColor BackgroundColor = ConsoleColor.Black)
        {
            Console.ForegroundColor = Color;
            Console.BackgroundColor = BackgroundColor;            
            Console.Write(line);
        }
    }
}
