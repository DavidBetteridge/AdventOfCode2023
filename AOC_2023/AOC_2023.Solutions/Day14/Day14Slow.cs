namespace AOC_2023.Solutions;

public class Day14Slow
{
    public long Part1(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var numberOfColumns = lines[0].Length;
        var numberRows = lines.Length;
        var grid = new int[numberRows,numberOfColumns];

        for (var rowNumber = 0; rowNumber < numberRows; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                if (lines[rowNumber][columnNumber] == '#')
                    grid[columnNumber,rowNumber] = 2;
                else if (lines[rowNumber][columnNumber] == '.')
                    grid[columnNumber,rowNumber] = 1;
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
                 if (grid[columnNumber,t] == 0)
                 {
                     result += numberRows - b;
                     b++;
                 }
                 else if (grid[columnNumber,t] == 2)
                 {
                     b = t+1;
                 }
             }
        }
        
        return result;
    }
}