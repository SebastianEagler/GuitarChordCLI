using System.Runtime.InteropServices;


namespace GuitarChordCLI.Setup
{
    internal class ConsoleSetup
    {
        private static readonly short _fontSize = 24;
        private static readonly int _width = 120;
        private static readonly int _height = 40;
        private static readonly string _fontName = "Consolas";

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CONSOLE_FONT_INFOEX
        {
            public uint cbSize;
            public uint nFont;
            public COORD dwFontSize;
            public uint FontFamily;
            public uint FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(nint consoleOutput, bool maximumWindow, ref CONSOLE_FONT_INFOEX consoleCurrentFontEx);

        [DllImport("kernel32.dll")]
        static extern nint GetStdHandle(int nStdHandle);

        public static void ChangeFont()
        {
            nint hnd = GetStdHandle(-11);
            CONSOLE_FONT_INFOEX fontInfo = new CONSOLE_FONT_INFOEX();
            fontInfo.cbSize = (uint)Marshal.SizeOf(fontInfo);
            fontInfo.dwFontSize.Y = _fontSize; // Zmiana rozmiaru czcionki
            fontInfo.FaceName = _fontName;
            SetCurrentConsoleFontEx(hnd, false, ref fontInfo);                        
        }

        public static void SetWindowSize()
        {
            int width = Math.Min(Console.LargestWindowWidth, _width);
            int height = Math.Min(Console.LargestWindowHeight, _height);
            Console.SetWindowSize(width, height);
        }

    }
}
