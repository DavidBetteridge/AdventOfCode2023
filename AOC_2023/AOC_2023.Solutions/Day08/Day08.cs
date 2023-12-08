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
    
    private record Ghost
    {
        public ulong MovesMade { get; set; }
        public string CurrentLocation { get; set; }
    }
    
    public ulong Part2(string filename)
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

        // One ghost per node ending in a A
        var ghosts = rules.Keys.Where(r => r.EndsWith('A'))
                               .Select(r => new Ghost { MovesMade = 0, CurrentLocation = r })
                               .ToList();
        
        // Move each ghost until it's on a terminal
        var minMoves = ulong.MaxValue;
        ulong maxMoves = 0;
        foreach (var ghost in ghosts)
        {
            while (!ghost.CurrentLocation.EndsWith('Z'))
            {
                var nextMove = instructions[(int)ghost.MovesMade % instructions.Length];
                if (nextMove == 'L')
                    ghost.CurrentLocation = rules[ghost.CurrentLocation].LHS;
                else
                    ghost.CurrentLocation = rules[ghost.CurrentLocation].RHS;
                ghost.MovesMade++;
            }

            minMoves = Math.Min(minMoves, ghost.MovesMade);
            maxMoves = Math.Max(maxMoves, ghost.MovesMade);
        }

        Dictionary<(string currentLocation, int numberOfMoves), (string newLocation, int movesNeeded)> cache = new();
        while (minMoves != maxMoves)
        {
            foreach (var ghost in ghosts.Where(g => g.MovesMade < maxMoves))
            {
                MoveGhostToNextTerminal(instructions, ghost, rules, cache);

                maxMoves = Math.Max(maxMoves, ghost.MovesMade);
            }

            minMoves = ghosts.MinBy(g => g.MovesMade)?.MovesMade??0;
        }
  

        return maxMoves;
    }

    private static void MoveGhostToNextTerminal(string instructions, Ghost ghost, 
                                                Dictionary<string, Rule> rules,
                                                Dictionary<(string currentLocation,int numberOfMoves),(string newLocation,int movesNeeded)> cache)
    {
        var cacheKey = (ghost.CurrentLocation, (int)(ghost.MovesMade % (ulong)instructions.Length));
        if (cache.TryGetValue(cacheKey, out var movement))
        {
            ghost.CurrentLocation = movement.newLocation;
            ghost.MovesMade += (ulong)movement.movesNeeded;
        }
        else
        {
            var movesNeeded = 0;
            do
            {
                movesNeeded++;
                var nextMove = instructions[(int)(ghost.MovesMade % (ulong)instructions.Length)];
                if (nextMove == 'L')
                    ghost.CurrentLocation = rules[ghost.CurrentLocation].LHS;
                else
                    ghost.CurrentLocation = rules[ghost.CurrentLocation].RHS;
                ghost.MovesMade++;
            } while (!ghost.CurrentLocation.EndsWith('Z'));
            
            cache.Add(cacheKey, (ghost.CurrentLocation, movesNeeded));
        }
    }
}