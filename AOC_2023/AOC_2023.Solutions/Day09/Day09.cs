namespace AOC_2023.Solutions;

public class Day09
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var lrParser = new LRParser();
        var total = 0;
        var ns = new List<int>();
        foreach (var line in lines)
        {
            ns.Clear();
            lrParser.Reset(line);
            do
            {
                ns.Add(lrParser.EatNumber());
                lrParser.EatWhitespace();
            } while (!lrParser.EOF);
            
            var allZeros = false;
            var level = ns.Count-1;
            while (!allZeros)
            {
                allZeros = true;
                for (var i = 0; i < level; i++)
                {
                    ns[i] = ns[i + 1] - ns[i];
                    if (ns[i] != 0) allZeros = false;
                }
                level--;
            }

            total += ns.Sum();
        }
        
        return total;
    }
    
    public int Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var lrParser = new LRParser();
        var total = 0;
        var ns = new List<int>();
        foreach (var line in lines)
        {
            ns.Clear();
            lrParser.Reset(line);
            do
            {
                ns.Add(lrParser.EatNumber());
                lrParser.EatWhitespace();
            } while (!lrParser.EOF);
            
            // Work out other levels until we get to all zeros
            var allZeros = false;
            var level = 0;
            while (!allZeros)
            {
                allZeros = true;
                for (var i = ns.Count - 1; i > level; i--)
                {
                    ns[i] -= ns[i - 1];
                    if (ns[i] != 0) allZeros = false;
                }
                level++;
            }

            var lhs = 0;
            for (var i = level-1; i >= 0; i--)
                lhs = ns[i]-lhs;
            total += lhs;
        }
        
        return total;
    }
}