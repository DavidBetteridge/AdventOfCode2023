namespace AOC_2023.Solutions;

public class Day04Peter
{
    public int Part1(string input) =>
        Parse(input)
            .Where(matches => matches > 0)
            .Sum(matches => 1 << (matches - 1));
    public int Part2(string input)
    {
        var cards = Parse(input);
        var copies = new int[cards.Length];
        Array.Fill(copies, 1);
        for (var c = 0; c < cards.Length; c++)
        {
            for (var i = 1; i <= cards[c]; i++)
            {
                copies[c + i] += copies[c];
            }
        }
        return copies.Sum();
    }
    private int[] Parse(string input) =>
        input.Split(Environment.NewLine)
            .Select(c => c.Split(':', '|'))
            .Select(c =>
            {
                var winners = c[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var numbers = c[2].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
                return winners.Count(w => numbers.Contains(w));
            })
            .ToArray();
}