using CLITextTable.UI;

namespace GuitarChordCLI.Rendering
{
    /// <summary>A piece of text with colors.</summary>
    public record Segment(string Text, ConsoleColor Foreground, ConsoleColor Background);

    /// <summary>An <see cref="ICliOutput"/> that stores printed text (with colors) instead of writing it.</summary>
    public sealed class BufferedOutput : ICliOutput
    {
        private readonly List<List<Segment>> _lines = [[]];

        public void Write(string text, ConsoleColor color, ConsoleColor backgroundColor)
            => _lines[^1].Add(new Segment(text, color, backgroundColor));

        public void WriteLine(string text, ConsoleColor color, ConsoleColor backgroundColor)
        {
            Write(text, color, backgroundColor);
            _lines.Add([]);
        }

        /// <summary>Stored lines without trailing empty ones.</summary>
        public List<List<Segment>> Lines
        {
            get
            {
                var lines = _lines.Select(l => l.ToList()).ToList();
                while (lines.Count > 0 && lines[^1].All(s => s.Text.Length == 0))
                    lines.RemoveAt(lines.Count - 1);
                return lines;
            }
        }
    }

    /// <summary>A titled block of colored lines (for example one printed table).</summary>
    public record Block(string Title, List<List<Segment>> Lines);

    /// <summary>Puts several blocks next to each other.</summary>
    public static class SideBySide
    {
        public static int LineWidth(IEnumerable<Segment> line) => line.Sum(s => s.Text.Length);

        /// <summary>
        /// Builds output lines: first the titles, then the blocks line by line. Every block is padded
        /// to its own width so all returned lines have the same length.
        /// </summary>
        public static List<List<Segment>> Compose(IReadOnlyList<Block> blocks, ConsoleColor titleColor, ConsoleColor padColor, ConsoleColor background, int gap = 3)
        {
            var result = new List<List<Segment>>();
            if (blocks.Count == 0)
                return result;

            var widths = blocks
                .Select(b => Math.Max(b.Title.Length, b.Lines.Count == 0 ? 0 : b.Lines.Max(LineWidth)))
                .ToList();
            int height = blocks.Max(b => b.Lines.Count);

            Segment Pad(int count) => new(new string(' ', count), padColor, background);

            var titleLine = new List<Segment>();
            for (int i = 0; i < blocks.Count; i++)
            {
                titleLine.Add(new Segment(blocks[i].Title.PadRight(widths[i]), titleColor, background));
                if (i < blocks.Count - 1)
                    titleLine.Add(Pad(gap));
            }
            result.Add(titleLine);

            for (int row = 0; row < height; row++)
            {
                var line = new List<Segment>();
                for (int i = 0; i < blocks.Count; i++)
                {
                    int used = 0;
                    if (row < blocks[i].Lines.Count)
                    {
                        line.AddRange(blocks[i].Lines[row]);
                        used = LineWidth(blocks[i].Lines[row]);
                    }
                    line.Add(Pad(widths[i] - used));
                    if (i < blocks.Count - 1)
                        line.Add(Pad(gap));
                }
                result.Add(line);
            }

            return result;
        }

        /// <summary>Writes composed lines to the console.</summary>
        public static void Print(IReadOnlyList<Block> blocks, ConsoleColor titleColor, ConsoleColor padColor, ConsoleColor background, int gap = 3)
        {
            var output = new ConsoleCliOutput();
            foreach (var line in Compose(blocks, titleColor, padColor, background, gap))
            {
                foreach (var segment in line)
                    output.Write(segment.Text, segment.Foreground, segment.Background);
                output.WriteLine("", padColor, background);
            }
        }
    }
}
