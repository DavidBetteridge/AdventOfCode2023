namespace AOC_2023.Solutions;

public class Day08
{
    private struct Rule
    {
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
            rules.Add(start, new Rule { LHS = left, RHS = right });
        }
        
        var y = 0L;
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

            y = y == 0 ? offset : Lcm(y, offset);
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