using GuitarChordCLI.Generators;
using GuitarChordCLI.MusicalRepository;
using GuitarChordCLI.Table;

namespace GuitarChordCLI
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            ConsoleSetup.SetWindowSize();
            ConsoleSetup.ChangeFont();          
            ChordBuilder chord = new ChordBuilder();
            var c = chord.GetChord( MusicNotes.C, ChordTonal.Major );

            //foreach (var note in c)
            //{
            //    Console.WriteLine(note);
            //}

            //Console.WriteLine("-----");

            //var cm = chord.GetChord(MusicNotes.C, ChordTonal.Minor);
            //foreach (var note in cm)
            //{
            //    Console.WriteLine(note);
            //}

            var builder = new  FreadboardBuilder();
            builder.AddNotes( c ).Print();

            Console.ReadKey();
        }
    }

}

