namespace AOC_2023.Solutions;

public class Day02
{
    public int Part1(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        var lrParser = new LRParser();
        foreach (var line in lines)
        {
            var invalid = false;
            lrParser.Reset(line);
            
            lrParser.Eat("Game ");
            var id = lrParser.EatNumber();
            lrParser.Eat(": ");
            while (!lrParser.EOF)
            {
                var quantity = lrParser.EatNumber();
                lrParser.Eat(' ');
                var colour = lrParser.EatWord();
                if (!lrParser.TryEat(", "))
                    lrParser.TryEat("; ");
                if (colour == "red" && quantity > 12)
                {
                    invalid = true;
                    break;
                }

                if (colour == "green" && quantity > 13)
                {
                    invalid = true;
                    break;
                }
                    
                if (colour == "blue" && quantity > 14)
                {
                    invalid = true;
                    break;
                }
            }
            if (!invalid)
                result += id;
        }

        return result;
    }

    public int Part2(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        var lrParser = new LRParser();
        foreach (var line in lines)
        {
            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;
            lrParser.Reset(line);
            lrParser.Eat("Game ");
            lrParser.EatNumber();
            lrParser.Eat(": ");
            while (!lrParser.EOF)
            {
                var quantity = lrParser.EatNumber();
                lrParser.Eat(' ');
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