namespace AOC_2023.Solutions;

public class Day14
{
    public long Part1(string filename)
    {
        var grid = File.ReadAllLines(filename);
        var result = 0;

        var numberOfColumns = grid[0].Length;
        for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
        {
            var t = -1;
            var b = 0;
            
            while (t+1 < grid.Length)
            {
                t++;
                if (grid[t][columnNumber] == 'O')
                {
                    result += grid.Length - b;
                    b++;
                }
                else if (grid[t][columnNumber] == '#')
                {
                    b = t+1;
                }
            }
        }
        
        return result;
    }
}