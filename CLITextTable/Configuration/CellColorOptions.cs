using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLITextTable.Configuration
{
    public class CellColorOptions
    {
        public const ConsoleColor DefaultColor = ConsoleColor.White;
        public const ConsoleColor DefaultBackgroundColor = ConsoleColor.Black;

        public ConsoleColor Color = ConsoleColor.White;
        public ConsoleColor BackgroundColor = ConsoleColor.Black;        
    }
}
