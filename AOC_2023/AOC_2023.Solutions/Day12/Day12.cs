namespace AOC_2023.Solutions;

public class Day12
{
    public long Part1And2(string filename, int repeat = 5)
    {
        var lines = File.ReadAllLines(filename);
        var solutions = 0L;
        var cache = new Dictionary<uint, long>();
        foreach (var line in lines)
        {
            var records = line.Split(' ')[0];
            var checksums = line.Split(' ')[1].Split(',').Select(a => int.Parse(a)).ToArray();

            // * means 0 or more dots
            var checksumsExpanded = "";
            foreach (var checksum in checksums)
                checksumsExpanded += "".PadLeft(checksum, '#') + ".*";

            var checksumsDuplicated = "*" + checksumsExpanded;
            var recordsDuplicated = records;
            for (var i = 1; i < repeat; i++)
            {
                checksumsDuplicated += checksumsExpanded;
                recordsDuplicated += $"?{records}";
            }
            cache.Clear();
            solutions += Solve(recordsDuplicated, checksumsDuplicated, cache);
        }

        return solutions;
    }

    private long Solve(ReadOnlySpan<char> records, ReadOnlySpan<char> checksumsExpanded, Dictionary<uint, long> cache)
    {
        var cacheKey = (uint)((records.Length << 7) + checksumsExpanded.Length);
        if (cache.TryGetValue(cacheKey, out var x))
            return x;

        if (records.IsEmpty && checksumsExpanded.Length <= 2)
        {
            cache.Add(cacheKey, 1);
            return 1;
        }

        if (records.IsEmpty)
        {
            cache.Add(cacheKey, 0);
            return 0; //failed
        }

        var results = 0L;

        if (checksumsExpanded[0] == '.')
        {
            // We must have a dot next
            if (records[0] == '#')
            {
                cache.Add(cacheKey, 0);
                return 0;
            }

            results += Solve(records[1..], checksumsExpanded[1..], cache);
        }
        else if (checksumsExpanded[0] == '#')
        {
            // We must have a # next
            if (records[0] == '.')
            {
                cache.Add(cacheKey, 0);
                return 0;
            }

            results += Solve(records[1..], checksumsExpanded[1..], cache);
        }
        else
        {
            // We are on a star, so either a ?, # or . is ok.
            if (records[0] == '.')
                results += Solve(records[1..], checksumsExpanded, cache); // Stay on the star
            else if (records[0] == '#')
            {
                if (checksumsExpanded.Length >= 2)
                    results += Solve(records[1..], checksumsExpanded[2..], cache); // Skip both the * and the first #
            }
            else
            {
                // We have a question, so we can do both
                results += Solve(records[1..], checksumsExpanded, cache); //Pretend it's a dot

                if (checksumsExpanded.Length >= 2)
                    results += Solve(records[1..], checksumsExpanded[2..], cache);
            }
        }

        cache.Add(cacheKey, results);
        return results;
    }
}