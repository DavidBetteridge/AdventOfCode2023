namespace AOC_2023.Solutions;

public class Day04
{
    public int Part1(string filename)
    {
        // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        var result = 0;
        var lines = File.ReadAllLines(filename);
        
        var parser = new RLParser();
        foreach (var line in lines)
        {
            parser.Reset(line);
            var availableNumbers = new bool[100];
            while (parser.TryNotEat('|'))
            {
                var nextNumber = parser.EatNumber();
                availableNumbers[nextNumber] = true;
                parser.EatWhitespace();
            }
            parser.EatWhitespace();

            var matches = 0;
            while (parser.TryNotEat(':'))
            {
                var nextNumber = parser.EatNumber();
                parser.EatWhitespace();
                
                if (availableNumbers[nextNumber])
                    matches = matches == 0 ? 1 : matches << 1;
            }
            result += matches;
        }

        return result;
    }
    
    public int Part2(string filename)
    {
        var cardId = 0;
        var lines = File.ReadAllLines(filename);
        var quantities = new int[lines.Length];
        
        var parser = new RLParser();
        foreach (var line in lines)
        {
            quantities[cardId]++;
            parser.Reset(line);
            var availableNumbers = new bool[100];

            do
            {
                var nextNumber = parser.EatNumber();
                availableNumbers[nextNumber] = true;
            } while (parser.TryNotEat('|'));
            parser.EatWhitespace();

            var matches = 0;

            do
            {
                var nextNumber = parser.EatNumber();
                if (availableNumbers[nextNumber])
                    matches++;
            } while (parser.TryNotEat(':'));
            
            for (var nextCardOffset = 1; nextCardOffset <= matches; nextCardOffset++)
                quantities[cardId + nextCardOffset] += quantities[cardId];

            cardId++;
        }

        return quantities.Sum();
    }
}