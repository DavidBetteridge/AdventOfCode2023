namespace AOC_2023.Solutions;

public class Day14
{
    const int Fixed = 2;
    const int Gap = 1;
    const int Ball = 0;

    public long Part1(string filename)
    {
        var grid = File.ReadAllLines(filename);
        var result = 0;

        var numberOfColumns = grid[0].Length;
        for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
        {
            var t = -1;
            var b = 0;

            while (t + 1 < grid.Length)
            {
                t++;
                if (grid[t][columnNumber] == 'O')
                {
                    result += grid.Length - b;
                    b++;
                }
                else if (grid[t][columnNumber] == '#')
                {
                    b = t + 1;
                }
            }
        }

        return result;
    }

    public int Part2(string filename)
    {
        var lines = File.ReadAllLines(filename);
        var numberOfColumns = lines[0].Length;
        var numberOfRows = lines.Length;
        var grid = new int[numberOfRows, numberOfColumns];

        for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                if (lines[rowNumber][columnNumber] == '#')
                    grid[columnNumber, rowNumber] = Fixed;
                else if (lines[rowNumber][columnNumber] == '.')
                    grid[columnNumber, rowNumber] = Gap;
            }
        }

        var cache = new Dictionary<string, int>();
        var scores = new List<int>();
        for (var cycle = 0; cycle < 1000000000; cycle++)
        {
            var cacheKey = "";
            // for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
            // {
            //     for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            //     {
            //         cacheKey += grid[columnNumber, rowNumber];
            //     }
            // }

            for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
            {
                var count = 0;
                for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
                {
                    if (grid[columnNumber, rowNumber] == Ball)
                        count++;
                }

                cacheKey += count;
            }

            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            
            {
                var count = 0;
                for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)    
                {
                    if (grid[columnNumber, rowNumber] == Ball)
                        count++;
                }

                cacheKey += count;
            }
            
            
            if (cache.TryGetValue(cacheKey, out var warmup))
            {
                var cycleSize = cycle - warmup;
                var ind = warmup + ((1000000000 - warmup) % cycleSize) - 1;
                return scores[ind];
            }
            else
                cache.Add(cacheKey, cycle); //Cycles applied.


            // North
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                var t = -1;
                var b = 0;

                while (t + 1 < numberOfRows)
                {
                    t++;
                    if (grid[columnNumber, t] == Ball)
                    {
                        grid[columnNumber, t] = Gap;
                        grid[columnNumber, b] = Ball;
                        b++;
                    }
                    else if (grid[columnNumber, t] == Fixed)
                    {
                        b = t + 1;
                    }
                }
            }

            // West
            for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
            {
                var t = -1;
                var b = 0;

                while (t + 1 < numberOfColumns)
                {
                    t++;
                    if (grid[t, rowNumber] == Ball)
                    {
                        grid[t, rowNumber] = Gap;
                        grid[b, rowNumber] = Ball;
                        b++;
                    }
                    else if (grid[t, rowNumber] == Fixed)
                    {
                        b = t + 1;
                    }
                }
            }

            // South
            for (var columnNumber = 0; columnNumber < numberOfRows; columnNumber++)
            {
                var t = numberOfRows;
                var b = numberOfRows - 1;

                while (t > 0)
                {
                    t--;
                    if (grid[columnNumber, t] == Ball)
                    {
                        grid[columnNumber, t] = Gap;
                        grid[columnNumber, b] = Ball;
                        b--;
                    }
                    else if (grid[columnNumber, t] == Fixed)
                    {
                        b = t - 1;
                    }
                }
            }

            // East
            for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
            {
                var t = numberOfColumns;
                var b = numberOfColumns - 1;

                while (t > 0)
                {
                    t--;
                    if (grid[t, rowNumber] == Ball)
                    {
                        grid[t, rowNumber] = Gap;
                        grid[b, rowNumber] = Ball;
                        b--;
                    }
                    else if (grid[t, rowNumber] == Fixed)
                    {
                        b = t - 1;
                    }
                }
            }

            scores.Add(Score(numberOfRows, numberOfColumns, grid));
        }

        return -1;
    }

    private static int Score(int numberOfRows, int numberOfColumns, int[,] grid)
    {
        var total = 0;
        for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                if (grid[columnNumber, rowNumber] == Ball)
                    total += numberOfRows - rowNumber;
            }
        }

        return total;
    }
}