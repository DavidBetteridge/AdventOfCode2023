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
            
            while (!lrParser.EOF)
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
}