using GuitarChordCLI.MusicalRepository;
using GuitarChordCLI.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarChordCLI.Generators
{
    internal class FreadboardBuilder
    {
        private readonly CliTable _cliTable;
        public FreadboardBuilder()
        {
            var x = Enumerable.Range(0, Freadboard.Count + 1).Select(n => n.ToString());
            _cliTable = new CliTable(options =>
            {
                options.MinRowWidth = 2;
                options.LeftRowMargin = 1;
                options.RightRowMargin = 2;
                options.DrawHeaderLine = false;
                options.DrawRowLines = false;
            }, x.ToArray());

            for (int i = Freadboard.Frets.Count - 1; i >= 0; i--)
            {
                _cliTable.AddRow(Freadboard.Notes.ElementAt(i).Value.Select(n => n.ToString()).ToList());
            }
        }
        public void Print()
        {
            _cliTable.Print();
        }

        public FreadboardBuilder AddNotes(List<string> notes)
        {
            return this;
        }
    }
}