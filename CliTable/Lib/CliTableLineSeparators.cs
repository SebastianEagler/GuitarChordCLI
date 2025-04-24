using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarChordCLI.Table.Lib
{
    public sealed class CliTableLineSeparators
    {
        public CliTableLineSeparators(int leftRowMargin, int rightRowMargin, string separator = "|")
        {
            Separator = string.Concat( new string(' ', leftRowMargin) , separator , new string(' ', rightRowMargin) );
            Left = string.Concat( separator , new string(' ', rightRowMargin) );
            Rigth = string.Concat( new string(' ', leftRowMargin) , separator );
        }
        public string Separator { get; } = string.Empty;
        public string Left { get; } = string.Empty;
        public string Rigth { get; } = string.Empty;
    }
}
