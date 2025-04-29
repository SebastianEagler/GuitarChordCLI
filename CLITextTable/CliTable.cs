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
        private List<RowOptions> column = new List<RowOptions>();
        private readonly CliConfigureOptions options;

        public CliCellCollection Cell { get => cell; }
        public List<RowOptions> Column { get => column; }
        public CliTable(Action<CliConfigureOptions> options, string[] headers)
        {
            this.options = new CliConfigureOptions();
            options?.Invoke(this.options);

            this.headers = headers.ToList();
            foreach (var header in headers)
                column.Add( new RowOptions() {Color = this.options.DefaultRowColor, Width = this.options.MinCellDataWidth } );            
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
            for (int i = 0; i < column.Count; i++)
            {
                column[i].Width = Math.Max(column[i].Width, row.ElementAt(i).Value.Length);
            }
            //columnLength = columnLength.Zip(row, (col, rowItem) => Math.Max(col, rowItem.Value.Length)).ToList();
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
        public void Print()
        {
            var separators = new CliLineSeparators(options.RowSeparatorMarginLeft, options.RowSeparatorMarginRight);
            var formater = new CliLineFormater(this.options, column, separators);
            var printLine = formater.PrepareLine();

            if (options.DrawHeaderLine)
                CliPrinter.WriteLine(printLine, options.DefaultHeaderColor);

            CliPrinter.WriteLine(formater.PrepareHeader(headers), options.DefaultHeaderColor);
            CliPrinter.WriteLine(printLine);

            foreach (var row in cell)
            {
                formater.CalculateCellWidth(row);
                CliPrinter.Write(separators.LeftSeparator, column.First().Color);
                for( var x=0; x < column.Count; x++)
                {
                    CliPrinter.Write(row[x].Value, row[x].Color, row[x].BackgroundColor);
                    if (x < column.Count-1)
                        CliPrinter.Write(separators.Separator, column[x].Color );
                }                
                CliPrinter.WriteLine(separators.RightSeparator, column.Last().Color);
                if (options.DrawRowLines)
                    CliPrinter.WriteLine(printLine);
            }
            if (!options.DrawRowLines || cell.Count == 0)
                CliPrinter.Write(printLine);
            CliPrinter.WriteLine("");
        }
    }
}