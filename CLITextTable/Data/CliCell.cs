using CLITextTable.Configuration;

namespace CLITextTable.Data
{
    public sealed class CliCell
    {        
        public string Value { get; set; }
        public ConsoleColor Color { get; set; }
        public ConsoleColor BackgroundColor { get; set; } = CellColorOptions.DefaultBackgroundColor;
        public CliCell(string value, ConsoleColor color = CellColorOptions.DefaultColor )
        {
            Value = value;
            Color = color;
        }
    }
}