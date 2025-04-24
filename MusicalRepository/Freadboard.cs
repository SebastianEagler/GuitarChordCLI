using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarChordCLI.MusicalRepository
{
    internal static class Freadboard
    {
        public const int Count = 24;

        public static readonly Dictionary<string, MusicNotes[]> Frets = new Dictionary<string, MusicNotes[]>{
        {"E_low", new MusicNotes[] { MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E } },
        {"B", new MusicNotes[] { MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B } },
        {"G", new MusicNotes[] { MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G } },
        {"D", new MusicNotes[] { MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D } },
        {"A", new MusicNotes[] { MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A } },
        {"E_high", new MusicNotes[] { MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E, MusicNotes.F, MusicNotes.F_sharp, MusicNotes.G, MusicNotes.G_sharp, MusicNotes.A, MusicNotes.A_sharp, MusicNotes.B, MusicNotes.C, MusicNotes.C_sharp, MusicNotes.D, MusicNotes.D_sharp, MusicNotes.E } }
      };

        public static readonly Dictionary<string, string[]> Notes = new Dictionary<string, string[]>{
    {"E_low", new string[] { "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#",
                         "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#","E" } },
    {"B", new string[] { "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#",
                         "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" } },
    {"G", new string[] { "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#",
                         "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#","G" } },
    {"D", new string[] { "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#",
                         "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#","D" } },
    {"A", new string[] { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#",
                         "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#","A" } },
    {"E_high", new string[] { "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#",
                         "E", "F", "F#", "G", "G#", "A", "A#", "B", "C", "C#", "D", "D#","E" } }
    };

    }


}