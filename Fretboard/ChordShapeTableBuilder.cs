using CLITextTable.CliTable;
using GuitarChordCLI.MusicalRepository;
using GuitarChordCLI.Rendering;
using GuitarChordCLI.Themes;

namespace GuitarChordCLI.Fretboard
{
    /// <summary>Draws a chord shape as a small fretboard diagram with chord tone numbers.</summary>
    internal static class ChordShapeTableBuilder
    {
        // Rows are drawn from the highest string to the lowest, like the main fretboard.
        private static readonly string[] StringNames = ["e", "B", "G", "D", "A", "E"];
        private const string Empty = ".";

        public static CliTable Build(ChordShape shape, ChordNote chord, ColorTheme theme)
        {
            // Column 0 holds the string letter, column 1 is the zero fret (open string) column.
            var header = new List<string> { "", "0" };
            for (int i = 0; i < ChordShapeFinder.WindowSize; i++)
                header.Add((shape.BaseFret + i).ToString());

            var table = new CliTable(options =>
            {
                options.MinCellDataWidth = 1;
                options.RowSeparatorMarginLeft = 1;
                options.RowSeparatorMarginRight = 1;
                options.DrawHeaderLine = false;
                options.DrawRowLines = false;
                options.DefaultRowColor = theme.FretboardNotes;
                options.DefaultHeaderColor = theme.FretNumbers;
                options.DefaultRowLineColor = theme.Lines;
                options.DefaultBackgroundColor = theme.Background;
            }, header.ToArray());

            // Chord tone number of every open string (0 = the open string is not a chord tone), lowest string first.
            var tones = chord.Notes.Select(n => NotesData.Notes.IndexOf(n)).ToList();
            var openDegrees = FretboardData.OpenStringsLowToHigh.Select(open => tones.IndexOf((int)open) + 1).ToArray();

            int strings = shape.Frets.Length;
            for (int row = 0; row < strings; row++)
            {
                int s = strings - 1 - row; // index from the lowest string
                var cells = new List<string> { StringNames[row] };

                int? fret = shape.Frets[s];
                // Zero fret column: x = muted, chord tone number = open string played.
                string marker = fret == null ? "x" : fret == 0 ? shape.Degrees[s]!.Value.ToString() : Empty;
                cells.Add(marker);

                for (int i = 0; i < ChordShapeFinder.WindowSize; i++)
                    cells.Add(fret == shape.BaseFret + i ? shape.Degrees[s]!.Value.ToString() : Empty);

                table.AddStringRow(cells);
            }

            // Highlight played notes.
            for (int row = 0; row < strings; row++)
            {
                int s = strings - 1 - row;
                int? fret = shape.Frets[s];

                // The string letter is colored when the open string is a chord tone (zero fret note),
                // regardless of whether the open string is played in this shape.
                if (openDegrees[s] > 0)
                    table.SetColor(0, row, theme.Chord.Color, theme.Chord.BackgroundColor);

                if (fret == null)
                    continue;

                if (fret == 0)
                    table.SetColor(1, row, theme.Chord.Color, theme.Chord.BackgroundColor);
                else
                    table.SetColor(2 + (fret.Value - shape.BaseFret), row, theme.Chord.Color, theme.Chord.BackgroundColor);
            }

            return table;
        }

        public static string Title(int number, ChordShape shape)
            => shape.BaseFret == 1
                ? $"Shape {number} (open, frets 1-{ChordShapeFinder.WindowSize})"
                : $"Shape {number} (frets {shape.BaseFret}-{shape.BaseFret + ChordShapeFinder.WindowSize - 1})";

        public static Block ToBlock(int number, ChordShape shape, ChordNote chord, ColorTheme theme)
        {
            var buffer = new BufferedOutput();
            using var table = Build(shape, chord, theme);
            table.Print(buffer);
            return new Block(Title(number, shape), buffer.Lines);
        }
    }
}
