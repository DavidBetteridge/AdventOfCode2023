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
}