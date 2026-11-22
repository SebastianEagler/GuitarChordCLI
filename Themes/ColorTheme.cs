using CLITextTable.Configuration;

namespace GuitarChordCLI.Themes
{
    /// <summary>Set of console colors used by the menu and the fretboard.</summary>
    internal sealed class ColorTheme
    {
        public required string Name { get; init; }

        /// <summary>Console background (also used as the table background).</summary>
        public required ConsoleColor Background { get; init; }

        /// <summary>Plain text (menu values, welcome text).</summary>
        public required ConsoleColor Text { get; init; }

        /// <summary>Menu table header.</summary>
        public required ConsoleColor MenuHeader { get; init; }

        /// <summary>Table borders.</summary>
        public required ConsoleColor Lines { get; init; }

        /// <summary>Fret numbers above the fretboard.</summary>
        public required ConsoleColor FretNumbers { get; init; }

        /// <summary>Fretboard notes that are not highlighted.</summary>
        public required ConsoleColor FretboardNotes { get; init; }

        public required CellColorOptions Chord { get; init; }
        public required CellColorOptions Scale { get; init; }

        /// <summary>Sets console colors to the theme defaults.</summary>
        public void Apply()
        {
            Console.BackgroundColor = Background;
            Console.ForegroundColor = Text;
        }

        public static readonly ColorTheme Dark = new()
        {
            Name = "Dark",
            Background = ConsoleColor.Black,
            Text = ConsoleColor.Gray,
            MenuHeader = ConsoleColor.Yellow,
            Lines = ConsoleColor.DarkGray,
            FretNumbers = ConsoleColor.DarkCyan,
            FretboardNotes = ConsoleColor.DarkGray,
            Chord = new CellColorOptions { Color = ConsoleColor.Red, BackgroundColor = ConsoleColor.Black },
            Scale = new CellColorOptions { Color = ConsoleColor.Green, BackgroundColor = ConsoleColor.Black },
        };

        public static readonly ColorTheme Light = new()
        {
            Name = "Light",
            Background = ConsoleColor.White,
            Text = ConsoleColor.Black,
            MenuHeader = ConsoleColor.DarkBlue,
            Lines = ConsoleColor.Gray,
            FretNumbers = ConsoleColor.DarkCyan,
            FretboardNotes = ConsoleColor.Gray,
            Chord = new CellColorOptions { Color = ConsoleColor.DarkRed, BackgroundColor = ConsoleColor.White },
            Scale = new CellColorOptions { Color = ConsoleColor.DarkGreen, BackgroundColor = ConsoleColor.White },
        };

        /// <summary>High contrast: chord notes are black on yellow, scale notes cyan on black.</summary>
        public static readonly ColorTheme Contrast = new()
        {
            Name = "Contrast",
            Background = ConsoleColor.Black,
            Text = ConsoleColor.White,
            MenuHeader = ConsoleColor.Yellow,
            Lines = ConsoleColor.White,
            FretNumbers = ConsoleColor.Cyan,
            FretboardNotes = ConsoleColor.White,
            Chord = new CellColorOptions { Color = ConsoleColor.Black, BackgroundColor = ConsoleColor.Yellow },
            Scale = new CellColorOptions { Color = ConsoleColor.Cyan, BackgroundColor = ConsoleColor.Black },
        };

        public static readonly IReadOnlyList<ColorTheme> All = [Dark, Light, Contrast];

        public static ColorTheme Next(ColorTheme current)
        {
            int index = All.ToList().IndexOf(current);
            return All[(index + 1) % All.Count];
        }
    }
}
