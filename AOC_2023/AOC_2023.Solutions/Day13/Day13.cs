namespace AOC_2023.Solutions;

public class Day13
{
    public long Part1(string filename)
    {
        var lines = File.ReadAllText(filename);
        var patterns = lines.Split("\n\n");
        var rowResult = 0;
        var colResult = 0;
        
        foreach (var pattern in patterns)
        {
            var grid = pattern.Split("\n");

            var rows = grid.Select(line =>
            {
                uint n = 0;
                foreach (var c in line)
                {
                    n <<= 1;
                    if (c == '#') n++;
                }
                return n;
            }).ToArray();

            var cols = new uint[grid[0].Length];
            for (var c = 0; c < grid[0].Length; c++)
            {
                uint n = 0;
                foreach (var row in grid)
                {
                    n <<= 1;
                    if (row[c] == '#') n++;
                }
                cols[c] = n;
            }
            
            var r = FindMirrorLine(rows);
            rowResult += r;
            if (r == 0)
                colResult += FindMirrorLine(cols);        
           
        }

        return (100 * rowResult) + colResult;
    }
    
    
    private static int FindMirrorLine(uint[] values)
    {
        for (var mirror = 1; mirror < values.Length; mirror++)
        {
            var first = mirror - 1;
            var last = mirror;
            var ok = true;
            while (first >= 0 && last < values.Length)
            {
                if (values[first] == values[last])
                {
                    first--;
                    last++;
                }
                else
                {
                    ok = false;
                    break;
                }
            }

            if (ok)
            {
                return mirror;
            }
        }
        return 0;
    }
}