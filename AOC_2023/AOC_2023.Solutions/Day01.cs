namespace AOC_2023.Solutions;

public class Day01
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var total = 0;
        foreach (var line in lines)
        {
            var first = line.FirstOrDefault(char.IsDigit);
            var last = line.LastOrDefault(char.IsDigit);
            var number = int.Parse($"{first}{last}");
            total += number;
        }
        return total;
    }
}