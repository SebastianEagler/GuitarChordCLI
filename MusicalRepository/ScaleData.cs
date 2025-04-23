using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarChordCLI.Chords
{
    using System;
    using System.Collections.Generic;

    public static class ScalesDefinition
    {
        private static readonly Dictionary<string, string[]> _majorScales = new Dictionary<string, string[]>
        {
            {"C", new string[] {"C", "D", "E", "F", "G", "A", "B"}},
            {"G", new string[] {"G", "A", "B", "C", "D", "E", "F#"}},
            {"D", new string[] {"D", "E", "F#", "G", "A", "B", "C#"}},
            {"A", new string[] {"A", "B", "C#", "D", "E", "F#", "G#"}},
            {"E", new string[] {"E", "F#", "G#", "A", "B", "C#", "D#"}},
            {"B", new string[] {"B", "C#", "D#", "E", "F#", "G#", "A#"}},
            {"F", new string[] {"F", "G", "A", "Bb", "C", "D", "E"}}
        };

        private static readonly Dictionary<string, string[]> _minorScales = new Dictionary<string, string[]>
        {
            {"A", new string[] {"A", "B", "C", "D", "E", "F", "G"}},
            {"E", new string[] {"E", "F#", "G", "A", "B", "C", "D"}},
            {"B", new string[] {"B", "C#", "D", "E", "F#", "G", "A"}},
            {"F#", new string[] {"F#", "G#", "A", "B", "C#", "D", "E"}},
            {"C#", new string[] {"C#", "D#", "E", "F#", "G#", "A", "B"}},
            {"G", new string[] {"G", "A", "Bb", "C", "D", "Eb", "F"}},
            {"D", new string[] {"D", "E", "F", "G", "A", "Bb", "C"}}
        };

        public static Dictionary<string, string[]> MajorScales => _majorScales;
        public static Dictionary<string, string[]> MinorScales => _minorScales;
    }
}
