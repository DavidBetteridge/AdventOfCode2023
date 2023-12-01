namespace AOC_2023.Solutions;

public class Day01
{
    private static readonly Dictionary<char, int> CharToInt = new()
    {
        ['0'] = 0, ['1'] = 1, ['2'] = 2, ['3'] = 3, ['4'] = 4, ['5'] = 5, ['6'] = 6, ['7'] = 7, ['8'] = 8, ['9'] = 9
    };

    public int Part1(string filename) => 
        File.ReadAllLines(filename).Sum(line => CharToInt[line.First(char.IsDigit)] * 10 + 
                                                CharToInt[line.Last(char.IsDigit)]);
}