namespace GuitarChordCLI.MusicalRepository
{
    public class ChordNote
    {
        public List<string> Notes{ get; set; } = new List<string>();
        public string Name { get; set; } = String.Empty;  

    }

    public class ChordBuilder
    {
        private static int GetRootIndex(MusicNotes rootNote)
        {
            int rootIndex = (int)rootNote;
            if (rootIndex < 0 || rootIndex >= NotesData.Notes.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(rootNote), "Invalid root note.");
            }
            return rootIndex;
        }

        /// <summary>
        /// Builds chord notes from semitone intervals above the root.
        /// Inversion n moves the first n notes to the end (bass = n-th chord tone).
        /// </summary>
        private static ChordNote Build(MusicNotes rootNote, string name, int[] intervals, int inversion)
        {
            int rootIndex = GetRootIndex(rootNote);
            var notes = NotesData.Notes;

            var chordNotes = intervals
                .Select(i => notes[(rootIndex + i) % notes.Count])
                .ToList();

            int shift = inversion % chordNotes.Count;
            var ordered = chordNotes.Skip(shift).Concat(chordNotes.Take(shift)).ToList();

            return new ChordNote
            {
                Name = inversion == 0 ? name : $"{name} (inversion {inversion})",
                Notes = ordered
            };
        }

        public ChordNote GetChord(MusicNotes rootNote, ChordTonal chordTonal, ChordInversion chordInversion = ChordInversion.Root)
        {
            int[] intervals = chordTonal == ChordTonal.Major ? [0, 4, 7] : [0, 3, 7];
            return Build(rootNote, $"{NotesData.Get(rootNote)} {chordTonal}", intervals, (int)chordInversion);
        }

        public ChordNote GetSeventhChord(MusicNotes rootNote, ChordSeventhTonal chordTonal, ChordSeventhInversion chordInversion = ChordSeventhInversion.Root)
        {
            int[] intervals = chordTonal switch
            {
                ChordSeventhTonal.Major => [0, 4, 7, 11],
                ChordSeventhTonal.Minor => [0, 3, 7, 10],
                _ => [0, 4, 7, 10] // Dominant (and None)
            };
            return Build(rootNote, $"{NotesData.Get(rootNote)} {chordTonal} 7", intervals, (int)chordInversion);
        }

        public ChordNote GetExtendedChord(MusicNotes rootNote, ChordExtendedType chordType, ChordInversion chordInversion = ChordInversion.Root)
        {
            int[] intervals = chordType switch
            {
                ChordExtendedType.Diminished => [0, 3, 6],
                ChordExtendedType.Augmented => [0, 4, 8],
                _ => [0, 5, 7] // Suspended (sus4)
            };
            return Build(rootNote, $"{NotesData.Get(rootNote)} {chordType}", intervals, (int)chordInversion);
        }
    }
}
