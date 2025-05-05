using CLITextTable.Configuration;
using GuitarChordCLI.Chords;
using GuitarChordCLI.Generators;
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


            var CellColor = new CellColorOptions { Color = ConsoleColor.Red };
            var CellColorforScale = new CellColorOptions { Color = ConsoleColor.Green };

            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowMenu((chordTonal, chordRoot, showScaleNotes) =>
            {
                ChordBuilder chord = new ChordBuilder();
                var fret = new FreadboardBuilder().Build();
                var notes = chord.GetChord(chordRoot, chordTonal);

                if (showScaleNotes)
                    if (chordTonal == ChordTonal.Major)
                        fret.ColorCells(ScalesDefinition.MajorScales[NotesData.Get(chordRoot)].ToList(), CellColorforScale);
                    else
                        fret.ColorCells(ScalesDefinition.MinorScales[NotesData.Get(chordRoot)].ToList(), CellColorforScale);


                fret.ColorCells(notes.Notes, CellColor);
                Console.WriteLine(notes.Name);
                fret.Print();
            });

        }
    }

}

