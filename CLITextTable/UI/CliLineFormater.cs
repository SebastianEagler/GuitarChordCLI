using CLITextTable.Data;
using CLITextTable.Lib;
using System.Text;


namespace CLITextTable.UI
{
    public class CliLineFormater
    {
        private readonly CliConfigureOptions options;
        private readonly List<int> columnLength;
        private readonly CliLineSeparators separators;

        public CliLineFormater(CliConfigureOptions options, List<int> columnLength, CliLineSeparators separators )
        {
            this.options = options;
            this.columnLength = columnLength;
            this.separators = separators;
        }
        public string PrepareLine()
        {
            var colSeparator = options.RowSeparatorMarginLeft + 1 + options.RowSeparatorMarginRight;

            var totalLength = options.RowSeparatorMarginLeft + 1 + 1 + options.RowSeparatorMarginRight + columnLength.Sum(h => h) + colSeparator * (columnLength.Count - 1);

            var sb = new StringBuilder(new string(options.RowSeparator, totalLength));
            int index = 0;
            for (int i = 0; i < columnLength.Count; i++)
            {
                sb[index] = '+';
                index += columnLength[i] + options.RowSeparatorMarginLeft + 1 + options.RowSeparatorMarginRight;
            }
            sb[sb.Length - 1] = options.FrameConnector;
            return sb.ToString();
        }
        public void CalculateCellWidth( List<CliCell> row)
        {
            for (int i = 0; i < row.Count; i++)
            {
                row[i].Value = SetRowDataLength( row[i].Value, columnLength[i], options.MaxCellDataWidth);
            }   
            // var _row = row.Zip(columnLength, (r, col) => CliHelper.SetRowDataLength(r.Value, col, options.MaxCellDataWidth)).ToList();            
        }
        public string PrepareHeader(List<string> headers)
        {
            var separators = new CliLineSeparators(options.RowSeparatorMarginLeft, options.RowSeparatorMarginRight, options.DrawHeaderLine ? options.ColumnSeparator : ' ');
            var _headers = headers.Zip(columnLength, (he, col) => he.PadRight(col)).ToList();
            return separators.LeftSeparator + string.Join(separators.Separator, _headers) + separators.RightSeparator;
        }

        public static string SetRowDataLength(string str, int minlength, int maxlength)
        {
            str = str.Trim().PadRight(minlength);
            str = str.Length > maxlength ? str.Substring(0, maxlength) : str;
            return str;
        }
    }
}
