namespace GuitarChordCLI.MusicalRepository
{
    /// <summary>
    /// One chord fingering. Arrays have one entry per string, from the lowest (E) to the highest (e).
    /// </summary>
    /// <param name="BaseFret">First fret of the 4-fret window the shape lies in.</param>
    /// <param name="Frets">Fret per string; 0 = open string, null = muted string.</param>
    /// <param name="Degrees">Chord tone number per string (1 = root, 2 = third, 3 = fifth, 4 = seventh); null = muted.</param>
    public record ChordShape(int BaseFret, int?[] Frets, int?[] Degrees);

    /// <summary>
    /// Finds playable chord shapes in root position (root in the bass) inside a window of 4 frets.
    /// </summary>
    public static class ChordShapeFinder
    {
        public const int WindowSize = 4;
        public const int MinStrings = 4;
        public const int MaxFingers = 4;
        public const int MaxBaseFret = 12;
        private const int MinBaseDistance = 3;

        /// <summary>Returns up to <paramref name="max"/> shapes spread along the neck, ordered by position.</summary>
        public static List<ChordShape> Find(ChordNote chord, int max = 4)
        {
            var candidates = FindAll(chord)
                .Select(shape => (shape, score: Score(shape)))
                .OrderByDescending(x => x.score)
                .ThenBy(x => x.shape.BaseFret)
                .ToList();

            var picked = new List<ChordShape>();
            foreach (var (shape, _) in candidates)
            {
                if (picked.Count >= max)
                    break;
                if (picked.Any(p => Math.Abs(p.BaseFret - shape.BaseFret) < MinBaseDistance))
                    continue;
                picked.Add(shape);
            }
            return picked.OrderBy(s => s.BaseFret).ToList();
        }

        /// <summary>Returns the best shape for every window position (base fret 1..12) that has one.</summary>
        public static List<ChordShape> FindAll(ChordNote chord)
        {
            var tones = chord.Notes.Select(n => NotesData.Notes.IndexOf(n)).ToArray();
            var open = FretboardData.OpenStringsLowToHigh.Select(n => (int)n).ToArray();
            var result = new List<ChordShape>();

            for (int baseFret = 1; baseFret <= MaxBaseFret; baseFret++)
            {
                ChordShape? best = null;
                int bestScore = int.MinValue;

                foreach (var frets in Enumerate(baseFret, tones, open))
                {
                    var shape = new ChordShape(baseFret, frets, frets.Select((f, s) => f is null ? (int?)null : Array.IndexOf(tones, (open[s] + f.Value) % 12) + 1).ToArray());
                    int score = Score(shape);
                    if (score > bestScore)
                    {
                        best = shape;
                        bestScore = score;
                    }
                }

                if (best != null)
                    result.Add(best);
            }

            return result;
        }

        /// <summary>Number of fingers needed; strings on the lowest fretted fret count as one finger (barre).</summary>
        public static int CountFingers(int?[] frets)
        {
            var fretted = frets.Where(f => f > 0).Select(f => f!.Value).ToList();
            if (fretted.Count == 0)
                return 0;
            int min = fretted.Min();
            int atMin = fretted.Count(f => f == min);
            return atMin >= 2 ? fretted.Count - atMin + 1 : fretted.Count;
        }

        private static int Score(ChordShape shape)
        {
            int sounding = shape.Frets.Count(f => f != null);
            return sounding * 10 - CountFingers(shape.Frets) * 3;
        }

        private static IEnumerable<int?[]> Enumerate(int baseFret, int[] tones, int[] open)
        {
            var frets = new int?[open.Length];

            IEnumerable<int?[]> Recurse(int stringIndex)
            {
                if (stringIndex == open.Length)
                {
                    if (IsValid(frets, baseFret, tones, open))
                        yield return (int?[])frets.Clone();
                    yield break;
                }

                frets[stringIndex] = null;
                foreach (var shape in Recurse(stringIndex + 1))
                    yield return shape;

                foreach (int fret in AllowedFrets(baseFret))
                {
                    if (Array.IndexOf(tones, (open[stringIndex] + fret) % 12) < 0)
                        continue;
                    frets[stringIndex] = fret;
                    foreach (var shape in Recurse(stringIndex + 1))
                        yield return shape;
                }
                frets[stringIndex] = null;
            }

            return Recurse(0);
        }

        private static IEnumerable<int> AllowedFrets(int baseFret)
        {
            if (baseFret == 1)
                yield return 0; // open strings only belong to the first position
            for (int f = baseFret; f < baseFret + WindowSize; f++)
                yield return f;
        }

        private static bool IsValid(int?[] frets, int baseFret, int[] tones, int[] open)
        {
            int first = Array.FindIndex(frets, f => f != null);
            int last = Array.FindLastIndex(frets, f => f != null);
            if (first < 0 || last - first + 1 < MinStrings)
                return false;

            // Played strings must be adjacent (no muted string in the middle).
            for (int s = first; s <= last; s++)
                if (frets[s] == null)
                    return false;

            // Bass must be the root (chord tone number 1 = tones[0]).
            if ((open[first] + frets[first]!.Value) % 12 != tones[0])
                return false;

            // All chord tones must be present.
            var present = new HashSet<int>();
            for (int s = first; s <= last; s++)
                present.Add((open[s] + frets[s]!.Value) % 12);
            if (tones.Any(t => !present.Contains(t)))
                return false;

            // Anchor the window: at least one note on its first fret.
            if (!frets.Any(f => f == baseFret))
                return false;

            return CountFingers(frets) <= MaxFingers;
        }
    }
}
