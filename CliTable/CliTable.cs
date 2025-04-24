using GuitarChordCLI.Table.Lib;
using System.Text;

namespace GuitarChordCLI.Table
{
    public sealed class CliTable
    {
        private readonly List<string> headers;
        private readonly List<List<string>> rows = new List<List<string>>();
        private List<int> columnLength = new List<int>();
        private readonly CliTableOptions options;

        public CliTable(Action<CliTableOptions> options, string[] headers)
        {
            this.options = new CliTableOptions();
            options?.Invoke(this.options);

            this.headers = headers.ToList();
            foreach (var header in headers)
                columnLength.Add(header.Length);
            columnLength = columnLength.Select(item => Math.Max(this.options.MinRowWidth, item)).ToList();
        }
        private void Write(string line)
        {
            Console.WriteLine(line);
        }
        public void AddRow(IEnumerable<string> row)
        {
            if (row.Count() != headers.Count)
            {
                throw new ArgumentException("Row length must match header length.");
            }
            rows.Add(row.ToList());
            columnLength = columnLength.Zip(row, (col, rowItem) => Math.Max(col, rowItem.Length)).ToList();
        }
        public void Print()
        {
            var formater = new CliTableLineFormater(this.options, columnLength);
            var printLine = formater.prepareLine();

            if (options.DrawHeaderLine)
                Write(printLine);

            Write(formater.prepareHeader(headers));

            Write(printLine);

            foreach (var row in rows)
            {
                var line = formater.prepareRow(row);
                Write(line);
                if (options.DrawRowLines)
                    Write(printLine);
            }
            if (!options.DrawRowLines || rows.Count == 0)
                Write(printLine);
        }
    }
}