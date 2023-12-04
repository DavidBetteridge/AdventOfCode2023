namespace AOC_2023.Solutions;

public class Day04
{
    public int Part1(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        
        var lrParser = new LRParser("");
        foreach (var line in lines)
        {
            var availableNumbers = new HashSet<int>();
            var numbersToFind = new HashSet<int>();
            lrParser.Reset(line);
            lrParser.Eat("Card");
            lrParser.EatWhitespace();
            var cardId = lrParser.EatNumber();
            lrParser.Eat(":");
            lrParser.EatWhitespace();
            
            while (!lrParser.TryEat("|"))
            {
                var nextNumber = lrParser.EatNumber();
                numbersToFind.Add(nextNumber);
                lrParser.EatWhitespace();
            }
            lrParser.EatWhitespace();
            
            while (!lrParser.EOL)
            {
                var nextNumber = lrParser.EatNumber();
                availableNumbers.Add(nextNumber);
                lrParser.EatWhitespace();
            }

            var matches = 0;
            foreach (var numberToFind in numbersToFind)
            {
                if (availableNumbers.Contains(numberToFind))
                    matches = matches == 0 ? 1 : matches << 1;
            }

            result += matches;
        }

        return result;
    }

    public int Part2(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        var quantities = new int[lines.Length];
        
        var lrParser = new LRParser("");
        foreach (var line in lines)
        {
            var availableNumbers = new HashSet<int>();
            var numbersToFind = new HashSet<int>();
            lrParser.Reset(line);
            lrParser.Eat("Card");
            lrParser.EatWhitespace();
            var cardId = lrParser.EatNumber();
            quantities[cardId-1]++;

            lrParser.Eat(":");
            lrParser.EatWhitespace();
            
            while (!lrParser.TryEat("|"))
            {
                var nextNumber = lrParser.EatNumber();
                numbersToFind.Add(nextNumber);
                lrParser.EatWhitespace();
            }
            lrParser.EatWhitespace();
            
            while (!lrParser.EOL)
            {
                var nextNumber = lrParser.EatNumber();
                availableNumbers.Add(nextNumber);
                lrParser.EatWhitespace();
            }

            var matches = 0;
            foreach (var numberToFind in numbersToFind)
            {
                if (availableNumbers.Contains(numberToFind))
                    matches++;
            }

            for (var nextCardOffset = 1; nextCardOffset <= matches; nextCardOffset++)
            {
                if ((cardId-1) + nextCardOffset < lines.Length)
                    quantities[cardId + nextCardOffset-1] += quantities[cardId-1];
            }
        }

        return quantities.Sum();
    }
}