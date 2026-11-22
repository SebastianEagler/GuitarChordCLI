namespace GuitarChordCLI.MusicalRepository
{
    /// <summary>
    /// Note names on a 6-string guitar fretboard, generated from the standard tuning.
    /// </summary>
    public static class FretboardData
    {
        /// <summary>Number of frets (positions 0..Count, where 0 is the open string).</summary>
        public const int Count = 24;

        /// <summary>Standard tuning in display order: the highest string first (as in tablature), the lowest last.</summary>
        private static readonly (string Name, MusicNotes Open)[] Tuning =
        [
            ("E_high", MusicNotes.E),
            ("B", MusicNotes.B),
            ("G", MusicNotes.G),
            ("D", MusicNotes.D),
            ("A", MusicNotes.A),
            ("E_low", MusicNotes.E),
        ];

        /// <summary>Open strings from the lowest to the highest one (E A D G B E).</summary>
        public static readonly MusicNotes[] OpenStringsLowToHigh = Tuning.Select(s => s.Open).Reverse().ToArray();

        /// <summary>Note names per string, index = fret (0..Count).</summary>
        public static readonly Dictionary<string, string[]> Notes = BuildNotes();

        private static Dictionary<string, string[]> BuildNotes()
        {
            var result = new Dictionary<string, string[]>();
            foreach (var (name, open) in Tuning)
            {
                result[name] = Enumerable.Range(0, Count + 1)
                    .Select(fret => NotesData.Notes[((int)open + fret) % NotesData.Notes.Count])
                    .ToArray();
            }
            return result;
        }
    }
}
