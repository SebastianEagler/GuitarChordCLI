using GuitarChordCLI.Fretboard;
using GuitarChordCLI.Menu;
using GuitarChordCLI.MusicalRepository;

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
            });
        }
    }
}
