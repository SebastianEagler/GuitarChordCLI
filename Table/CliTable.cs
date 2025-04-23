using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace GuitarChordCLI.Table
{
    public class CliTable
    {
        private readonly List<string> headers;
        private readonly List<List<string>> rows = new List<List<string>>();
        private List<int> columnLength = new List<int>();

        private string VertSeparator = "|";
        private string VertSeparatorLeft = "|";
        private string VertSeparatorRigth = "|";
        private int leftMargin = 0;
        private int rightMargin = 0;
        public void Margins(int left, int right)
        {
            leftMargin = left;
            rightMargin = right;
            VertSeparator = new string(' ', left) + "|" + new string(' ', right);
            VertSeparatorLeft = "|" + new string(' ', right);
            VertSeparatorRigth = new string(' ', left) + "|";
        }

        public CliTable(string[] headers)
        {
            this.headers = headers.ToList();
            foreach (var header in headers)
            {
                columnLength.Add(header.Length);
            }
        }               

        public void AddRow(IEnumerable<string> row)
        {
            if (row.Count() != headers.Count)
            {
                throw new ArgumentException("Row length must match header length.");
            }
            rows.Add(row.ToList() );
            columnLength = columnLength.Zip(row, (col, rowItem) => Math.Max(col, rowItem.Length)).ToList();
        }        
        private void printLine()
        {
            var colSeparator = leftMargin + 1 + rightMargin;

            var totalLength = leftMargin + 1+1 + rightMargin + columnLength.Sum(h => h) + colSeparator * (columnLength.Count - 1);

            var sb = new StringBuilder(new string('-', totalLength));
            int index = 0;
            for (int i = 0; i < columnLength.Count; i++)
            {
                sb[index] = '+';
                index += columnLength[i] + leftMargin+1+rightMargin;
            }
            sb[sb.Length-1] = '+';
            Console.WriteLine(sb.ToString());
        }
        public void Print()
        {
            var _headers = headers.Zip(columnLength, (he, col) => he.PadRight(col )).ToList();

            printLine();

            Console.WriteLine(VertSeparatorLeft +string.Join(VertSeparator, _headers)+ VertSeparatorRigth );
            printLine();

            // Print rows
            foreach (var row in rows)
            {
                var _row = row.Zip(columnLength, (r, col) => r.PadRight(col )).ToList();
                Console.WriteLine(VertSeparatorLeft + string.Join(VertSeparator , _row)+ VertSeparatorRigth );
            }
            printLine();
        }
    }
}