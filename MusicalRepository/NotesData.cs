using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarChordCLI.MusicalRepository
{
    internal enum MusicNotes {
        C, C_sharp, D, D_sharp, Em, F, F_sharp, G, G_sharp, A, A_sharp, B  }

    internal static class NotesData
    {
        private static readonly List<string> _notes = new List<string>
        {
            "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
        };

        public static List<string> Notes
        {
            get
            {
                return _notes;
            }
        }
    }
}