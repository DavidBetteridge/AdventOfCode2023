namespace AOC_2023.Solutions;

public class Day08
{
    private struct Rule
    {
        public string Start { get; set; }
        public string LHS { get; set; }
        public string RHS { get; set; }
    }

    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var lrParser = new LRParser();

        var instructions = lines[0];
        var rules = new Dictionary<string, Rule>();
        foreach (var line in lines[2..])
        {
            lrParser.Reset(line);
            var start = lrParser.EatWord();
            lrParser.EatWhitespace();
            lrParser.Eat('=');
            lrParser.EatWhitespace();
            lrParser.Eat('(');
            var left = lrParser.EatWord();
            lrParser.Eat(',');
            lrParser.EatWhitespace();
            var right = lrParser.EatWord();
            lrParser.Eat(')');
            rules.Add(start, new Rule { LHS = left, RHS = right });
        }

        var moves = 0;
        var current = "AAA";
        while (current != "ZZZ")
        {
            var nextMove = instructions[moves % instructions.Length];
            if (nextMove == 'L')
                current = rules[current].LHS;
            else
                current = rules[current].RHS;
            moves++;
        }

        return moves;
    }
    
    
    public long Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var length = lines.Length - 2;
        var instructions = lines[0];
        var rules = new Rule[length];
        var mapping = new Dictionary<string, int>(length);

        for (var i = 0; i < length; i++)
        {
            var line = lines[i + 2];
            var start = line[..3];
            var left = line[7..10];
            var right = line[12..15];
            rules[i] = new Rule { Start = start, LHS = left, RHS = right };
            mapping.Add(start, i);
        }
        
        var rulesLhs = new int[length];
        var rulesRhs = new int[length];
        var starts = new bool[length];
        var ends = new bool[length];
        foreach (var ghost in rules)
        {
            var i = mapping[ghost.Start];
            starts[i] = ghost.Start.EndsWith("A");
            ends[i] = ghost.Start.EndsWith("Z");
            rulesLhs[i] = mapping[ghost.LHS];
            rulesRhs[i] = mapping[ghost.RHS];
        }
        
        var y = 0L;
        for (var ghost = 0; ghost < length; ghost++)
        {
            if (starts[ghost])
            {
                var currentLocation = ghost;
                var offset = 0;

                // Move each ghost until it's on a terminal
                while (!ends[currentLocation])
                {
                    var nextMove = instructions[offset % instructions.Length];
                    if (nextMove == 'L')
                        currentLocation = rulesLhs[currentLocation];
                    else
                        currentLocation = rulesRhs[currentLocation];
                    offset++;
                }

                y = y == 0 ? offset : Lcm(y, offset);
            }
        }
        return y;
    }

    private long Gcd(long a, long b)
    {
        while (true)
        {
            if (b == 0) return a;
            var a1 = a;
            a = b;
            b = a1 % b;
        }
    }

    private long Lcm(long a, long b)
    {
        if (a > b)
            return (a / Gcd(a, b)) * b;
        else
            return (b / Gcd(a, b)) * a;
    }
}