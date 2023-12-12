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
}