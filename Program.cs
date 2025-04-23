using GuitarChordCLI.Setup;
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
            

            Console.WriteLine("Hello, World!");


            ChordGenerator chord = new ChordGenerator();

            var c = chord.GetChord( MusicNotes.C, ChordTonal.Major );
            foreach (var note in c)
            {
                Console.WriteLine(note);
            }

            Console.WriteLine("-----");

            var cm = chord.GetChord(MusicNotes.C, ChordTonal.Minor);
            foreach (var note in cm)
            {
                Console.WriteLine(note);
            }

            CliTable table = new CliTable(["Imie", "Nazwisko","Zawód", "Wiek" ]);
            table.AddRow([ "Sebastian", "Kowalski","programista", "30" ]);
            table.AddRow([ "Anna", "Nowak-Rowawska-Gajowa", "nauczyciekla", "25" ]);
            table.AddRow(new List<string> { "Piotr", "Zielinski","bezrobotny", "40" });
            table.Margins(2, 1);
            table.Print();  

            Console.ReadKey();
        }
    }

}

