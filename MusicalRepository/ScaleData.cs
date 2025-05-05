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
        {"C#", new string[] {"C#", "D#", "E#", "F#", "G#", "A#", "B#"}},
        {"D", new string[] {"D", "E", "F#", "G", "A", "B", "C#"}},
        {"D#", new string[] {"D#", "E#", "F##", "G#", "A#", "B#", "C##"}},
        {"E", new string[] {"E", "F#", "G#", "A", "B", "C#", "D#"}},
        {"F", new string[] {"F", "G", "A", "Bb", "C", "D", "E"}},
        {"F#", new string[] {"F#", "G#", "A#", "B", "C#", "D#", "E#"}},
        {"G", new string[] {"G", "A", "B", "C", "D", "E", "F#"}},
        {"G#", new string[] {"G#", "A#", "B#", "C#", "D#", "E#", "F##"}},
        {"A", new string[] {"A", "B", "C#", "D", "E", "F#", "G#"}},
        {"A#", new string[] {"A#", "B#", "C##", "D#", "E#", "F##", "G##"}},
        {"B", new string[] {"B", "C#", "D#", "E", "F#", "G#", "A#"}}
    };

        private static readonly Dictionary<string, string[]> _minorScales = new Dictionary<string, string[]>
    {
        {"C", new string[] {"C", "D", "Eb", "F", "G", "Ab", "Bb"}},
        {"C#", new string[] {"C#", "D#", "E", "F#", "G#", "A", "B"}},
        {"D", new string[] {"D", "E", "F", "G", "A", "Bb", "C"}},
        {"D#", new string[] {"D#", "E#", "F#", "G#", "A#", "B", "C#"}},
        {"E", new string[] {"E", "F#", "G", "A", "B", "C", "D"}},
        {"F", new string[] {"F", "G", "Ab", "Bb", "C", "Db", "Eb"}},
        {"F#", new string[] {"F#", "G#", "A", "B", "C#", "D", "E"}},
        {"G", new string[] {"G", "A", "Bb", "C", "D", "Eb", "F"}},
        {"G#", new string[] {"G#", "A#", "B", "C#", "D#", "E", "F#"}},
        {"A", new string[] {"A", "B", "C", "D", "E", "F", "G"}},
        {"A#", new string[] {"A#", "B#", "C#", "D#", "E#", "F#", "G#"}},
        {"B", new string[] {"B", "C#", "D", "E", "F#", "G", "A"}}
    };

        public static Dictionary<string, string[]> MajorScales => _majorScales;
        public static Dictionary<string, string[]> MinorScales => _minorScales;
    }
}
