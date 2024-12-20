using System.Collections;

namespace AOC_2023.Solutions;

public class Day14
{
    // const int Fixed = 2;
    // const int Gap = 1;
    // const int Ball = 0;

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
        
        var ball = new bool[numberOfRows*numberOfColumns];
        var perm = new bool[numberOfRows*numberOfColumns];

        for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                if (lines[rowNumber][columnNumber] == '#')
                    perm[columnNumber*numberOfRows+rowNumber] = true;
                else if (lines[rowNumber][columnNumber] == 'O')
                    ball[columnNumber*numberOfRows+rowNumber] = true;
            }
        }

        var cache = new Dictionary<ulong, int>();
        ulong cacheKey = 0;
        for (var cycle = 0; cycle < 1000000000; cycle++)
        {
            if (cache.TryGetValue(cacheKey, out var warmup))
            {
                var cycleSize = cycle - warmup;
                var ind = warmup + ((1000000000 - warmup) % cycleSize) - 1;
                return (int)(cache.ElementAt(ind+1).Key >> 32);
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
                    if (ball[columnNumber*numberOfRows+t])
                    {
                        ball[columnNumber*numberOfRows+t] = false;
                        ball[columnNumber*numberOfRows+b] = true;
                        b++;
                    }
                    else if (perm[columnNumber*numberOfRows+t])
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
                    if (ball[t*numberOfRows+rowNumber])
                    {
                        ball[t*numberOfRows+rowNumber] = false;
                        ball[b*numberOfRows+rowNumber] = true;
                        b++;
                    }
                    else if (perm[t*numberOfRows+rowNumber])
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
                    if (ball[columnNumber*numberOfRows+t])
                    {
                        ball[columnNumber*numberOfRows+t] = false;
                        ball[columnNumber*numberOfRows+b] = true;
                        b--;
                    }
                    else if (perm[columnNumber*numberOfRows+t])
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
                    if (ball[t*numberOfRows+rowNumber])
                    {
                        ball[t*numberOfRows+rowNumber] = false;
                        ball[b*numberOfRows+rowNumber] = true;
                        b--;
                    }
                    else if (perm[t*numberOfRows+rowNumber])
                    {
                        b = t - 1;
                    }
                }
            }
            cacheKey = Score(numberOfRows, numberOfColumns, ball);
        }

        return -1;
    }

    private static ulong Score(int numberOfRows, int numberOfColumns, bool[] grid)
    {
        var northTotal = 0;
        var westTotal = 0;
        for (var rowNumber = 0; rowNumber < numberOfRows; rowNumber++)
        {
            for (var columnNumber = 0; columnNumber < numberOfColumns; columnNumber++)
            {
                if (grid[columnNumber * numberOfRows + rowNumber])
                {
                    northTotal += numberOfRows - rowNumber;
                    westTotal += numberOfColumns - columnNumber;
                }
            }
        }

        return ((ulong)northTotal << 32) | (ulong)westTotal;
    }
}