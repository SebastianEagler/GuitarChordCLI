namespace GuitarChordCLI.MusicalRepository
{
    /// <summary>
    /// Note names on a 6-string guitar fretboard, generated from the standard tuning.
    /// </summary>
    public static class FretboardData
    {
        /// <summary>Number of frets (positions 0..Count, where 0 is the open string).</summary>
        public const int Count = 24;

        /// <summary>Standard tuning, from the lowest to the highest string.</summary>
        private static readonly (string Name, MusicNotes Open)[] Tuning =
        [
            ("E_low", MusicNotes.E),
            ("B", MusicNotes.B),
            ("G", MusicNotes.G),
            ("D", MusicNotes.D),
            ("A", MusicNotes.A),
            ("E_high", MusicNotes.E),
        ];

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
