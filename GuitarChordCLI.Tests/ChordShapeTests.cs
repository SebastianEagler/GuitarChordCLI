using GuitarChordCLI.MusicalRepository;
using GuitarChordCLI.Rendering;
using Xunit;
using Xunit.Abstractions;

namespace GuitarChordCLI.Tests
{
    public class ChordShapeTests
    {
        private static readonly ChordBuilder Builder = new();

        public static IEnumerable<object[]> AllChords()
        {
            foreach (MusicNotes root in Enum.GetValues<MusicNotes>())
            {
                yield return [root, "Major"];
                yield return [root, "Minor"];
                yield return [root, "Major7"];
                yield return [root, "Minor7"];
                yield return [root, "Dominant7"];
            }
        }

        private static ChordNote Chord(MusicNotes root, string kind) => kind switch
        {
            "Major" => Builder.GetChord(root, ChordTonal.Major),
            "Minor" => Builder.GetChord(root, ChordTonal.Minor),
            "Major7" => Builder.GetSeventhChord(root, ChordSeventhTonal.Major),
            "Minor7" => Builder.GetSeventhChord(root, ChordSeventhTonal.Minor),
            _ => Builder.GetSeventhChord(root, ChordSeventhTonal.Dominant),
        };

        [Theory]
        [MemberData(nameof(AllChords))]
        public void EveryChordHasShapes_AndTheyAreValid(MusicNotes root, string kind)
        {
            var chord = Chord(root, kind);
            var shapes = ChordShapeFinder.Find(chord);

            Assert.InRange(shapes.Count, 1, 4);

            var tones = chord.Notes.Select(n => NotesData.Notes.IndexOf(n)).ToList();
            var open = FretboardData.OpenStringsLowToHigh.Select(n => (int)n).ToArray();

            foreach (var shape in shapes)
            {
                int first = Array.FindIndex(shape.Frets, f => f != null);
                int last = Array.FindLastIndex(shape.Frets, f => f != null);

                // At least 4 adjacent played strings.
                Assert.True(last - first + 1 >= ChordShapeFinder.MinStrings);
                for (int s = first; s <= last; s++)
                    Assert.NotNull(shape.Frets[s]);

                // Root in the bass, all chord tones present, numbering matches chord order.
                var pitches = Enumerable.Range(first, last - first + 1)
                    .Select(s => (open[s] + shape.Frets[s]!.Value) % 12).ToList();
                Assert.Equal(tones[0], pitches[0]);
                Assert.All(tones, t => Assert.Contains(t, pitches));
                for (int s = first; s <= last; s++)
                    Assert.Equal(tones.IndexOf(pitches[s - first]) + 1, shape.Degrees[s]);

                // Frets fit the 4-fret window (or are open in the first position).
                Assert.All(shape.Frets.Where(f => f > 0), f =>
                    Assert.InRange(f!.Value, shape.BaseFret, shape.BaseFret + ChordShapeFinder.WindowSize - 1));
                Assert.True(ChordShapeFinder.CountFingers(shape.Frets) <= ChordShapeFinder.MaxFingers);
            }
        }

        [Fact]
        public void CMajor_ContainsOpenShape_X32010()
        {
            var shapes = ChordShapeFinder.FindAll(Builder.GetChord(MusicNotes.C, ChordTonal.Major));

            var open = shapes.FirstOrDefault(s => s.BaseFret == 1);
            Assert.NotNull(open);

            // Best first-position shape for C major is the open chord x32010.
            Assert.Equal(new int?[] { null, 3, 2, 0, 1, 0 }, open!.Frets);
            Assert.Equal(new int?[] { null, 1, 2, 3, 1, 2 }, open.Degrees);
        }

        [Fact]
        public void Shapes_AreSortedAndSpreadAlongTheNeck()
        {
            var shapes = ChordShapeFinder.Find(Builder.GetChord(MusicNotes.G, ChordTonal.Major));

            Assert.Equal(shapes.OrderBy(s => s.BaseFret).Select(s => s.BaseFret), shapes.Select(s => s.BaseFret));
            for (int i = 1; i < shapes.Count; i++)
                Assert.True(shapes[i].BaseFret - shapes[i - 1].BaseFret >= 3);
        }

        [Fact]
        public void Fingers_BarreCountsAsOne()
        {
            Assert.Equal(1, ChordShapeFinder.CountFingers([null, 3, 3, 3, 3, 3]));
            Assert.Equal(3, ChordShapeFinder.CountFingers([null, 3, 2, 0, 1, 0]));
        }
    }

    public class FretboardLayoutTests
    {
        [Fact]
        public void Strings_AreListedFromHighestToLowest()
        {
            Assert.Equal(["E_high", "B", "G", "D", "A", "E_low"], FretboardData.Notes.Keys);
        }

        [Fact]
        public void OpenStrings_LowToHigh_AreEADGBE()
        {
            Assert.Equal(
                [MusicNotes.E, MusicNotes.A, MusicNotes.D, MusicNotes.G, MusicNotes.B, MusicNotes.E],
                FretboardData.OpenStringsLowToHigh);
        }
    }

    public class ChordShapeTableTests
    {
        [Fact]
        public void StringLetters_AreColoredWhenOpenStringIsChordTone()
        {
            // D major shape in frets 2-5: open D (root) and open A (fifth) are chord tones,
            // but both strings are fretted in this shape.
            var chord = new ChordBuilder().GetChord(MusicNotes.D, ChordTonal.Major);
            var shape = new ChordShape(2,
                [null, 5, 4, 2, 3, 2],
                [null, 1, 2, 3, 1, 2]);
            var theme = GuitarChordCLI.Themes.ColorTheme.Dark;

            var buffer = new BufferedOutput();
            using (var table = GuitarChordCLI.Fretboard.ChordShapeTableBuilder.Build(shape, chord, theme))
                table.Print(buffer);

            var lines = buffer.Lines;
            // lines: header, separator, rows e B G D A E, closing separator.
            // Segments in a row: left border, string letter, separator, zero fret marker, ...
            Segment Letter(int row) => lines[2 + row][1];
            Segment Marker(int row) => lines[2 + row][3];

            // rows from the top: e, B, G, D, A, E
            Assert.Equal("D", Letter(3).Text.Trim());
            Assert.Equal(theme.Chord.Color, Letter(3).Foreground); // open D = root
            Assert.Equal(theme.Chord.Color, Letter(4).Foreground); // open A = fifth

            Assert.Equal(theme.FretboardNotes, Letter(0).Foreground); // open e = E, not a chord tone
            Assert.Equal(theme.FretboardNotes, Letter(1).Foreground); // B
            Assert.Equal(theme.FretboardNotes, Letter(2).Foreground); // G
            Assert.Equal(theme.FretboardNotes, Letter(5).Foreground); // E

            // Zero fret column shows only what is played: strings D and A are fretted, low E is muted.
            Assert.Equal(".", Marker(3).Text.Trim());
            Assert.Equal(".", Marker(4).Text.Trim());
            Assert.Equal("x", Marker(5).Text.Trim());
        }

        [Fact]
        public void StringLetters_AreColoredEvenForMutedChordToneStrings()
        {
            // Muted low E string of a C chord: open E is a chord tone (third).
            var chord = new ChordBuilder().GetChord(MusicNotes.C, ChordTonal.Major);
            var shape = new ChordShape(1,
                [null, 3, 2, 0, 1, 0],
                [null, 1, 2, 3, 1, 2]);
            var theme = GuitarChordCLI.Themes.ColorTheme.Dark;

            var buffer = new BufferedOutput();
            using (var table = GuitarChordCLI.Fretboard.ChordShapeTableBuilder.Build(shape, chord, theme))
                table.Print(buffer);

            var lines = buffer.Lines;
            Assert.Equal("E", lines[2 + 5][1].Text.Trim());
            Assert.Equal(theme.Chord.Color, lines[2 + 5][1].Foreground);
            Assert.Equal("x", lines[2 + 5][3].Text.Trim());
        }
        [Fact]
        public void ZeroFretColumn_PlayedOpenString_UsesChordColor()
        {
            var chord = new ChordBuilder().GetChord(MusicNotes.C, ChordTonal.Major);
            var shape = new ChordShape(1,
                [null, 3, 2, 0, 1, 0],
                [null, 1, 2, 3, 1, 2]);
            var theme = GuitarChordCLI.Themes.ColorTheme.Dark;

            var buffer = new BufferedOutput();
            using (var table = GuitarChordCLI.Fretboard.ChordShapeTableBuilder.Build(shape, chord, theme))
                table.Print(buffer);

            var lines = buffer.Lines;
            var openG = lines[2 + 2][3]; // third row from the top = G string
            Assert.Equal("3", openG.Text.Trim());
            Assert.Equal(theme.Chord.Color, openG.Foreground);
        }
    }

    public class SideBySideTests
    {
        private readonly ITestOutputHelper _output;

        public SideBySideTests(ITestOutputHelper output) => _output = output;

        private static Block MakeBlock(string title, params string[] lines)
            => new(title, lines.Select(l => new List<Segment> { new(l, ConsoleColor.Gray, ConsoleColor.Black) }).ToList());

        [Fact]
        public void Compose_ProducesLinesOfEqualWidth()
        {
            var blocks = new List<Block>
            {
                MakeBlock("A", "+--+", "|ab|"),
                MakeBlock("A longer title", "+-+", "|x|", "+-+"),
            };

            var lines = SideBySide.Compose(blocks, ConsoleColor.Yellow, ConsoleColor.Gray, ConsoleColor.Black);

            Assert.Equal(1 + 3, lines.Count); // title row + tallest block
            Assert.All(lines, l => Assert.Equal(lines[0].Sum(s => s.Text.Length), l.Sum(s => s.Text.Length)));
            Assert.StartsWith("A    ", string.Concat(lines[0].Select(s => s.Text)));
        }

        [Fact]
        public void ShapeTables_AreComposedSideBySide()
        {
            var chord = new ChordBuilder().GetChord(MusicNotes.C, ChordTonal.Major);
            var theme = GuitarChordCLI.Themes.ColorTheme.Dark;
            var blocks = ChordShapeFinder.Find(chord)
                .Select((s, i) => GuitarChordCLI.Fretboard.ChordShapeTableBuilder.ToBlock(i + 1, s, chord, theme))
                .ToList();

            var lines = SideBySide.Compose(blocks, ConsoleColor.Yellow, ConsoleColor.Gray, ConsoleColor.Black);

            foreach (var line in lines)
                _output.WriteLine(string.Concat(line.Select(s => s.Text)));

            Assert.True(lines.Count >= 8);
            Assert.All(lines, l => Assert.Equal(lines[0].Sum(s => s.Text.Length), l.Sum(s => s.Text.Length)));
        }
    }
}
