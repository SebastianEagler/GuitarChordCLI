namespace CLITextTable.UI
{
    public sealed class CliLineSeparators
    {
        public CliLineSeparators(int leftRowMargin, int rightRowMargin, char separator = '|')
        {
            Separator = string.Concat( new string(' ', leftRowMargin) , separator , new string(' ', rightRowMargin) );  // xx|xx
            LeftSeparator = string.Concat( separator , new string(' ', rightRowMargin) );  // xx|
            RightSeparator = string.Concat( new string(' ', leftRowMargin) , separator ); //  |xx            
        }
        public string Separator { get; } = string.Empty;
        public string LeftSeparator { get; } = string.Empty;
        public string RightSeparator { get; } = string.Empty;
    }
}
