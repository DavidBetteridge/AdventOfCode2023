namespace AOC_2023.Solutions;

public class Day14Flipped
{
    public long Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var numberOfColumns = lines[0].Length;
        var numberRows = lines.Length;
        var grid = new int[lines.Length,lines[0].Length];

        for (var rowNumber = 0; rowNumber < numberRows; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                grid[rowNumber, columnNumber] = lines[rowNumber][columnNumber] switch
                {
                    'O' => 0,
                    '.' => 1,
                    _ => 2
                };
            }
        }
        
        var result = 0;
        for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
        {
            var t = -1;
            var b = 0;
            
            while (t+1 < numberRows)
            {
                t++;
                if (grid[t,columnNumber] == 0)
                {
                    result += numberRows - b;
                    b++;
                }
                else if (grid[t,columnNumber] == 2)
                {
                    b = t+1;
                }
            }
        }
        
        return result;
    }
}