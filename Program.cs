using GuitarChordCLI.Fretboard;
using GuitarChordCLI.Menu;
using GuitarChordCLI.MusicalRepository;
using GuitarChordCLI.Rendering;
using GuitarChordCLI.Themes;

namespace GuitarChordCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleSetup.SetWindowSize();
            ConsoleSetup.ChangeFont();
            ConsoleSetup.SetUtf8();

            var chordBuilder = new ChordBuilder();
            var mainMenu = new MainMenu();

            mainMenu.ShowMenu(selection =>
            {
                var theme = selection.Theme;
                var fretboard = new FretboardBuilder(theme).Build();

                var chord = selection.SeventhMode
                    ? chordBuilder.GetSeventhChord(selection.Root, selection.SeventhTonal)
                    : chordBuilder.GetChord(selection.Root, selection.Tonal);

                // Scale is painted first so chord notes override it.
                if (selection.ShowScale)
                {
                    bool minor = selection.SeventhMode
                        ? selection.SeventhTonal == ChordSeventhTonal.Minor
                        : selection.Tonal == ChordTonal.Minor;
                    var scale = ScalesDefinition.GetScale(selection.Root, minor ? ScaleType.Minor : ScaleType.Major);
                    fretboard.ColorCells(scale, theme.Scale);
                }

                fretboard.ColorCells(chord.Notes, theme.Chord);
                theme.Apply();
                Console.WriteLine(chord.Name);
                fretboard.Print();
                theme.Apply();

                PrintChordShapes(chord, theme);
            });
        }

        private static void PrintChordShapes(ChordNote chord, ColorTheme theme)
        {
            var shapes = ChordShapeFinder.Find(chord);

            // Two empty lines between the fretboard and the chord shapes.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(chord.Notes.Count == 4
                ? "Chord shapes (1 = root, 2 = third, 3 = fifth, 4 = seventh, x = muted, . = not played)"
                : "Chord shapes (1 = root, 2 = third, 3 = fifth, x = muted, . = not played)");

            Console.WriteLine("Colored string letter = open string is a chord tone (zero fret); number in the first column = open string played");

            if (shapes.Count == 0)
            {
                Console.WriteLine("No shape found.");
                return;
            }

            var blocks = shapes
                .Select((shape, index) => ChordShapeTableBuilder.ToBlock(index + 1, shape, chord, theme))
                .ToList();
            SideBySide.Print(blocks, theme.MenuHeader, theme.Text, theme.Background);
            theme.Apply();
        }
    }
}
