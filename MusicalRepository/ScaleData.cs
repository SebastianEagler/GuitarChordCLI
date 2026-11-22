namespace GuitarChordCLI.MusicalRepository
{
    public enum ScaleType
    {
        Major,
        Minor
    }

    /// <summary>
    /// Generates scales from semitone intervals, using the same sharp-only
    /// notation as <see cref="NotesData"/> and the fretboard.
    /// </summary>
    public static class ScalesDefinition
    {
        private static readonly int[] MajorIntervals = [0, 2, 4, 5, 7, 9, 11];
        private static readonly int[] MinorIntervals = [0, 2, 3, 5, 7, 8, 10]; // natural minor

        public static List<string> GetScale(MusicNotes root, ScaleType type)
        {
            var intervals = type == ScaleType.Major ? MajorIntervals : MinorIntervals;
            return intervals
                .Select(i => NotesData.Notes[((int)root + i) % NotesData.Notes.Count])
                .ToList();
        }
    }
}
