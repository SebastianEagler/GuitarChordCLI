using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLITextTable.Configuration
{
    public sealed class RowOptions
    {
        public ConsoleColor Color { get; set; } = ConsoleColor.White;        
       public int Width { get; set; }  = 0;
    }

}
