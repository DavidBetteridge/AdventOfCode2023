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
        for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            var line = lines[lineNumber];
            for (var columnNumber = 0; columnNumber < lines[lineNumber].Length; columnNumber++)
            {
                var c = line[columnNumber];
                if (c == '*')
                {
                    var numbers = new List<int>(2);

                    var left = ReadNumber(line, columnNumber-1);
                    if (!string.IsNullOrWhiteSpace(left))
                        numbers.Add(int.Parse(left));

                    var right = ReadNumber(line, columnNumber+1);
                    if (!string.IsNullOrWhiteSpace(right))
                        numbers.Add(int.Parse(right));

                    if (lineNumber > 0)
                    {
                        var lineAbove = lines[lineNumber - 1];
                        var above = ReadNumber(lineAbove, columnNumber);
                        if (!string.IsNullOrWhiteSpace(above))
                            numbers.Add(int.Parse(above));
                        else
                        {
                            var aboveLeft = ReadNumber(lineAbove, columnNumber-1);
                            if (!string.IsNullOrWhiteSpace(aboveLeft))
                                numbers.Add(int.Parse(aboveLeft));
                            
                            var aboveRight = ReadNumber(lineAbove, columnNumber+1);
                            if (!string.IsNullOrWhiteSpace(aboveRight))
                                numbers.Add(int.Parse(aboveRight));
                        }
                    }
                    
                    if (lineNumber+1 < line.Length)
                    {
                        var lineBelow = lines[lineNumber + 1];
                        var below = ReadNumber(lineBelow, columnNumber);
                        if (!string.IsNullOrWhiteSpace(below))
                            numbers.Add(int.Parse(below));
                        else
                        {
                            var belowLeft = ReadNumber(lineBelow, columnNumber-1);
                            if (!string.IsNullOrWhiteSpace(belowLeft))
                                numbers.Add(int.Parse(belowLeft));
                            
                            var belowRight = ReadNumber(lineBelow, columnNumber+1);
                            if (!string.IsNullOrWhiteSpace(belowRight))
                                numbers.Add(int.Parse(belowRight));
                        }
                    }
                    
   
                    if (numbers.Count == 2)
                        result += (numbers[0] * numbers[1]);
                }
            }
        }

        return result;
    }

    private string ReadNumber(string line, int columnNumber)
    {
        if (!char.IsDigit(line[columnNumber])) return "";
        
        // Find the start of the number?
        var offset = -1;
        while ((columnNumber + offset) >= 0 && char.IsDigit(line[columnNumber + offset]))
            offset--;
        offset++;
        
        // Read the number
        var current = "";
        while ((columnNumber + offset) < line.Length && char.IsDigit(line[columnNumber + offset]))
        {
            current += line[columnNumber + offset];
            offset++;
        }

        return current;
    }
}