using CLITextTable.CliTable;
using GuitarChordCLI.MusicalRepository;
using GuitarChordCLI.Themes;


namespace GuitarChordCLI.Menu
{
    /// <summary>Current user choice made in the main menu.</summary>
    internal record ChordSelection(
        ChordTonal Tonal,
        ChordSeventhTonal SeventhTonal,
        bool SeventhMode,
        MusicNotes Root,
        bool ShowScale,
        ColorTheme Theme);

    delegate void MenuAction(ChordSelection selection);

    internal class MainMenu
    {
        private ChordTonal _chordTonal = ChordTonal.Major;
        private ChordSeventhTonal _seventhTonal = ChordSeventhTonal.Major;
        private bool _seventhMode = false;
        private string _chordRoot = "C";
        private int _chosenIndex = 0;
        private bool _showScaleNotes = true;
        private ColorTheme _theme = ColorTheme.Dark;

        private void Build()
        {
            using var cliTable = new CliTable(options =>
            {
                options.MinCellDataWidth = 15;
                options.RowSeparatorMarginLeft = 1;
                options.RowSeparatorMarginRight = 1;
                options.DrawHeaderLine = true;
                options.DrawRowLines = false;
                options.MaxCellDataWidth = 20;
                options.DefaultRowColor = _theme.Text;
                options.DefaultHeaderColor = _theme.MenuHeader;
                options.DefaultRowLineColor = _theme.Lines;
                options.DefaultBackgroundColor = _theme.Background;

            }, ["Quality [m]", "Root [\u2190 \u2192]", "Show scale [s]", "Chord type [t]", "Colors [c]", "Exit"]);
            string quality = _seventhMode ? $"{_seventhTonal} 7" : _chordTonal.ToString();
            cliTable.AddStringRow(new List<string> { quality, _chordRoot, _showScaleNotes.ToString(), _seventhMode ? "Seventh" : "Triad", _theme.Name, "Esc" });
            cliTable.Print();
        }

        private void PrintLegend()
        {
            _theme.Apply();
            Console.Write("Legend: ");
            WriteColored("chord notes", _theme.Chord.Color, _theme.Chord.BackgroundColor);
            _theme.Apply();
            Console.Write(", ");
            WriteColored("scale notes", _theme.Scale.Color, _theme.Scale.BackgroundColor);
            _theme.Apply();
            Console.WriteLine();
        }

        private static void WriteColored(string text, ConsoleColor foreground, ConsoleColor background)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Write(text);
        }

        private static ChordSeventhTonal NextSeventh(ChordSeventhTonal current) => current switch
        {
            ChordSeventhTonal.Major => ChordSeventhTonal.Minor,
            ChordSeventhTonal.Minor => ChordSeventhTonal.Dominant,
            _ => ChordSeventhTonal.Major
        };

        /// <summary>Handles one key press. Returns true when the user wants to exit.</summary>
        private bool HandleKey()
        {
            var key = Console.ReadKey(true); // read the key without echoing it
            int notesCount = NotesData.Notes.Count;

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    _chosenIndex = (_chosenIndex - 1 + notesCount) % notesCount;
                    _chordRoot = NotesData.Notes[_chosenIndex];
                    break;
                case ConsoleKey.RightArrow:
                    _chosenIndex = (_chosenIndex + 1) % notesCount;
                    _chordRoot = NotesData.Notes[_chosenIndex];
                    break;
                case ConsoleKey.M:
                    if (_seventhMode)
                        _seventhTonal = NextSeventh(_seventhTonal);
                    else
                        _chordTonal = _chordTonal == ChordTonal.Major ? ChordTonal.Minor : ChordTonal.Major;
                    break;
                case ConsoleKey.T:
                    _seventhMode = !_seventhMode;
                    break;
                case ConsoleKey.S:
                    _showScaleNotes = !_showScaleNotes;
                    break;
                case ConsoleKey.C:
                    _theme = ColorTheme.Next(_theme);
                    break;
                case ConsoleKey.Escape:
                    return true;
            }
            return false;
        }

        public void ShowMenu(MenuAction menuAction)
        {
            try
            {
                while (true)
                {
                    // Set the theme background first so Clear() paints the whole window with it.
                    _theme.Apply();
                    Console.Clear();
                    Console.WriteLine("Welcome to the Guitar Chord CLI!");
                    Build();
                    PrintLegend();
                    Console.WriteLine();

                    var root = NotesData.Get(_chordRoot) ?? MusicNotes.C;
                    _theme.Apply();
                    menuAction(new ChordSelection(_chordTonal, _seventhTonal, _seventhMode, root, _showScaleNotes, _theme));

                    if (HandleKey())
                        break;
                }
            }
            finally
            {
                // Restore the user's original console colors.
                Console.ResetColor();
                Console.Clear();
            }
        }
    }
}
