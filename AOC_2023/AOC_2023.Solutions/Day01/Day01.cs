namespace AOC_2023.Solutions;

public class Day01
{
    public int Part1(string filename) => 
        File.ReadAllLines(filename).Sum(line => FirstMatch(line, false) * 10 + LastMatch(line, false));

    public int Part2(string filename) =>
        File.ReadAllLines(filename).Sum(line => FirstMatch(line, true) * 10 + LastMatch(line, true));
    
    private static int FirstMatch(string line, bool matchWords)
    {
        for (var i = 0; i < line.Length; i++)
        {
            var result = TryMatch(line, i, matchWords);
            if (result is not null)
                return result.Value;
        }

        throw new Exception("No match");
    }

    private int LastMatch(string line, bool matchWords)
    {
        for (var i = line.Length - 1; i >= 0; i--)
        {
            var result = TryMatch(line, i, matchWords);
            if (result is not null)
                return result.Value;
        }

        throw new Exception("No match");
    }

    private static int? TryMatch(string line, int i, bool matchWords)
    {
        if (line[i] == '0') return 0;
        if (line[i] == '1') return 1;
        if (line[i] == '2') return 2;
        if (line[i] == '3') return 3;
        if (line[i] == '4') return 4;
        if (line[i] == '5') return 5;
        if (line[i] == '6') return 6;
        if (line[i] == '7') return 7;
        if (line[i] == '8') return 8;
        if (line[i] == '9') return 9;

        if (matchWords && line.Length > i + 2)
        {
            if (line[i] == 'o' && line[i + 1] == 'n' && line[i + 2] == 'e') return 1;
            if (line[i] == 't' && line[i + 1] == 'w' && line[i + 2] == 'o') return 2;
            if (line[i] == 's' && line[i + 1] == 'i' && line[i + 2] == 'x') return 6;

            if (line.Length > i + 3)
            {
                if (line[i] == 'f' && line[i + 1] == 'o' && line[i + 2] == 'u' && line[i + 3] == 'r') return 4;
                if (line[i] == 'f' && line[i + 1] == 'i' && line[i + 2] == 'v' && line[i + 3] == 'e') return 5;
                if (line[i] == 'n' && line[i + 1] == 'i' && line[i + 2] == 'n' && line[i + 3] == 'e') return 9;
                
                if (line.Length > i + 4)
                {
                    if (line[i] == 't' && line[i + 1] == 'h' && line[i + 2] == 'r' && line[i + 3] == 'e' &&
                        line[i + 4] == 'e') return 3;
                    if (line[i] == 's' && line[i + 1] == 'e' && line[i + 2] == 'v' && line[i + 3] == 'e' &&
                        line[i + 4] == 'n') return 7;
                    if (line[i] == 'e' && line[i + 1] == 'i' && line[i + 2] == 'g' && line[i + 3] == 'h' &&
                        line[i + 4] == 't') return 8;
                }
            }
        }

        return null;
    }
}