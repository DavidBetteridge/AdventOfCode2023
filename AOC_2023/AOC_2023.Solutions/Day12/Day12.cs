namespace AOC_2023.Solutions;

public class Day12
{
    public long Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var solutions = new List<string>();
        foreach (var line in lines)
        {
            var records = line.Split(' ')[0];
            var checksums = line.Split(' ')[1].Split(',').Select(a => int.Parse(a)).ToArray();
            Solve(records,  "", checksums);
        }
        return solutions.Count;
        
        
        bool IsValid(string proposed, int[] checksums)
        {
            var springs = proposed.Split('.', StringSplitOptions.RemoveEmptyEntries).Select(a=>a.Length).ToArray();
            if (springs.Length != checksums.Length) return false;
            for (int i = 0; i < springs.Length; i++)
                if (springs[i] != checksums[i]) return false;

            return true;
        }

        void Solve(string remainingRecords, string soFar, int[] checksums)
        {
            if (string.IsNullOrWhiteSpace(remainingRecords))
            {
                if (IsValid(soFar,checksums))
                {
                    solutions.Add(soFar);
                }
                return;
            }
    
            if (remainingRecords[0] == '#')
            {
                // Do nothing
                Solve(remainingRecords[1..], soFar + '#', checksums);
            }
            else if (remainingRecords[0] == '.')
            {
                // Do nothing
                Solve(remainingRecords[1..], soFar + '.', checksums);
            }
            else
            {
                // Insert a #
                Solve(remainingRecords[1..], soFar + '#', checksums);
        
                // Insert a .
                Solve(remainingRecords[1..], soFar + '.', checksums);
            }
        }
    }
    
    
    public long Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var solutions = 0L;
        foreach (var line in lines)
        {
            var records = line.Split(' ')[0];
            var checksums = line.Split(' ')[1].Split(',').Select(a => int.Parse(a)).ToArray();;

            var checksumsExpanded = "";
            foreach (var checksum in checksums)
                checksumsExpanded += "".PadLeft(checksum, '#')+".*";
            
            records = $"{records}?{records}?{records}?{records}?{records}";
            checksumsExpanded = checksumsExpanded+checksumsExpanded+checksumsExpanded+checksumsExpanded+checksumsExpanded;

            var r = Solve(records, "*" + checksumsExpanded, new Dictionary<string, long>());
            
            Console.WriteLine(r);
            solutions += r;
        }
        return solutions;

        long Solve(string records, string checksumsExpanded, Dictionary<string,long> cache)
        {
            var cacheKey = $"{records}--{checksumsExpanded}";
            if (cache.TryGetValue(cacheKey, out var x))
                return x;
            
            if (string.IsNullOrWhiteSpace(records) && checksumsExpanded.Length <= 2)
            {
                cache.Add(cacheKey,1);
                return 1;
            }
        
            if (string.IsNullOrWhiteSpace(records) || string.IsNullOrWhiteSpace(checksumsExpanded))
            {
                cache.Add(cacheKey,0);
                return 0; //failed
            }

            var results = 0L;
            
            if (checksumsExpanded[0] == '.')
            {
                // We must have a dot next
                if (records[0] == '#')
                {
                    cache.Add(cacheKey,0);
                    return 0;
                }

                results += Solve(records[1..], checksumsExpanded[1..],cache);
            }
            else if (checksumsExpanded[0] == '#')
            {
                // We must have a # next
                if (records[0] == '.')
                {
                    cache.Add(cacheKey,0);
                    return 0;
                }
                results += Solve(records[1..], checksumsExpanded[1..],cache);
            }
            else
            {
                // We are on a star, so either a ?, # or . is ok.
                if (records[0] == '.')
                    results += Solve(records[1..], checksumsExpanded,cache);  // Stay on the star
                else if (records[0] == '#')
                {
                    if (checksumsExpanded.Length >= 2)
                        results += Solve(records[1..], checksumsExpanded[2..],cache); // Skip both the * and the first #
                }
                else
                {
                    // We have a question, so we can do both
                    results += Solve(records[1..], checksumsExpanded,cache);  //Pretend it's a dot
                    
                    if (checksumsExpanded.Length >= 2)
                        results += Solve(records[1..], checksumsExpanded[2..],cache);
                }
            }
            cache.Add(cacheKey,results);
            return results;
        }
    }
}