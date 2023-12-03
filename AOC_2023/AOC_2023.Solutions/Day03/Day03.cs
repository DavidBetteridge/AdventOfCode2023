namespace AOC_2023.Solutions;

public class Day03
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
                        if (check(lines, lineNumber-1, columnNumber-1) || check(lines, lineNumber-1, columnNumber) || check(lines, lineNumber-1, columnNumber+1 )
                          ||  check(lines, lineNumber, columnNumber-1) || check(lines, lineNumber, columnNumber+1)
                          ||  check(lines, lineNumber+1, columnNumber-1) || check(lines, lineNumber+1, columnNumber) || check(lines, lineNumber+1, columnNumber+1))
                        inPartNumber = true;
                    }
                }
                else if (current != "" && inPartNumber)
                {
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

    private bool check(string[] lines, int lineNumber, int columnNumber)
    {
        if (lineNumber < 0 || lineNumber >= lines.Length) return false;
        if (columnNumber < 0 || columnNumber >= lines[lineNumber].Length) return false;
        var c = lines[lineNumber][columnNumber];
        if (c == '.') return false;
        if (char.IsDigit(c)) return false;
        return true;
    }
}