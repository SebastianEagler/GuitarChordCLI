using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GuitarChordCLI.Table.Lib
{
    public class CliTableLineFormater
    {
        private readonly CliTableOptions options;
        private readonly List<int> columnLength;       
        private readonly CliTableLineSeparators separators;

        public CliTableLineFormater(CliTableOptions options, List<int> columnLength )
        {
            this.options = options;
            this.columnLength = columnLength;
            separators = new CliTableLineSeparators(options.LeftRowMargin, options.RightRowMargin);
        }
        public string prepareLine()
        {
            var colSeparator = options.LeftRowMargin + 1 + options.RightRowMargin;

            var totalLength = options.LeftRowMargin + 1 + 1 + options.RightRowMargin + columnLength.Sum(h => h) + colSeparator * (columnLength.Count - 1);

            var sb = new StringBuilder(new string('-', totalLength));
            int index = 0;
            for (int i = 0; i < columnLength.Count; i++)
            {
                sb[index] = '+';
                index += columnLength[i] + options.LeftRowMargin + 1 + options.RightRowMargin;
            }
            sb[sb.Length - 1] = '+';
            return sb.ToString();
        }
        public string prepareRow(List<string> row)
        {
            var _row = row.Zip(columnLength, (r, col) => r.PadRight(col)).ToList();
            return separators.Left + string.Join(separators.Separator, _row) + separators.Rigth;
        }
        public string prepareHeader(List<string> headers )
        {
            var separators = new CliTableLineSeparators(options.LeftRowMargin, options.RightRowMargin, options.DrawHeaderLine ? "|" : " ");
            var _headers = headers.Zip(columnLength, (he, col) => he.PadRight(col)).ToList();
            return separators.Left + string.Join(separators.Separator, _headers) + separators.Rigth;
        }
    }
}
