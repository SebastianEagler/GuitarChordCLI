namespace GuitarChordCLI.MusicalRepository
{
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
        public List<string> GetTriads(MusicNotes rootNote, ChordTonal chordTonal, ChordInversion chordInversion = ChordInversion.Root)
        {
            return GetChord(rootNote, chordTonal, chordInversion);
        }

        public List<string> GetChord(MusicNotes rootNote, ChordTonal chordTonal, ChordInversion chordInversion = ChordInversion.Root)
        {
            var chord = new List<string>();
            var notes = NotesData.Notes;
            int rootIndex = GetRootIndex(rootNote);
            int thirdInterval = chordTonal == ChordTonal.Major ? 4 : 3;
            int fifthInterval = 7;

            switch (chordInversion)
            {
                case ChordInversion.First:
                    chord.Add(notes[(rootIndex + thirdInterval) % 12]);
                    chord.Add(notes[(rootIndex + fifthInterval) % 12]);
                    chord.Add(notes[rootIndex]);
                    break;

                case ChordInversion.Second:
                    chord.Add(notes[(rootIndex + fifthInterval) % 12]);
                    chord.Add(notes[rootIndex]);
                    chord.Add(notes[(rootIndex + thirdInterval) % 12]);
                    break;

                default:
                    chord.Add(notes[rootIndex]);
                    chord.Add(notes[(rootIndex + thirdInterval) % 12]);
                    chord.Add(notes[(rootIndex + fifthInterval) % 12]);
                    break;
            }

            return chord;
        }
                
        public List<string> GetSeventhChord(MusicNotes rootNote, ChordSeventhTonal chordTonal, ChordSeventhInversion chordInversion = ChordSeventhInversion.Root)
        {
            var seventhChord = new List<string>();
            var notes = NotesData.Notes;
            int rootIndex = GetRootIndex(rootNote); 

            // Wybór interwałów w zależności od rodzaju akordu
            int thirdInterval = chordTonal == ChordSeventhTonal.Major || chordTonal == ChordSeventhTonal.Dominant ? 4 : 3;
            int fifthInterval = 7;
            int seventhInterval = chordTonal == ChordSeventhTonal.Major ? 11 : 10; // Major 7 ma septymę wielką, reszta małą

            switch (chordInversion)
            {
                case ChordSeventhInversion.First:
                    seventhChord.Add(notes[(rootIndex + thirdInterval) % 12]); // Tercja w basie
                    seventhChord.Add(notes[(rootIndex + fifthInterval) % 12]);
                    seventhChord.Add(notes[(rootIndex + seventhInterval) % 12]);
                    seventhChord.Add(notes[rootIndex]); // Tonika na górze
                    break;

                case ChordSeventhInversion.Second:
                    seventhChord.Add(notes[(rootIndex + fifthInterval) % 12]); // Kwinta w basie
                    seventhChord.Add(notes[(rootIndex + seventhInterval) % 12]);
                    seventhChord.Add(notes[rootIndex]);
                    seventhChord.Add(notes[(rootIndex + thirdInterval) % 12]); // Tercja na górze
                    break;

                case ChordSeventhInversion.Third:
                    seventhChord.Add(notes[(rootIndex + seventhInterval) % 12]); // Septyma w basie
                    seventhChord.Add(notes[rootIndex]);
                    seventhChord.Add(notes[(rootIndex + thirdInterval) % 12]);
                    seventhChord.Add(notes[(rootIndex + fifthInterval) % 12]); // Kwinta na górze
                    break;

                default:
                    seventhChord.Add(notes[rootIndex]); // Root position
                    seventhChord.Add(notes[(rootIndex + thirdInterval) % 12]);
                    seventhChord.Add(notes[(rootIndex + fifthInterval) % 12]);
                    seventhChord.Add(notes[(rootIndex + seventhInterval) % 12]);
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
