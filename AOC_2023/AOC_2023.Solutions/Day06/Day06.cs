namespace AOC_2023.Solutions;

public class Day06
{
    public int Part1(string filename)
    {
        var text = File.ReadAllText(filename);
        var lrParser = new LRParser(text);

        var times = new List<int>();
        lrParser.Eat("Time:");
        lrParser.EatWhitespace();
        do
        {
            var number = lrParser.EatNumber();
            times.Add(number);
            lrParser.EatWhitespace();
        } while (!lrParser.TryEat('\n'));

        var distances = new List<int>();
        lrParser.Eat("Distance:");
        lrParser.EatWhitespace();
        do
        {
            var number = lrParser.EatNumber();
            distances.Add(number);
            lrParser.EatWhitespace();
        } while (!lrParser.EOF);


        var total = 0;
        for (var gameNumber = 0; gameNumber < times.Count; gameNumber++)
        {
            var distance = distances[gameNumber];
            var time = times[gameNumber];
            var numberOfWins = 0;
            for (var delay = 0; delay < time; delay++)
            {
                var timeRemaining = time - delay;
                if (delay * timeRemaining > distance)
                    numberOfWins++;
                else if (numberOfWins > 0)
                    break;
            }
            total = (total == 0) ? numberOfWins : total * numberOfWins;
        }
        
        return total;
    }

    public int Part2(string filename)
    {
        var text = File.ReadAllText(filename);
        var lrParser = new LRParser(text);

        var timeStr = "";
        lrParser.Eat("Time:");
        lrParser.EatWhitespace();
        do
        {
            timeStr += lrParser.EatWord();
            lrParser.EatWhitespace();
        } while (!lrParser.TryEat('\n'));
        var time = long.Parse(timeStr);
        
        var distanceStr = "";
        lrParser.Eat("Distance:");
        lrParser.EatWhitespace();
        do
        {
            distanceStr += lrParser.EatWord();
            lrParser.EatWhitespace();
        } while (!lrParser.EOF);
        var distance = long.Parse(distanceStr);

        var inner = Math.Sqrt((time * time) - (4 * distance));
        var firstWin= ((-time + (int) Math.Ceiling(inner)) / -2);
        var secondWin= ((-time - (int) Math.Floor(inner)) / -2);
        
        return (int)(secondWin - firstWin);
    }
}