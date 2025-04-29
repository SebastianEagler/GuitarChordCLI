
using GuitarChordCLI.MusicalRepository;
using CLITextTable.CliTable;

namespace GuitarChordCLI.Generators
{
    internal class FreadboardBuilder
    {
        private readonly CliTable _cliTable;
        public FreadboardBuilder()
        {
            var header = Enumerable.Range(0, Freadboard.Count + 1).Select(n => n.ToString());

            _cliTable = new CliTable(options =>
            {
                options.MinCellDataWidth = 2;
                options.RowSeparatorMarginLeft = 1; 
                options.RowSeparatorMarginRight = 2;
                options.DrawHeaderLine = false;
                options.DrawRowLines = false;
                options.DefaultRowColor = ConsoleColor.DarkGray;
                options.DefaultHeaderColor = ConsoleColor.DarkCyan;

            }, header.ToArray());

            for (int i = 0; i < Freadboard.Frets.Count ; i++)
            {
                _cliTable.AddStringRow(Freadboard.Notes.ElementAt(i).Value.Select(n => n.ToString()).ToList() );
            }
            _cliTable.Column[0].Color = ConsoleColor.Red;
            _cliTable.Column[3].Color = ConsoleColor.Yellow;
            _cliTable.Column[5].Color = ConsoleColor.Red;
            _cliTable.Column[7].Color = ConsoleColor.Yellow;
            _cliTable.Column[9].Color = ConsoleColor.Yellow;
            _cliTable.Column[12].Color = ConsoleColor.Red;
                        
            _cliTable.Column[15].Color = ConsoleColor.Yellow;
            _cliTable.Column[17].Color = ConsoleColor.Red;
            _cliTable.Column[19].Color = ConsoleColor.Yellow;
            _cliTable.Column[21].Color = ConsoleColor.Yellow;
            _cliTable.Column[24].Color = ConsoleColor.Red;
        }
        public CliTable Build()
        {
          return _cliTable;
        }
    }
}