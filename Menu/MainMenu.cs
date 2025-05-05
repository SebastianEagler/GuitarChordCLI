using CLITextTable.CliTable;
using GuitarChordCLI.MusicalRepository;


namespace GuitarChordCLI.Menu
{
    
    delegate void MenuAction(ChordTonal chordTonal, MusicNotes chordRoot, bool showScaleNotes );
    internal class MainMenu
    {

        private ChordTonal chordTonal { get; set; } = ChordTonal.Major;
        private string chordRoot { get; set; } = "C";
        private int _choosenIndex = 0;
        private bool _showScaleNotes = true;

        public MainMenu()
        {
        }

        private void Build()
        {
            using var _cliTable = new CliTable(options =>
            {
                options.MinCellDataWidth = 15;
                options.RowSeparatorMarginLeft = 1;
                options.RowSeparatorMarginRight = 1;
                options.DrawHeaderLine = true;
                options.DrawRowLines = false;
                options.MaxCellDataWidth = 20;
                options.DefaultRowColor = ConsoleColor.Gray;
                options.DefaultHeaderColor = ConsoleColor.Yellow;
                options.DefaultRowLineColor = ConsoleColor.DarkGray;

            }, ["Major/Minor [m]", "Chord [\u2190 \u2192]","Show scale [s]", "Exit"]);
            _cliTable.AddStringRow(new List<string> { chordTonal.ToString(), chordRoot, _showScaleNotes.ToString(), "Esc" });
            _cliTable.Print();
        }
        private string WaitForKeypressed()
        {
            var key = Console.ReadKey(true); // Odczytaj klawisz bez jego wyświetlania
            if (key.Key == ConsoleKey.LeftArrow)
            {
                _choosenIndex--;
                if (_choosenIndex <= 0)
                    _choosenIndex = NotesData.Notes.Count-1;
                _choosenIndex = _choosenIndex % NotesData.Notes.Count;
                chordRoot = NotesData.Notes[_choosenIndex];
                return "";
            }
            if (key.Key == ConsoleKey.RightArrow)
            {
                _choosenIndex++;
                if (_choosenIndex >= NotesData.Notes.Count)
                    _choosenIndex = 0;
                _choosenIndex = _choosenIndex % NotesData.Notes.Count;
                chordRoot = NotesData.Notes[_choosenIndex];
                return "";
            }
            if (key.Key == ConsoleKey.M) 
            {
                if (chordTonal == ChordTonal.Major)
                    chordTonal = ChordTonal.Minor;
                else
                    chordTonal = ChordTonal.Major;
            }

            if (key.Key == ConsoleKey.S)
            {
                _showScaleNotes = !_showScaleNotes;
            }

            if (key.Key == ConsoleKey.Escape) 
                    { 
                      return "exit";
                    }

            return "";
        }

        public void ShowMenu(MenuAction menuAction )
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Guitar Chord CLI!");
                Build();
              //  Console.WriteLine("Colors. White: chords notes, Green: scale notes");

                var root = NotesData.Get(chordRoot);
                menuAction!.Invoke(chordTonal, root ?? MusicNotes.C, _showScaleNotes );

                var result = WaitForKeypressed();
                if (result == "exit")
                    break;

            }
        }
    }
}
