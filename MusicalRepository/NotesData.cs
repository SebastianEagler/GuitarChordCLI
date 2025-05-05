using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarChordCLI.MusicalRepository
{
    internal enum MusicNotes
    {
        C, C_sharp, D, D_sharp, E, F, F_sharp, G, G_sharp, A, A_sharp, B
    }

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

        public static readonly string C = "C";
        public static readonly string C_sharp = "C#";
        public static readonly string D = "D";
        public static readonly string D_sharp = "D#";
        public static readonly string E = "E";
        public static readonly string F = "F";
        public static readonly string F_sharp = "F#";
        public static readonly string G = "G";
        public static readonly string G_sharp = "G#";
        public static readonly string A = "A";
        public static readonly string A_sharp = "A#";
        public static readonly string B = "B";

        private static readonly Dictionary<string, MusicNotes> _noteMapping = new Dictionary<string, MusicNotes>
            {
                {C, MusicNotes.C},
                {C_sharp, MusicNotes.C_sharp},
                {D, MusicNotes.D},
                {D_sharp, MusicNotes.D_sharp},
                {E, MusicNotes.E},
                {F, MusicNotes.F},
                {F_sharp, MusicNotes.F_sharp},
                {G, MusicNotes.G},
                {G_sharp, MusicNotes.G_sharp},
                {A, MusicNotes.A},
                {A_sharp, MusicNotes.A_sharp},
                {B, MusicNotes.B}
            };

        public static MusicNotes? Get(string note)
        {
            if (_noteMapping.TryGetValue(note, out var musicNote))
            {
                return musicNote;
            }
            return null;
        }

        public static string Get(MusicNotes note)
        {
            foreach (var kvp in _noteMapping)
            {
                if (kvp.Value == note)
                {
                    return kvp.Key;
                }
            }
            return C;
        }
    }
}