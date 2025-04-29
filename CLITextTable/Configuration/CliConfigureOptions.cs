namespace CLITextTable.Lib
{
    public sealed class CliConfigureOptions
    {
        public char ColumnSeparator { get; set; } = '|';
        public char RowSeparator { get; set; } = '-';
        public char FrameConnector { get; set; } = '+';
        public bool DrawHeaderLine { get; set; } = true;
        public bool DrawRowLines { get; set; } = true;
        public int MinCellDataWidth { get; set; } = 3;
        public int MaxCellDataWidth { get; set; } = 10;
        public int RowSeparatorMarginLeft { get; set; } = 0;  // xx|
        public int RowSeparatorMarginRight { get; set; } = 0; // |xx

        public ConsoleColor DefaultHeaderColor = ConsoleColor.White;
        public ConsoleColor DefaultRowColor = ConsoleColor.White;        
        public ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;

    }
}
