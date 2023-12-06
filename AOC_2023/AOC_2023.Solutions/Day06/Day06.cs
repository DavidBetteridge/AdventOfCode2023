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
        var time = ulong.Parse(timeStr);
        
        var distanceStr = "";
        lrParser.Eat("Distance:");
        lrParser.EatWhitespace();
        do
        {
            distanceStr += lrParser.EatWord();
            lrParser.EatWhitespace();
        } while (!lrParser.EOF);
        var distance = ulong.Parse(distanceStr);

 
        ulong numberOfWins = 0;
        for (ulong delay = 0; delay < time; delay++)
        {
            var timeRemaining = time - delay;
            if (delay * timeRemaining > distance)
                numberOfWins++;
        }
        
        return (int)numberOfWins;
    }
}