using GuitarChordCLI.MusicalRepository;
using Xunit;

namespace GuitarChordCLI.Tests
{
    public class ChordTests
    {
        private readonly ChordBuilder _builder = new();

        [Theory]
        [InlineData(MusicNotes.C, ChordTonal.Major, "C E G")]
        [InlineData(MusicNotes.C, ChordTonal.Minor, "C D# G")]
        [InlineData(MusicNotes.A, ChordTonal.Minor, "A C E")]
        [InlineData(MusicNotes.B, ChordTonal.Major, "B D# F#")]
        public void Triads(MusicNotes root, ChordTonal tonal, string expected)
        {
            Assert.Equal(expected, string.Join(' ', _builder.GetChord(root, tonal).Notes));
        }

        [Theory]
        [InlineData(ChordSeventhTonal.Major, "C E G B")]
        [InlineData(ChordSeventhTonal.Minor, "C D# G A#")]
        [InlineData(ChordSeventhTonal.Dominant, "C E G A#")]
        public void SeventhChords(ChordSeventhTonal tonal, string expected)
        {
            Assert.Equal(expected, string.Join(' ', _builder.GetSeventhChord(MusicNotes.C, tonal).Notes));
        }

        [Theory]
        [InlineData(ChordInversion.Root, "C E G")]
        [InlineData(ChordInversion.First, "E G C")]
        [InlineData(ChordInversion.Second, "G C E")]
        public void TriadInversions(ChordInversion inversion, string expected)
        {
            Assert.Equal(expected, string.Join(' ', _builder.GetChord(MusicNotes.C, ChordTonal.Major, inversion).Notes));
        }

        [Fact]
        public void SeventhThirdInversionHasSeventhInBass()
        {
            var chord = _builder.GetSeventhChord(MusicNotes.C, ChordSeventhTonal.Major, ChordSeventhInversion.Third);
            Assert.Equal("B C E G", string.Join(' ', chord.Notes));
        }

        [Theory]
        [InlineData(ChordExtendedType.Diminished, "C D# F#")]
        [InlineData(ChordExtendedType.Augmented, "C E G#")]
        [InlineData(ChordExtendedType.Suspended, "C F G")]
        public void ExtendedChords(ChordExtendedType type, string expected)
        {
            Assert.Equal(expected, string.Join(' ', _builder.GetExtendedChord(MusicNotes.C, type).Notes));
        }

        [Fact]
        public void RootChordNameHasNoInversionSuffix()
        {
            Assert.Equal("B Major", _builder.GetChord(MusicNotes.B, ChordTonal.Major).Name);
            Assert.Equal("C Dominant 7", _builder.GetSeventhChord(MusicNotes.C, ChordSeventhTonal.Dominant).Name);
        }
    }

    public class ScaleTests
    {
        [Theory]
        [InlineData(MusicNotes.F, ScaleType.Major, "F G A A# C D E")]
        [InlineData(MusicNotes.C, ScaleType.Minor, "C D D# F G G# A#")]
        [InlineData(MusicNotes.C, ScaleType.Major, "C D E F G A B")]
        public void Scales(MusicNotes root, ScaleType type, string expected)
        {
            Assert.Equal(expected, string.Join(' ', ScalesDefinition.GetScale(root, type)));
        }
    }

    public class FretboardTests
    {
        [Fact]
        public void StandardTuningOpenStrings()
        {
            Assert.Equal("E", FretboardData.Notes["E_low"][0]);
            Assert.Equal("B", FretboardData.Notes["B"][0]);
            Assert.Equal("G", FretboardData.Notes["G"][0]);
            Assert.Equal("D", FretboardData.Notes["D"][0]);
            Assert.Equal("A", FretboardData.Notes["A"][0]);
            Assert.Equal("E", FretboardData.Notes["E_high"][0]);
        }

        [Fact]
        public void TwelfthFretRepeatsOpenNote()
        {
            foreach (var notes in FretboardData.Notes.Values)
            {
                Assert.Equal(FretboardData.Count + 1, notes.Length);
                Assert.Equal(notes[0], notes[12]);
                Assert.Equal(notes[0], notes[24]);
            }
        }
    }
}
