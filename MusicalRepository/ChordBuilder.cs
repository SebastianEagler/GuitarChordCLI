namespace GuitarChordCLI.MusicalRepository
{
    public class ChordNote
    {
        public List<string> Notes{ get; set; } = new List<string>();
        public string Name { get; set; } = String.Empty;  

    }
    internal class ChordBuilder
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
        public ChordNote GetTriads(MusicNotes rootNote, ChordTonal chordTonal, ChordInversion chordInversion = ChordInversion.Root)
        {
            var chord = GetChord(rootNote, chordTonal, chordInversion);                        
            return chord;
        }

        public ChordNote GetChord(MusicNotes rootNote, ChordTonal chordTonal, ChordInversion chordInversion = ChordInversion.Root)
        {            
            var chord = new ChordNote();
            chord.Name = $"{rootNote} {chordTonal} {chordInversion}";
            var notes = NotesData.Notes;
            int rootIndex = GetRootIndex(rootNote);
            int thirdInterval = chordTonal == ChordTonal.Major ? 4 : 3;
            int fifthInterval = 7;

            switch (chordInversion)
            {
                case ChordInversion.First:
                    chord.Notes.Add(notes[(rootIndex + thirdInterval) % 12]);
                    chord.Notes.Add(notes[(rootIndex + fifthInterval) % 12]);
                    chord.Notes.Add(notes[rootIndex]);
                    break;

                case ChordInversion.Second:
                    chord.Notes.Add(notes[(rootIndex + fifthInterval) % 12]);
                    chord.Notes.Add(notes[rootIndex]);
                    chord.Notes.Add(notes[(rootIndex + thirdInterval) % 12]);
                    break;

                default:
                    chord.Notes.Add(notes[rootIndex]);
                    chord.Notes.Add(notes[(rootIndex + thirdInterval) % 12]);
                    chord.Notes.Add(notes[(rootIndex + fifthInterval) % 12]);
                    break;
            }

            return chord;
        }
                
        public ChordNote GetSeventhChord(MusicNotes rootNote, ChordSeventhTonal chordTonal, ChordSeventhInversion chordInversion = ChordSeventhInversion.Root)
        {
            var seventhChord = new ChordNote();
            seventhChord.Name = $" {rootNote} {chordTonal} 7 {chordInversion}";
            var notes = NotesData.Notes;
            int rootIndex = GetRootIndex(rootNote); 

            // Wybór interwałów w zależności od rodzaju akordu
            int thirdInterval = chordTonal == ChordSeventhTonal.Major || chordTonal == ChordSeventhTonal.Dominant ? 4 : 3;
            int fifthInterval = 7;
            int seventhInterval = chordTonal == ChordSeventhTonal.Major ? 11 : 10; // Major 7 ma septymę wielką, reszta małą

            switch (chordInversion)
            {
                case ChordSeventhInversion.First:
                    seventhChord.Notes.Add(notes[(rootIndex + thirdInterval) % 12]); // Tercja w basie
                    seventhChord.Notes.Add(notes[(rootIndex + fifthInterval) % 12]);
                    seventhChord.Notes.Add(notes[(rootIndex + seventhInterval) % 12]);
                    seventhChord.Notes.Add(notes[rootIndex]); // Tonika na górze
                    break;

                case ChordSeventhInversion.Second:
                    seventhChord.Notes.Add(notes[(rootIndex + fifthInterval) % 12]); // Kwinta w basie
                    seventhChord.Notes.Add(notes[(rootIndex + seventhInterval) % 12]);
                    seventhChord.Notes.Add(notes[rootIndex]);
                    seventhChord.Notes.Add(notes[(rootIndex + thirdInterval) % 12]); // Tercja na górze
                    break;

                case ChordSeventhInversion.Third:
                    seventhChord.Notes.Add(notes[(rootIndex + seventhInterval) % 12]); // Septyma w basie
                    seventhChord.Notes.Add(notes[rootIndex]);
                    seventhChord.Notes.Add(notes[(rootIndex + thirdInterval) % 12]);
                    seventhChord.Notes.Add(notes[(rootIndex + fifthInterval) % 12]); // Kwinta na górze
                    break;

                default:
                    seventhChord.Notes.Add(notes[rootIndex]); // Root position
                    seventhChord.Notes.Add(notes[(rootIndex + thirdInterval) % 12]);
                    seventhChord.Notes.Add(notes[(rootIndex + fifthInterval) % 12]);
                    seventhChord.Notes.Add(notes[(rootIndex + seventhInterval) % 12]);
                    break;
            }

            return seventhChord;
        }
        
        public List<string> GetExtendedChord(MusicNotes rootNote, ChordExtendedType chordType, ChordInversion chordInversion = ChordInversion.Root)
        {
            var chord = new List<string>();
            var notes = NotesData.Notes;
            int rootIndex = GetRootIndex(rootNote); 
            
            // Wybór interwałów w zależności od typu akordu
            int thirdInterval = chordType == ChordExtendedType.Diminished ? 3 :
                                chordType == ChordExtendedType.Augmented ? 4 : 5;

            int fifthInterval = chordType == ChordExtendedType.Diminished ? 6 :
                                chordType == ChordExtendedType.Augmented ? 8 : 7;

            switch (chordInversion)
            {
                case ChordInversion.First:
                    chord.Add(notes[(rootIndex + thirdInterval) % 12]); // Tercja w basie
                    chord.Add(notes[(rootIndex + fifthInterval) % 12]);
                    chord.Add(notes[rootIndex]); // Tonika na górze
                    break;

                case ChordInversion.Second:
                    chord.Add(notes[(rootIndex + thirdInterval) % 12]); // Tercja w basie
                    chord.Add(notes[rootIndex]);
                    chord.Add(notes[(rootIndex + fifthInterval) % 12]); // Kwinta na górze
                    break;

                default:
                    chord.Add(notes[rootIndex]); // Root position
                    chord.Add(notes[(rootIndex + thirdInterval) % 12]);
                    chord.Add(notes[(rootIndex + fifthInterval) % 12]);
                    break;
            }

            return chord;
        }



    }
}
