namespace AOC_2023.Solutions;

public class Day02_LR2
{
    private record Meta
    {
        public int Id;
    }
    
    public int Part1(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        var meta = new Meta();
        foreach (var line in lines)
        {
            var invalid = false;
            foreach (var values in ParseLine(line, meta))
            {
                if (values.Item1 == "red" && values.Item2 > 12)
                {
                    invalid = true;
                    break;
                }

                if (values.Item1 == "green" && values.Item2 > 13)
                {
                    invalid = true;
                    break;
                }
                    
                if (values.Item1 == "blue" && values.Item2 > 14)
                {
                    invalid = true;
                    break;
                }
            }
            if (!invalid)
                result += meta.Id;
        }

        return result;
    }

    private IEnumerable<Tuple<string, int>> ParseLine(string line, Meta meta)
    {
        var lrParser = new LRParser(line);
        lrParser.Eat("Game ");
        meta.Id = lrParser.EatNumber();
        lrParser.Eat(": ");
        while (!lrParser.EOF)
        {
            var quantity = lrParser.EatNumber();
            lrParser.Eat(" ");
            var colour = lrParser.EatWord();
            if (!lrParser.TryEat(", "))
                lrParser.TryEat("; ");
            yield return new Tuple<string, int>(colour, quantity);
        }
    }
    
    public int Part2(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;
            
            var lrParser = new LRParser(line);
            lrParser.Eat("Game ");
            lrParser.EatNumber();
            lrParser.Eat(": ");
            while (!lrParser.EOF)
            {
                var quantity = lrParser.EatNumber();
                lrParser.Eat(" ");
                var colour = lrParser.EatWord();
                if (!lrParser.TryEat(", "))
                    lrParser.TryEat("; ");
                
                switch (colour)
                {
                    case "red":
                        maxRed = Math.Max(maxRed, quantity);
                        break;
                    case "green":
                        maxGreen = Math.Max(maxGreen, quantity);
                        break;
                    default:
                        maxBlue = Math.Max(maxBlue, quantity);
                        break;
                }
            }
            result += (maxRed * maxGreen * maxBlue);
        }

        return result;
    }
}