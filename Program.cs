using CLITextTable.Configuration;
using GuitarChordCLI.Chords;
using GuitarChordCLI.Generators;
using GuitarChordCLI.MusicalRepository;

namespace GuitarChordCLI
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            ConsoleSetup.SetWindowSize();
            ConsoleSetup.ChangeFont();
            ChordBuilder chord = new ChordBuilder();

            var CellColor = new CellColorOptions { Color = ConsoleColor.Red };
            var CellColorforScale = new CellColorOptions { Color = ConsoleColor.Green };

            var fret0 = new FreadboardBuilder().Build();
            var cm0 = chord.GetChord(MusicNotes.A, ChordTonal.Minor);
            fret0.ColorCells(ScalesDefinition.MinorScales[NotesData.A].ToList(), CellColorforScale);
            fret0.ColorCells(cm0.Notes, CellColor);
            Console.WriteLine(cm0.Name);
            fret0.Print();
            Console.WriteLine("");

            var fret1 = new FreadboardBuilder().Build();
            var cm1 = chord.GetChord(MusicNotes.C, ChordTonal.Major);
            fret1.ColorCells(ScalesDefinition.MajorScales[NotesData.C].ToList(), CellColorforScale);
            fret1.ColorCells(cm1.Notes, CellColor);
            Console.WriteLine(cm1.Name);
            fret1.Print();
            Console.WriteLine("");

            var fret2 = new FreadboardBuilder().Build();
            var cm2 = chord.GetSeventhChord(MusicNotes.C, ChordSeventhTonal.Major);
            fret2.ColorCells(ScalesDefinition.MajorScales[NotesData.C].ToList(), CellColorforScale);
            fret2.ColorCells(cm2.Notes, CellColor);
            Console.WriteLine(cm2.Name);
            fret2.Print();

            Console.ReadKey();
        }
    }

}

