using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarChordCLI.Table.Lib
{
    public sealed class CliTableOptions
    {
        public bool DrawHeaderLine { get; set; } = true;
        public bool DrawRowLines { get; set; } = true;        
        public int MinRowWidth { get; set; } = 3;
        public int LeftRowMargin { get; set; } = 0;
        public int RightRowMargin { get; set; } = 0;
    }
}
