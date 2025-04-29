using CLITextTable.Configuration;
using CLITextTable.Data;
using CLITextTable.Lib;
using CLITextTable.UI;

namespace CLITextTable.CliTable
{

    public sealed class CliCellCollection : List<List<CliCell>> { }

    public sealed class CliTable
    {
        private readonly List<string> headers;
        private readonly CliCellCollection cell = new CliCellCollection();
        private List<int> columnLength = new List<int>();
        private readonly CliConfigureOptions options;

        public CliCellCollection Cell { get => cell; }
        public CliTable(Action<CliConfigureOptions> options, string[] headers)
        {
            this.options = new CliConfigureOptions();
            options?.Invoke(this.options);

            this.headers = headers.ToList();
            foreach (var header in headers)
                columnLength.Add(header.Length);
            columnLength = columnLength.Select(item => Math.Max(this.options.MinCellDataWidth, item)).ToList();
        }

        public void AddStringRow(List<string> row)
        {
            AddRow(row.Select(x => new CliCell(x,options.DefaultRowColor) ).ToList());
        }
        public void AddStringRow(IEnumerable<string> row)
        {
            AddStringRow(row.ToList());
        }

        public void AddRow(List<CliCell> row)
        {
            if (row.Count() != headers.Count)
            {
                throw new ArgumentException("Row length must match header length.");
            }
            cell.Add(row.ToList());
            for (int i = 0; i < columnLength.Count; i++)
            {
                columnLength[i] = Math.Max(columnLength[i], row.ElementAt(i).Value.Length);
            }
            //columnLength = columnLength.Zip(row, (col, rowItem) => Math.Max(col, rowItem.Value.Length)).ToList();
        }
        public void Print()
        {
            var separators = new CliLineSeparators(options.RowSeparatorMarginLeft, options.RowSeparatorMarginRight);
            var formater = new CliLineFormater(this.options, columnLength, separators);
            var printLine = formater.PrepareLine();

            if (options.DrawHeaderLine)
                CliPrinter.WriteLine(printLine);

            CliPrinter.WriteLine(formater.PrepareHeader(headers), options.DefaultHeaderColor );
            CliPrinter.WriteLine(printLine);

            foreach (var row in cell)
            {
                formater.CalculateCellWidth(row);
                CliPrinter.Write(separators.LeftSeparator);
                foreach (var cell in row)
                {
                    CliPrinter.Write(cell.Value, cell.Color, cell.BackgroundColor);
                    if (cell != row.Last())
                        CliPrinter.Write(separators.Separator);
                }
                CliPrinter.WriteLine(separators.RightSeparator);
                if (options.DrawRowLines)
                    CliPrinter.WriteLine(printLine);
            }
            if (!options.DrawRowLines || cell.Count == 0)
                CliPrinter.Write(printLine);
            CliPrinter.WriteLine("");
        }

        public void SetColor(int columnIndex, int rowIndex, ConsoleColor color, ConsoleColor BackgroundColor = ConsoleColor.Black)
        {
            if (columnIndex < 0 || columnIndex >= headers.Count)
                throw new ArgumentOutOfRangeException(nameof(columnIndex), "Column index is out of range.");
            if (rowIndex < 0 || rowIndex >= cell.Count)
                throw new ArgumentOutOfRangeException(nameof(rowIndex), "Row index is out of range.");

            cell[rowIndex][columnIndex].Color = color;
            cell[rowIndex][columnIndex].BackgroundColor = BackgroundColor;
        }

        public void ColorCells( List<string> notes, CellColorOptions options)
        {
            
            for (int i = 0; i < cell.Count; i++)

                for (int j = 0; j <cell[i].Count; j++)

                    foreach (string note in notes)
                        if (note == cell[i][j].Value)
                        {
                            cell[i][j].Color = options.Color;
                            cell[i][j].BackgroundColor = options.BackgroundColor;
                        }
            // todo ClearUnmatchedNotes
        }
    }
}