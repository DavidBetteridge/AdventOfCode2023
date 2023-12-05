using System.Runtime.InteropServices.JavaScript;

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
            for (int delay = 0; delay < time; delay++)
            {
                var timeRemaining = time - delay;
                if (delay * timeRemaining > distance)
                    numberOfWins++;
            }
            total = (total == 0) ? numberOfWins : total * numberOfWins;
        }
        
        return total;
    }
}