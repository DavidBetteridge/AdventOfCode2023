namespace AOC_2023.Solutions;

public class Day09
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var lrParser = new LRParserSpan();
        var total = 0;
        foreach (var line in lines)
        {
            // Level 0 contains the original numbers.
            var ns = new List<List<int>>() { new ()};
            lrParser.Reset(line);
            do
            {
                ns[0].Add(lrParser.EatNumber());
                lrParser.EatWhitespace();
            } while (!lrParser.EOF);
            
            // Work out other levels until we get to all zeros
            var allZeros = false;
            var level = 0;
            while (!allZeros)
            {
                allZeros = true;
                ns.Add(new List<int>());
                for (var i = 0; i < ns[level].Count - 1; i++)
                {
                    var diff = ns[level][i + 1] - ns[level][i];
                    ns[level+1].Add(ns[level][i+1]-ns[level][i]);
                    if (diff != 0) allZeros = false;
                }
                level++;
            }

            ns[level].Add(0);
            for (level = ns.Count - 2; level >= 0; level--)
                ns[level].Add(ns[level+1][^1]+ns[level][^1]);
            total += ns[0][^1];
        }
        
        return total;
    }
    
    public int Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var lrParser = new LRParserSpan();
        var total = 0;
        foreach (var line in lines)
        {
            // Level 0 contains the original numbers.
            var ns = new List<List<int>>() { new ()};
            lrParser.Reset(line);
            do
            {
                ns[0].Add(lrParser.EatNumber());
                lrParser.EatWhitespace();
            } while (!lrParser.EOF);
            
            // Work out other levels until we get to all zeros
            var allZeros = false;
            var level = 0;
            while (!allZeros)
            {
                allZeros = true;
                ns.Add(new List<int>());
                for (var i = 0; i < ns[level].Count - 1; i++)
                {
                    var diff = ns[level][i + 1] - ns[level][i];
                    ns[level+1].Add(ns[level][i+1]-ns[level][i]);
                    if (diff != 0) allZeros = false;
                }
                level++;
            }

            ns[level].Add(0);
            for (level = ns.Count - 2; level >= 0; level--)
                ns[level].Add(ns[level][0]-ns[level+1][^1]);
            total += ns[0][^1];
        }
        
        return total;
    }
}