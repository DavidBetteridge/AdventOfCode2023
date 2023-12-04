using System.Security;

namespace AOC_2023.Solutions;

public class Day03B
{
    public int Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var total = 0;
        for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            var inPartNumber = false;
            var current = "";
            for (var columnNumber = 0; columnNumber < lines[lineNumber].Length; columnNumber++)
            {
                var c = lines[lineNumber][columnNumber];
                if (char.IsDigit(c))
                {
                    current += c;
                    if (!inPartNumber)
                    {
                        if (CheckP1(lines, lineNumber-1, columnNumber-1) || CheckP1(lines, lineNumber-1, columnNumber) || CheckP1(lines, lineNumber-1, columnNumber+1 )
                          ||  CheckP1(lines, lineNumber, columnNumber-1) || CheckP1(lines, lineNumber, columnNumber+1)
                          ||  CheckP1(lines, lineNumber+1, columnNumber-1) || CheckP1(lines, lineNumber+1, columnNumber) || CheckP1(lines, lineNumber+1, columnNumber+1))
                        inPartNumber = true;
                    }
                }
                else if (current != "")
                {
                    if (inPartNumber)
                        total += int.Parse(current);
                    inPartNumber = false;
                    current = "";
                }
            }
            if (current != "" && inPartNumber)
                total += int.Parse(current);
        }        
        return total;
    }

    private bool CheckP1(string[] lines, int lineNumber, int columnNumber)
    {
        if (lineNumber < 0 || lineNumber >= lines.Length) return false;
        if (columnNumber < 0 || columnNumber >= lines[lineNumber].Length) return false;
        var c = lines[lineNumber][columnNumber];
        if (c == '.') return false;
        if (char.IsDigit(c)) return false;
        return true;
    }
    
    public int Part2(string filename)
    {
        var result = 0;
        var lines = File.ReadAllLines(filename);
        var stars = new Dictionary<Tuple<int, int>, int>();
        for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            Tuple<int,int>? starLocation = null;
            var current = "";
            for (var columnNumber = 0; columnNumber < lines[lineNumber].Length; columnNumber++)
            {
                var c = lines[lineNumber][columnNumber];
                if (char.IsDigit(c))
                {
                    current += c;
                    if (starLocation is null)
                    {
                        starLocation = 
                            CheckP2(lines, lineNumber - 1, columnNumber - 1) ??
                            CheckP2(lines, lineNumber, columnNumber - 1) ??
                            CheckP2(lines, lineNumber + 1, columnNumber - 1) ??
                            
                            CheckP2(lines, lineNumber - 1, columnNumber) ??
                            CheckP2(lines, lineNumber + 1, columnNumber) ??
                            
                            CheckP2(lines, lineNumber - 1, columnNumber + 1) ??
                            CheckP2(lines, lineNumber, columnNumber + 1) ??
                            CheckP2(lines, lineNumber + 1, columnNumber + 1);
                    }
                }
                else if (current != "")
                {
                    if (starLocation is not null)
                    {
                        if (!stars.ContainsKey(starLocation))
                            stars.Add(starLocation, int.Parse(current));
                        else
                            result+=stars[starLocation]*int.Parse(current);
                    }
                    starLocation = null;
                    current = "";
                }
            }

            if (current != "" && starLocation is not null)
            {
                if (!stars.ContainsKey(starLocation))
                    stars.Add(starLocation, int.Parse(current));
                else
                    result+=stars[starLocation]*int.Parse(current);
            }
        }

        return result;
    }
    
    private Tuple<int,int>? CheckP2(string[] lines, int lineNumber, int columnNumber)
    {
        if (lineNumber < 0 || lineNumber >= lines.Length) return null;
        if (columnNumber < 0 || columnNumber >= lines[lineNumber].Length) return null;
        var c = lines[lineNumber][columnNumber];
        if (c == '*')
            return new Tuple<int, int>(columnNumber, lineNumber);
        else
            return null;
    }
}