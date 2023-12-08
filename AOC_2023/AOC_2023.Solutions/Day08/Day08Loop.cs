namespace AOC_2023.Solutions;

public class Day08Loop
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
        var lrParser = new LRParserSpan();

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
            rules.Add(start, new Rule{Start = start, LHS = left, RHS = right});
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
        var lrParser = new LRParserSpan();

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
            rules.Add(start, new Rule{Start = start, LHS = left, RHS = right});
        }


        var offsets = new List<int>();
        foreach (var ghost in rules.Keys.Where(r => r.EndsWith('A')))
        {
            var currentLocation = ghost;
            var offset = 0;
            
            // Move each ghost until it's on a terminal
            while (!currentLocation.EndsWith('Z'))
            {
                var nextMove = instructions[offset % instructions.Length];
                if (nextMove == 'L')
                    currentLocation = rules[currentLocation].LHS;
                else
                    currentLocation = rules[currentLocation].RHS;
                offset++;
            }
            offsets.Add(offset);
        }

        var y = lcm(offsets[0], offsets[1]);
        foreach (var offset in offsets.Skip(2))
            y = lcm(y, offset);
        
        return y;
    }

    private long lcm(long p, long q)
    {
        var x = p;
        var y = q;
        while (x != y)
        {
            if (x > y)
                x -= y;
            else
                y -= x;
        }

        return (p * q) / x;
    }
    
}