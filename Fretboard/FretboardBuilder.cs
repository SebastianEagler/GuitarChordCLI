using GuitarChordCLI.MusicalRepository;
using GuitarChordCLI.Themes;
using CLITextTable.CliTable;

namespace GuitarChordCLI.Fretboard
{
    internal class FretboardBuilder
    {
        private readonly CliTable _cliTable;

        public FretboardBuilder(ColorTheme theme)
        {
            var header = Enumerable.Range(0, FretboardData.Count + 1).Select(n => n.ToString());

            _cliTable = new CliTable(options =>
            {
                options.MinCellDataWidth = 2;
                options.RowSeparatorMarginLeft = 1; 
                options.RowSeparatorMarginRight = 2;
                options.DrawHeaderLine = false;
                options.DrawRowLines = false;
                options.DefaultRowColor = theme.FretboardNotes;
                options.DefaultHeaderColor = theme.FretNumbers;
                options.DefaultRowLineColor = theme.Lines;
                options.DefaultBackgroundColor = theme.Background;

            }, header.ToArray());

            // One row per string, lowest string first.
            foreach (var strNotes in FretboardData.Notes.Values)
            {
                _cliTable.AddStringRow(strNotes.ToList());
            }
        }

        public CliTable Build()
        {
          return _cliTable;
        }
    }
}
