namespace AOC_2023.Solutions;

public class Day02_LR
{
    public int Part1(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        
        foreach (var line in lines)
        {
            var lrParser = new LRParser(line);
            lrParser.Eat("Game ");
            var id = lrParser.EatNumber();
            lrParser.Eat(": ");
            var invalid = false;
            while (!lrParser.EOF)
            {
                do
                {
                    var quantity = lrParser.EatNumber();
                    lrParser.Eat(" ");
                    var colour = lrParser.EatWord();
                    lrParser.TryEat(", ");
                    
                    if (colour == "red" && quantity > 12)
                        invalid = true;

                    if (colour == "green" && quantity > 13)
                        invalid = true;
                    
                    if (colour == "blue" && quantity > 14)
                        invalid = true;
                    
                } while (!lrParser.EOF && !lrParser.TryEat("; "));
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
        foreach (var line in lines)
        {
            var lrParser = new LRParser(line);
            lrParser.Eat("Game ");
            lrParser.EatNumber();
            lrParser.Eat(": ");
            var maxRed = 0;
            var maxGreen = 0;
            var maxBlue = 0;
            while (!lrParser.EOF)
            {
                do
                {
                    var quantity = lrParser.EatNumber();
                    lrParser.Eat(" ");
                    var colour = lrParser.EatWord();
                    lrParser.TryEat(", ");
                    
                    if (colour == "red")
                        maxRed = Math.Max(maxRed, quantity);
                    
                    if (colour == "green")
                        maxGreen = Math.Max(maxGreen, quantity);
                    
                    if (colour == "blue")
                        maxBlue = Math.Max(maxBlue, quantity);
                    
                } while (!lrParser.EOF && !lrParser.TryEat("; "));
            }
            result += (maxRed * maxGreen * maxBlue);
        }

        return result;
    }
}