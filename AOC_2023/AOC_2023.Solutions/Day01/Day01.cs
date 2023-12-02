namespace AOC_2023.Solutions;

public class Day01
{
    private static readonly Dictionary<string, int> CharToInt = new()
    {
        ["0"] = 0, ["1"] = 1, ["2"] = 2, ["3"] = 3, ["4"] = 4, ["5"] = 5, ["6"] = 6, ["7"] = 7, ["8"] = 8, ["9"] = 9
    };

    private static readonly Dictionary<string, int> WordToInt = new()
    {
        ["zero"] = 0, ["one"] = 1, ["two"] = 2, ["three"] = 3, ["four"] = 4, ["five"] = 5,
        ["six"] = 6, ["seven"] = 7, ["eight"] = 8, ["nine"] = 9,
        ["0"] = 0, ["1"] = 1, ["2"] = 2, ["3"] = 3, ["4"] = 4, ["5"] = 5, ["6"] = 6, ["7"] = 7, ["8"] = 8, ["9"] = 9
    };
    
    public int Part1(string filename) => 
        File.ReadAllLines(filename).Sum(line => FirstMatch(line, CharToInt) * 10 + LastMatch(line, CharToInt));

    public int Part2(string filename) =>
        File.ReadAllLines(filename).Sum(line => FirstMatch(line, WordToInt) * 10 + LastMatch(line, WordToInt));

    private static int FirstMatch(string line, Dictionary<string, int> maps)
    {
        for (var i = 0; i < line.Length; i++)
        {
            foreach (var key in maps.Keys)
            {
                if (i + key.Length <= line.Length)
                {
                    var toTest = line[i..(i + key.Length)];
                    if (toTest == key)
                        return maps[key];
                }
            }
        }

        throw new Exception("No match");
    }

    private int LastMatch(string line, Dictionary<string, int> maps)
    {
        for (var i = line.Length - 1; i >= 0; i--)
        {
            foreach (var key in maps.Keys)
            {
                if (i + key.Length <= line.Length)
                {
                    var toTest = line[i..(i + key.Length)];
                    if (toTest == key)
                        return maps[key];
                }
            }
        }

        throw new Exception("No match");
    }
}